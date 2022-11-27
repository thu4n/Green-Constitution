using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SC_BackgroundScaler : MonoBehaviour
{
    Image backgroundImage;
    RectTransform rt;
    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
