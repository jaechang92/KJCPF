using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceControl : MonoBehaviour
{
    public Animator ani;
    public List<GameObject> BossSceneMobList;

    public bool checkNull = false;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ani.Play("OpenFence");
        //    Debug.Log("마우스클릭");
        //}

        if (checkNull == false)
        {
            CheckMob();
        }

        if(checkNull == true && Vector3.Distance(GameManager.instance.player.transform.position,this.transform.position) <= 5.0f && count == 0)
        {
            OnceTrigger();
        }
        
    }


    private void CheckMob()
    {
        for (int i = 0; i < BossSceneMobList.Count; i++)
        {
            if (BossSceneMobList[i].activeSelf == true)
            {
                return;
            }
        }
        checkNull = true;
    }


    int count = 0;
    private void OnceTrigger()
    {

        // 실행내용
        ani.Play("OpenFence");
        count++;

    }


}
