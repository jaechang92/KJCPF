using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public Text text;

    private float alphaTime = 2.0f;
    private float currentTime;
    private Color originColor;
    private Color tmpColor;
    private RectTransform myRect;

    public int sortIndex = 500;
    private void Start()
    {
        originColor = text.color;
        myRect = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        tmpColor.a -= Time.deltaTime / alphaTime;
        text.color = tmpColor;
        if (Input.GetKeyDown(KeyCode.A))
        {

            SetText("input");
        }
    }

    public void SetText(string str)
    {
        myRect.SetSiblingIndex(sortIndex);
        tmpColor = originColor;
        text.text = str;
    }

}
