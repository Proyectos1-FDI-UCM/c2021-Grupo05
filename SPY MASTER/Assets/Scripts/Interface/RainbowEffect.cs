using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowEffect : MonoBehaviour
{
    Image thisImage;
    Text thisText;

    float vel = 2;

    float R = 0;
    float G = 1;
    float B = 0;

    Ins doThis;

    enum Ins { Rup, Rdown, Gup, Gdown, Bup, Bdown }


    private void Awake()
    {
        if (GetComponent<Image>() != null) thisImage = GetComponent<Image>();
        else if (GetComponent<Text>() != null) thisText = GetComponent<Text>();

        doThis = Ins.Rup;
    }


    private void Update()
    {
        //// RED
        //if (G == 1 && B == 0) Add(ref R);
        //else if (G == 0 && B == 1) Substract(ref R);

        //// GREEN
        //else if (R == 0 && B == 1) Add(ref G);
        //else if (R == 1 && B == 0) Substract(ref G);

        //// BLUE
        //else if (R == 1 && B == 0) Add(ref B);
        //else if (R == 0 && B == 1) Substract(ref B);

        switch (doThis)
        {
            case Ins.Rup:

                Add(ref R);
                if (R + Time.deltaTime > 1)
                {
                    R = 1; doThis = Ins.Gdown;
                }
                break;

            case Ins.Rdown:
                Substract(ref R);
                if (R + Time.deltaTime < 0)
                {
                    R = 0; doThis = Ins.Gup;
                }
                break;

            case Ins.Gup:
                Add(ref G);
                if (G + Time.deltaTime > 1)
                {
                    G = 1; doThis = Ins.Bdown;
                }
                break;

            case Ins.Gdown:
                Substract(ref G);
                if (G - Time.deltaTime < 0)
                {
                    G = 0; doThis = Ins.Bup;
                }
                break;

            case Ins.Bup:
                Add(ref B);
                if (B + Time.deltaTime > 1)
                {
                    B = 1; doThis = Ins.Rdown;
                }
                break;

            case Ins.Bdown:
                Substract(ref B);
                if (B - Time.deltaTime < 0)
                {
                    B = 0; doThis = Ins.Rup;
                }
                break;
        }

        //newColor = new Color ( Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        Color newColor = new Color(R, G, B);

        if (thisImage != null) thisImage.color = newColor;
        else if (thisText != null) thisText.color = newColor;
    }

    void Add(ref float value)
    {
        value += Time.unscaledDeltaTime * vel;
    }

    void Substract(ref float value)
    {
        value -= Time.unscaledDeltaTime * vel;
    }

    private void OnDisable()
    {
        if (thisImage)
            thisImage.color = Color.white;
        if (thisText)
            thisText.color = Color.white;

    }
}
