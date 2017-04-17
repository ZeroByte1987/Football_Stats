namespace Football_Stats.Logic
{
	using System.Collections.Generic;
	using System.Linq;
	using HtmlAgilityPack;
	using Configs;
	using Models;


	public static class PagesParser
	{
		public static List<ClubInfo>	GetAllTeams(string countryName)
		{
			var htmlDoc = WebPagesAccess.GetHtmlBody(countryName == "England" ? Config.EnglandTeamsUrl : Config.OtherLeaguesUrl);
			if (htmlDoc == null) return null;

			var teamList = htmlDoc.GetElementbyId("team-list").ChildNodes
				.Where(a => a.Name == "li"  &&  a.FirstChild.Name == "a")
				.Select(a => a.FirstChild)
				.Select(a => new ClubInfo { ClubName = a.InnerText, Url = a.Attributes["href"].Value }).ToList();

			return countryName == "England"
				? getEnglishTeams(teamList)
				: getOtherTeams(teamList, countryName);
		}

		private static List<ClubInfo>	getEnglishTeams(List<ClubInfo> teamList)
		{
			var englishLeagueSizes = Config.GetEnglishLeagueSizes(Config.LastYear);

			for (var i = 0; i < teamList.Count; i++)
			{
				var totalSize = 0;
				for (var j = 0; j < englishLeagueSizes.Length; j++)
				{
					totalSize += englishLeagueSizes[j];
					if (i < totalSize)
					{
						teamList[i].CurrentDivision = j+1;
						break;
					}
				}
			}

			return teamList;
		}
		
		private static List<ClubInfo>	getOtherTeams(IEnumerable<ClubInfo> teamList, string countryName)
		{
			var leagueInfo = Config.LeaguesInfo[countryName];
			var teams = teamList.Skip(leagueInfo.OffsetOnSite).Take(leagueInfo.TopDivisionSize).ToList();
			foreach (var team in teams)
			{
				team.CurrentDivision = 1;
			}
			return teams;
		}


		public static ClubHistory		GetAllSeasonsForTeam(string country, string clubName, string url)
		{
			var result = new ClubHistory { ClubName = clubName };
			var htmlDoc = WebPagesAccess.GetHtmlBody(Config.MainSite + url + "/history");

			result.Seasons = htmlDoc.DocumentNode
				.SelectSingleNode("//table[@class='table history lightbox']")
				.SelectNodes("//tr[@data-row]")
				.Select(a => new Season
					             {
									Country			= country,
									FinishYear		= int.Parse(a.ChildNodes[0].InnerText.Substring(5)),
									LeagueOrder		= getCellValue(a, 1),
									Position		= getCellValue(a, 2),
									GamesPlayed		= getCellValue(a, 3),
									Wins			= getCellValue(a, 4),
									Draws			= getCellValue(a, 5),
									Losses			= getCellValue(a, 6),
									Points			= getCellValue(a, 23),
					             }).ToList();
			return result;
		}
		
		
		public static List<Game>		GetSeasonGamesForTeam(Season season, string clubName, string url)
		{
			var fullUrl = string.Format("{0}{1}/{2}-{3}/results", Config.MainSite, url, season.FinishYear - 1, season.FinishYear);
			var htmlDoc = WebPagesAccess.GetHtmlBody(fullUrl);

			return htmlDoc.DocumentNode
				.SelectSingleNode("//table[@class='results team-results']")
				.SelectSingleNode("tbody")
				.SelectNodes("tr")
				.Where(r => r.Attributes[0].Value != "fix")
				.Select(tr =>
					        {
						        var goals = tr.ChildNodes[4].InnerText.Substring(1).Split('-');
						        return new Game
							               {
											   Season		= season,
								               GoalsAgainst = int.Parse(goals[0]),
								               GoalsFor		= int.Parse(goals[1]),
								               PositionAfter = getCellValue(tr, 5),
							               };
					        }).ToList();
		}


		private static int				getCellValue(HtmlNode row, int cellIndex)
		{
			return int.Parse(row.ChildNodes[cellIndex].InnerText);
		}
	}
}