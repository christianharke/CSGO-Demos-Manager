﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace Core.Models
{
	public class Team : ObservableObject
	{
		private string _name;

		[JsonProperty("team_name")]
		public string Name
		{
			get { return _name; }
			set { Set(() => Name, ref _name, value); }
		}

		[JsonProperty("team_players", IsReference = false)]
		public ObservableCollection<Player> Players { get; set; }

		/// <summary>
		/// Keep track of loss serie to determine money reward at each round
		/// </summary>
		[JsonIgnore]
		public int LossRowCount { get; set; }

		[JsonIgnore]
		public int EntryHoldKillCount => Players.SelectMany(p => p.EntryHoldKills).Count();

		[JsonIgnore]
		public int EntryKillCount => Players.SelectMany(p => p.EntryKills).Count();

		[JsonIgnore]
		public int EntryHoldKillWonCount => Players.SelectMany(p => p.EntryHoldKills).Count(e => e.HasWon);

		[JsonIgnore]
		public int EntryHoldKillLossCount => Players.SelectMany(p => p.EntryHoldKills).Count(e => e.HasWon == false);

		[JsonIgnore]
		public decimal RatioEntryHoldKill
		{
			get
			{
				int total = Players.SelectMany(p => p.EntryHoldKills).Count();
				int won = Players.SelectMany(p => p.EntryHoldKills).Count(e => e.HasWon);
				int loss = Players.SelectMany(p => p.EntryHoldKills).Count(e => e.HasWon == false);

				decimal percent = 0;
				if (EntryKillWonCount == 0) return percent;
				if (loss == 0) return 100;
				percent = won / (decimal)total * 100;
				percent = Math.Round(percent, 0);

				return percent;
			}
		}

		[JsonIgnore]
		public int EntryKillWonCount => Players.SelectMany(p => p.EntryKills).Count(e => e.HasWon);

		[JsonIgnore]
		public int EntryKillLossCount => Players.SelectMany(p => p.EntryKills).Count(e => e.HasWon == false);

		[JsonIgnore]
		public int FlashbangThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.FlashbangThrownCount);

		[JsonIgnore]
		public int HeGrenadeThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.HeGrenadeThrownCount);

		[JsonIgnore]
		public int SmokeThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.SmokeThrownCount);

		[JsonIgnore]
		public int MolotovThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.MolotovThrownCount);

		[JsonIgnore]
		public int IncendiaryThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.IncendiaryThrownCount);

		[JsonIgnore]
		public int DecoyThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.DecoyThrownCount);

		[JsonIgnore]
		public int TradeKillCount => Players.Sum(p => p.TradeKillCount);

		[JsonIgnore]
		public int TradeDeathCount => Players.Sum(p => p.TradeDeathCount);

		[JsonIgnore]
		public decimal RatioEntryKill
		{
			get
			{
				int entryKillCount = Players.SelectMany(p => p.EntryKills).Count();
				int entryKillWin = Players.SelectMany(p => p.EntryKills).Count(e => e.HasWon);
				int entryKillLoss = Players.SelectMany(p => p.EntryKills).Count(e => e.HasWon == false);

				decimal entryKillPercent = 0;
				if (EntryKillWonCount == 0) return entryKillPercent;
				if (entryKillLoss == 0) return 100;
				entryKillPercent = (entryKillWin / (decimal)entryKillCount) * 100;
				entryKillPercent = Math.Round(entryKillPercent, 0);

				return entryKillPercent;
			}
		}

		[JsonIgnore]
		public int MatchCount { get; set; } = 1;

		[JsonIgnore]
		public int WinCount { get; set; } = 0;

		[JsonIgnore]
		public int LostCount { get; set; } = 0;

		[JsonIgnore]
		public int KillCount => Players.Sum(player => player.KillCount);

		[JsonIgnore]
		public int AssistCount => Players.Sum(player => player.AssistCount);

		[JsonIgnore]
		public int DeathCount => Players.Sum(player => player.DeathCount);

		[JsonIgnore]
		public int RoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundCtCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundCtCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundTCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundTCount { get; set; } = 0;

		[JsonIgnore]
		public int WinPistolRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinEcoRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinSemiEcoRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinForceBuyRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int BombPlantedCount => Players.Sum(player => player.BombPlantedCount);

		[JsonIgnore]
		public int BombDefusedCount => Players.Sum(player => player.BombDefusedCount);

		[JsonIgnore]
		public int BombExplodedCount => Players.Sum(player => player.BombExplodedCount);

		[JsonIgnore]
		public int BombPlantedOnACount { get; set; } = 0;

		[JsonIgnore]
		public int BombPlantedOnBCount { get; set; } = 0;

		[JsonIgnore]
		public int FiveKillCount => Players.Sum(player => player.FiveKillCount);

		[JsonIgnore]
		public int FourKillCount => Players.Sum(player => player.FourKillCount);

		[JsonIgnore]
		public int ThreeKillCount => Players.Sum(player => player.ThreeKillCount);

		[JsonIgnore]
		public int TwoKillCount => Players.Sum(player => player.TwoKillCount);

		[JsonIgnore]
		public int OneKillCount => Players.Sum(player => player.OneKillCount);

		[JsonIgnore]
		public int JumpKillCount => Players.Sum(player => player.JumpKillCount);

		[JsonIgnore]
		public int CrouchKillCount => Players.Sum(player => player.CrouchKillCount);

		public override bool Equals(object obj)
		{
			var item = obj as Team;

			return item != null
				&& string.Compare(Name, item.Name, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public Team()
		{
			Players = new ObservableCollection<Player>();
			Players.CollectionChanged += OnPlayersCollectionChanged;
		}

		public void Clear()
		{
			Players.Clear();
			LossRowCount = 0;
		}

		public Team Clone()
		{
			return (Team)MemberwiseClone();
		}

		private void OnPlayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (Players.Any())
			{
				foreach (var player in Players)
				{
					if (player != null)
					{
						player.Kills.CollectionChanged += OnKillsCollectionChanged;
						player.Deaths.CollectionChanged += OnDeathsCollectionChanged;
						player.Assists.CollectionChanged += OnAssistsCollectionChanged;
						player.EntryKills.CollectionChanged += OnEntryKillsCollectionChanged;
						player.EntryHoldKills.CollectionChanged += OnEntryHoldKillsCollectionChanged;
					}
				}
			}
		}

		private void OnDeathsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => DeathCount);
		}

		private void OnAssistsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => AssistCount);
		}

		private void OnKillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => KillCount);
			RaisePropertyChanged(() => JumpKillCount);
			RaisePropertyChanged(() => CrouchKillCount);
		}

		private void OnEntryKillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => EntryKillCount);
			RaisePropertyChanged(() => EntryKillWonCount);
			RaisePropertyChanged(() => EntryKillLossCount);
			RaisePropertyChanged(() => RatioEntryKill);
		}

		private void OnEntryHoldKillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => EntryHoldKillCount);
			RaisePropertyChanged(() => EntryHoldKillWonCount);
			RaisePropertyChanged(() => EntryHoldKillLossCount);
			RaisePropertyChanged(() => RatioEntryHoldKill);
		}
	}
}