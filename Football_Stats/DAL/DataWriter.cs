namespace Football_Stats.DAL
{
	using System.Collections.Generic;
	using System.IO;
	using Configs;
	using Models;


	public static class DataWriter
	{
		public static void		WriteFullData(string countryName, List<ClubHistory> clubHistories)
		{
			if (!Directory.Exists(Config.ClubsDataDirectory))
			{
				Directory.CreateDirectory(Config.ClubsDataDirectory);
			}

			var fileName = Config.ClubsDataDirectory + @"\" + countryName + ".data";
			var file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			var writer = new BinaryWriter(file);

			writer.Write((byte)clubHistories.Count);

			foreach (var club in clubHistories)
			{
				WriteClubHistoryInternal(writer, club);
			}

			writer.Close();
		}

		public static void		WriteClubHistory(string countryName, ClubHistory clubHistory)
		{
			var fileName = Config.ClubsDataDirectory + @"\" + countryName + @"\" + clubHistory.ClubName + ".data";
			var file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			var writer = new BinaryWriter(file);

			WriteClubHistoryInternal(writer, clubHistory);

			writer.Close();
		}


		private static void		WriteClubHistoryInternal(BinaryWriter writer, ClubHistory club)
		{
			writer.Write(club.ClubName);
			writer.Write((byte)club.Seasons.Count);

			foreach (var season in club.Seasons)
			{
				WriteClubSeason(writer, season);
			}
		}


		private static void		WriteClubSeason(BinaryWriter writer, Season season)
		{
			writer.Write(season.FinishYear);
			writer.Write((byte)season.LeagueOrder);
			writer.Write((byte)season.Position);
			writer.Write((byte)season.Points);

			writer.Write((byte)season.GamesPlayed);
			writer.Write((byte)season.Wins);
			writer.Write((byte)season.Draws);
			writer.Write((byte)season.Losses);
					
			writer.Write((byte)season.Games.Count);
			foreach (var game in season.Games)
			{
				WriteGameResult(writer, game);
			}
		}


		private static void		WriteGameResult(BinaryWriter writer, Game game)
		{
			writer.Write((byte)game.GoalsFor);
			writer.Write((byte)game.GoalsAgainst);
			writer.Write((byte)game.PositionAfter);
		}
	}
}