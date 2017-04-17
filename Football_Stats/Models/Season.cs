namespace Football_Stats.Models
{
	using System.Collections.Generic;
	using System.Linq;
	using Configs;


	public class Season
	{
		public string	Country			{ get; set; }
		public int		FinishYear		{ get; set; }
		public int		LeagueOrder		{ get; set; }
		public int		Position		{ get; set; }
		public int		RealPosition	{ get { return Position + GetLeaguePositionOffset(); }}

		public int		GamesInSeason	{ get { return Config.GetGamesPerSeason(Country, FinishYear, LeagueOrder); }}
		public int		GamesPlayed		{ get; set; }
		public int		Wins			{ get; set; }
		public int		Draws			{ get; set; }
		public int		Losses			{ get; set; }
		public int		Points			{ get; set; }

		public int		GoalDifference	{ get { return Games == null ? 0 : Games.Sum(a => a.GoalDifference); }}

		public List<Game> Games		{ get; set; }


		public Season()
		{
			Games = new List<Game>();
		}

		public int GetLeaguePositionOffset()
		{
			return Config.GetLeaguePositionOffset(this);
		}
	}


	public class Game
	{
		public Season	Season			{ get; set; }
		public int		GoalsFor		{ get; set; }
		public int		GoalsAgainst	{ get; set; }
		public int		PositionAfter	{ get; set; }
		public int		RealPosition	{ get { return PositionAfter + Season.GetLeaguePositionOffset(); }}

		public int		GoalDifference	{ get { return GoalsFor - GoalsAgainst; }}
	}
}
