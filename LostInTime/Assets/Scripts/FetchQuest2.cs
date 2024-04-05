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

        fqs = GameObject.FindGameObjectWithTag("FechQuestSystem").GetComponent<FetchQuestSystem>();
        fqs.unlocked.Add(this);
    }


    public void PickUp()
    {
        fetchable.PickUp();
    }

    public override void OnCompletion()
    {
        // todo do whatever unlocks here
    }
}
