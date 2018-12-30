using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sample
{
    public static class Teams
    {
        private static readonly List<Team> TeamList = GetAll().ToList();
        private static readonly Random RandomNumber = new Random();

        public static Team GetTeamByCity(string cityId)
        {
            return TeamList.SingleOrDefault(t => t.CityId == cityId.ToLowerInvariant());
        }

        public static Team GetTeam(string id)
        {
            return TeamList.SingleOrDefault(t => t.Id == id.ToLowerInvariant());
        }
        
        public static string GenerateAFakeScore(Team team)
        {
            Team otherTeam;
            
            while (true)
            {
                otherTeam = TeamList[RandomNumber.Next(0, 7)];
                if (otherTeam.Id != team.Id)
                {
                    break;
                }
            }

            var home = RandomNumber.Next(0, 8);
            var visiting = RandomNumber.Next(0, 8);

            if (home == visiting)
            {
                home++;
            }

            if (home > visiting)
            {
                return $"{team.Name} beat the {otherTeam.Name} with a score of {home} to {visiting}";
            }
            else
            {
                return $"{otherTeam.Name} beat the {team.Name} with a score of {visiting} to {home}";
            }
        }
        
        
        
        private static IEnumerable<Team> GetAll()
        {
            yield return new Team("yyc", "Calgary", "cyf", "Flames");
            yield return new Team("ywg", "Winnipeg", "wgj", "Jets");
            yield return new Team("yeg", "Edmonton", "edo", "Oilers");
            yield return new Team("yvr", "Vancouver", "vnc", "Canucks");
            yield return new Team("lax", "Los Angeles", "lak", "Kings");
            yield return new Team("sjc", "San Jose", "sjs", "Sharks");
            yield return new Team("sna", "Anaheim", "and", "Ducks");
            //Yes I know Arizona is not a city...It's how the team is called :-)
            yield return new Team("phx", "Arizona", "azc", "Coyotes");            
        }
    }
}