using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPool : MonoBehaviour
{
    

    public List<QuestWindowInfo> windowL;
    


    public void Start()
    {
        for (int i = 0; i < windowL.Count; i++)
        {
            windowL[i].gameObject.SetActive(false);
        }

        ResetQuestWindow2();
    }

    public void ResetQuestWindow2()
    {
        if (GameManager.instance.userInvenSlotInfo.questList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.questList.Count; i++)
        {
            windowL[i].gameObject.SetActive(true);
            windowL[i].questName = GameManager.instance.userInvenSlotInfo.questList[i].questName;
            windowL[i].text.text = GameManager.instance.userInvenSlotInfo.questList[i].mainDescription;

            if (i == 3)
            {
                return;
            }
        }

    }

    public void ReMoveQuestWindow(string _questName)
    {
        for (int i = 0; i < windowL.Count; i++)
        {
            if (windowL[i].questName == _questName)
            {
                windowL[i].questName = "";
                windowL[i].text.text = "";
                windowL[i].gameObject.SetActive(false);
                i--;
            }
        }
    }

    public void AddQuestWindow(string _questName,string _text)
    {
        for (int i = 0; i < windowL.Count; i++)
        {
            if (windowL[i].gameObject.activeSelf == false)
            {
                windowL[i].gameObject.SetActive(true);
                windowL[i].questName = _questName;
                windowL[i].text.text = _text;
                return;
            }
        }
    }

    public void ResetQuestWindow(QuestInfo _questInfo)
    {
        for (int i = 0; i < windowL.Count; i++)
        {

            if (_questInfo.questName == windowL[i].questName)
            {
                Debug.Log(_questInfo.mainDescription);
                windowL[i].text.text = _questInfo.mainDescription;

            }
        }
    }


}
