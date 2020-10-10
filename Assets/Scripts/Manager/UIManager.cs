using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // public------------------------------------
    public List<Slot> quickSlots;
    public List<GameObject> canvasList = new List<GameObject>();
    public GameObject STARTCANVAS { get { return canvasList[0]; } }
    public GameObject MAINCANVAS { get { return canvasList[1]; } }
    public GameObject BLURCANVAS { get { return canvasList[2]; } }
    public RectTransform portalCanvas;
    public string [] talkString;
    public Text talkText;
    public bool isPointerDown = false;
    public GameObject tmpImage;
    public GameObject draggingItem = null;
    public List<Text> userStatsText;
    public List<UISlot> uISlotsList;
    public Image moveIcon;
    public bool test;
    public string testSceneName;
    public Text descriptionText;
    public Button acceptBtn;
    public List<GameObject> InitList;
    public int OpenInvenCountCheck = 0;
    public SecondCamPos secondCam;
    public QuestList qList;
    public Image hp, mp;
    public Text lv;
    public Stats userInfo;
    public Chat chatText;
    public MonsterHpBarPool monsterHpBarPool;
    public QuestPool qPool;
    public Text goldText;
    public List<Sprite> sprites;
    public GameObject pointer;
    
    public List<Text> textPool;
    public List<MobInfoTag> tagPool;
    public Image expBar;
    public Animator LvUpText;
    public CrossHairDistanceToObject CHDTO;
    // private------------------------------------
    int talkCount = 1;
    int i = 0;

    //public Text userStatsText;



    // Test
    public void OnClick()
    {
        //Debug.Log(EventTrigger.);
    }

    
    

    private void Start()
    {
        
        for (int i = 0; i < textPool.Count; i++)
        {
            textPool[i].gameObject.SetActive(false);
        }

        for (int j = 0; j < tagPool.Count; j++)
        {
            tagPool[j].gameObject.SetActive(false);
        }






        quickSlots = uISlotsList[2].list;

        InitScene(SceneControllManager.instance.nowScene);

        if (test)
        {
            InitScene(testSceneName);
        }

        userInfo = GameManager.instance.userStats;
        SetUpExp();


    }

    private void Update()
    {
        PointerPos();
    }


    // Func---------------------------------------
    public void InitScene(string sceneName)
    {

        for (int i = 0; i < InitList.Count; i++)
        {
            InitList[i].SetActive(false);
        }

        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].gameObject.SetActive(false);
        }


        if (sceneName == "StartScene")
        {
            //Debug.Log(STARTCANVAS);
            STARTCANVAS.gameObject.SetActive(true);
        }
        else
        {
            MAINCANVAS.gameObject.SetActive(true);
        }

        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.isLoadingComplete = true;
        }

        if (portalCanvas.gameObject.activeSelf == true)
        {
            portalCanvas.gameObject.SetActive(false);
        }
    }

    

    
    public void PlayerAndNpcTalkStart()
    {
        // 가까이 있는 NPC

        //BLURCANVAS.gameObject.SetActive(true);
        if (secondCam == null)
        {
            secondCam = CamManager.instance.SecondCam;
        }
        secondCam.gameObject.SetActive(true);
        secondCam.SecondCamPosReset(GameManager.instance.ClosedNPC.transform);

        talkText.transform.parent.parent.gameObject.SetActive(true);
        GameManager.instance.player.isTalk = true;
        GameManager.instance.ClosedNPC.TalkToNPC();

    }
    

    public void Portal()
    {
        GameManager.instance.player.isTalk = true;
        portalCanvas.gameObject.SetActive(true);
    }

    public void PlayerAndNpcTalkEnd()
    {
        GameManager.instance.ClosedNPC.tmpIndex = 0;
        acceptBtn.gameObject.SetActive(false);
        talkText.transform.parent.parent.gameObject.SetActive(false);
        secondCam.gameObject.SetActive(false);
        //BLURCANVAS.gameObject.SetActive(false);
        GameManager.instance.player.isTalk = false;
        Debug.Log("End");
    }

    public void PortalTalkEnd()
    {
        portalCanvas.gameObject.SetActive(false);
        GameManager.instance.player.isTalk = false;
    }
    
    public void ItemCreateTest()
    {
        PoolingSystem.instance.itemPool.Remove(PoolingSystem.instance.Pull(new Vector3(-19, 2, 3)));


        //tmp.GetItemOption(TableManager.instance.itemTable.itemList[i]);
        i++;
    }

    public UISlot FindTargetSlot(string name)
    {
        for (int i = 0; i < uISlotsList.Count; i++)
        {
            if (uISlotsList[i].name == name)
            {
                return uISlotsList[i];
            }
        }

        return null;

    }

    public void NextScene(string sceneName)
    {
        SceneControllManager.instance.NextScene(sceneName);
    }




    public void OpenUI()
    {
        InitList[11].SetActive(true);
        GameManager.instance.player.isTalk = true;
    }

    public void CloseUI()
    {
        InitList[11].SetActive(false);
        GameManager.instance.player.isTalk = false;
    }
    
    public void GotoVillage()
    {
        SceneControllManager.instance.NextScene("Village");
        GameManager.instance.player.CanControl = true;
        GameManager.instance.userStats.uStats.Hp = GameManager.instance.userStats.uStats.MaxHp;
    }

    public void SetUserStatsInfo()
    {
        userStatsText[0].text = "Lv : " + TableManager.instance.userTable.userInfo.lv;

        if (GameManager.instance.userInvenSlotInfo.PlusEquipStats().hp != 0)
        {
            userStatsText[1].text = "Hp : " + TableManager.instance.userTable.userInfo.hp + "+" + "<color=lime>" + GameManager.instance.userInvenSlotInfo.PlusEquipStats().hp.ToString() + "</color>";
        }
        else
        {
            userStatsText[1].text = "Hp : " + TableManager.instance.userTable.userInfo.hp;
        }

        if (GameManager.instance.userInvenSlotInfo.PlusEquipStats().mp != 0)
        {
            userStatsText[2].text = "Mp : " + TableManager.instance.userTable.userInfo.mp + "+" + "<color=lime>" + GameManager.instance.userInvenSlotInfo.PlusEquipStats().mp.ToString() + "</color>";
        }
        else
        {
            userStatsText[2].text = "Mp : " + TableManager.instance.userTable.userInfo.mp;
        }

        

        if (GameManager.instance.userInvenSlotInfo.PlusEquipStats().attack != 0)
        {
            userStatsText[3].text = "Attack : " + TableManager.instance.userTable.userInfo.attack + "+" + "<color=lime>" + GameManager.instance.userInvenSlotInfo.PlusEquipStats().attack.ToString() + "</color>";
        }
        else
        {
            userStatsText[3].text = "Attack : " + TableManager.instance.userTable.userInfo.attack;
        }

        if (GameManager.instance.userInvenSlotInfo.PlusEquipStats().defense != 0)
        {
            userStatsText[4].text = "Defense : " + TableManager.instance.userTable.userInfo.defense + "+" + "<color=lime>" + GameManager.instance.userInvenSlotInfo.PlusEquipStats().defense.ToString() + "</color>";
        }
        else
        {
            userStatsText[4].text = "Defense : " + TableManager.instance.userTable.userInfo.defense;
        }

        userStatsText[5].text = "Exp : " + TableManager.instance.userTable.userInfo.exp;


        SetHpMpUI();
    }

    public void SetHpMpUI()
    {
        userInfo.uStats.MaxHp = TableManager.instance.userTable.userInfo.hp + GameManager.instance.userInvenSlotInfo.PlusEquipStats().hp;
        userInfo.uStats.MaxMp = TableManager.instance.userTable.userInfo.mp + GameManager.instance.userInvenSlotInfo.PlusEquipStats().mp;

        if(userInfo.uStats.Hp > userInfo.uStats.MaxHp)
        {
            userInfo.uStats.Hp = userInfo.uStats.MaxHp;
        }
        if (userInfo.uStats.Mp > userInfo.uStats.MaxMp)
        {
            userInfo.uStats.Mp = userInfo.uStats.MaxMp;
        }


        hp.fillAmount = userInfo.uStats.Hp / userInfo.uStats.MaxHp;
        mp.fillAmount = userInfo.uStats.Mp / userInfo.uStats.MaxMp;
        lv.text = GameManager.instance.userStats.uStats.Lv.ToString();

        if (userInfo.uStats.Hp <= 0)
        {
            GameManager.instance.player.CanControl = false;
        }

    }
    
    public void InvenOnOff(GameObject obj)
    {
        if (obj.activeSelf == false)
        {
            OpenInvenCountCheck++;
        }
        else if (obj.activeSelf == true)
        {
            OpenInvenCountCheck--;
        }


        if (GameManager.instance.player != null)
        {
            if (OpenInvenCountCheck == 0)
            {
                Debug.Log("in");
                GameManager.instance.player.CanControl = true;
            }
            else if (OpenInvenCountCheck > 0)
            {
                GameManager.instance.player.CanControl = false;
            }
        }
        obj.SetActive(!obj.activeSelf);
        switch (obj.gameObject.name)
        {
            case "EquipTItleBarCanvas":
                SetUserStatsInfo();
                EquiptReset();
                break;
            case "SkillTitleBarCanvas":

                break;
            case "QuestBarCanvas":
                qList.SetQuestListAll();
                break;
            case "InventoryTitleBarCanvas":
                InvenReset();
                break;
            default:
                break;
        }

    }

    //---------------------------------------
    public void ResetInvenQuickEquip()
    {
        InvenReset();
        EquiptReset();
        QuickReset();
    }

    public void InvenReset()
    {
        if (GameManager.instance.userInvenSlotInfo.invenList.Count == 0)
        {
            return;
        }

        //Debug.Log(GameManager.instance.userInvenSlotInfo.invenList.Count);
        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.invenList.Count; i++)
        {
            if (GameManager.instance.userInvenSlotInfo.invenList[i] == "")
            {
                continue;
            }
            ItemInfo tmp = TableManager.instance.itemTable.FindItem(GameManager.instance.userInvenSlotInfo.invenList[i]);

            uISlotsList[0].list[i].option.option.plusHp = tmp.plusHp;
            uISlotsList[0].list[i].option.option.plusMp = tmp.plusMp;
            uISlotsList[0].list[i].option.option.plusAttack = tmp.plusAttack;
            uISlotsList[0].list[i].option.option.plusDefense = tmp.plusDefense;
            uISlotsList[0].list[i].option.option.itemName = tmp.itemName;
            uISlotsList[0].list[i].option.option.isEquip = tmp.isEquip;
            uISlotsList[0].list[i].option.option.isSpell = tmp.isSpell;
            uISlotsList[0].list[i].option.option.coolTime = tmp.coolTime;
            uISlotsList[0].list[i].Icon.sprite = tmp.itemSprite;
            uISlotsList[0].list[i].Icon.gameObject.SetActive(true);

        }

        //for (int i = 0; i < GameManager.instance.invenList.Count; i++)
        //{
        //    uISlotsList[0].list[i].option.option.plusHp = GameManager.instance.invenList[i].plusHp;
        //    uISlotsList[0].list[i].option.option.plusMp = GameManager.instance.invenList[i].plusMp;
        //    uISlotsList[0].list[i].option.option.plusAttack = GameManager.instance.invenList[i].plusAttack;
        //    uISlotsList[0].list[i].option.option.plusDefense = GameManager.instance.invenList[i].plusDefense;
        //    uISlotsList[0].list[i].option.option.itemName = GameManager.instance.invenList[i].itemName;
        //    uISlotsList[0].list[i].option.option.isEquip = GameManager.instance.invenList[i].isEquip;
        //    uISlotsList[0].list[i].option.option.isSpell = GameManager.instance.invenList[i].isSpell;
        //    uISlotsList[0].list[i].option.option.coolTime = GameManager.instance.invenList[i].coolTime;


        //    uISlotsList[0].list[i].Icon.sprite = GameManager.instance.invenList[i].itemSprite;

        //}
    }

    public void EquiptReset()
    {
        if (GameManager.instance.userInvenSlotInfo.equipList.Count == 0)
        {
            return;
        }
        Debug.Log("---------------------------------------------");
        Debug.Log(GameManager.instance.userInvenSlotInfo.equipList.Count);
        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.equipList.Count; i++)
        {
            if (GameManager.instance.userInvenSlotInfo.equipList[i] == "")
            {
                continue;
            }
            
            ItemInfo tmp = TableManager.instance.itemTable.FindItem(GameManager.instance.userInvenSlotInfo.equipList[i]);
            
            uISlotsList[1].list[i].option.option.plusHp = tmp.plusHp;
            uISlotsList[1].list[i].option.option.plusMp = tmp.plusMp;
            uISlotsList[1].list[i].option.option.plusAttack = tmp.plusAttack;
            uISlotsList[1].list[i].option.option.plusDefense = tmp.plusDefense;
            uISlotsList[1].list[i].option.option.itemName = tmp.itemName;
            uISlotsList[1].list[i].option.option.isEquip = tmp.isEquip;
            uISlotsList[1].list[i].option.option.isSpell = tmp.isSpell;
            uISlotsList[1].list[i].option.option.coolTime = tmp.coolTime;
            uISlotsList[1].list[i].Icon.gameObject.SetActive(true);
            uISlotsList[1].list[i].Icon.sprite = tmp.itemSprite;
            
        }
    }

    public void QuickReset()
    {
        if (GameManager.instance.userInvenSlotInfo.quickSlotList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.quickSlotList.Count; i++)
        {
            if (GameManager.instance.userInvenSlotInfo.quickSlotList[i] == "")
            {
                continue;
            }
            uISlotsList[2].list[i].Icon.gameObject.SetActive(true);
            ItemInfo tmp = TableManager.instance.itemTable.FindItem(GameManager.instance.userInvenSlotInfo.quickSlotList[i]);
            uISlotsList[2].list[i].option.option.plusHp = tmp.plusHp;
            uISlotsList[2].list[i].option.option.plusMp = tmp.plusMp;
            uISlotsList[2].list[i].option.option.plusAttack = tmp.plusAttack;
            uISlotsList[2].list[i].option.option.plusDefense = tmp.plusDefense;
            uISlotsList[2].list[i].option.option.itemName = tmp.itemName;
            uISlotsList[2].list[i].option.option.isEquip = tmp.isEquip;
            uISlotsList[2].list[i].option.option.isSpell = tmp.isSpell;
            uISlotsList[2].list[i].option.option.coolTime = tmp.coolTime;
            uISlotsList[2].list[i].Icon.sprite = tmp.itemSprite;
        }
        Debug.Log("QuickReset");

    }
    //---------------------------------------
    public void ClearQuest()
    {
        GameManager.instance.ClosedNPC.questProgress++;
    }

    public void SetQuestInfo(string text)
    {
        //// 얻어온 문자열을 재가공
        //string[] strTmp = FindQuest(text).Split('.');

        descriptionText.text = FindQuest(text);

    }

    public string FindQuest(string questName)
    {
        for (int i = 0; i < qList.myQuest.Count; i++)
        {
            Debug.Log(questName);
            Debug.Log(qList.myQuest[i].questName);
            if (questName == qList.myQuest[i].questName)
            {
                return qList.myQuest[i].mainDescription;
            }
        }
        return null;
    }

    public void ClickAcceptBtn()
    {
        PlayerAndNpcTalkEnd();

        if (GameManager.instance.ClosedNPC.transform.name == "BeginnerTutorial"&& GameManager.instance.ClosedNPC.infos[GameManager.instance.ClosedNPC.questProgress].func == "NextScene")
        {
            SceneControllManager.instance.NextScene("Village");
            return;
        }

        // 보상을 받을수 있다면 보상을 받는다.
        // 받을 보상이 없다면 다음 퀘스트를 받는다.
        // 다음 퀘스트도 없다면 인사를 한다.

        GameManager.instance.userInvenSlotInfo.AddQuest(GameManager.instance.ClosedNPC.infos[GameManager.instance.ClosedNPC.questProgress]);

        if (GameManager.instance.ClosedNPC.infos[GameManager.instance.ClosedNPC.questProgress].questMobName == "rpgpp_lt_box_wood_01")
        {
            pointer.SetActive(true);
        }

        if (!string.IsNullOrEmpty(GameManager.instance.ClosedNPC.infos[GameManager.instance.ClosedNPC.questProgress].func))
        {
            //판단
            // 공백으로 문자열을 나눠준다
            string [] tmp = GameManager.instance.ClosedNPC.infos[GameManager.instance.ClosedNPC.questProgress].func.Split(' ');
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] == "Add")
                {
                    GameManager.instance.userInvenSlotInfo.AddInvenList(tmp[i + 1]);
                }
            }
        }

        GameManager.instance.ClosedNPC.myBillboard.spriteRenderer.sprite = sprites[1];
        GameManager.instance.ClosedNPC.myBillboard.spriteRenderer.color = Color.white;
    }


    public void PointerPos()
    {
        if (pointer.activeSelf == true)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(GameManager.instance.InteractionObejctList[0].transform.position + Vector3.up * 1.5f);
            pointer.transform.position = pos;
        }
    }

    public void SetUpExp()
    {
        Debug.Log("SetUp");
        expBar.fillAmount = GameManager.instance.userStats.uStats.Exp / GameManager.instance.userStats.uStats.MaxExp;
    }

    public void PlayLvUpText()
    {
        LvUpText.transform.gameObject.SetActive(true);
        LvUpText.Play("LvUpTextAni");
        StartCoroutine(SetFalseForTime(LvUpText.transform.gameObject,1.0f));
    }

    IEnumerator SetFalseForTime(GameObject obj, float time)
    {
        Debug.Log("코루틴");
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    public void TestBTN()
    {
        //GameManager.instance.player.agent.
    }

}
