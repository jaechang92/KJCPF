using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<QuestInfo> clearList;

    QuestInfo questTuto;
    
    private void initQuest()
    {
        // 퀘스트 인덱스 9999는 임의 할당한 퀘스트
        questTuto.index = 9999;
        questTuto.npcName = "조작가이드";
        questTuto.npcName = "시스템";
        questTuto.description = "WSAD키를 눌러 캐릭터를 이동시켜 보세요.";
        questTuto.mainDescription = "WSAD키를 눌러 캐릭터를 이동시켜 보세요.";
        questTuto.reward = "EXP 100";
        questList.Add(questTuto);
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
        if (questList[0].questName == "조작가이드" )
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                UIManager.instance.tutorialKey[0].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                UIManager.instance.tutorialKey[1].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                UIManager.instance.tutorialKey[2].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                UIManager.instance.tutorialKey[3].SetActive(false);
                GetReward(questList[0].reward);
                clearList.Add(questList[0]);
                questList.Remove(questList[0]);
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
                }
            }


        }
    }


}
