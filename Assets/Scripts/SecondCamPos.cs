using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamPos : MonoBehaviour
{

    public const float m_rotation = 30;
    Vector3 distanceVector3 = Vector3.zero;

    Vector3 DISTANCEVECTOR3
    {
        get
        {
            if (distanceVector3 == Vector3.zero)
            {
                distanceVector3 = new Vector3(0, 0.7f, 3);
            }
            return distanceVector3;
        }

    }

    Vector3 newVector3;
    

    public void SecondCamPosReset(Transform targetPos)
    {
        // 타겟의 위치에서
        // distanceVector3만큼 떨어진다
        // 타겟 방향으로 돌아본다.
        //Debug.Log("세컨캠");

        newVector3 = targetPos.position;
        newVector3 += targetPos.forward * DISTANCEVECTOR3.z;
        newVector3 += targetPos.right * DISTANCEVECTOR3.x;
        newVector3 += Vector3.up * DISTANCEVECTOR3.y;
        transform.position = newVector3;
        transform.forward = -targetPos.forward;

        newVector3 = transform.eulerAngles;
        newVector3.y += m_rotation;
        transform.eulerAngles = newVector3;
        



    }
}
