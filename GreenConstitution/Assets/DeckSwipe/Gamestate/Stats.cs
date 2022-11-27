using System.Collections.Generic;
using DeckSwipe.CardModel;
using DeckSwipe.World;
using UnityEngine;

namespace DeckSwipe.Gamestate {
	
	public static class Stats {
		
		private const int _maxStatValue = 100;
		private const int _startingHp = 50;
		private const int _startingEco = 50;
		private const int _startingPl = 50;
		private const int _startingRsc = 50;
		
		private static readonly List<StatsDisplay> _changeListeners = new List<StatsDisplay>();
		
		public static int Hp { get; private set; }
		public static int Eco { get; private set; }
		public static int Pl { get; private set; }
		public static int Rsc { get; private set; }
		
		public static float HpPercentage => (float) Hp / _maxStatValue;
		public static float EcoPercentage => (float) Eco / _maxStatValue;
		public static float PlPercentage => (float) Pl / _maxStatValue;
		public static float RscPercentage => (float) Rsc / _maxStatValue;
		
		public static void ApplyModification(StatsModification mod) {
			Hp = ClampValue(Hp + mod.hp);
			Eco = ClampValue(Eco + mod.eco);
			Pl = ClampValue(Pl - mod.pl);
			Rsc = ClampValue(Rsc + mod.rsc);
			TriggerAllListeners();
		}
		
		public static void ResetStats() {
			ApplyStartingValues();
			TriggerAllListeners();
		}
		
		private static void ApplyStartingValues() {
			Hp = ClampValue(_startingHp);
			Eco = ClampValue(_startingEco);
			Pl = ClampValue(_startingPl);
			Rsc = ClampValue(_startingRsc);
		}
		
		private static void TriggerAllListeners() {
			for (int i = 0; i < _changeListeners.Count; i++) {
				if (_changeListeners[i] == null) {
					_changeListeners.RemoveAt(i);
				}
				else {
					_changeListeners[i].TriggerUpdate();
				}
			}
		}
		
		public static void AddChangeListener(StatsDisplay listener) {
			_changeListeners.Add(listener);
		}
		
		private static int ClampValue(int value) {
			return Mathf.Clamp(value, 0, _maxStatValue);
		}
		
	}
	
}
