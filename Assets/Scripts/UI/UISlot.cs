using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image moveIcon;
    public List<Slot> list = new List<Slot>();
    int iWorkingSlot = -1;  // 작업중인 슬롯 번호
    Transform moveIconOriginParent;
    public bool isDrag;
    
    public int tmpHp = 0;
    public int tmpMp = 0;
    void Start()
    {
        moveIcon = UIManager.instance.moveIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 원래 부모를 저장
        moveIconOriginParent = moveIcon.transform.parent;
        // 원래 부모의 부모로 이동
        moveIcon.transform.SetParent(moveIcon.transform.parent.parent);
        isDrag = false;
        Vector2 uiPos = eventData.position;

        // 슬롯을 찾는다.
        FindSlot(uiPos);
        moveIcon.transform.position = eventData.position;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveIcon.transform.SetParent(moveIconOriginParent);
        moveIcon.gameObject.SetActive(false);

        // 드레그중이 아니고 클릭한 슬롯이 쿨타임 슬롯이라면 스킬또는 아이템 사용
        if (iWorkingSlot != -1 && !isDrag && list[iWorkingSlot].isCoolTimeSlot)
        {
            list[iWorkingSlot].ClickCoolTimeSlot();
        }

    }

    // 드래그 시작시 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (iWorkingSlot == -1) return;
        // 원래 부모로 이동
        moveIcon.transform.SetParent(moveIconOriginParent);
        // 드래그 하려는 슬롯이 쿨타임 슬롯이고 쿨타임이 돌고있다면 리턴
        if (list[iWorkingSlot].isCoolTimeSlot && list[iWorkingSlot].cool.fillAmount != 0) return;

        // 드래그 시작
        isDrag = true;

        
        moveIcon.gameObject.SetActive(true);
        // 터치시 어느 슬롯을 클릭했는지 검색
        Vector2 uiPos = eventData.position;

        // 터치한 곳으로 MoveIcon을 옮겨 준다.
        moveIcon.transform.position = eventData.position;

        //FindSlot(uiPos);
    }

    // 드래그 진행중 호출
    public void OnDrag(PointerEventData eventData)
    {
        if (iWorkingSlot == -1)
            return;

        moveIcon.transform.position = eventData.position;
    }

    // 드래그 종료시 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 중이 아니라면 리턴
        if (!isDrag) return;

        //1. 내려놓는곳의 슬롯을 알아야 한다.
        // 리스트를 순회하면서 드래그위치가 어느슬롯에 포함되는지를 검사
        // 드래그 위치 (uiPos)
        Vector2 uiPos = eventData.position;

        moveIcon.transform.position = eventData.position;
        // 포인터가 현재 있는 위치에 맞는 타겟슬롯을 가져온다. ex) 인벤슬롯인지 장비슬롯인지 퀵스롯인지
        UISlot targetSlot = UIManager.instance.FindTargetSlot(eventData.pointerCurrentRaycast.gameObject.name);

        // 알맞는 슬롯이 아닌 공백부분이라면
        if (targetSlot == null)
        {
            // 현재 작업중인 슬롯이 퀵슬롯이 아니면 리턴
            if(this != UIManager.instance.uISlotsList[2]) return;

            // 퀵슬롯일땐 퀵슬롯의 아이템을 제거 해주는 로직
            if (iWorkingSlot == -1) return;
            InitSlot(list[iWorkingSlot]);

            // 차후에 인벤 슬롯이거나 장비슬롯일때 인벤 밖부분 누르면 아이템을 버리는 UI띄워주고 버릴것인지 물어본다.




            //영역밖에 내려놓을 경우는 아래의 코드가 실행된다.
            Debug.Log("영역밖");
            GameManager.instance.userInvenSlotInfo.RemoveQuickList(iWorkingSlot);
            moveIcon.sprite = null;
            moveIcon.gameObject.SetActive(false);
            iWorkingSlot = -1;
        }

        
        if (targetSlot != null)
        {

            // 타겟슬롯의 슬롯 리스트만큼 돌면서 마우스위치의 슬롯을 찾아낸다.
            for (int i = 0; i < targetSlot.list.Count; i++)
            {
                // i번째 슬롯에 마우스가 있다면
                if (targetSlot.list[i].IsInRect(uiPos))
                {
                    // 시작슬롯이 쿨타임이 돌고있는것은 OnBeginDrag에서 이미 걸러줬음
                    // 만약 타겟 슬롯이 쿨타임 돌고있으면 스왑X
                    if (targetSlot.list[i].cool != null && targetSlot.list[i].cool.fillAmount != 0)
                    {
                        return;
                    }

                    // 만약 타겟 슬롯이 장비 슬롯인데 작업중인 슬롯이 장비가 아니라면 리턴
                    if (!list[iWorkingSlot].option.option.isEquip && targetSlot.list[i].tag == "EquipSlot")
                    {
                        return;
                    }

                    // 현재 작업중인 슬롯이 장비 슬롯인데 만약 타겟 슬롯이 비어있지 않고 타겟슬롯이 장비슬롯이 아니라면 스왑X
                    if (list[iWorkingSlot].tag == "EquipSlot" && targetSlot.list[i].Icon.sprite != null && !targetSlot.list[i].option.option.isEquip)
                    {
                        return;
                    }


                    SwapImage(list[iWorkingSlot], targetSlot.list[i],i);



                    //영역밖에 내려놓을 경우는 아래의 코드가 실행된다.
                    moveIcon.sprite = null;
                    moveIcon.gameObject.SetActive(false);
                    iWorkingSlot = -1;

                }
            }
        }

    }

    public void FindSlot(Vector2 uiPos)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // 영역안에 클릭했고, 
            // 아이콘 게임오브젝트가 활성화 되어 있다면

            // 영역 안이라면 list[i].IsInRect(uiPos) = true 값을 반환
            // list[i].Icon.gameObject.activeSelf == true  아이콘이 있다면 
            if (list[i].IsInRect(uiPos) && list[i].Icon.gameObject.activeSelf == true)
            {

                // MoveIcon의 스프라이트를 작업중인 슬롯의 스프라이트로 교체
                // 작업중인 슬롯은 i번째 슬롯
                string strName = list[i].Icon.sprite.name;
                // 리소스 폴더에서 로드하여 MoveIcon에 대입
                
                    moveIcon.sprite = Resources.Load<Sprite>("IconData/" + strName);
                
                // 터치시 MoveIcon을 활성화 시켜준다.
                
                // moveIcon의 사이즈를 슬롯의 사이즈로 바꿔준다.
                moveIcon.rectTransform.sizeDelta = list[i].Icon.rectTransform.sizeDelta;

                // 현재 작업중인 슬롯의 인덱스
                iWorkingSlot = i;
                break;
            }
        }
    }


    public void SwapImage(Slot first, Slot second,int secondIndex)
    {
        string strTmp;
        strTmp = first.Icon.sprite.name;
        Debug.Log(second.tag);
        // 두번째 슬롯이 비어있지 않으면
        if (second.Icon.sprite != null)
        {
            Debug.Log(second.tag);
            //if (first.tag != "QuickSlot" && second.tag == "QuickSlot")
            //{
            //    first.Icon.sprite = Resources.Load<Sprite>("IconData/" + second.Icon.sprite.name);
            //    second.Icon.sprite = Resources.Load<Sprite>("IconData/" + strTmp);
            //    initOption(first);
            //    initOption(second);
            //    Debug.Log("not Quick Send Quick");
            //}
            //else
            //{
            //    first.Icon.sprite = Resources.Load<Sprite>("IconData/" + second.Icon.sprite.name);
            //    second.Icon.sprite = Resources.Load<Sprite>("IconData/" + strTmp);
            //    initOption(first);
            //    initOption(second);
            //}

            if (first.tag == "InventoryItemSlot" && second.tag == "InventoryItemSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddInvenList(first.option.option.itemName, secondIndex);
                GameManager.instance.userInvenSlotInfo.AddInvenList(second.option.option.itemName, iWorkingSlot);
            }

            // 인벤슬롯에서 퀵슬롯으로 갈때
            if (first.tag == "InventoryItemSlot" && second.tag == "QuickSlot" )
            {
                if (first.option.option.isEquip) return;
                GameManager.instance.userInvenSlotInfo.AddQuickList(first.option.option.itemName, secondIndex);
                
                second.Icon.sprite = Resources.Load<Sprite>("IconData/" + strTmp);
                initOption(second);
                Debug.Log("비어있지 않을때");
                return;
            }

            // 퀵슬롯에서 인벤슬롯으로 갈때
            if (first.tag == "QuickSlot" && second.tag == "InventoryItemSlot")
            {
                if (first.option.option.isSpell) return;
                GameManager.instance.userInvenSlotInfo.RemoveQuickList(iWorkingSlot);
            }

            // 인벤슬롯에서 장비슬롯으로 갈때
            if (first.tag == "InventoryItemSlot" && second.tag == "EquipSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddEquipList(first.Icon.sprite.name, secondIndex);
                GameManager.instance.userInvenSlotInfo.RemoveInvenList(iWorkingSlot);
            }

            // 장비슬롯에서 인벤 슬롯으로 갈때
            if (first.tag == "EquipSlot" && second.tag == "InventoryItemSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddEquipList(first.Icon.sprite.name, secondIndex);
                GameManager.instance.userInvenSlotInfo.RemoveEquipList(iWorkingSlot);
            }
            Debug.Log(second.tag);

            if (first.tag == "SkillSlot" && second.tag == "QuickSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddQuickList(first.Icon.sprite.name, secondIndex);
            }

            first.Icon.sprite = Resources.Load<Sprite>("IconData/" + second.Icon.sprite.name);
            second.Icon.sprite = Resources.Load<Sprite>("IconData/" + strTmp);
            initOption(first);
            initOption(second);

        }
        else
        {
            if (first.tag == "InventoryItemSlot" && second.tag == "InventoryItemSlot")
            {
                GameManager.instance.userInvenSlotInfo.RemoveInvenList(iWorkingSlot);
                GameManager.instance.userInvenSlotInfo.AddInvenList(first.option.option.itemName, secondIndex);
                InitSlot(first);
            }

            if (first.tag == "EquipSlot" && second.tag == "QuickSlot")
            {
                return;
            }

            if (first.tag == "InventoryItemSlot" && second.tag == "QuickSlot")
            {
                Debug.Log("비어 있을때");
                if (first.option.option.isEquip) return;
                GameManager.instance.userInvenSlotInfo.AddQuickList(first.Icon.sprite.name, secondIndex);
            }

            if (first.tag == "QuickSlot" && second.tag == "InventoryItemSlot")
            {
                if (first.option.option.isSpell) return;
                GameManager.instance.userInvenSlotInfo.RemoveQuickList(iWorkingSlot);
            }

            if (first.tag == "QuickSlot" && second.tag == "QuickSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddQuickList(first.Icon.sprite.name, secondIndex);
                GameManager.instance.userInvenSlotInfo.RemoveQuickList(iWorkingSlot);
                InitSlot(first);
            }

            
            if (first.tag == "QuickSlot" && second.tag != "QuickSlot")
            {
                Debug.Log("퀵에서 퀵이아닌곳");
                GameManager.instance.userInvenSlotInfo.RemoveQuickList(iWorkingSlot);
                InitSlot(first);
                return;
            }

            if (first.tag == "InventoryItemSlot" && second.tag == "EquipSlot")
            {
                
                GameManager.instance.userInvenSlotInfo.RemoveInvenList(iWorkingSlot);
                GameManager.instance.userInvenSlotInfo.AddEquipList(first.Icon.sprite.name, secondIndex);
                InitSlot(first);
            }

            if (first.tag == "EquipSlot" && second.tag == "InventoryItemSlot")
            {
                GameManager.instance.userInvenSlotInfo.RemoveInvenList(iWorkingSlot);
                GameManager.instance.userInvenSlotInfo.AddEquipList(first.Icon.sprite.name, secondIndex);
                InitSlot(first);
            }

            if (first.tag == "SkillSlot" && second.tag == "QuickSlot")
            {
                GameManager.instance.userInvenSlotInfo.AddQuickList(first.Icon.sprite.name, secondIndex);
            }

            second.Icon.gameObject.SetActive(true);
            second.Icon.sprite = Resources.Load<Sprite>("IconData/" + strTmp);
            initOption(second);
            // 첫번째 슬롯은 초기화
            //InitSlot(first);
        }

        // 두개의 슬롯중 어느 하나라도 장비라면 유저정보 갱신
        if (first.transform.parent.name == "Equip" || second.transform.parent.name == "Equip")
        {
            UIManager.instance.SetUserStatsInfo();
        }

    }

    public void initOption(Slot initSlot)
    {
        //public int plusHp, plusMp, plusAttack, plusDefense;
        //public string itemName;
        //public bool isEquip;
        //public bool isSpell;
        //public float coolTime;
        // 테이블에서 이름으로 찾아오고 해당하는 옵션을 설정
        if (initSlot.Icon.sprite != null)
        {
            ItemInfo tmp = TableManager.instance.FindItemTable(initSlot.Icon.sprite.name);

            initSlot.option.option.plusHp = tmp.plusHp;
            initSlot.option.option.plusMp = tmp.plusMp;
            initSlot.option.option.plusAttack = tmp.plusAttack;
            initSlot.option.option.plusDefense = tmp.plusDefense;
            initSlot.option.option.itemName = tmp.itemName;
            initSlot.option.option.isEquip = tmp.isEquip;
            initSlot.option.option.isSpell = tmp.isSpell;
            initSlot.option.option.coolTime = tmp.coolTime;

            initSlot.coolTime = tmp.coolTime;
        }
        
    }


    public void TakeEquipment(Slot tmp)
    {

        tmpHp += tmp.option.option.plusHp;
        tmpMp += tmp.option.option.plusMp;
    }


    public void InitSlot(Slot slot)
    {
        // 슬롯을 초기화해주는 함수
        slot.Icon.sprite = null;
        slot.Icon.gameObject.SetActive(false);
        if (slot.cool != null)
        {

            slot.coolTime = 0;
            slot.cool.fillAmount = 0;
        }
        slot.option.InitOption();

    }

}
