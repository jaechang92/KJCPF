using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text text;
    public float upSpeed;
    private float alphaTime = 2.0f;
    private float currentTime;
    public Color originColor;
    public MonsterAI target;
    private Color tmpColor;
    private RectTransform myRect;
    private Rect rt;
    public int sortIndex = 500;
    private void Start()
    {
        text = GetComponent<Text>();
        originColor = text.color;
        
        myRect = GetComponent<RectTransform>();
        tmpColor = originColor;
    }
    float upPoint = 0;
    void Update()
    {
        //nText.gameObject.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

        tmpColor.a -= Time.deltaTime / alphaTime;
        text.color = tmpColor;
        Vector3 tmp = Camera.main.WorldToScreenPoint(target.monsterSkinMesh.bounds.extents.y * Vector3.up + target.transform.position);
        upPoint += Time.deltaTime * upSpeed;
        tmp.y += upPoint;
        transform.position = tmp;

        if (tmpColor.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
        text.enabled = true;
    }

    private void OnEnable()
    {
        
        tmpColor = originColor;
        text.color = originColor;
        upPoint = 0;
    }


    private void OnDisable()
    {
        target = null;
        text.enabled = false;
        UIManager.instance.monsterHpBarPool.PushText(this);
    }

}
