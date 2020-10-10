using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    
    void Start()
    {
        
    }


    void Update()
    {
        LookAtPlayer();
    }

    public float lookDistance = 2.0f;

    public float DISTANCETOPLAYER
    {
        get
        {
            return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
        }
    }

    void LookAtPlayer()
    {
        if (GameManager.instance.player == null)
        {
            return;
        }

        if (DISTANCETOPLAYER <= lookDistance)
        {
            GameManager.instance.ClosedPortal = this;
        }
        else if (GameManager.instance.ClosedPortal == this)
        {
            GameManager.instance.ClosedPortal = null;
        }

    }
}
