namespace Sample
{
    public class Team
    {
        public Team(string cityId,
            string cityName,
            string id,
            string name)
        {
            this.City = cityName;
            this.Name = name;
            this.Id = id.ToLowerInvariant();
            this.CityId = cityId.ToLowerInvariant();
        }
        
        public string CityId { get; set; }
        
        public string City { get; set; }
        
        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}