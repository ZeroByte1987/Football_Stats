namespace Football_Stats.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Threading;
	using System.Windows.Forms;
	using Configs;
	using DAL;
	using Logic;
	using Models;
	using UI;

	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}


		private const bool	IS_LOCAL_DATA_USED	= false;
		
		private bool				IsInitialized;
		private bool				IsComboboxChangesIgnored;
		private bool				IsSingleSeasonView;
		private int					CurrentActiveYear;

		private List<ClubInfo>		ClubShortInfo;
		private List<ComboBox>		TeamSelectors;
		private List<ClubHistory>	FetchedClubs;

		private string				ActiveCountryName		{ get { return CountryCMB.SelectedItem.ToString().Trim(); }} 
		
		private Dictionary<int, ClubHistory>	SelectedClubs;


		#region Form_Load & Form_Paint

		private void	FetchAndWriteAllData()
		{
			foreach (var countryName in from object item in CountryCMB.Items select ((string) item).Trim())
			{
				var allClubs = PagesParser.GetAllTeams(countryName);
				if (allClubs == null)
				{
					return;
				}

				var clubHistories = DataReader.ReadFullData(countryName);;

//				var clubHistories = new List<ClubHistory>();
//				foreach (var club in allClubs.Where(c => c.CurrentDivision < 5))
//				{
//					clubHistories.Add(PagesParser.GetAllSeasonsForTeam(club.ClubName, club.Url));
//					Thread.Sleep(5000);
//				}

				foreach (var club in clubHistories)
				{
					var fileName = Config.ClubsDataDirectory + @"\" + countryName + @"\" + club.ClubName + ".data";
					if (File.Exists(fileName)) continue;

					foreach (var season in club.Seasons.Where(s => s.FinishYear > 1990))
					{
						var clubUrl = allClubs.Single(c => c.ClubName == club.ClubName).Url;
						season.Games = PagesParser.GetSeasonGamesForTeam(season, club.ClubName, clubUrl);
						Thread.Sleep(10000);
					}

					DataWriter.WriteClubHistory(countryName, club);
				}

//				DataWriter.WriteFullData(countryName, clubHistories);
			}
		}


		private void	MainForm_Load(object sender, EventArgs e)
		{
//			FetchAndWriteAllData();

			Init();
			CreateTeamSelectors();
			ControlsManager.DrawCounters();
			SeasonYearCMB.Visible = IsSingleSeasonView;
			SeasonYearCMB.SelectedIndex = CurrentActiveYear - Config.FirstYear;
		}

		
		#endregion


		private void	ChangeFormDimensions()
		{
			Width = ConfigUI.SeasonsTotalWidth + 490;
			GraphsPanel.Width = ConfigUI.SeasonsTotalWidth + 206;
			GraphicsPB.Width = ConfigUI.SeasonsTotalWidth + 170;
		}

		private void	Init()
		{
			ChangeFormDimensions();

			CountryCMB.SelectedIndex = 0;
			
			IsInitialized = true;
			DoubleBuffered = true;

			CurrentActiveYear = Config.LastYear;
			GraphicsPB.Left = 0;
			GraphicsPB.Height = 960;
			GraphsPanel.Controls.Add(GraphicsPB);
			
			FetchedClubs  = FetchedClubs ?? new List<ClubHistory>();
			SelectedClubs = SelectedClubs ?? new Dictionary<int, ClubHistory>();
			TableMarkup.Initialize(GraphicsPB);
			ControlsManager.Initialize(YearsLbl, YearsLbl2);
			CriteriaCMB.SelectedIndex = 0;

			for (var i = Config.FirstYear; i <= Config.LastYear; i++)
			{
				SeasonYearCMB.Items.Add("         Season  " + (i-1) + " / " + i);
			}
		}

		private void	CreateTeamSelectors()
		{
			var font = new Font("Microsoft Sans Serif", 10);
			TeamSelectors = new List<ComboBox>();

			for (var i = 0; i < 12; i++)
			{
				var combobox = new ComboBox
					{
						Width = 200,
						Font = font,
						Location = new Point(20, 60 + i*40),
						DropDownStyle = ComboBoxStyle.DropDownList,
						Tag = i
					};
				combobox.Items.AddRange(ClubShortInfo.Select(a => (object)(a.CurrentDivision + "  -  " + a.ClubName)).ToArray());
				combobox.Items.Insert(0, "");
				combobox.SelectedIndexChanged += TeamComboBox_SelectedIndexChanged;

				TeamSelectors.Add(combobox);
				Controls.Add(combobox);
			}
		}		
	}
}