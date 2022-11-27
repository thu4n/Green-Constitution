using System;
﻿using DeckSwipe.Gamestate;

namespace DeckSwipe.CardModel {

	[Serializable]
	public class StatsModification {

		public int hp;
		public int eco;
		public int pl;
		public int rsc;

		public StatsModification(int hp, int eco, int pl, int rsc) {
			this.hp = hp;
			this.eco = eco;
			this.pl = pl;
			this.rsc = rsc;
		}

		public void Perform() {
			// TODO Pass through status effects
			Stats.ApplyModification(this);
		}

	}

}
