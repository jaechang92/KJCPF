using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObejct : MonoBehaviour
{
    public GameObject effect;

    private void OnEnable()
    {
        GameManager.instance.InteractionObejctList.Add(this);
    }

    private void OnDisable()
    {
        GameManager.instance.InteractionObejctList.Remove(this);
    }

    public void StartInteraction()
    {
        Debug.Log("??");
        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.questList.Count; i++)
        {
            Debug.Log("???");
            if (GameManager.instance.userInvenSlotInfo.questList[i].questMobModelname == this.name)
            {
                QuestInfo tmp = GameManager.instance.userInvenSlotInfo.questList[i];

                Debug.Log(tmp);
                //tmp.checkClear = true;
                //GameManager.instance.userInvenSlotInfo.questList[i] = tmp;

                GameManager.instance.userInvenSlotInfo.MinusQuestMob(tmp.questMobName);
                //UIManager.instance.qPool.ResetQuestWindow(GameManager.instance.userInvenSlotInfo.questList[i]);

            }
        }

    }

    void Update()
    {
        InteractionAtPlayer();
    }

    public float lookDistance = 2.0f;

    public float DISTANCETOPLAYER
    {
        get
        {
            return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
        }
    }

    void InteractionAtPlayer()
    {
        if (GameManager.instance.player == null)
        {
            return;
        }

        if (DISTANCETOPLAYER <= lookDistance)
        {
            GameManager.instance.Interaction = this;
        }
        else
        {
            GameManager.instance.Interaction = null;
        }

        
        



    }


}
