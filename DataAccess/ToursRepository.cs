using Extensions;
using Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class ToursRepository : RepositoryBase, IToursRepository
    {
        public ToursRepository(IDatebase database) : base(database) { }

        private const string createTourSQL = "INSERT INTO \"tour\" (name, description, startingArea, targetArea, distance, imagePath) " +
                "VALUES (@name, @description, @startingArea, @targetArea, @distance, @imagePath)";

        private const string deleteTourSQL = "DELETE FROM \"tour\" WHERE name=@name";
        private const string deleteLogSQL = "DELETE FROM \"log\" WHERE tourname=@name";

        private const string updateTourSQL = "UPDATE \"tour\" SET " +
                                "(description, startingArea, targetArea, name, distance, imagepath) =" +
                                "(@newDescription, @newStartingArea, @newTargetArea, @newName, @newDistance, @newImagePath)" +
                                "WHERE name=@currentName";

        private const string getTourSQL = "SELECT * FROM \"tour\" WHERE name=@name";
        private const string getLogsSQL = "SELECT * FROM \"log\" WHERE tourname=@tourname";
        private const string addLogSql = "INSERT INTO \"log\" (date, tourname, report, totaltime, rating, author, hasmcdonalds, hascampingspots, members, accomodation) " +
                                "VALUES (@date, @tourname, @report, @totaltime, @rating, @author, @hasmcdonalds, @hascampingspots, @members, @accomodation)";

        private const string deleteLogSql = "DELETE FROM \"log\" WHERE id=@id";
        public void Create(Tour tour)
        {
            database.ExecuteNonQuery(
                    createTourSQL,
                    database.Param("name", tour.Name),
                    database.Param("description", tour.Description),
                    database.Param("startingArea", tour.StartingArea),
                    database.Param("targetArea", tour.TargetArea),
                    database.Param("distance", tour.Distance),
                    database.Param("imagePath", tour.Image)
                );

            // add logs
            tour.Logs.ForEach(log => AddLog(tour.Name, log));
        }
        public void Delete(Tour tour)
        {
            // delete image from filesystem first
            File.Delete(tour.Image);
            database.ExecuteNonQuery(deleteLogSQL, database.Param("name", tour.Name));
            database.ExecuteNonQuery(deleteTourSQL, database.Param("name", tour.Name));
        }

        public void Update(string tourName, string imagePath, Tour tour)
        {
            // update will cascade to foreign key, we can safely update all values
            // update "log" set tourname = "newName" where tourname = "oldName"
            database.ExecuteNonQuery(updateTourSQL,
                database.Param("newDescription", tour.Description),
                database.Param("newStartingArea", tour.StartingArea),
                database.Param("newTargetArea", tour.TargetArea),
                database.Param("newName", tour.Name),
                database.Param("currentName", tourName),
                database.Param("newDistance", tour.Distance),
                database.Param("newImagePath", tour.Image)
                );

            // delete old image from filesystem
            File.Delete(imagePath);
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
            var tour = database.ExecuteQuery(getTourSQL, TourReader, database.Param("name", tourName)).First();
            tour.Logs = GetLogs(tour.Name).ToList();
            return tour;
        }

        public IEnumerable<TourLog> GetLogs(string tourName)
        {
            return database.ExecuteQuery(getLogsSQL, TourLogReader, database.Param("tourname", tourName));
        }

        public void AddLog(string tourName, TourLog log)
        {
            database.ExecuteNonQuery(addLogSql,
                    database.Param("date", log.DateTime),
                    database.Param("tourname", tourName),
                    database.Param("report", log.Report),
                    database.Param("totaltime", log.TotalTime),
                    database.Param("rating", log.Rating),
                    database.Param("author", log.Author),
                    database.Param("hasmcdonalds", log.HasMcDonalds),
                    database.Param("hascampingspots", log.HasCampingSpots),
                    database.Param("members", log.Members),
                    database.Param("accomodation", log.Accomodation)
                    );

        }
        public void DeleteLog(TourLog log)
        {
            database.ExecuteNonQuery(deleteLogSql, database.Param("id", log.Id));
        }

        public void UpdateLog(TourLog log)
        {
            throw new NotImplementedException();
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
                TargetArea = reader.GetValue<string>("targetArea"),
                Image = reader.GetValue<string>("imagePath")
            };
        }

        private TourLog TourLogReader(DbDataReader reader)
        {
            return new TourLog()
            {
                Id = reader.GetValue<int>("id"),
                Rating = reader.GetValue<int>("rating"),
                Report = reader.GetValue<string>("report"),
                TotalTime = reader.GetValue<double>("totalTime"),
                DateTime = reader.GetValue<DateTime>("date"),
                TourName = reader.GetValue<string>("tourName"),
                Author = reader.GetValue<string>("author"),
                HasMcDonalds = reader.GetValue<bool>("hasMcDonalds"),
                Accomodation = reader.GetValue<string>("accomodation"),
                HasCampingSpots = reader.GetValue<bool>("hasCampingSpots"),
                Members = reader.GetValue<int>("members")
            };
        }

        public bool TourExists(string tourName) => Exists("tour", "name", tourName);
        public bool LogExists(int id) => Exists("log", "id", id);




    }
}
