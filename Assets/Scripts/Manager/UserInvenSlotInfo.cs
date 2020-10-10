using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EquipStatsTotal
{
    public float hp;
    public float mp;
    public float attack;
    public float defense;
}


public class UserInvenSlotInfo : MonoBehaviour
{
    public List<string> invenList = new List<string>();
    public List<string> equipList = new List<string>();
    public List<string> quickSlotList = new List<string>();
    public List<QuestInfo> questList = new List<QuestInfo>();

    
    


    private void Start()
    {
        InitItem();
    }

    public void InitItem()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    int tmp = Random.Range(0, TableManager.instance.itemTable.itemList.Count);

        //    invenList[i] = TableManager.instance.itemTable.itemList[tmp].itemName;
        //}
        
    }

    public void AddEquipList(string name, int index)
    {
        equipList[index] = name;
    }

    public void RemoveEquipList(int index)
    {
        equipList[index] = "";
    }

    public void AddInvenList(string name, int index)
    {
        invenList[index] = name;
    }

    public void AddInvenList(string name)
    {
        for (int i = 0; i < invenList.Count; i++)
        {
            if (invenList[i] == "")
            {
                invenList[i] = name;
                return;
            }
        }
        UIManager.instance.chatText.SetText("인벤토리가 가득 차있습니다.");
        Debug.Log("인벤토리가 가득 차있습니다.");
    }


    public void RemoveInvenList(int index)
    {
        invenList[index] = "";
    }

    public void AddQuickList(string name, int index)
    {
        Debug.Log(name);
        Debug.Log(index);
        quickSlotList[index] = name;
    }

    public void RemoveQuickList(int index)
    {
        quickSlotList[index] = "";
    }




    public EquipStatsTotal PlusEquipStats()
    {
        EquipStatsTotal tmp = new EquipStatsTotal();
        if (equipList.Count == 0) return tmp;

        for (int i = 0; i < equipList.Count; i++)
        {
            tmp.hp += TableManager.instance.FindItemTable(equipList[i]).plusHp;
            tmp.mp += TableManager.instance.FindItemTable(equipList[i]).plusMp;
            tmp.attack += TableManager.instance.FindItemTable(equipList[i]).plusAttack;
            tmp.defense += TableManager.instance.FindItemTable(equipList[i]).plusDefense;
        }
        return tmp;
    }

    
    public void AddQuest(QuestInfo _questInfo)
    {
        questList.Add(_questInfo);
        UIManager.instance.qPool.AddQuestWindow(_questInfo.questName, _questInfo.mainDescription);
    }

    public void QuestClear(QuestInfo _questInfo)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questName == _questInfo.questName)
            {
                questList.Remove(questList[i]);
                UIManager.instance.qPool.ReMoveQuestWindow(_questInfo.questName);
                return;
            }
        }
    }


    public void MinusQuestMob(string questMobName)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            Debug.Log(questList[i].questMobModelname);
            Debug.Log(questMobName);
            if (questList[i].questMobModelname == questMobName)
            {
                QuestInfo tmp = questList[i];
                tmp.questMobCount--;
                if (tmp.questMobCount <=0)
                {
                    tmp.questMobCount = 0;
                    tmp.mainDescription = "퀘스트 완료";
                    //GameManager.instance.thisSceneNPCList[]
                    UIManager.instance.pointer.SetActive(false);
                    tmp.checkClear = true;
                }
                else
                {
                //Werewolf몬스터를 10마리 사냥하고 돌아와줘
                    tmp.mainDescription = "<color=#FF0000>" + tmp.questMobName+"</color>" + " 몬스터를 " + tmp.questMobCount + "사냥하고 돌아와줘";
                }
                questList[i] = tmp;
                UIManager.instance.qPool.ResetQuestWindow(questList[i]);
            }
        }
    }

    public bool FindQuestName(string str)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questName == str)
            {
                return true;
            }
        }
        return false;
    }


    public QuestInfo? FindQuest(string str)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].questName == str)
            {
                return questList[i];
            }
        }
        return null;
    }

    public void SetQuestLast()
    {

    }

    


}
