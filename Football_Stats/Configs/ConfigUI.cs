namespace Football_Stats.Configs
{
	public static class ConfigUI
	{
		public const int	CellHeight		= 12;
		public const int	SeasonCellWidth	= 29;
		public const int	WeekCellWidth	= 18;

		public static int	SeasonsTotalWidth
		{
			get { return Config.SeasonCount * SeasonCellWidth; }
		}

		public static int WeekTotalWidth
		{
			get { return Config.SeasonGamesCount * WeekCellWidth; }
		}
	}
}