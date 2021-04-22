using System;
using System.Collections.Generic;

namespace FootballTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class TeamResponse
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public List<Match> data { get; set; }
    }

    public class Match
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team1Goals { get; set; }
        public string Team2Goals { get; set; }
    }
}
