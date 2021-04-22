using FootballTeam.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballTeam.Services
{
    public class TeamService
    {
        public const string BaseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
        public int GetTotalGoals(string team, int year)
        {
            int TotalGoals = 0;
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(BaseUrl);
                builder.Query = $"year={year}&team1={team}";
                var response = httpClient.GetAsync(builder.Uri).Result;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    TeamResponse teamResponse = JsonSerializer.Deserialize<TeamResponse>(result);
                    TotalGoals += teamResponse.data.Sum(m => Convert.ToInt32(m.team1goals));
                    builder.Query = $"year={year}&team2={team}";
                    response = httpClient.GetAsync(builder.Uri).Result;
                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        teamResponse = JsonSerializer.Deserialize<TeamResponse>(result);
                        TotalGoals += teamResponse.data.Sum(m => Convert.ToInt32(m.team2goals));
                    }
                    else 
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
                return TotalGoals;
            }
        }
    }
}
