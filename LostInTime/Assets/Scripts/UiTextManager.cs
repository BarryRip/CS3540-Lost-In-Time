using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTextManager : MonoBehaviour
{
    public Text notifText;
    public Text collectText;

    private bool fadeNotification;
    private bool fadeCollect;
    private Color notifStartColor;
    private Color collectStartColor;
    private Color notifEndColor;
    private Color collectEndColor;
    private float secondsBeforeFading = 2f;

    // Start is called before the first frame update
    void Start()
    {
        notifStartColor = notifText.color;
        collectStartColor = collectText.color;
        notifEndColor = new Color(notifText.color.r, notifText.color.g, notifText.color.b, 0f);
        collectEndColor = new Color(collectText.color.r, collectText.color.g, collectText.color.b, 0f);
        notifText.color = notifEndColor;
        collectText.color = collectEndColor;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;
        if (fadeNotification)
        {
            notifText.color = Color.Lerp(notifText.color, notifEndColor, t);
        }
    }

    public void SetNotificationText(string txt)
    {
        notifText.text = txt;
        notifText.color = notifStartColor;
        fadeNotification = false;
        Invoke("StartFadingNotif", secondsBeforeFading);
    }

    public void SetCollectableText(string txt)
    {
        collectText.text = txt;
        collectText.color = collectStartColor;
        fadeCollect = false;
    }

    private void StartFadingNotif()
    {
        fadeNotification = true;
    }
}
