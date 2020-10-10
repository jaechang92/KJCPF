using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairDistanceToObject : MonoBehaviour
{
    public Text distance;


    public Vector2 UIWorldPoint;

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

    
    void Update()
    {
        CalculateDistanceToObject();
        v2 = transform.position;
    }


    public void CalculateDistanceToObject()
    {
        UIWorldPoint = Camera.main.ScreenToWorldPoint(transform.position);
    }

    public Vector2 v2;
    public bool IsInRect(Vector2 uiPos)
    {
        // 한점의 x좌표가 사각형영역의 x 최소값보다 크고
        // 한점의 x좌표가 사각형영역의 x 최대값보다 작다
        // 한점의 y표가 사각형영역의 y 최소값보다 크고
        // 한점의 y좌표가 사각형영역의 y 최대값보다 작다

        if (uiPos.x >= rc.x &&
             uiPos.x <= rc.x + rc.width &&
             uiPos.y >= rc.y - rc.height &&
             uiPos.y <= rc.y)
        {
            return true;
        }
        return false;
    }

    //public bool IsInRect(Vector2 uiPosMin, Vector2 uiPosMax)
    //{
    //    // 한점의 x좌표가 사각형영역의 x 최소값보다 크고
    //    // 한점의 x좌표가 사각형영역의 x 최대값보다 작다
    //    // 한점의 y표가 사각형영역의 y 최소값보다 크고
    //    // 한점의 y좌표가 사각형영역의 y 최대값보다 작다

    //    if (uiPosMin.x >= rc.x && uiPosMin.y <= rc.y - rc.height && uiPosMin.x <= rc.x + rc.width && uiPosMin.y >= rc.y)
    //    {
    //        Debug.Log("물체가 크로스헤드 안에 있음");
    //        return true;
    //    }
    //    else if (uiPosMax.x >= rc.x && uiPosMax.y <= rc.y - rc.height && uiPosMax.x <= rc.x + rc.width && uiPosMax.y >= rc.y)
    //    {
    //        Debug.Log("물체가 크로스헤드 안에 있음");
    //        return true;
    //    }
    //    Debug.Log("물체가 크로스헤드 안에 없음");
    //    return false;
        
    //}




}
