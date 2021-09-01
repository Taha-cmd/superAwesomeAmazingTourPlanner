using Extensions;
using Models;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;


namespace DataAccess
{

    // sql kata always names its parameters like this: p0, p1, p2, p3 ---
    // the Query object will bind the values to the parameters, since I am only compiling the sql statement string,
    // I need to bind the values to the parameters again. Thus, I cannot name the parameters
    //ex: Query("table").Where("name", "john") will result in the query "select * from table where name = @p0"
    // and the value john being bound to p0


    // insert, update, delete: column names are passed as keys in an object and not as strings, pay attention to letter case
    public class ToursRepository : RepositoryBase, IToursRepository
    {
        public ToursRepository(IDatebase database, Compiler compiler) : base(database, compiler) { }

        private static readonly object tourDataContainer =
            new { name = "p0", description = "p1", startingarea = "p2", targetarea = "p3", distance = "p4", imagepath = "p5" };

        private static readonly object logDataContainer =
            new
            {
                date = "p0",
                tourname = "p1",
                report = "p2",
                totaltime = "p3",
                rating = "p4",
                author = "p5",
                hasmcdonalds = "p6",
                hascampingspots = "p7",
                members = "p8",
                accomodation = "p9"
            };

        private readonly Query SELECT_TOUR_QUERY    = new Query("tour").Where("name", "p0");
        private readonly Query SELECT_TOURS_QUERY   = new Query("tour");
        private readonly Query SELECT_LOGS_QUERY    = new Query("log").Where("tourname", "p0");

        private readonly Query DELETE_LOG_QUERY     = new Query("log").Where("id", "p0").AsDelete();
        private readonly Query DELETE_LOGS_QUERY    = new Query("log").Where("tourname", "p0").AsDelete();
        private readonly Query DELETE_TOUR_QUERY    = new Query("tour").Where("name", "p0").AsDelete();

        private readonly Query CREATE_TOUR_QUERY    = new Query("tour").AsInsert(tourDataContainer);
        private readonly Query CREATE_LOG_QUERY     = new Query("log").AsInsert(logDataContainer);

        private readonly Query UPDATE_TOUR_QUERY    = new Query("tour").Where("name", "p6").AsUpdate(tourDataContainer); // Where clause params comes last
        private readonly Query UPDATE_LOG_QUERY     = new Query("log").Where("id", "p10").AsUpdate(logDataContainer);

        public void Create(Tour tour)
        {
            database.ExecuteNonQuery(
                    queryCompiler.Compile(CREATE_TOUR_QUERY).Sql,
                    database.Param("p0", tour.Name),
                    database.Param("p1", tour.Description),
                    database.Param("p2", tour.StartingArea),
                    database.Param("p3", tour.TargetArea),
                    database.Param("p4", tour.Distance),
                    database.Param("p5", tour.Image)
                );

            
            // add logs to the database as well
            tour.Logs.ForEach(log => AddLog(tour.Name, log));
        }
        public void Delete(Tour tour)
        {
            // delete image from filesystem first
            File.Delete(tour.Image);
            database.ExecuteNonQuery(queryCompiler.Compile(DELETE_LOGS_QUERY).Sql, database.Param("p0", tour.Name));
            database.ExecuteNonQuery(queryCompiler.Compile(DELETE_TOUR_QUERY).Sql, database.Param("p0", tour.Name));
        }

        public void Update(string tourName, string imagePath, Tour tour)
        {
            // update will cascade to foreign key, we can safely update all values
            // update "log" set tourname = "newName" where tourname = "oldName"
            database.ExecuteNonQuery(
                    queryCompiler.Compile(UPDATE_TOUR_QUERY).Sql,
                    database.Param("p0", tour.Name), // the new tour Name
                    database.Param("p1", tour.Description),
                    database.Param("p2", tour.StartingArea),
                    database.Param("p3", tour.TargetArea),
                    database.Param("p4", tour.Distance),
                    database.Param("p5", tour.Image),
                    database.Param("p6", tourName) // the current tourName to identify the tour
                );

            // delete old image from filesystem
            File.Delete(imagePath);
        }

        public IEnumerable<Tour> GetTours(int? limit = null)
        {
            //Limit not working for now
            var tours = database.ExecuteQuery(queryCompiler.Compile(SELECT_TOURS_QUERY).Sql, TourReader);
            tours.ToList().ForEach(tour => tour.Logs = GetLogs(tour.Name).ToList());

            return tours;
            
        }
        public Tour GetTour(string tourName)
        {
            var tour = database.ExecuteQuery(queryCompiler.Compile(SELECT_TOUR_QUERY).Sql, TourReader, database.Param("p0", tourName)).First();
            tour.Logs = GetLogs(tour.Name).ToList();
            return tour;
        }

        public IEnumerable<TourLog> GetLogs(string tourName)
        {
            return database.ExecuteQuery(queryCompiler.Compile(SELECT_LOGS_QUERY).Sql, TourLogReader, database.Param("p0", tourName));
        }

        public void AddLog(string tourName, TourLog log)
        {
            database.ExecuteNonQuery(queryCompiler.Compile(CREATE_LOG_QUERY).Sql,
                    database.Param("p0", log.DateTime),
                    database.Param("p1", tourName),
                    database.Param("p2", log.Report),
                    database.Param("p3", log.TotalTime),
                    database.Param("p4", log.Rating),
                    database.Param("p5", log.Author),
                    database.Param("p6", log.HasMcDonalds),
                    database.Param("p7", log.HasCampingSpots),
                    database.Param("p8", log.Members),
                    database.Param("p9", log.Accomodation)
                    );

        }
        public void DeleteLog(TourLog log)
        {
            database.ExecuteNonQuery(queryCompiler.Compile(DELETE_LOG_QUERY).Sql, database.Param("p0", log.Id));
        }

        public void UpdateLog(TourLog log)
        {
            database.ExecuteNonQuery(
                    queryCompiler.Compile(UPDATE_LOG_QUERY).Sql,
                    database.Param("p0", log.DateTime),
                    database.Param("p1", log.TourName),
                    database.Param("p2", log.Report),
                    database.Param("p3", log.TotalTime),
                    database.Param("p4", log.Rating),
                    database.Param("p5", log.Author),
                    database.Param("p6", log.HasMcDonalds),
                    database.Param("p7", log.HasCampingSpots),
                    database.Param("p8", log.Members),
                    database.Param("p9", log.Accomodation),
                    database.Param("p10", log.Id)
                    );
        }


        // DbDataReader is the common abstract class in c# that all database providers implement
        private Tour TourReader(DbDataReader reader)
        {
            return new Tour()
            {
                Name = database.TypeConverter.To<string>(reader.GetValue("name")),
                Description = database.TypeConverter.To<string>(reader.GetValue("description")),
                Distance = database.TypeConverter.To<double>(reader.GetValue("distance")),
                StartingArea = database.TypeConverter.To<string>(reader.GetValue("startingArea")),
                TargetArea = database.TypeConverter.To<string>(reader.GetValue("targetArea")),
                Image = database.TypeConverter.To<string>(reader.GetValue("imagePath"))
            };
        }

        private TourLog TourLogReader(DbDataReader reader)
        {
            return new TourLog()
            {
                Id = database.TypeConverter.To<int>(reader.GetValue("id")),
                Rating = database.TypeConverter.To<int>(reader.GetValue("rating")),
                Report = database.TypeConverter.To<string>(reader.GetValue("report")),
                TotalTime = database.TypeConverter.To<double>(reader.GetValue("totalTime")),
                DateTime = database.TypeConverter.To<DateTime>(reader.GetValue("date")),
                TourName = database.TypeConverter.To<string>(reader.GetValue("tourName")),
                Author = database.TypeConverter.To<string>(reader.GetValue("author")),
                HasMcDonalds = database.TypeConverter.To<bool>(reader.GetValue("hasMcDonalds")),
                Accomodation = database.TypeConverter.To<string>(reader.GetValue("accomodation")),
                HasCampingSpots = database.TypeConverter.To<bool>(reader.GetValue("hasCampingSpots")),
                Members = database.TypeConverter.To<int>(reader.GetValue("members"))
            };
        }

        public bool TourExists(string tourName) => Exists("tour", "name", tourName);
        public bool LogExists(int id) => Exists("log", "id", id);

    }
}
