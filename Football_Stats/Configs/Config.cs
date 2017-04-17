namespace Football_Stats.Configs
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using Models;


	public static class Config
	{
		public static string	EnglandTeamsUrl		= "http://www.statto.com/football/teams";
		public static string	OtherLeaguesUrl		= "http://www.statto.com/football/teams/european-and-world-league";
		public static string	MainSite			= "http://www.statto.com";

		public static string	ClubsDataDirectory	= "ClubsData";

		public static int		FirstYear			= 1993;
		public static int		LastYear			= 2017;
		public static int		SeasonCount			= LastYear - FirstYear + 1;
		public static int		SeasonGamesCount	= 38;
		public static int		PositionCount		= 98;

		private static Tuple<int, int[]>[] EnglishLeagueSizes = new[]
			{
				new Tuple<int, int[]>(1992, new [] { 22, 24, 24, 22, 24 }),
				new Tuple<int, int[]>(1996, new [] { 20, 24, 24, 24, 24 })
			};

		public static Dictionary<string, CountryLeague> LeaguesInfo = new Dictionary<string, CountryLeague>
			{
				{ "England", new CountryLeague("England", 1993, LastYear, 20, 0, 0,  98) },
				{ "Spain",	 new CountryLeague("Spain",   1999, LastYear, 20, 0, 0,  20) },
				{ "Italy",	 new CountryLeague("Italy",   1999, LastYear, 20, 1, 20, 20) },
				{ "Germany", new CountryLeague("Germany", 1999, LastYear, 18, 2, 40, 18) },
				{ "France",	 new CountryLeague("France",  1999, LastYear, 20, 3, 58, 20) }
			};

		
		public static int[]		GetEnglishLeagueSizes(int year)
		{
			return EnglishLeagueSizes.Last(w => w.Item1 <= year).Item2;
		}

		public static int[]		GetLeaguePositionOffsets(string country, int year)
		{
			if (country != "England")
				return new [] { 0, LeaguesInfo[country].TopDivisionSize };

			var leagueSizes = EnglishLeagueSizes.Last(w => w.Item1 <= year).Item2;
			var offsets = new int[5];
			var offset = 0;
			for (var i = 0; i < 5; i++)
			{
				offsets[i] = offset;
				offset += leagueSizes[i];				
			}
			return offsets;
		}

		public static int		GetLeaguePositionOffset(Season season)
		{
			return GetLeaguePositionOffsets(season.Country, season.FinishYear)[season.LeagueOrder - 1];
		}

		public static int		GetGamesPerSeason(string country, int year, int leagueOrder)
		{
			if (country != "England")
				return LeaguesInfo[country].TopDivisionSize * 2 - 2;

			var leagueSizes = GetEnglishLeagueSizes(year);
			return leagueSizes[leagueOrder-1] * 2 - 2;
		}


		public static Color[]	Colors	= new []
			{
				Color.FromArgb(0, 0, 255),
				Color.FromArgb(255, 0, 0),
				Color.FromArgb(0, 255, 0),
				Color.FromArgb(255, 0, 255),
				Color.FromArgb(180, 180, 0),
				Color.FromArgb(0, 255, 255),

				Color.FromArgb(0, 0, 180),
				Color.FromArgb(180, 0, 0),
				Color.FromArgb(0, 180, 0),
				Color.FromArgb(180, 0, 180),
				Color.FromArgb(255, 255, 0),
				Color.FromArgb(0, 180, 180),
			};
	}
}