using FootballTeam.Services;
using System;
using System.Collections.Generic;

namespace FootballTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            TeamService teamService = new TeamService();
            int Total = teamService.GetTotalGoals("Barcelona", 2011);
            Console.WriteLine("El equipo {0} en el año {1} hizo {2} goles.", "Barcelona", 2011, Total);
            Total = teamService.GetTotalGoals("AC Milan", 2011);
            Console.WriteLine("El equipo {0} en el año {1} hizo {2} goles.", "AC Milan", 2011, Total);
            Console.Read();
        }
    }

}
