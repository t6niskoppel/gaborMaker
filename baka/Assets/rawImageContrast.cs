using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class rawImageContrast : MonoBehaviour {

    private RawImage rawImage;
    private Slider slider;
    public Text alphaValue;

	void Start () {
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ChangeContrast(); });
	}

    void ChangeContrast()
    {
        Color c = rawImage.color;
        rawImage.color = new Color(c.r , c.g, c.b ,slider.value);
        alphaValue.text = "" + slider.value*100 + "%";
    }
	
	
}
