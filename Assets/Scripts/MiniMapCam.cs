using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{

    


    void Start()
    {
        
    }


    Vector3 nVector3;

    void Update()
    {
        CamPositionAndRotation();

    }


    void CamPositionAndRotation()
    {
        nVector3 = GameManager.instance.player.transform.position;
        nVector3.y = 20;
        transform.position = nVector3;

        nVector3 = GameManager.instance.player.transform.eulerAngles;
        nVector3.x = 90;
        nVector3.z = 0;
        transform.eulerAngles = nVector3;


    }

}
