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
            string statement = $"DELETE FROM \"tour\" WHERE name=@name";
            database.ExecuteNonQuery(statement, database.Param("name", name));
        }

        public void Update(string tourName, Tour tour)
        {
            throw new NotImplementedException();
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
            return database.ExecuteQuery(statement, TourReader, database.Param("name", tourName)).First();
        }

        public IEnumerable<TourLog> GetLogs(string tourName)
        {
            string statement = $"SELECT * FROM \"log\" WHERE tourname=@tourname";
            return database.ExecuteQuery(statement, TourLogReader, database.Param("tourname", tourName));
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
