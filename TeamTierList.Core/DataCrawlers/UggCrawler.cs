using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TeamTierList.Core
{
    public class UggCrawler : ICrawlerService
    {
        private readonly string sourceUrl = "https://u.gg/lol/champions";
        private readonly string statsUrl = "https://u.gg/lol/champions/";

        public List<Champion> GetData()
        {
            var client = new WebClient();
            var source = client.DownloadString(sourceUrl);

            var championDataStrings = GetChampionDataStrings(source);

            var champions = new List<Champion>();

            foreach (var championDataString in championDataStrings)
            {
                var xElement = XElement.Parse(championDataString);

                var championName = xElement.LastAttribute.Value;
                var newChampion = new Champion
                {
                    Name = championName,
                    ImageUrl = xElement.Attribute("src").Value,
                    StatsUrl = $"{statsUrl}{championName}"
                };

                champions.Add(newChampion);
            }

            return champions;
        }

        public static IEnumerable<string> GetChampionDataStrings(string source)
        {
            Regex regex = new Regex("<img style=\"width:100%\" src=\"https://static.u.gg/assets/lol/riot_static/.+?/img/champion/[a-zA-Z]+.png\" alt=\".+?\"/>", RegexOptions.IgnoreCase);

            var matches = regex.Matches(source);

            foreach (var match in matches)
            {
                yield return match.ToString();
            }
        }
    }
}
