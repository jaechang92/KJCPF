using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBarPool : MonoBehaviour
{
    public GameObject originObject;
    public int PoolSize;

    
    public List<FloatingText> textList;

    
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject tmp = Instantiate(originObject, transform);
            FloatingText obj = tmp.GetComponent<FloatingText>();

            textList.Add(obj);
            PushText(obj);
        }

        for (int i = 0; i < textList.Count; i++)
        {
            //textList[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    PullText("100", Input.mousePosition);
        //}
    }

    public void PullText(string damage,MonsterAI target)
    {
        if (textList.Count == 0)
        {
            GameObject tmp = Instantiate(originObject, transform);
            FloatingText obj = tmp.GetComponent<FloatingText>();

            textList.Add(obj);
            PushText(obj);
        }
        FloatingText nText = textList[0];
        nText.gameObject.SetActive(true);
        nText.text.text = damage;
        nText.target = target;
        

        textList.Remove(nText);
    }

    public void PushText(FloatingText obj)
    {

        if (obj.gameObject.activeSelf == true)
        {
            obj.gameObject.SetActive(false);
        }


    }

}
