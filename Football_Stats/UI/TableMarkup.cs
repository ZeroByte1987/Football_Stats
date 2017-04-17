namespace Football_Stats.UI
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;
	using Configs;
	using Models;


	public static class TableMarkup
	{
		private static Graphics _mainGraphics;
		private static PictureBox _pictureBox;


		public static void	Initialize(PictureBox pictureBox)
		{
			_pictureBox = pictureBox;
		}


		public static void	DrawMarkup(bool isSingleSeasonView, string country, int activeYear, Dictionary<int, ClubHistory> selectedClubs)
		{
			var selectedLeaguesGameCounts = selectedClubs.Values
				.SelectMany(x => x.Seasons.Where(s => s.FinishYear == activeYear))
				.Select(w => w.GamesInSeason)
				.ToArray();

			var newSeasonGamesCount = isSingleSeasonView  &&  selectedClubs.Count > 0
				? selectedLeaguesGameCounts.Max()
				: Config.SeasonGamesCount;

			var _tempImage = new Bitmap(_pictureBox.Width, _pictureBox.Height);
			_mainGraphics = Graphics.FromImage(_tempImage);

			SetNewSeasonGamesCount(isSingleSeasonView, newSeasonGamesCount);
			DrawVerticalLines(isSingleSeasonView, Config.PositionCount, selectedLeaguesGameCounts);
			DrawHorizontalLines(isSingleSeasonView, Config.PositionCount, country, activeYear);
			_pictureBox.Image = _tempImage;
		}

		private static void	DrawVerticalLines(bool isSingleSeasonView, int positionCount, int[] gameCounts)
		{
			_mainGraphics.DrawLine(Pens.Black, 46, 9, 46, (positionCount+1) * ConfigUI.CellHeight);
			
			var width = isSingleSeasonView ? ConfigUI.WeekCellWidth : ConfigUI.SeasonCellWidth;
			var linesCount = isSingleSeasonView ? Config.SeasonGamesCount : Config.SeasonCount;
			for (var i = 0; i < linesCount; i++)
			{
				var xOffset = 47 + (i+1) * width;
				var penColor = (isSingleSeasonView  &&  gameCounts.Contains(i+1))  ||  i == linesCount-1 ? Pens.Black : Pens.LightGray;
				_mainGraphics.DrawLine(penColor, xOffset, 10, xOffset, (positionCount+1) * ConfigUI.CellHeight);
			}
		}

		private static void	DrawHorizontalLines(bool isSingleSeasonView, int positionCount, string country, int activeYear)
		{
			var index = 0;
			var grayBrush = new SolidBrush(Color.DimGray);
			var width = isSingleSeasonView ? ConfigUI.WeekCellWidth : ConfigUI.SeasonCellWidth;

			var tokensCount = isSingleSeasonView ? Config.SeasonGamesCount : Config.SeasonCount;
			var oldDivisionOffsets = Config.GetLeaguePositionOffsets(country, isSingleSeasonView ? activeYear : Config.FirstYear);

			for (var j = 0; j < tokensCount; j++)
			{
				var xPos1 = 47 + j * width;
				var xPos2 = 46 + (j+1) * width;
				var divisionOffsets = Config.GetLeaguePositionOffsets(country, isSingleSeasonView ? activeYear : Config.FirstYear + j);

				for (var i = 0; i <= positionCount; i++)
				{
					var yPos = 10 + i * ConfigUI.CellHeight;

					if (divisionOffsets.Contains(i)  ||  i == positionCount)
					{
						_mainGraphics.FillRectangle(grayBrush, xPos1, yPos-1, width+1, 2);
						index = 0;
					}
					else
					{
						var penColor = index % 5 == 0 ? Pens.DarkGray : Pens.LightGray;
						_mainGraphics.DrawLine(penColor, xPos1, yPos, xPos2, yPos);
					}
					index++;
				}

				DrawDivisionSeparators(divisionOffsets, oldDivisionOffsets, grayBrush, xPos1);
				oldDivisionOffsets = divisionOffsets;
			}
		}
		
		private static void DrawDivisionSeparators(int[] divisionOffsets, int[] oldDivisionOffsets, Brush grayBrush, int xPosition)
		{
			for (var i = 0; i < divisionOffsets.Length; i++)
			{
				if (divisionOffsets[i] != oldDivisionOffsets[i])
				{
					var topY = divisionOffsets[i] > oldDivisionOffsets[i] ? oldDivisionOffsets[i] : divisionOffsets[i];
					var length = Math.Abs(divisionOffsets[i] - oldDivisionOffsets[i]) * ConfigUI.CellHeight + 1;
					_mainGraphics.FillRectangle(grayBrush, xPosition, 10 + topY * ConfigUI.CellHeight, 2, length);
				}
			}
		}


		public static void	DrawTeamPositions(bool isSingleSeasonView, string country, int activeYear, Dictionary<int, ClubHistory> selectedClubs)
		{
			var processedTeams = new List<string>();
			var width = isSingleSeasonView ? ConfigUI.WeekCellWidth : ConfigUI.SeasonCellWidth;

			foreach (var club in selectedClubs)
			{
				var clubIndex = club.Key;
				var clubInfo = club.Value;
				if (processedTeams.Contains(clubInfo.ClubName)) continue;
				processedTeams.Add(clubInfo.ClubName);

				var brush = new SolidBrush(Config.Colors[clubIndex]);
				var pen = new Pen(Config.Colors[clubIndex]);
				var previousPoint = new Point(0, 0);
				
				var season = clubInfo.Seasons.First(a => a.FinishYear == activeYear);
				var maxCount = isSingleSeasonView ? season.Games.Count : Config.SeasonCount;

				_mainGraphics.FillRectangle(brush, 4, 44 + clubIndex*40, 30, 23);

				for (var i = 0; i < maxCount; i++)
				{
					var realPosition = GetRealPosition(isSingleSeasonView, clubInfo, season, i);
					if (realPosition == -1)
					{
						previousPoint = new Point(0, 0);
						continue;
					}

					var newPoint = new Point(48 + width/2 + i*width, 10-(ConfigUI.CellHeight/2) + realPosition * ConfigUI.CellHeight);
					_mainGraphics.FillRectangle(brush, newPoint.X-1, newPoint.Y-1, 3, 3);

					if (previousPoint.X != 0)
					{
						_mainGraphics.DrawLine(pen, previousPoint, newPoint);
					}
					previousPoint = newPoint;
				}
			}
		}

		private static int	GetRealPosition(bool isSingleSeasonView, ClubHistory clubInfo, Season season, int index)
		{
			if (isSingleSeasonView)
				return season.Games[index].PositionAfter + season.GetLeaguePositionOffset();

			season = clubInfo.Seasons.FirstOrDefault(a => a.FinishYear == Config.FirstYear + index);
			return season == null ? -1 : season.RealPosition;
		}


		private static void SetNewSeasonGamesCount(bool isSingleSeasonView, int newSeasonGamesCount)
		{
			if (newSeasonGamesCount != Config.SeasonGamesCount)
			{
				Config.SeasonGamesCount = newSeasonGamesCount;
				ControlsManager.DrawCounters(isSingleSeasonView);
			}
		}
	}
}