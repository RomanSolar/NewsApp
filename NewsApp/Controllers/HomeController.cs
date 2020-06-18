using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace NewsApp.Controllers{


    class NewsAPIParse
    {
        public NewsAPIParse()
        {

        }


        public static List<NewsEntry> Parse()
        {


            Console.WriteLine("==================================================");
            string jsonText = "";

            WebClient client = new WebClient();
            try
            {
                Stream stream = client.OpenRead("https://newsapi.org/v2/top-headlines?country=us&apiKey=1085fdb5600240acbd2b45cc9d32361d");
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




    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index(){
            List<NewsEntry> articles = NewsAPIParse.Parse();
            var url = "https://newsapi.org/v2/top-headlines?country=us&apiKey=1085fdb5600240acbd2b45cc9d32361d";
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

