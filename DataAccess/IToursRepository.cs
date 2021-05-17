using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DataAccess
{
    // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
    // only an aggregate root can have a repoistory
    // in this case, the tours repoistory
    public interface IToursRepository
    {
        // tour crud methods

        void Create(Tour tour);
        IEnumerable<Tour> GetTours(int? limit = null);
        Tour GetTour(string tourName);
        void Update(string tourName, string imagePath, Tour tour);
        void Delete(Tour tour);
        bool TourExists(string tourName);

        // log crud methods
        IEnumerable<TourLog> GetLogs(string tourName);
        void AddLog(string tourName, TourLog log);
        void DeleteLog(TourLog log);
        void UpdateLog(TourLog log);
        bool LogExists(int id);
        
    }
}
