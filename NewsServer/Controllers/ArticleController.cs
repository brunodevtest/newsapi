using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using NewsServer.Model;

namespace NewsServer.Controllers
{
    [ApiController]
    [EnableCors("Policy1")]
    [Route("api/[controller]/[action]")]
    public class ArticleController : ControllerBase
    {
        private string apiURL = "https://hacker-news.firebaseio.com/v0/";
        public ArticleController() {}

        [HttpGet(Name = "GetLatest")]
        public async Task<List<Article>> GetLatest(int page)
        {
            var latestArticlesList = new List<int>();
            var sequence = new List<Article>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync( apiURL + "topstories.json?print=pretty" ))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    latestArticlesList = JsonConvert.DeserializeObject<List<int>>(apiResponse);
                }
            }

            foreach (int articleId in latestArticlesList.GetRange((page-1)*10,10))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(string.Format( apiURL + "item/{0}.json", articleId)))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var article = JsonConvert.DeserializeObject<Article>(apiResponse);
                        sequence.Add(article);
                    }
                }
            }

            return sequence;
        }
    }
}

