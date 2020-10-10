//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;


//public class ItemDragHandler : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler
//{
//    private RectTransform _myRectTransform;
//    public RectTransform MyRectTransform
//    {
//        get
//        {
//            return _myRectTransform;
//        }
        
//    }
    
//    public Image image;
//    private Transform originTr;
//    private Color alpha;
//    private SlotOption m_itemOption;
//    Transform originParent;
//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        if (this.gameObject.tag == "Untagged")
//        {
//            return;
//        }
//        //originTr = transform.parent;
//        UIManager.instance.dragImage.gameObject.SetActive(true);

//        UIManager.instance.draggingItemStartPoint = this;
//        //transform.SetParent(UIManager.instance.INVENTORYCANVAS.transform);
//        UIManager.instance.dragImage.transform.position = Input.mousePosition;
//        UIManager.instance.dragImage.rectTransform.sizeDelta = MyRectTransform.sizeDelta;
//        UIManager.instance.dragImage.sprite = image.sprite;
//        originParent = UIManager.instance.dragImage.transform.parent;
//        UIManager.instance.dragImage.transform.parent = UIManager.instance.dragImage.transform.parent.parent;


//        if (UIManager.instance.dragImage.sprite == null)
//        {
//            alpha = UIManager.instance.dragImage.color;
//            alpha.a = 0f;
//        }
//        else
//        {
//            alpha = UIManager.instance.dragImage.color;
//            alpha.a = 0.5f;
//        }
//        UIManager.instance.dragImage.color = alpha;

//        UIManager.instance.draggingItem = this.gameObject;

//        image.raycastTarget = false;
//    }
//    public void OnDrag(PointerEventData eventData)
//    {
//        UIManager.instance.dragImage.transform.parent = originParent;
//        UIManager.instance.dragImage.transform.position = Input.mousePosition;
//    }

//    public void OnDrop(PointerEventData eventData)
//    {

//        //transform.localPosition = Vector3.zero;
//        UIManager.instance.draggingItemDropPoint = this;


        
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        if (UIManager.instance.draggingItemStartPoint == null)
//        {
//            return;
//        }
//        SlotSwap(UIManager.instance.draggingItemStartPoint, UIManager.instance.draggingItemDropPoint);
//        //UIManager.instance.draggingItem.transform.SetParent(originTr);
//        UIManager.instance.draggingItemStartPoint = null;
//        UIManager.instance.draggingItemDropPoint = null;


//        //transform.localPosition = Vector3.zero;
//        image.raycastTarget = true;

//        alpha = UIManager.instance.dragImage.color;
//        alpha.a = 1.0f;
//        UIManager.instance.dragImage.color = alpha;
//        UIManager.instance.dragImage.gameObject.SetActive(false);
//    }

//    private void Awake()
//    {
//        image = GetComponent<Image>();
//        _myRectTransform = GetComponent<RectTransform>();
//        m_itemOption = GetComponent<SlotOption>();
//    }


//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void SlotSwap(ItemDragHandler first, ItemDragHandler second)
//    {
//        // 스왑부분
//        CompareSlot(first, second);


//        if (first.tag != "Untagged")
//        {
//            if (first.image.sprite == null)
//            {
//                first.image.color = new Color(1, 1, 1, 0);
//            }
//            else
//            {
//                first.image.color = new Color(1, 1, 1, 1);
//            }
//        }


//        if (second.tag != "Untagged")
//        {
//            if (second.image.sprite == null)
//            {
//                second.image.color = new Color(1, 1, 1, 0);
//            }
//            else
//            {
//                second.image.color = new Color(1, 1, 1, 1);
//            }
//        }

//    }

    

//    void CompareSlot(ItemDragHandler first, ItemDragHandler second)
//    {
//        if (first == null)
//        {
//            return;
//        }

//        if (first.gameObject.tag == "InventoryItemSlot" && second.gameObject.tag == "EquipSlot" || first.gameObject.tag == "EquipSlot" && second.gameObject.tag == "InventoryItemSlot")
//        {
//            if (first.m_itemOption.option.isEquip)
//            {
//                ImageSwap(first, second);
//                OptionSwap(first, second);
//            }
//        }

//        if (first.gameObject.tag == "InventoryItemSlot" && second.gameObject.tag == "InventoryItemSlot")
//        {
//            ImageSwap(first, second);
//            OptionSwap(first, second);
//        }

//        if (first.gameObject.tag == "InventoryItemSlot" && second.gameObject.tag == "QuickSlot")
//        {
//            if (first.m_itemOption.option.isEquip)
//            {
//                return;
//            }
//            CopyImage(first, second);
//        }

//        if (first.gameObject.tag == "QuickSlot" && (second.gameObject.tag == "InventoryItemSlot" || second.gameObject.tag == "Untagged"))
//        {
//            DeleteImage(first, second);
//        }

//        //Debug.Log(second.gameObject.tag);
//        //if (first.gameObject.tag == second.gameObject.tag && first.gameObject.tag  != "SkillSlot")
//        //{
//        //    ImageSwap(first, second);
//        //}
//        //else if(first.gameObject.tag == "InventoryItemSlot" && second.gameObject.tag == "EquipSlot")
//        //{
//        //    if (first.m_itemOption.thisItemInfo.isEquip)
//        //    {
//        //        ImageSwap(first, second);
//        //        OptionSwap(first, second);
//        //    }
//        //}
//        //else if(first.gameObject.tag != "QuickSlot" && second.gameObject.tag == "QuickSlot")
//        //{
//        //    CopyImage(first, second);
//        //}
//        //else if(first.gameObject.tag == "QuickSlot" && second.gameObject.tag != "QuickSlot" )
//        //{
//        //    Debug.Log("deletein?");
//        //    DeleteImage(first, second);
//        //}
//    }

//    void ImageSwap(ItemDragHandler first, ItemDragHandler second)
//    {
//        Sprite tmp;
//        tmp = first.image.sprite;
//        first.image.sprite = second.image.sprite;
//        second.image.sprite = tmp;
//    }

//    void CopyImage(ItemDragHandler first, ItemDragHandler second)
//    {
//        second.image.sprite = first.image.sprite;
//    }

//    void DeleteImage(ItemDragHandler first, ItemDragHandler second)
//    {
//        Debug.Log("delete");
        
//        first.image.sprite = null;
//        //first.image.color = new Color(1, 1, 1, 0);
//    }

//    void OptionSwap(ItemDragHandler first, ItemDragHandler second)
//    {
//        //if (first.gameObject.tag == "EquipSlot" && second.gameObject.tag == "InventoryItemSlot")
//        //{
//        //    Debug.Log(GameManager.instance.userStats.Hp);
//        //    Debug.Log(first.m_itemOption.thisItemInfo.plusHp);
//        //    GameManager.instance.userStats.Hp -= first.m_itemOption.thisItemInfo.plusHp;
//        //    GameManager.instance.userStats.Mp -= first.m_itemOption.thisItemInfo.plusMp;
//        //    GameManager.instance.userStats.Attack -= first.m_itemOption.thisItemInfo.plusAttack;
//        //    GameManager.instance.userStats.Defense -= first.m_itemOption.thisItemInfo.plusDefense;
//        //    GameManager.instance.userStats.Hp += second.m_itemOption.thisItemInfo.plusHp;
//        //    GameManager.instance.userStats.Mp += second.m_itemOption.thisItemInfo.plusMp;
//        //    GameManager.instance.userStats.Attack += second.m_itemOption.thisItemInfo.plusAttack;
//        //    GameManager.instance.userStats.Defense += second.m_itemOption.thisItemInfo.plusDefense;

//        //}
//        //else if(first.gameObject.tag == "InventoryItemSlot" && second.gameObject.tag == "EquipSlot")
//        //{
//        //    Debug.Log(GameManager.instance.userStats.Mp);
//        //    Debug.Log(second.m_itemOption.thisItemInfo.plusHp);
//        //    GameManager.instance.userStats.Hp -= second.m_itemOption.thisItemInfo.plusHp;
//        //    GameManager.instance.userStats.Mp -= second.m_itemOption.thisItemInfo.plusMp;
//        //    GameManager.instance.userStats.Attack -= second.m_itemOption.thisItemInfo.plusAttack;
//        //    GameManager.instance.userStats.Defense -= second.m_itemOption.thisItemInfo.plusDefense;
//        //    GameManager.instance.userStats.Hp += first.m_itemOption.thisItemInfo.plusHp;
//        //    GameManager.instance.userStats.Mp += first.m_itemOption.thisItemInfo.plusMp;
//        //    GameManager.instance.userStats.Attack += first.m_itemOption.thisItemInfo.plusAttack;
//        //    GameManager.instance.userStats.Defense += first.m_itemOption.thisItemInfo.plusDefense;
//        //}
//        //// 업데이트

//        //UIManager.instance.SetUserStatsInfo();

//        //ItemInfo tmp;
//        //tmp = first.m_itemOption.thisItemInfo;
//        //first.m_itemOption.thisItemInfo = second.m_itemOption.thisItemInfo;
//        //second.m_itemOption.thisItemInfo = tmp;
//    }

//}
