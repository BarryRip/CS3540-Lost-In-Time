using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


abstract public class Fetchable
{
    public abstract bool FulfilledRequirements();
    public abstract void PickUp();
}

public abstract class FetchQuest : MonoBehaviour
{
    public int id;
    public string QuestName;
    public string QuestDiscription;
    public Fetchable fetchable;
    public FetchQuestSystem fqs;

    public bool FulfilledRequirements()
    {
        return fetchable.FulfilledRequirements();
    }

    public void TurnIn()
    {
        fqs.unlocked.Remove(this);
        fqs.completed.Add(this);

        OnCompletion();
    }

    public abstract string GiveQuestText();
    public abstract string TurnInQuestText();

    public abstract void OnCompletion();
    public abstract void PickUp();
}

public class FetchQuestSystem : MonoBehaviour
{
    public List<FetchQuest> unlocked;
    public List<FetchQuest> invisible_locked;
    public List<FetchQuest> visible_locked;
    public List<FetchQuest> completed;

    public void TryToCompleteFetchQuest(FetchQuest quest)
    {
        if (quest.FulfilledRequirements())
        {
            CompleteFetchQuest(quest);
        }
        else
        {
            //Do nothing
        }
    }

    //called when unlocking a fetch quest
    public void UnlockFetchQuestById(int fetchQuestId)
    {
        unlocked.AddRange(invisible_locked.Where(x => x.id == fetchQuestId));
        unlocked.AddRange(visible_locked.Where(x => x.id == fetchQuestId));
    }


    //called when completing a fetch quest;
    public void CompleteFetchQuest(FetchQuest quest)
    {
        List<FetchQuest> tmp = unlocked.Where(x => x.id == quest.id).ToList();
        if (new List<FetchQuest>() != tmp)
        {
            completed.AddRange(tmp);
        }
        else
        {
            //Debug.Log("attempted to turn in quest that was not unlocked, id: " +quest.id);
            //Should never happen not sure whether to print debug or throw error
        }
    }


    public void LockFetchQuestById(int fetchQuestId, bool VisibleWhenLocked)
    {
        List<FetchQuest> toLock = unlocked.Where(x => x.id == fetchQuestId).ToList();
        if (VisibleWhenLocked)
        {
            visible_locked.AddRange(toLock);
        }
        else
        {
            invisible_locked.AddRange(toLock);
        }
    }
}
