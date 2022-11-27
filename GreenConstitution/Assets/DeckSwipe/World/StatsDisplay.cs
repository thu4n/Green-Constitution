using DeckSwipe.Gamestate;
using Xelvis64;
using UnityEngine;
using UnityEngine.UI;

namespace DeckSwipe.World {
	
	public class StatsDisplay : MonoBehaviour {
		
		public Image hpBar;
		public Image ecoBar;
		public Image plBar;
		public Image rscBar;
		public float relativeMargin;
		
		private float minFillAmount;
		private float maxFillAmount;
		
		private void Awake() {
			minFillAmount = Mathf.Clamp01(relativeMargin);
			maxFillAmount = Mathf.Clamp01(1.0f - relativeMargin);
			
			if (!Util.IsPrefab(gameObject)) {
				Stats.AddChangeListener(this);
				TriggerUpdate();
			}
		}
		
		public void TriggerUpdate() {
			hpBar.fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, Stats.HpPercentage);
			ecoBar.fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, Stats.EcoPercentage);
			plBar.fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, Stats.PlPercentage);
			rscBar.fillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, Stats.RscPercentage);
		}
		
	}
	
}
