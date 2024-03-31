using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


abstract public class Fetchable
{
    public abstract bool fulfilledRequirements();
}

public class FetchQuest : MonoBehaviour
{
    public int id;
    public string QuestName;
    public string QuestDiscription;
    Fetchable fetchable;

    public bool fulfilledRequirements()
    {
        return fetchable.fulfilledRequirements();
    }
}

public class FetchQuestSystem : MonoBehaviour
{
    List<FetchQuest> unlocked;
    List<FetchQuest> invisible_locked;
    List<FetchQuest> visible_locked;
    List<FetchQuest> completed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TryToCompleteFetchQuest(FetchQuest quest)
    {
        if (quest.fulfilledRequirements())
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
            Debug.Log("attempted to turn in quest that was not unlocked, id: " +quest.id);
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
