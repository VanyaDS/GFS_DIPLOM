using GeoFlat.Server.Automapper.RequestModels;
using GeoFlat.Server.Models.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GeoFlat.Server.Helpers
{
    internal static class FiltersHealper
    {
        public static IEnumerable<Record> GetFilteredRecords(IEnumerable<Record> records, FilterModel filters)
        {
            if (records is null || filters is null)
            {
                return null;
            }

            var filteredRecords = records.AsQueryable();

            if (filters.MinPrice is not null)
            {
                filteredRecords = filteredRecords.Where(records => records.Price >= filters.MinPrice);
            }
            if (filters.MaxPrice is not null)
            {
                filteredRecords = filteredRecords.Where(records => records.Price <= filters.MinPrice);
            }
            if (!string.IsNullOrEmpty(filters.CityName))
            {
                filteredRecords = filteredRecords.Where(records => records.Flat.Geolocation.CityName
                                                 .ToLower()
                                                 .Contains(filters.CityName
                                                 .ToLower()));
            }
            if (filters.RentType.HasValue)
            {
                if (filters.RentType.Value) // flat
                {
                    filteredRecords = filteredRecords.Where(records => records.RentType);
                }
                if (!filters.RentType.Value) // room
                {
                    filteredRecords = filteredRecords.Where(records => !records.RentType);
                }
            }

            if(filters.HasFurniture.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.HasFurniture);
            }
            
            if (filters.NotForStudents.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.NotForStudents);
            }
            if (filters.WithoutAnimals.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.WithoutAnimals);
            }
            if (filters.WithoutChildren.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.WithoutChildren);
            }
            if (filters.IsAgent.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.IsAgent);
            }
            if (filters.WithInternet.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.WithInternet);
            }
            if (filters.ForDay.HasValue)
            {
                filteredRecords = filteredRecords.Where(records => records.ForDay);
            }
           
            if (filters.RoomNumber.HasValue)
            {
                if (filters.RoomNumber.Value > 0)
                {
                    filteredRecords = filteredRecords.Where(records => records.Flat.RoomNumber == filters.RoomNumber.Value);
                }
            }

            if(string.IsNullOrEmpty(filters.CityName)
                && !filters.ForDay.HasValue
                && !filters.HasFurniture.HasValue
                && !filters.RoomNumber.HasValue
                && !filters.IsAgent.HasValue
                && !filters.MaxPrice.HasValue
                && !filters.MinPrice.HasValue
                && !filters.NotForStudents.HasValue
                && !filters.RentType.HasValue
                && !filters.WithInternet.HasValue
                && !filters.WithoutAnimals.HasValue 
                && !filters.WithoutChildren.HasValue)
            {
                return null;
            }           
            return filteredRecords.ToList();
        }
    }
}
