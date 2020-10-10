using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCheckpoint : MonoBehaviour
{
    // target은 player
    private Transform target;
    private MeshRenderer mr;

    public iTweenCamMove itc;
    public Vector3 maxVector3;
    public Vector3 minVector3;
    public Transform TeleportPosition;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameManager.instance.player.transform;
        mr = GetComponent<MeshRenderer>();
        itc = Camera.main.GetComponent<iTweenCamMove>();

        Debug.Log(mr.bounds.max + " = 최대");
        Debug.Log(mr.bounds.min + " = 최소");
        maxVector3 = mr.bounds.max;
        minVector3 = mr.bounds.min;

    }

    // Update is called once per frame
    void Update()
    {
        CheckInPlayer();
    }


    //GameManager.instance.player.transform.position
    //float GameManager.instance.player.transform.position;
    private void CheckInPlayer()
    {
        if (GameManager.instance.player != null)
        {
            if (minVector3.x < GameManager.instance.player.transform.position.x && minVector3.y < GameManager.instance.player.transform.position.y && minVector3.z < GameManager.instance.player.transform.position.z
                && maxVector3.x > GameManager.instance.player.transform.position.x && maxVector3.y > GameManager.instance.player.transform.position.y && maxVector3.z > GameManager.instance.player.transform.position.z)
            {
                Teleport();
                itc.ITweenMove();
            }
        }
    }


    private void Teleport()
    {
        Debug.Log("Teleport");
        GameManager.instance.player.agent.enabled = false;
        GameManager.instance.player.transform.position = TeleportPosition.position;
        GameManager.instance.player.agent.enabled = true;
        
    }


}
