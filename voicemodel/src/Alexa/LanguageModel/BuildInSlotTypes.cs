namespace VoiceBridge.Most.VoiceModel.Alexa.LanguageModel
{
    public static class BuildInSlotTypes
    {
        public static class Generic
        {
            
            public const string Literal = "AMAZON.LITERAL";        
            public const string FourDigitNumber = "AMAZON.FOUR_DIGIT_NUMBER";
            public const string Number = "AMAZON.NUMBER";
            public const string NumberOrdinal = "AMAZON.Ordinal";
            public const string PhoneNumber = "AMAZON.PhoneNumber";
            public const string Time = "AMAZON.TIME";
            public const string GeneralSearchQuery = "AMAZON.SearchQuery";
            public const string ActorNames = "AMAZON.Actor";
            public const string Duration = "AMAZON.Duration";
            public const string Date = "AMAZON.Date";         
            public const string Color = "AMAZON.Color";        
            public const string DayOfTheWeek = "AMAZON.DayOfWeek";
            public const string DeviceType = "AMAZON.DeviceType";
            public const string EventType = "AMAZON.EventType";
            public const string Genre = "AMAZON.Genre";          
            public const string Month = "AMAZON.Month";           
            public const string CorporationName = "AMAZON.Corporation";        
            public const string RoomType = "AMAZON.Room";       
            public const string Animal = "AMAZON.Animal";
        }

        public static class Geography
        {
            public const string USCity = "AMAZON.US_CITY";
            public const string USState = "AMAZON.US_STATE";      
            public const string StreetName = "AMAZON.StreetName";         
            public const string Country = "AMAZON.Country";
            public const string LocalBusiness = "AMAZON.LocalBusiness";
        }

        public static class TV
        {
            public const string TVSeries = "AMAZON.TVSeries";      
            public const string VideoGame = "AMAZON.VideoGame";
            public const string Movie = "AMAZON.Movie";
        }

        public static class Music
        {          
            public const string MusicAlbum = "AMAZON.MusicAlbum";
            public const string MusicAlbumType = "AMAZON.MusicCreativeWorkType";
            public const string MusicEvent = "AMAZON.MusicEvent";
            public const string MusicGroup = "AMAZON.MusicGroup";
            public const string Musician = "AMAZON.Musician";
            public const string CommonMusicPlaylist = "AMAZON.MusicPlaylist";
            public const string MusicVenue = "AMAZON.MusicVenue";
            public const string MusicVideo = "AMAZON.MusicVideo";
        }

        public static class People
        {
            
            public const string FictionalCharacter = "AMAZON.Fictional-Character";
            public const string FamousPeople = "AMAZON.Person";
            public const string CommonUSNames = "AMAZON.US_FIRST_NAME";
            public const string Author = "AMAZON.Author";
            public const string Language = "AMAZON.Language";
        }

        public static class FoodAndBeverage
        {
            public const string Food = "AMAZON.Food";    
            public const string FoodEstablishment = "AMAZON.FoodEstablishment";
        }      
    }
}