using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public List<Button> contentBtnList = new List<Button>();
    public List<QuestInfo> myQuest = new List<QuestInfo>();
    //public List<Text> texts;

    public ScrollRect sr;
    public GameObject contentPrefab;
    void Start()
    {
        
    }

    //public void SetQuest(QuestInfo info)
    //{
    //    myQuest.Add(info);
    //    for (int i = 0; i < texts.Count; i++)
    //    {
    //        if (texts[i].text == "")
    //        {
    //            texts[i].text = info.questName;
    //        }
    //    }
    //}

    public void SetQuestListAll()
    {
        myQuest = GameManager.instance.userInvenSlotInfo.questList;


        if (contentBtnList.Count < myQuest.Count)
        {
            for (int i = 0; i < myQuest.Count - contentBtnList.Count; i++)
            {
                GameObject obj = Instantiate(contentPrefab, sr.content.transform);

                Button btn = obj.GetComponent<Button>();
                contentBtnList.Add(btn);
                btn.GetComponentInChildren<Text>().text = myQuest[i].questName;

                int _i = i;
                obj.GetComponent<Button>().onClick.AddListener(() => { UIManager.instance.SetQuestInfo(myQuest[_i].questName); });
            }
        }

    }
}
