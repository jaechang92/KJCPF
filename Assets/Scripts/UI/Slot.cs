using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[System.Serializable]
//public struct SlotInfo
//{
//    int slotIndex;
//    int ItemName;
//    Sprite slotItemImage;
//    int itemCount;
//    ItemInfo itemInfo;
//}

public class Slot : MonoBehaviour
{
    
    public Image Icon;
    public Image cool;
    public Text coolTimeText;
    public float coolTime = 0;
    public bool isCoolTimeSlot = false;
    public SlotOption option;
    
    // Rect는 사각형 영역 정보를 저장하는 구조체
    // RectTransform안에 Rect정보가 있지만 UI의 구조상 부모, 자식 관계를 
    // 포함하므로 좌표계산이 다소 까다로워 Rect정보를 재구성하여 계산하도록 제작
    // Rect정보는 UI 슬롯을 사각형 영역정보를 포함하면 된다.
    private Rect rc;
    public Rect RECT
    {
        get
        {
            rc.x = tr.position.x - tr.rect.width / 2;
            rc.y = tr.position.y + tr.rect.height / 2;
            return rc;
        }
    }

    // RectTransform은 UI전용 transform
    RectTransform tr;

    private void Awake()
    {
        if (option == null)
        {
            option = gameObject.AddComponent<SlotOption>();
        }
    }

    void Start()
    {
        tr = GetComponent<RectTransform>();
        // Rect를 재구성한다.
        
        rc.xMax = tr.rect.width;
        rc.yMax = tr.rect.height;
        rc.width = tr.rect.width;
        rc.height = tr.rect.height;
    }

    // 한점이 사각형영역에 들어왔는지를 검사하는 함수
    public bool IsInRect(Vector2 uiPos)
    {
        // 한점의 x좌표가 사각형영역의 x 최소값보다 크고
        // 한점의 x좌표가 사각형영역의 x 최대값보다 작다
        // 한점의 y표가 사각형영역의 y 최소값보다 크고
        // 한점의 y좌표가 사각형영역의 y 최대값보다 작다


        if (uiPos.x >= RECT.x &&
             uiPos.x <= RECT.x + RECT.width &&
             uiPos.y >= RECT.y - RECT.height &&
             uiPos.y <= RECT.y)
        {
            return true;
        }
        return false;
    }

    private float currentTime;
    private void Update()
    {
        if (isCoolTimeSlot)
        {
            cool.fillAmount -= Time.deltaTime / coolTime;
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                coolTimeText.text = "";
            }
            else
            {
                coolTimeText.text = (Mathf.Ceil(currentTime)).ToString();
            }
        }
    }

    public void ClickCoolTimeSlot()
    {
        if (cool.fillAmount != 0)
        {
            return;
        }

        cool.fillAmount = 1;
        currentTime = coolTime;
    }

}
