using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    /*
     * 1. 게임의 데이터 로딩
     *      게임의 데이터에는
     *      유저 데이터, 몬스터 데이터
     * 
     * 2. 게임이 변경사항이 생겼을때 데이터를 업데이트 한다.
     *      2.1 User의 데이터가 변경되었을때
     *          ex) 스테이지가 시작되고 피로도를 소모했을때
     *              스테이지가 끝나고 경험치 골드등 재화를 획득할때
     *              새로운 캐릭터를 얻었을때
     *              상점에서 상품을 구매했을때
     *              
     * 3. 게임이 종료됬을때 데이터 세이브
     *    
     * 
     * 
     */

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        initPressKeyValuePairs();

    }

    public List<Monster> thisSceneMonsterList = new List<Monster>();
    public List<NPCController> thisSceneNPCList = new List<NPCController>();
    
    public List<QuestInfo> questList = new List<QuestInfo>();
    

    public CharacterInput player;
    public NPCController ClosedNPC;
    public List<InteractionObejct> InteractionObejctList;
    public InteractionObejct Interaction;
    public PortalControl ClosedPortal;
    public CheckDistance ClosedItem;
    
    public Stats userStats;

    public GameObject LoadingCanvas;

    public List<MonsterAI> nowTraceMonsterList;

    public UserInvenSlotInfo userInvenSlotInfo;
    public GameObject lvUpEffect;
    public FollowCam mainCam;
    public GameObject boss;

    public IEnumerator MinimumWaitTime(float time)
    {
        WaitForSeconds waitTime = new WaitForSeconds(time);
        yield return waitTime;
    }



    public Image loadingGauge;
    public Text loadingText;
    public void SetLoadingText(float value)
    {
        loadingGauge.fillAmount = value;
        value *= 100;
        if (value >= 98)
        {
            value = 100;
        }
        loadingText.text = (int)value + "%";
    }

    public GameObject dropItemPrefab;
    public void DropItem(Vector3 position)
    {
        GameObject obj = Instantiate(dropItemPrefab, position, Quaternion.identity);
        obj.name = TableManager.instance.itemTable.RandomItem();
    }

    public void GetItem()
    {
        userInvenSlotInfo.AddInvenList(ClosedItem.name);
        Destroy(ClosedItem.gameObject);
    }


    private KeyCode[] keyCodes = { KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12,
                                    KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9};
    public KeyCode nowKey = KeyCode.None;
    Dictionary<KeyCode, int> pressKeyValuePairs;

    public void QuickSlotAction(KeyCode key)
    {
        if (UIManager.instance.quickSlots[pressKeyValuePairs[key]].cool.fillAmount != 0)
        {
            nowKey = KeyCode.None;
            return;
        }

        // 퀵슬롯 눌렀을때 눌린녀석이 스킬인지 소비류인지 나눠야한다.
        // 스펠류일때
        if (UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.isSpell)
        {
            Vector3 SkillTargetPosition = player.transform.position;
            if (!string.IsNullOrEmpty(UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.itemName))
            {
                GameObject tmp = Resources.Load<GameObject>("EffectsPrefab/" + UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.itemName);
                tmp.GetComponent<MeteoController>().SetGroundPos();
                Instantiate(tmp, SkillTargetPosition + Vector3.up * 30 + Vector3.right * 25, Quaternion.identity);
            }
        }
        else 
        {
            if (userInvenSlotInfo.questList.Count != 0)
            {
                for (int i = 0; i < userInvenSlotInfo.questList.Count; i++)
                {
                    if (UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.itemName == userInvenSlotInfo.questList[i].questMobName)
                    {
                        userInvenSlotInfo.MinusQuestMob(userInvenSlotInfo.questList[i].questMobName);
                    }
                }
            }
            userStats.uStats.Hp += UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.plusHp;
            userStats.uStats.Mp += UIManager.instance.quickSlots[pressKeyValuePairs[key]].option.option.plusMp;
            UIManager.instance.SetHpMpUI();
        }
        UIManager.instance.quickSlots[pressKeyValuePairs[key]].ClickCoolTimeSlot();
        nowKey = KeyCode.None;

    }


    private void initPressKeyValuePairs()
    {
        pressKeyValuePairs = new Dictionary<KeyCode, int>();
        pressKeyValuePairs.Add(KeyCode.Alpha1, 0);
        pressKeyValuePairs.Add(KeyCode.Alpha2, 1);
        pressKeyValuePairs.Add(KeyCode.Alpha3, 2);
        pressKeyValuePairs.Add(KeyCode.Alpha4, 3);
        pressKeyValuePairs.Add(KeyCode.Alpha5, 4);
        pressKeyValuePairs.Add(KeyCode.Alpha6, 5);
        pressKeyValuePairs.Add(KeyCode.Alpha7, 6);
        pressKeyValuePairs.Add(KeyCode.Alpha8, 7);
        pressKeyValuePairs.Add(KeyCode.Alpha9, 8);
        pressKeyValuePairs.Add(KeyCode.Alpha0, 9);

    }

    private void Update()
    {
        FindKey();

        if (nowKey != KeyCode.None)
        {
            QuickSlotAction(nowKey);

        }

    }

    public void FindKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode item in keyCodes)
            {
                if (Input.GetKeyDown(item))
                {
                    nowKey = item;
                    return;
                    //isPress = true;
                }
                nowKey = KeyCode.None;
            }
        }
    }

    public void DataLoadingAll()
    {
        UIManager.instance.SetHpMpUI();
        UIManager.instance.SetUserStatsInfo();
        UIManager.instance.ResetInvenQuickEquip();
    }

    public void NPCInit(NPCController npc)
    {
        thisSceneNPCList.Add(npc);
        //npc.myBillboard.

        if (npc.name == "Beginner")
        {
            npc.myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[2];
            npc.myBillboard.spriteRenderer.color = Color.white;
            if (userInvenSlotInfo.questList.Count == 0) return;
            for (int i = 0; i < userInvenSlotInfo.questList.Count; i++)
            {
                if(userInvenSlotInfo.questList[i].checkClear)
                {
                    npc.myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[1];
                    npc.myBillboard.spriteRenderer.color = Color.red;
                    return;
                }
            }


            //npc.myBillboard.spriteRenderer.sprite = UIManager.instance.sprites[2];
            //npc.myBillboard.spriteRenderer.sprite = null;

        }
    }

    public void LvUpEffect()
    {
        Instantiate(lvUpEffect, player.transform.position,Quaternion.identity);
        UIManager.instance.PlayLvUpText();
    }
}
