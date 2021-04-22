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
            var httpClient = new HttpClient();
            var listUri = new List<Uri>();
            var lisTask = new List<Task<HttpResponseMessage>>();

            UriBuilder builder = new UriBuilder(BaseUrl);
            builder.Query = $"year={year}&team1={team}";
            listUri.Add(builder.Uri);

            builder = new UriBuilder(BaseUrl);
            builder.Query = $"year={year}&team2={team}";
            listUri.Add(builder.Uri);

            foreach (Uri uri in listUri)
            {
                lisTask.Add(httpClient.GetAsync(uri));
            }
            Task.WaitAll(lisTask.ToArray());

            var listResponse = new List<TeamResponse>();
            foreach (var task in lisTask)
            {
                var res = task.Result;
                var tResponse = res.StatusCode == HttpStatusCode.OK ? JsonSerializer.Deserialize<TeamResponse>(res.Content.ReadAsStringAsync().Result) : default;
                listResponse.Add(tResponse);
            }

            if (listResponse.ElementAt(0).total_pages > 1)
            {
                listUri = new List<Uri>();
                for (int i = 2; i <= listResponse.ElementAt(0).total_pages; i++)
                {
                    builder = new UriBuilder(BaseUrl);
                    builder.Query = $"year={year}&team1={team}&page={i}";
                    listUri.Add(builder.Uri);
                    builder = new UriBuilder(BaseUrl);
                    builder.Query = $"year={year}&team2={team}&page={i}";
                    listUri.Add(builder.Uri);
                }
                lisTask = new List<Task<HttpResponseMessage>>();
                foreach (Uri uri in listUri)
                {
                    lisTask.Add(httpClient.GetAsync(uri));
                }
                Task.WaitAll(lisTask.ToArray());
                foreach (var task in lisTask)
                {
                    var res = task.Result;
                    var tResponse = res.StatusCode == HttpStatusCode.OK ? JsonSerializer.Deserialize<TeamResponse>(res.Content.ReadAsStringAsync().Result) : default;
                    listResponse.Add(tResponse);
                }
            }

            var TotalGoalsVisit = (from r in listResponse
                                  from m in r.data
                                  where m.team1 == team
                                  select Convert.ToInt32(m.team1goals))
                                  .ToList()
                                  .Sum();

            var TotalGoalsHome = (from r in listResponse
                                   from m in r.data
                                   where m.team2 == team
                                   select Convert.ToInt32(m.team2goals))
                                  .ToList()
                                  .Sum();

            return TotalGoalsVisit + TotalGoalsHome;

            //using (var httpClient = new HttpClient())
            //{
            //    UriBuilder builder = new UriBuilder(BaseUrl);
            //    builder.Query = $"year={year}&team1={team}";
            //    var response = httpClient.GetAsync(builder.Uri).Result;
            //    if (response.StatusCode.Equals(HttpStatusCode.OK))
            //    {
            //        var result = response.Content.ReadAsStringAsync().Result;
            //        TeamResponse teamResponse = JsonSerializer.Deserialize<TeamResponse>(result);
            //        TotalGoals += teamResponse.data.Sum(m => Convert.ToInt32(m.team1goals));
            //        builder.Query = $"year={year}&team2={team}";
            //        response = httpClient.GetAsync(builder.Uri).Result;
            //        if (response.StatusCode.Equals(HttpStatusCode.OK))
            //        {
            //            result = response.Content.ReadAsStringAsync().Result;
            //            teamResponse = JsonSerializer.Deserialize<TeamResponse>(result);
            //            TotalGoals += teamResponse.data.Sum(m => Convert.ToInt32(m.team2goals));
            //        }
            //        else 
            //        {
            //            return -1;
            //        }
            //    }
            //    else
            //    {
            //        return -1;
            //    }
            //    return TotalGoals;
            //}
        }
    }
}
