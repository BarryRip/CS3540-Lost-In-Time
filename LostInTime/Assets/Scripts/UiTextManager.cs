using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles showing and writing the text on the UI.
/// <para />
/// Should be attached to the LevelManager and exist in a scene with the NotificationText and CollectableText tagged objects.
/// </summary>
public class UiTextManager : MonoBehaviour
{
    private TextMeshProUGUI notifText;
    private TextMeshProUGUI collectText;
    private bool fadeNotification;
    private Color notifStartColor;
    private Color notifEndColor;
    private float secondsBeforeFading = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize notifText only if the tagged object exists
        GameObject nTextObj = GameObject.FindGameObjectWithTag("NotificationText");
        if (nTextObj != null)
        {
            notifText = nTextObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("Object with NotificationText tag was not found in the scene. UI text may not work properly.");
        }
        // Initialize collectText only if the tagged object exists
        GameObject cTextObj = GameObject.FindGameObjectWithTag("CollectableText");
        if (cTextObj != null)
        {
            collectText = cTextObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("Object with CollectableText tag was not found in the scene. UI text may not work properly.");
        }

        if (notifText == null) return;

        notifStartColor = notifText.color;
        notifEndColor = new Color(notifText.color.r, notifText.color.g, notifText.color.b, 0f);
        notifText.color = notifEndColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (notifText == null) return;

        float t = Time.deltaTime;
        if (fadeNotification)
        {
            notifText.color = Color.Lerp(notifText.color, notifEndColor, t);
        }
    }

    /// <summary>
    /// Send a text notification to the UI, which will fade after a few seconds.
    /// </summary>
    /// <param name="txt">The notification to write to the UI.</param>
    public void SetNotificationText(string txt)
    {
        if (notifText == null) return;

        notifText.text = txt;
        notifText.color = notifStartColor;
        fadeNotification = false;
        Invoke("StartFadingNotif", secondsBeforeFading);
    }

    /// <summary>
    /// Update the collectable text on the UI, appearing in the top left corner.
    /// </summary>
    /// <param name="txt">The text to write to the UI.</param>
    public void SetCollectableText(string txt)
    {
        if (collectText == null) return;

        collectText.text = txt;
    }

    private void StartFadingNotif()
    {
        if (notifText == null) return;

        fadeNotification = true;
    }
}
