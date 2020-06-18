using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{

    public class NewsEntry {
        public string id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int totalResults { get; set; }
        public string urlToImage { get; set; }
        public string publishedAt { get; set; }
        public string content { get; set; }

        public override string ToString()
        {
            string s = "";
            s += $"Sourse id:{id}\nSourse name: {name}\narticle aouthor:{author}\narticle title: {title}\narticle description: {description}\narticle url: {url}\n published at {publishedAt}\ncontent: {content}";
            return s;
        }
    }

}
