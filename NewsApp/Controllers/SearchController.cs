using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using Newtonsoft.Json.Linq;

namespace NewsApp.Controllers
{
    class NewsAPIParseSearch
    {
        public NewsAPIParseSearch()
        {

        }


        public static List<NewsEntry> Parse(string url)
        {


            Console.WriteLine("==================================================");
            string jsonText = "";

            WebClient client = new WebClient();
            try
            {
                Stream stream = client.OpenRead(url);
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonText = reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("A network error occurred.  " + ex.Message);
                Console.WriteLine("Unable to continue.");
            }

            JObject newsJO = JObject.Parse(jsonText);
            var articlesJA = newsJO["articles"].Children().ToList();

            var articles = new List<NewsEntry>();
            foreach (var item in articlesJA)
            {
                articles.Add(item.ToObject<NewsEntry>());
            }

            foreach (NewsEntry article in articles)
            {

                Console.WriteLine(article);
            }

            return articles;
            //Console.ReadLine();
        }
    }



    public class SearchController : Controller
    {
        [HttpPost]
        public IActionResult Index(string searchString)
        {
            string url = "https://newsapi.org/v2/everything?" + "q=" + searchString +
                "&from=" + DateTime.Today.ToString() +
                "&sortBy=popularity&apiKey=1085fdb5600240acbd2b45cc9d32361d";

            List<NewsEntry> articles = NewsAPIParseSearch.Parse(url);
            
            return View(articles);
        }
    }
}