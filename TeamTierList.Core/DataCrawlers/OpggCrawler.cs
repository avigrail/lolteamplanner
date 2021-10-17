using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TeamTierList.Core
{
    public class OpggCrawler : ICrawlerService
    {
        private readonly string sourceUrl= "https://euw.op.gg/champion/statistics";
        private readonly string statsUrl = "https://euw.op.gg/champion/";


        public List<Champion> GetData()
        {
            WebClient client = new WebClient();
            string source = client.DownloadString(sourceUrl);

            var lines = source.Split('\n');

            var champions = new List<Champion>();

            for (int i= 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (!line.Contains("data-champion-key")) continue;
                line = line.Substring(0, line.IndexOf('>') + 1);
                var xel = XElement.Parse($"{line}</div>");
                string urlName = xel.Attribute("data-champion-key").Value;
                var imageUrl = $"https://opgg-static.akamaized.net/images/lol/champion/{urlName}.png?image";

                champions.Add(new Champion
                {
                    Name = urlName,
                    ImageUrl = imageUrl,
                    StatsUrl = $"{statsUrl}{urlName}"
                }) ;
            }

            return champions;
        }
    }
}
