namespace Football_Stats.Forms
{
	using System;
	using System.Linq;
	using System.Windows.Forms;
	using Configs;
	using DAL;
	using Logic;
	using Models;
	using UI;

	public partial class MainForm
	{
		private void	MainForm_Paint(object sender, PaintEventArgs e)
		{
			if (IsInitialized)
			{
				TableMarkup.DrawMarkup(IsSingleSeasonView, ActiveCountryName, CurrentActiveYear, SelectedClubs);
				TableMarkup.DrawTeamPositions(IsSingleSeasonView, ActiveCountryName, CurrentActiveYear, SelectedClubs);
			}
		}
		

		private void	TeamComboBox_SelectedIndexChanged(object sender, EventArgs args)
		{
			if (IsComboboxChangesIgnored)
				return;

			var comboBox = (ComboBox) sender;
			var index = (int)comboBox.Tag;
			var text = comboBox.SelectedItem.ToString();

			if (string.IsNullOrEmpty(text))
			{
				SelectedClubs.Remove(index);
				Refresh();
				return;
			}

			var clubName = text.Substring(6);
			var clubInfo = ClubShortInfo.First(a => a.ClubName == clubName);
			
			var fetchedHistory = FetchedClubs.FirstOrDefault(a => a.ClubName == clubName);
			if (fetchedHistory == null)
			{
				var clubHistory = PagesParser.GetAllSeasonsForTeam(ActiveCountryName, clubInfo.ClubName, clubInfo.Url);
				FetchedClubs.Add(clubHistory);
				SelectedClubs[index] = clubHistory;
			}
			else
			{
				SelectedClubs[index] = fetchedHistory;
			}

			if (IsSingleSeasonView)
			{
				var season = SelectedClubs[index].Seasons.SingleOrDefault(s => s.FinishYear == CurrentActiveYear);
				if (season != null  &&  (season.Games == null  ||  season.Games.Count == 0))
				{
					season.Games = PagesParser.GetSeasonGamesForTeam(season, clubInfo.ClubName, clubInfo.Url);
				}
			}

			Refresh();
		}

		private void	SeasonYearCMB_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentActiveYear = SeasonYearCMB.SelectedIndex + 1993;
			foreach (var club in SelectedClubs)
			{
				var clubInfo = ClubShortInfo.First(a => a.ClubName == club.Value.ClubName);
				var season = club.Value.Seasons.SingleOrDefault(s => s.FinishYear == CurrentActiveYear);
				if (season != null  &&  (season.Games == null  ||  season.Games.Count == 0))
				{
					season.Games = PagesParser.GetSeasonGamesForTeam(season, clubInfo.ClubName, clubInfo.Url);
				}
			}
			Refresh();
		}

		private void	CountryCMB_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IS_LOCAL_DATA_USED)
			{
				FetchedClubs = DataReader.ReadFullData(ActiveCountryName);
			}

			ClubShortInfo = IS_LOCAL_DATA_USED
				? FetchedClubs.Select(a => new ClubInfo { ClubName = a.ClubName }).ToList()
				: PagesParser.GetAllTeams(ActiveCountryName);
			
			if (ClubShortInfo == null)
			{
				MessageBox.Show("The site-source WWW.STATTO.COM is not available.", "Error");
				Close();
				return;
			}

			if (TeamSelectors == null) return;
			foreach (var teamSelector in TeamSelectors)
			{
				teamSelector.Items.Clear();
				teamSelector.Items.AddRange(ClubShortInfo.Select(a => (object)(a.CurrentDivision + "  -  " + a.ClubName)).ToArray());
				teamSelector.Items.Insert(0, "");
			}

			Config.SeasonGamesCount = Config.GetGamesPerSeason(ActiveCountryName, CurrentActiveYear, 1);
			Config.PositionCount = Config.LeaguesInfo[ActiveCountryName].PositionsCount;
			IsSingleSeasonView = true;
			ChangeViewBtn_Click(null, null);
		}

		private void	CriteriaCMB_SelectedIndexChanged(object sender, EventArgs e)
		{
		}


		private void	ClearBtn_Click(object sender, EventArgs e)
		{
			if (SelectedClubs.Count == 0)
				return;

			var comboboxesToClear = SelectedClubs.Keys.ToArray();
			SelectedClubs.Clear();

			IsComboboxChangesIgnored = true;
			foreach (var index in comboboxesToClear)
			{
				TeamSelectors[index].SelectedIndex = 0;
			}
			IsComboboxChangesIgnored = false;

			Refresh();
		}

		private void	ChangeViewBtn_Click(object sender, EventArgs e)
		{
			ChangeViewBtn.Text = IsSingleSeasonView ? "Single season" : "All seasons history";
			IsSingleSeasonView = !IsSingleSeasonView;

			ControlsManager.DrawCounters(IsSingleSeasonView);
			SeasonYearCMB.Visible = IsSingleSeasonView;
			SeasonYearCMB.SelectedIndex = CurrentActiveYear - Config.FirstYear;

			ClearBtn_Click(null, null);
			Refresh();
		}


		private void	GraphsPanel_Scroll(object sender, ScrollEventArgs e)
		{
			Invalidate();
			Update();
		}
	}
}