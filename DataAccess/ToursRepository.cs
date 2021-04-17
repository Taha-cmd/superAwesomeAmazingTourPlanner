using Extensions;
using Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess
{
    public class ToursRepository : RepositoryBase, IToursRepository
    {
        public ToursRepository(IDatebase database) : base(database) { }
        public void Create(Tour tour)
        {
            string statement = $"INSERT INTO \"tour\" (name, description, startingArea, targetArea, distance) " +
                $"VALUES (@name, @description, @startingArea, @targetArea, @distance)";

            database.ExecuteNonQuery(
                    statement,
                    database.Param("name", tour.Name),
                    database.Param("description", tour.Description),
                    database.Param("startingArea", tour.StartingArea),
                    database.Param("targetArea", tour.TargetArea),
                    database.Param("distance", tour.Distance)
                );
        }
        public void Delete(Tour tour) => Delete(tour.Name);
        public void Delete(string name)
        {
            string statement1 = $"DELETE FROM \"log\" WHERE tourname=@name";
            string statement2 = $"DELETE FROM \"tour\" WHERE name=@name";

            database.ExecuteNonQuery(statement1, database.Param("name", name));
            database.ExecuteNonQuery(statement2, database.Param("name", name));
        }

        public void Update(string tourName, Tour tour)
        {
            // update "log" set tourname = "newName" where tourname = "oldName"

            string statement =  "UPDATE \"tour\" SET " +
                                "(description, startingArea, targetArea, name) =" +
                                "(@newDescription, @newStartingArea, @newTargetArea, @newName)" +
                                "WHERE name=@currentName";

            database.ExecuteNonQuery(statement,
                database.Param("newDescription", tour.Description),
                database.Param("newStartingArea", tour.StartingArea),
                database.Param("newTargetArea", tour.StartingArea),
                database.Param("newName", tour.Name),
                database.Param("currentName", tourName)
                );

            Update("log", "tourname", tourName, "tourname", tour.Name); // update all related logs
        }

        public IEnumerable<Tour> GetTours(int? limit = null)
        {
            string statement = $"SELECT * FROM \"tour\"";

            if (!limit.IsNull())
                statement += $" limit {limit}";

            var tours = database.ExecuteQuery(statement, TourReader);
            tours.ToList().ForEach(tour => tour.Logs = GetLogs(tour.Name).ToList());

            return tours;
            
        }
        public Tour GetTour(string tourName)
        {
            string statement = $"SELECT * FROM \"tour\" WHERE name=@name";
            var tour = database.ExecuteQuery(statement, TourReader, database.Param("name", tourName)).First();
            tour.Logs = GetLogs(tour.Name).ToList();
            return tour;
        }

        public IEnumerable<TourLog> GetLogs(string tourName)
        {
            string statement = $"SELECT * FROM \"log\" WHERE tourname=@tourname";
            return database.ExecuteQuery(statement, TourLogReader, database.Param("tourname", tourName));
        }

        public void AddLog(string tourName, TourLog log)
        {
            string statement = $"INSERT INTO \"log\" (date, tourname, report, totaltime, rating) " +
                                $"VALUES (@date, @tourname, @report, @totaltime, @rating)";

            database.ExecuteNonQuery(statement,
                    database.Param("date", log.DateTime),
                    database.Param("tourname", tourName),
                    database.Param("report", log.Report),
                    database.Param("totaltime", log.TotalTime),
                    database.Param("rating", log.Rating)
                    );
        }


        // DbDataReader is the common abstract class in c# that all database providers implement
        private Tour TourReader(DbDataReader reader)
        {
            return new Tour()
            {
                Name = reader.GetValue<string>("name"),
                Description = reader.GetValue<string>("description"),
                Distance = reader.GetValue<double>("distance"),
                StartingArea = reader.GetValue<string>("startingArea"),
                TargetArea = reader.GetValue<string>("targetArea")
            };
        }

        private TourLog TourLogReader(DbDataReader reader)
        {
            return new TourLog()
            {
                Rating = reader.GetValue<int>("rating"),
                Report = reader.GetValue<string>("report"),
                TotalTime = reader.GetValue<double>("totalTime"),
                DateTime = reader.GetValue<DateTime>("date")
            };
        }
       
        public bool TourExists(string tourName) => Exists("tour", "name", tourName);


    }
}
