using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceControl : MonoBehaviour
{

    public List<GameObject> BossSceneMobList;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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

    }


}
