using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    public List<QuestInfo> questList;
    public List<QuestInfo> activeList;
    public List<QuestInfo> clearList;

    QuestInfo questTuto;
    
    private void initQuest()
    {
        // 퀘스트 인덱스 9999는 임의 할당한 퀘스트
        questTuto.index = 9999;
        questTuto.questName = "조작가이드";
        questTuto.npcName = "시스템";
        questTuto.description = "WSAD키를 눌러 캐릭터를 이동시켜 보세요.";
        questTuto.mainDescription = "WSAD키를 눌러 캐릭터를 이동시켜 보세요.";
        questTuto.reward = "EXP 100";
        activeList.Add(questTuto);

        QuestWindowAdd(activeList[0]);
        for (int i = 0; i < TableManager.instance.questTable.questList.Count; i++)
        {
            questList.Add(TableManager.instance.questTable.questList[i]);
        }
        



    }

    

    void Start()
    {
        initQuest();

    }

    void Update()
    {
        MoveTutorial();
    }



    private void MoveTutorial()
    {
        
        if (activeList.Count == 0)
        {
            return;
        }
        if (activeList[0].questName == "조작가이드")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(DisableUI(UIManager.instance.tutorialKey[0]));
                //UIManager.instance.tutorialKey[0].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(DisableUI(UIManager.instance.tutorialKey[1]));
                //UIManager.instance.tutorialKey[1].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(DisableUI(UIManager.instance.tutorialKey[2]));
                //UIManager.instance.tutorialKey[2].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(DisableUI(UIManager.instance.tutorialKey[3]));
                //UIManager.instance.tutorialKey[3].SetActive(false);
                QuestWindowRemove(activeList[0]);
                GetReward(activeList[0].reward);
                clearList.Add(activeList[0]);
                activeList.Remove(activeList[0]);
            }
        }
    }

    public void GetReward(string reward)
    {
        if (reward != string.Empty)
        {
            string[] strTmp = reward.Split(' ');

            for (int i = 0; i < strTmp.Length; i++)
            {
                if (strTmp[i] == "EXP")
                {
                    GameManager.instance.userStats.PlusExp(int.Parse(strTmp[i + 1]));
                    UIManager.instance.SetUpExp();
                }
            }
            
        }
    }


    private IEnumerator DisableUI(GameObject obj)
    {
        Image c1Image = obj.GetComponent<Image>();
        Text c2Text = obj.GetComponentInChildren<Text>();
        Color c1;
        Color c2;
        c1 = c1Image.color;
        c2 = c2Text.color;
        while (true)
        {
            c1.a -= 0.02f;
            c2.a -= 0.02f;
            c1Image.color = c1;
            c2Text.color = c2;
            if (c1.a <= 0 && c2.a <=0)
            {
                StopCoroutine(DisableUI(obj));
                obj.SetActive(false);
                if (obj.name == "Image (3)")
                {
                    UIManager.instance.tutorial.SetActive(false);
                }
            }
            yield return null;
        }
    }

    public void QuestWindowAdd(QuestInfo quest)
    {
        GameManager.instance.userInvenSlotInfo.AddQuest(quest);
    }

    public void QuestWindowRemove(QuestInfo quest)
    {
        GameManager.instance.userInvenSlotInfo.QuestClear(quest);
    }


}
