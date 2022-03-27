using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToHighscore : MonoBehaviour
{
    [SerializeField] float fadeTime = 2f;
    [SerializeField] Color mainColor = Color.white;
    [SerializeField] float fadeTimeFinal = 0.5f;
    [SerializeField] Color finalColor = Color.black;

    Image bgImage;

    float fadeTimer = 0;
    bool isFading = false;
    bool isChanging = false;

    System.Action finishFading;

    private void Awake()
    {
        bgImage = GetComponent<Image>();
        bgImage.color = new Color(mainColor.r, mainColor.g, mainColor.b, 0);
        fadeTimer = 0;
        isFading = false;
        isChanging = false;
    }

    public void StartFading(System.Action finishFading, float fadeTimer = 0)
    {
        if (fadeTimer > 0)
        {
            this.fadeTimer = fadeTimer;
        }
        this.finishFading = finishFading;
        isFading = true;
    }

    private void Update()
    {
        if (isFading)
        {
            float alpha = fadeTimer / fadeTime;
            if (alpha > 1f) alpha = 1f;
            bgImage.color = new Color(bgImage.color.r, bgImage.color.g, bgImage.color.b, alpha);
            fadeTimer += Time.deltaTime;

            if (alpha >= 1f)
            {
                isFading = false;
                isChanging = true;
                fadeTimer = 0f;
            }
        }
        else if (isChanging)
        {
            fadeTimer += Time.deltaTime;
            Color oldColor = bgImage.color;
            bgImage.color = Color.Lerp(oldColor, finalColor, fadeTimer / fadeTimeFinal);
            if (fadeTimer >= fadeTimeFinal)
            {
                isChanging = false;
                finishFading.Invoke();
            }
        }
    }
}
