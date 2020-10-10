using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    public GameObject spriteF;

    public float DISTANCETOPLAYER
    {
        get
        {
            return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
        }
    }

    private void Start()
    {
        spriteF.SetActive(false);
    }


    void Update()
    {
        // 거리가 1.0 이하로 가까워진다면
        if (DISTANCETOPLAYER <= 1.0f)
        {
            GameManager.instance.ClosedItem = this;
            spriteF.SetActive(true);
        }
        else if(DISTANCETOPLAYER > 1.0f && GameManager.instance.ClosedItem == this)
        {
            GameManager.instance.ClosedItem = null;
        }


        if (DISTANCETOPLAYER > 1.0f)
        {
            spriteF.SetActive(false);
        }
    }
}
