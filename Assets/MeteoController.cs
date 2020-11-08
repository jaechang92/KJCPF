using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoController : EffectController
{

    public GameObject effect;

    //public Vector3 groundPos;
    public float fallSpeed;

    public float durationTime;
    private float currentTime;
    void Start()
    {
        transform.position = transform.position + Vector3.up * 30 + Vector3.right * 25;
    }

    public bool check = false;
    void Update()
    {
        if (check)
        {
            currentTime += Time.deltaTime;
        }

        //groundPos = GameManager.instance.player.transform.position + player.transform.position + player.transform.forward * 5;
        transform.position = Vector3.MoveTowards(transform.position, groundPos,fallSpeed * Time.deltaTime);
        // 땅에 충돌
        if (check == false  &&transform.position == groundPos)
        {
            effect.SetActive(true);
            check = true;
            OperationAroundMob();
        }

        if (currentTime >= durationTime)
        {
            gameObject.SetActive(false);
        }


    }

    //public void SetGroundPos()
    //{
    //    groundPos.x = GameManager.instance.player.transform.position.x + GameManager.instance.player.transform.forward.x * 20;
    //    groundPos.z = GameManager.instance.player.transform.position.z + GameManager.instance.player.transform.forward.z * 20;
    //    groundPos.y = GameManager.instance.player.transform.position.y;
    //}

    private void OnDisable()
    {
        InitEffect();
    }

    private void InitEffect()
    {
        currentTime = 0;
        effect.SetActive(false);
        check = false;
        this.gameObject.transform.position = Vector3.up * 50;
    }

    public void OperationAroundMob()
    {
        for (int i = 0; i < GameManager.instance.thisSceneMonsterList.Count; i++)
        {
            if (Vector3.Distance(transform.position, GameManager.instance.thisSceneMonsterList[i].transform.position) < 10.0f)
            {
                GameManager.instance.thisSceneMonsterList[i].mobAI.Hit(200);
            }
        }
         
    }


}
