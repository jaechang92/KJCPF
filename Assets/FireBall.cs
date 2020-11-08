using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : EffectController
{
    public float speed;
    float step;
    public float AttackDist = 5.0f;

    private Vector3 tPos;
    private bool lockOn = false;
    private void Start()
    {
        transform.position = GameManager.instance.player.transform.position;

        if (GameManager.instance.rayHitTarget != null && AttackDist > (int)Vector3.Distance(GameManager.instance.player.transform.position, GameManager.instance.rayHitTarget.transform.position))
        {
            tPos = GameManager.instance.rayHitTarget.transform.position;
            lockOn = true;
        }



        OnDestroy();
    }


    private void Update()
    {
        step = speed * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, groundPos, fallSpeed * Time.deltaTime);

        // 락온되있고 어텍사정거리보다 가까이 있을경우 스킬의 도탁위치를 설정
        if (lockOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, tPos, step);
        }
        else
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetPos * 5.0f, step);
        }

    }


    private void OnDestroy()
    {
        if (true)
        {

        }
        Destroy(this.gameObject, 3.0f);
    }

}
