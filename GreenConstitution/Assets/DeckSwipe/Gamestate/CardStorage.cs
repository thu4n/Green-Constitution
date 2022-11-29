using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckSwipe.CardModel;
using DeckSwipe.CardModel.Import;
using DeckSwipe.CardModel.Import.Resource;
using DeckSwipe.CardModel.Prerequisite;
using UnityEngine;

namespace DeckSwipe.Gamestate {

	public class CardStorage {

		private static readonly Character _defaultGameOverCharacter = new Character("", null);

		private readonly Sprite defaultSprite;
		private readonly bool loadRemoteCollectionFirst;

		public Dictionary<int, Card> Cards { get; private set; }
		public Dictionary<string, SpecialCard> SpecialCards { get; private set; }

		public Task CardCollectionImport { get; }

		private List<Card> drawableCards = new List<Card>();

		public CardStorage(Sprite defaultSprite, bool loadRemoteCollectionFirst) {
			this.defaultSprite = defaultSprite;
			this.loadRemoteCollectionFirst = loadRemoteCollectionFirst;
			CardCollectionImport = PopulateCollection();
		}

		public Card Random(int number) {
			// if (number == 10) return drawableCards[number];
			// int numb = UnityEngine.Random.Range(0, number);
			// Card card = drawableCards[numb];
			// drawableCards[numb] = drawableCards[number -1];
			// drawableCards[number -1] = card;
			// if (number >= 0) return drawableCards[number -1];
			// else return null;
			return drawableCards[number];
		}

		public Card ForId(int id) {
			Card card;
			Cards.TryGetValue(id, out card);
			return card;
		}

		public SpecialCard SpecialCard(string id) {
			SpecialCard card;
			SpecialCards.TryGetValue(id, out card);
			return card;
		}

		public void ResolvePrerequisites() {
			foreach (Card card in Cards.Values) {
				card.ResolvePrerequisites(this);
				if (card.PrerequisitesSatisfied()) {
					AddDrawableCard(card);
				}
			}
		}

		public void AddDrawableCard(Card card) {
			drawableCards.Add(card);
		}

		private async Task PopulateCollection() {
			ImportedCards importedCards =
					await new CollectionImporter(defaultSprite, loadRemoteCollectionFirst).Import();
			Cards = importedCards.cards;
			SpecialCards = importedCards.specialCards;
			if (Cards == null || Cards.Count == 0) {
				PopulateFallback();
			}
			VerifySpecialCards();
		}

		private void PopulateFallback() {
			Cards = new Dictionary<int, Card>();
			Character placeholderPerson = new Character("Placeholder Person", defaultSprite);
			Cards.Add(0, new Card("Placeholder card 1",
					"A",
					"B",
					placeholderPerson,
					new ActionOutcome(-2, 4, -2, 2),
					new ActionOutcome(2, 0, 2, -2),
					new List<ICardPrerequisite>()));
			Cards.Add(1, new Card("Placeholder card 2",
					"A",
					"B",
					placeholderPerson,
					new ActionOutcome(-1, -1, -1, -1),
					new ActionOutcome(2, 2, 2, 2),
					new List<ICardPrerequisite>()));
			Cards.Add(2, new Card("Placeholder card 3",
					"A",
					"B",
					placeholderPerson,
					new ActionOutcome(1, 1, 0, -2),
					new ActionOutcome(2, 2, -2, -4),
					new List<ICardPrerequisite>()));
		}

		private void VerifySpecialCards() {
			if (SpecialCards == null) {
				SpecialCards = new Dictionary<string, SpecialCard>();
			}

			if (!SpecialCards.ContainsKey("gameover_hp")) {
				SpecialCards.Add("gameover_hp", new SpecialCard("Hunger consumes the city, as food reserves deplete.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_eco")) {
				SpecialCards.Add("gameover_eco", new SpecialCard("Your country has fallen due to the fallen of the economic system. You have lost the game.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_pl")) {
				SpecialCards.Add("gameover_pl", new SpecialCard("The country has been too polluted and is not inhabitable for humans anymore. You have lost the game.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_rsc")) {
				SpecialCards.Add("gameover_rsc", new SpecialCard("Your country has fallen due to the fallen of the economic system. You have lost the game.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_war")) {
				SpecialCards.Add("gameover_war", new SpecialCard("You become an ally of Rusticore and so the war begins. After 2 years of fighting, losing countless battlefronts, your capital falls and you are captured. Unikrom let you live for political reasons but your country has been completely under their control. You have lost the game.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_ecoded")) {
				SpecialCards.Add("gameover_war", new SpecialCard("Your activities lead you imprisoned.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}
			if (!SpecialCards.ContainsKey("gameover_win")) {
				SpecialCards.Add("gameover_win", new SpecialCard("Your ingenious leadership has helped the country overcome all obstacles and the path to steady growth has never been nearer. The end.", "", "",
						_defaultGameOverCharacter,
						new GameOverOutcome(),
						new GameOverOutcome()));
			}									
		}

	}

}
