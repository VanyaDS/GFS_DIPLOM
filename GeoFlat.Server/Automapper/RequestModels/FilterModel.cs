namespace GeoFlat.Server.Automapper.RequestModels
{
    public class FilterModel
    {
        public int? MinPrice { get; set; }     
        public int? MaxPrice { get; set; }
        public string CityName { get; set; }
        public bool? RentType { get; set; }
        public bool? HasFurniture { get; set; }
        public bool? NotForStudents { get; set; }
        public bool? WithoutAnimals { get; set; }
        public bool? WithoutChildren { get; set; }  
        public bool? IsAgent { get; set; }
        public bool? WithInternet { get; set; }
        public bool? ForDay { get; set; }
        public int? RoomNumber { get; set; }
    }
}
