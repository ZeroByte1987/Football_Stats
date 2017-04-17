namespace Football_Stats.DAL
{
	using System.Collections.Generic;
	using System.IO;
	using Configs;
	using Models;


	public static class DataReader
	{
		public static List<ClubHistory>	ReadFullData(string countryName)
		{
			var fileName = Config.ClubsDataDirectory + @"\" + countryName + ".data";
			if (!File.Exists(fileName)) return null;
			
			var file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			var reader = new BinaryReader(file);

			var clubCount = reader.ReadByte();
			var clubHistories = new List<ClubHistory>();

			for (var i = 0; i < clubCount; i++)
			{
				var clubHistory = ReadClubHistory(reader);
				clubHistories.Add(clubHistory);
			}

			reader.Close();
			return clubHistories;
		}


		private static ClubHistory		ReadClubHistory(BinaryReader reader)
		{
			reader.ReadByte();
			var clubHistory = new ClubHistory { ClubName = new string (reader.ReadChars(reader.ReadByte())) };
			var seasonCount = reader.ReadByte();

			for (var i = 0; i < seasonCount; i++)
			{
				var season = ReadClubSeason(reader);
				clubHistory.Seasons.Add(season);
			}

			return clubHistory;
		}


		private static Season			ReadClubSeason(BinaryReader reader)
		{
			var season = new Season();

			season.FinishYear	= reader.ReadInt32();
			season.LeagueOrder	= reader.ReadByte();
			season.Position		= reader.ReadByte();
			season.Points		= reader.ReadByte();

			season.GamesPlayed	= reader.ReadByte();
			season.Wins			= reader.ReadByte();
			season.Draws		= reader.ReadByte();
			season.Losses		= reader.ReadByte();

			var gameCount = reader.ReadByte();
			for (var i = 0; i < gameCount; i++)
			{
				var game = ReadGameResult(reader);
				season.Games.Add(game);
			}

			return season;
		}


		private static Game				ReadGameResult(BinaryReader reader)
		{
			return new Game
				{
					GoalsFor		= reader.ReadByte(),
					GoalsAgainst	= reader.ReadByte(),
					PositionAfter	= reader.ReadByte()
				};
		}
	}
}