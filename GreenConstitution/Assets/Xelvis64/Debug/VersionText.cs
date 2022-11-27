using TMPro;
using UnityEngine;

namespace Xelvis64.Debug {
	
	public class VersionText : MonoBehaviour {
		
		void Start () {
			GetComponent<TextMeshProUGUI>().SetText("Xelvis64_Game"
			                                        + " "
			                                        + Application.version
			                                        + (Application.buildGUID != ""
					                                        ? ", build " + Application.buildGUID
					                                        : ""));
		}
		
	}
	
}
