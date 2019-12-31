using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorFlasher : MonoBehaviour
{
    public float WhenTime;
    float RestartWhenTime;
    public Color FlashColor;
    public Color DefaultColor;
    public int NumberOfFlashed = 5;
    public float TimeBetweenFlashed = 0.5f;
    public Image Panel;
    public TextMeshProUGUI Text;
    string DefaultText = "Dead Desert Z";
    public string[] FlashText;
    public Sprite[] FlashImage;
    public Sprite DefaultImage;
    bool HasFlashed = false;
   

    private void Start()
    {
        RestartWhenTime = WhenTime;
        Text.text = DefaultText;
        Panel.sprite = DefaultImage;
        HasFlashed = false;

    }

    private void Update()
    {
        WhenTime -= Time.deltaTime;
        if(WhenTime <= 0f)
        {
            if (!HasFlashed)
            {
                StartCoroutine(Flash());
                WhenTime = RestartWhenTime;
                HasFlashed = (Random.value > 0.3f);
            }
            else
            {
                WhenTime = RestartWhenTime;
                HasFlashed = (Random.value > 0.3f);
            }
        }
         
    }

    IEnumerator Flash()
    { 
        for (int i = 0; i < NumberOfFlashed; i++)
        {
            // if the current color is the default color - change it to the flash color
            if (Panel.color == DefaultColor && Text.text == DefaultText && Panel.sprite == DefaultImage)
            {
                Panel.color = FlashColor;
                Text.text = FlashText[Random.Range(0, FlashText.Length)];
                Panel.sprite = FlashImage[Random.Range(0,FlashImage.Length)];
            }
            else // otherwise change it back to the default color
            {
                Panel.color = DefaultColor;
                Text.text = DefaultText;
                Panel.sprite = DefaultImage;
            }
            yield return new WaitForSeconds(TimeBetweenFlashed);
        }

        yield return new WaitForSeconds(0.2f);
        Panel.color = DefaultColor;
        Text.text = DefaultText;
        Panel.sprite = DefaultImage;

    }

}
