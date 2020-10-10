using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobInfoTag : MonoBehaviour
{
    public Text mobName;
    public Image hp;

    private RectTransform rt;

    public GameObject nowObj;
    public float width;

    public void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        WidthCalculationUsingFieldOfViewAndDistance();
        
    }


    public float tmp;
    public Vector3 fromTo;
    private void WidthCalculationUsingFieldOfViewAndDistance()
    {
        float dist = Vector3.Distance(nowObj.transform.position, GameManager.instance.player.transform.position);
        if (dist > 10)
        {
            width = 100 - (dist - 10);
        }
        else if(dist < 10)
        {
            width = 100;
        }
        SetWidth(Mathf.Clamp(width, 50, 100));


    }

    private void SetWidth(float width)
    {
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }

    


}
