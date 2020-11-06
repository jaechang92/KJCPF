using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public int questProgress;
    
    public bool isQuestClear = false;
    public bool isShop;
    
    private Dictionary<int, QuestInfo> myKeyValue;
    public List<QuestInfo> infos = new List<QuestInfo>();
    public Billboard myBillboard;

    public Text myText;

    

    void initQuestList()
    {

        
        

        Debug.Log("NPC퀘스트 셋");

        for (int i = 0; i < TableManager.instance.questTable.questList.Count; i++)
        {
            if (TableManager.instance.questTable.questList[i].npcName == transform.name)
            {
                infos.Add(TableManager.instance.questTable.questList[i]);
            }
        }



        myBillboard = GetComponentInChildren<Billboard>();

        if (infos.Count>questProgress)
        {
            myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[2];
        }

        //if (myBillboard != null)
        //{
        //    if (infos.Count != 0)
        //    {
        //        for (int i = 0; i < infos.Count; i++)
        //        {
        //            if (infos[i].checkClear != true)
        //            {
        //                myBillboard.spriteRenderer.sprite = myBillboard.questionMark;
        //                return;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        myBillboard.spriteRenderer.sprite = null;
        //    }
        //}


        


    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameManager.instance.thisSceneNPCList.Remove(this);
    }

    void Start()
    {
        initQuestList();

        SetUpNPCTag();

    }

    
    void Update()
    {
        LookAtPlayer();
        NPCNameTag();
    }

    public float lookDistance = 2.0f;

    public float DISTANCETOPLAYER
    {
        get
        {
            return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
        }
    }

    void LookAtPlayer()
    {
        if (GameManager.instance.player == null)
        {
            return;
        }

        if (DISTANCETOPLAYER <= lookDistance)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(GameManager.instance.player.transform.position.x - transform.position.x,0, GameManager.instance.player.transform.position.z - transform.position.z), Time.deltaTime * 10.0f, 0.0F);
            
            transform.rotation = Quaternion.LookRotation(newDir);
            GameManager.instance.ClosedNPC = this;
        }
        else if(GameManager.instance.ClosedNPC == this)
        {
            GameManager.instance.ClosedNPC = null;
        }

    }

    string[] tmp;
    public int tmpIndex = 0;
    public void TalkToNPC()
    {

        if (tmpIndex == -1)
        {
            return;
        }

        //TableManager.instance.questTable.useNameAndIndexDictionary[transform.name].Count-1 < questProgress
        if (infos.Count -1 <questProgress)
        {
            UIManager.instance.PlayerAndNpcTalkEnd();
            return;
        }

        //Debug.Log(TableManager.instance.questTable.useNameAndIndexDictionary[transform.name][questProgress].description);
        // \n 을 기준으로 문장을 다시 나눈다
        char[] chMark = { '\\', 'n' };
        tmp = infos[questProgress].mainDescription.Split(chMark);

        List<string> nQuestStringList = new List<string>();
        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i] != string.Empty)
            {
                nQuestStringList.Add(tmp[i]);
            }
        }


        Debug.Log(infos[questProgress].mainDescription);
        // 퀘스트 리스트에 없다면 퀘스트 추가
        if (GameManager.instance.userInvenSlotInfo.FindQuest(infos[questProgress].questName) == null)
        {
            UIManager.instance.talkText.text = nQuestStringList[tmpIndex];

            if (nQuestStringList.Count - 1 == tmpIndex)
            {
                Debug.Log("마지막 줄");
                UIManager.instance.acceptBtn.gameObject.SetActive(true);
                return;
            }
            tmpIndex++;
        }
        else if(GameManager.instance.userInvenSlotInfo.FindQuest(infos[questProgress].questName).Value.checkClear != true)
        {
            UIManager.instance.talkText.text = "임무를 끝내고 돌아와줘";
        }
        else
        {
            
            GameManager.instance.userInvenSlotInfo.QuestClear(infos[questProgress]);
            UIManager.instance.chatText.SetText(infos[questProgress].reward + "획득");
            Rewards(infos[questProgress].reward);
            Debug.Log("다음임무");
            questProgress++;
            if (questProgress >= infos.Count)
            {
                Debug.Log("더이상 다음 퀘스트가 없다");
                myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[0];
            }
            else
            {
                myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[2];
                myBillboard.spriteRenderer.color = Color.white;
            }
            UIManager.instance.PlayerAndNpcTalkEnd();
        }

        //UIManager.instance.qList.SetQuest(TableManager.instance.questTable.useNameAndIndexDictionary[transform.name][questProgress]);
        

    }

    public void GetQuest()
    {

    }

    public void SetUpNPCTag()
    {
        for (int i = 0; i < UIManager.instance.textPool.Count; i++)
        {
            if (UIManager.instance.textPool[i].gameObject.activeSelf == false)
            {
                UIManager.instance.textPool[i].gameObject.SetActive(true);
                myText = UIManager.instance.textPool[i];
                NPCName();
                return;
            }
        }
    }

    public void NPCNameTag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myBillboard.transform.position - Vector3.up * 0.5f);
        if (pos.z < 0)
        {
            return;
        }
        myText.transform.position = pos;
    }



    private void NPCName()
    {
        if (transform.name == "BeginnerTutorial")
        {
            myText.text = "테스";
        }

        if (transform.name == "Beginner")
        {
            myText.text = "비기너";
        }
        if (transform.name == "Mercenarysale")
        {
            myText.text = "마로니";
        }
        if (transform.name == "Smith")
        {
            myText.text = "스미스";
        }
        if (transform.name == "Priest")
        {
            myText.text = "마론";
        }
        if (transform.name == "SoldierMovePath")
        {
            myText.text = "경비병";
        }


    }
    [SerializeField]
    public Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

    public void Rewards(string str)
    {
        // 띄어스기로 1차 나눠준다.
        string [] tmp = str.Split(' ');
        string[] tmp2;

        // 언더바(_)로 2차 나눠준다.
        // 나뉜녀석들은 keyValue 값으로 저장한다.

        List<string> tmpList = new List<string>();
        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i] != string.Empty)
            {
                tmp2 = tmp[i].Split('_');
                for (int j = 0; j < tmp2.Length; j += 2)
                {
                    keyValuePairs.Add(tmp2[j], tmp2[j + 1]);
                }
            }
        }

        foreach (var item in keyValuePairs)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }

        if(keyValuePairs.ContainsKey("exp"))
        {
            GameManager.instance.userStats.uStats.Exp += int.Parse(keyValuePairs["exp"]);
            UIManager.instance.SetUpExp();
        }

        //if (keyValuePairs["gold"] != null)
        //{
        //    GameManager.instance.userStats.uStats.Gold += int.Parse(keyValuePairs["gold"]);
        //}

        //if (keyValuePairs["equip"] != null)
        //{
        //    GameManager.instance.userInvenSlotInfo.AddInvenList(keyValuePairs["equip"]);
        //}


        //for (int i = 0; i < tmp.Length; i++)
        //{
        //    if(tmp[i].Contains("Equip"))
        //    {
        //        GameManager.instance.userInvenSlotInfo.AddInvenList(tmp[i]);
        //    }


        //    string[] tmp2 = tmp[i].Split('_');
        //    if (tmp[i].Contains("골드"))
        //    {
        //        GameManager.instance.userStats.uStats.Gold += int.Parse(tmp2[0]);
        //    }

        //    if (tmp[i].Contains("exp"))
        //    {
        //        GameManager.instance.userStats.PlusExp(int.Parse(tmp2[0]));
        //        //GameManager.instance.userStats.uStats.Exp += int.Parse(tmp2[0]);
        //        UIManager.instance.SetUpExp();
        //    }
        //}





    }



    
}
