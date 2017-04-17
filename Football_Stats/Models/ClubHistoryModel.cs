namespace Football_Stats.Models
{
	using System.Collections.Generic;


	public class ClubHistory
	{
		public string		ClubName		{ get; set; }
		public List<Season>	Seasons			{ get; set; }

		public Season		CurrentSeason	{ get { return (Seasons != null  &&  Seasons.Count > 0) ? Seasons[0] : null; }}


		public ClubHistory()
		{
			Seasons = new List<Season>();
		}
	}
}