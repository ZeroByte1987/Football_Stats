namespace Football_Stats.Models
{
	public class CountryLeague
	{
		public string	Country				{ get; set; }
		public int		HistoryFirstYear	{ get; set; }
		public int		HistoryLastYear		{ get; set; }
		public int		TopDivisionSize		{ get; set; }
		public int		IndexOnSite			{ get; set; }
		public int		OffsetOnSite		{ get; set; }
		public int		PositionsCount		{ get; set; }

		public int		GamesPerSeason		{ get { return (TopDivisionSize - 1) * 2; }}

		public CountryLeague(string country, int firstYear, int lastYear, int divisionSize, int index, int offset, int positionsCount)
		{
			Country = country;
			HistoryFirstYear = firstYear;
			HistoryLastYear = lastYear;
			TopDivisionSize = divisionSize;
			IndexOnSite = index;
			OffsetOnSite = offset;
			PositionsCount = positionsCount;
		}
	}
}