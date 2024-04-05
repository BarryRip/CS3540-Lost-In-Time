using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuest2Fetchable : Fetchable
{
    bool hasSnack = false;

    public override bool FulfilledRequirements()
    {
        return hasSnack;
    }

    override public void PickUp()
    {
        hasSnack = true;
    }
}

public class FetchQuest2 : FetchQuest
{
    // Start is called before the first frame update
    void Start()
    {
        id = 1;
        QuestName = "The Snack Quest";
        QuestDiscription = "Get the samuri's snack from the larder";
        fetchable = new FetchQuest2Fetchable();

        fqs = GameObject.FindGameObjectWithTag("FetchQuestSystem").GetComponent<FetchQuestSystem>();
        fqs.unlocked.Add(this);

        Invoke("SetUiText1", .4f);
        Invoke("SetUiText2", 1f);
    }

    private void SetUiText1()
    {
        UiTextManager manager = GameObject.FindObjectOfType<UiTextManager>();
        manager.SetNotificationText(QuestName);
    }

    private void SetUiText2()
    {
        UiTextManager manager = GameObject.FindObjectOfType<UiTextManager>();
        manager.SetNotificationText(QuestDiscription);
    }

    override public void PickUp()
    {
        fetchable.PickUp();
    }

    public override void OnCompletion()
    {
        Gate.MoveGate();
    }
}
