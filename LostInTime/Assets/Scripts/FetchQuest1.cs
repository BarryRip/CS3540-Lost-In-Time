using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuest1Fetchable : Fetchable
{
    bool hasArmor = false;

    public override bool FulfilledRequirements()
    {
        return hasArmor;
    }

    override public void PickUp()
    {
        hasArmor = true;
    }
}

public class FetchQuest1 : FetchQuest
{
    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        QuestName = "The Armor Quest";
        QuestDiscription = "Get the knigts armor from the barracks";
        fetchable = new FetchQuest1Fetchable();

        fqs = GameObject.FindGameObjectWithTag("FechQuestSystem").GetComponent<FetchQuestSystem>();
        fqs.unlocked.Add(this);
    }


    public void PickUp()
    {
        fetchable.PickUp();
    }

    public override void OnCompletion()
    {
        Gate.MoveGate();
    }
}
