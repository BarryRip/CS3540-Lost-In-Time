using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTextManager : MonoBehaviour
{
    public Text notifText;
    public Text collectText;

    private bool fadeNotification;
    private Color notifStartColor;
    private Color notifEndColor;
    private float secondsBeforeFading = 2f;

    // Start is called before the first frame update
    void Start()
    {
        notifStartColor = notifText.color;
        notifEndColor = new Color(notifText.color.r, notifText.color.g, notifText.color.b, 0f);
        notifText.color = notifEndColor;
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
    }

    private void StartFadingNotif()
    {
        fadeNotification = true;
    }
}
