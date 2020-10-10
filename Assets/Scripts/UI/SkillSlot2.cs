using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이벤트 트리거를 사용하지 않고 터치 이벤트를 구현

// IPointerDownHandler 인터페이스 구현
// 인터페이스 이므로 반드시 구현해야 하는 함수가 존재한다. 
// (cpp에서 순수 가상함수가 포함된 클래스라고 생각)
// OnPointerDown


public class SkillSlot2 : MonoBehaviour
{
    public Image Icon;
    
    // Rect는 사각형 영역 정보를 저장하는 구조체
    // RectTransform안에 Rect정보가 있지만 UI의 구조상 부모, 자식 관계를 
    // 포함하므로 좌표계산이 다소 까다로워 Rect정보를 재구성하여 계산하도록 제작
    // Rect정보는 UI 슬롯을 사각형 영역정보를 포함하면 된다.
    private Rect rc;
    public Rect RECT
    {
        get { return rc; }
    }

    // RectTransform은 UI전용 transform
    RectTransform tr;

    void Start()
    {
        tr = GetComponent<RectTransform>();
        // Rect를 재구성한다.
        rc.x = tr.position.x - tr.rect.width / 2;
        rc.y = tr.position.y + tr.rect.height / 2;
        rc.xMax = tr.rect.width;
        rc.yMax = tr.rect.height;
        rc.width = tr.rect.width;
        rc.height = tr.rect.height;
    }

    // 한점이 사각형영역에 들어왔는지를 검사하는 함수
    public bool IsInRect( Vector2 uiPos)
    {
        // 한점의 x좌표가 사각형영역의 x 최소값보다 크고
        // 한점의 x좌표가 사각형영역의 x 최대값보다 작다
        // 한점의 y표가 사각형영역의 y 최소값보다 크고
        // 한점의 y좌표가 사각형영역의 y 최대값보다 작다

        if ( uiPos.x >= rc.x && 
             uiPos.x <= rc.x + rc.width && 
             uiPos.y >= rc.y - rc.height && 
             uiPos.y <= rc.y)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        
    }
}
