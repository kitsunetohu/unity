using UnityEngine;
using System.Collections;

public class Light_Flicker : MonoBehaviour
{

    public bool useSmooth;
    public bool useIntensity;
    public bool useColor;
    public float intnesitySmoothTime;
    public float colorSmoothTime;
    public float intensityMin;
    public float intensityMax;
    public Color lightColorStart = Color.white;
    public Color lightColorEnd = Color.white;

    private bool colorTurn;
    // Use this for initialization
    void Start()
    {

        gameObject.GetComponent<Light>().color = lightColorStart;

    }

    // Update is called once per frame
    void Update()
    {

        if (useIntensity && !useSmooth && !useColor && gameObject.GetComponent<Light>())
        {
            //单纯闪烁,不会变换颜色跟Smooth.
            gameObject.GetComponent<Light>().intensity = Mathf.Lerp(gameObject.GetComponent<Light>().intensity, Random.Range(intensityMin, intensityMax), Time.deltaTime);
        }
        else if (useIntensity && useSmooth && !useColor && gameObject.GetComponent<Light>())
        {
            //会闪烁跟Smooth,不会变换颜色.
            gameObject.GetComponent<Light>().intensity = Mathf.Lerp(gameObject.GetComponent<Light>().intensity, Random.Range(intensityMin, intensityMax), Time.deltaTime * intnesitySmoothTime);
        }
        else if (!useIntensity && !useSmooth && useColor && gameObject.GetComponent<Light>())
        {
            //单纯颜色变换,不会闪烁跟Smooth.
            if (!colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorEnd, Time.deltaTime);
                if (gameObject.GetComponent<Light>().color == lightColorEnd)
                {
                    colorTurn = true;
                }
            }
            else if (colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorStart, Time.deltaTime);
                if (gameObject.GetComponent<Light>().color == lightColorStart)
                {
                    colorTurn = false;
                }
            }
        }
        else if (!useIntensity && useSmooth && useColor && gameObject.GetComponent<Light>())
        {
            //会Smooth跟单纯变换颜色,不会闪烁.
            if (!colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorEnd, Time.deltaTime * colorSmoothTime);
                if (gameObject.GetComponent<Light>().color == lightColorEnd)
                {
                    colorTurn = true;
                }
            }
            else if (colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorStart, Time.deltaTime * colorSmoothTime);
                if (gameObject.GetComponent<Light>().color == lightColorStart)
                {
                    colorTurn = false;
                }
            }
        }
        else if (useIntensity && !useSmooth && useColor && gameObject.GetComponent<Light>())
        {
            //会闪烁跟变换颜色,不会Smooth.
            gameObject.GetComponent<Light>().intensity = Mathf.Lerp(gameObject.GetComponent<Light>().intensity, Random.Range(intensityMin, intensityMax), Time.deltaTime);
            if (!colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorEnd, Time.deltaTime);
                if (gameObject.GetComponent<Light>().color == lightColorEnd)
                {
                    colorTurn = true;
                }
            }
            else if (colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorStart, Time.deltaTime);
                if (gameObject.GetComponent<Light>().color == lightColorStart)
                {
                    colorTurn = false;
                }
            }
        }
        else if (useIntensity && useSmooth && useColor && gameObject.GetComponent<Light>())
        {
            //会闪烁还有Smooth跟变换颜色.
            gameObject.GetComponent<Light>().intensity = Mathf.Lerp(gameObject.GetComponent<Light>().intensity, Random.Range(intensityMin, intensityMax), Time.deltaTime * intnesitySmoothTime);
            if (!colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorEnd, Time.deltaTime * colorSmoothTime);
                if (gameObject.GetComponent<Light>().color == lightColorEnd)
                {
                    colorTurn = true;
                }
            }
            else if (colorTurn)
            {
                gameObject.GetComponent<Light>().color = Color.Lerp(gameObject.GetComponent<Light>().color, lightColorStart, Time.deltaTime * colorSmoothTime);
                if (gameObject.GetComponent<Light>().color == lightColorStart)
                {
                    colorTurn = false;
                }
            }
        }
    }

}
