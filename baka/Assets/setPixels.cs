using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setPixels : MonoBehaviour {

	private RawImage rawImage;
    public Text sizeText;
    public Text lambdaText;
    public Text thetaText;
    public Text sigmaText;
    public Text phaseText;
    public Text trimText;

	void Start () {
		rawImage = GetComponent<RawImage> ();
        rawImage.texture = gaborMaker(80, 12f, 45f, 10f, 0f, 0.01f); //teen default gabori
    }

	void createGabor(){

        int size = int.Parse(sizeText.text);
        float lambda = float.Parse(lambdaText.text); //siinuslaine pikkus
        float theta = float.Parse(thetaText.text); //laine orientatsioon
        float sigma = float.Parse(sigmaText.text); //standardhälve pikslites
        float phase = float.Parse(phaseText.text); //siinuslaine faas vahemikus [0,1]
        float trim = float.Parse(trimText.text); //sellest väiksemad normjaotuse väärtused jäetakse välja, kiirendab kuvamist
        
        rawImage.texture = gaborMaker(size, lambda, theta, sigma, phase, trim);
    }

    //http://www.icn.ucl.ac.uk/courses/MATLAB-Tutorials/Elliot_Freeman/html/gabor_tutorial.html
    public Texture2D gaborMaker(int size, float lambda, float theta, float sigma, float phase, float trim)
    {
        
        float pi = Mathf.PI;

        Color[] mesh = new Color[size * size];

        float freq = size / lambda;
        float thataRad = (theta / 360) * 2 * pi;
        float s = size/sigma;
        float radPhase = phase * 2 * pi;

        for (int i = 0; i < size; i++)
        {
            float Y = i * 1f / (size-1) - .5f;

            for (int j = 0; j < size; j++)
            {
                float X = j * 1f / (size-1) - .5f;

                float xprim = X * Mathf.Cos(thataRad);
                float yprim = Y * Mathf.Sin(thataRad);

                float radXY = (xprim + yprim) * freq * 2 * pi;

                float f = Mathf.Sin(radXY+radPhase);
                f = (f + 1) * .5f; //f vahemikku [0,1]
                
                float a = Mathf.Exp(-(X*X+Y*Y)/2*s*s);

                if (a <= trim) a = 0;
                
                mesh[(i * size) + j] = new Color(f, f, f, a); ;
            }
        }
        
        Texture2D temp = new Texture2D(size, size);
        temp.SetPixels(mesh);
        temp.Apply();
        return temp;
    }
}
