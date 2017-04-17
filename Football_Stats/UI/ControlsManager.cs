namespace Football_Stats.UI
{
	using System.Drawing;
	using System.Windows.Forms;
	using Configs;


	public static class ControlsManager
	{
		private static Label	_counterLabelTop;
		private static Label	_counterLabelBottom;
		private static Font		_yearsFont;
		private static Font		_weeksFont;



		public static void Initialize(Label counterLabelTop, Label counterLabelBottom)
		{
			_counterLabelTop = counterLabelTop;
			_counterLabelBottom = counterLabelBottom;
			_yearsFont = new Font("Microsoft Sans Serif", 9);
			_weeksFont = new Font("Microsoft Sans Serif", 8);
		}


		public static void	DrawCounters(bool isSingleSeasonView = false)
		{
			var counterText = string.Empty;
			if (isSingleSeasonView)
			{
				_counterLabelTop.Font = _weeksFont;
				_counterLabelTop.Left = 288;
				_counterLabelBottom.Top = 3;
				for (var i = 1; i <= Config.SeasonGamesCount; i++)
					counterText += i.ToString().PadLeft(2, '0') + "  ";
			}
			else
			{
				_counterLabelTop.Font = _yearsFont;
				_counterLabelTop.Left = 290;
				_counterLabelBottom.Top = 1;
				
				for (var i = Config.FirstYear; i < Config.FirstYear + Config.SeasonCount; i++)
					counterText += "'" + (i % 100).ToString().PadLeft(2, '0') + "    ";
			}

			_counterLabelBottom.Left = _counterLabelTop.Left;
			_counterLabelBottom.Font = _counterLabelTop.Font;			
			_counterLabelTop.Text = counterText;
			_counterLabelBottom.Text = counterText;			
		}
	}
}