using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public Vector3 groundPos;
    public Vector3 targetPos;

    public void SetPos()
    {
        SetGroundPos();
        SetTargetForward();
    }

    public void SetGroundPos()
    {
        groundPos.x = GameManager.instance.player.transform.position.x + GameManager.instance.player.transform.forward.x * 20;
        groundPos.z = GameManager.instance.player.transform.position.z + GameManager.instance.player.transform.forward.z * 20;
        groundPos.y = GameManager.instance.player.transform.position.y;
    }

    public void SetTargetForward()
    {
        targetPos.x = GameManager.instance.player.transform.position.x + GameManager.instance.player.transform.forward.x;
        targetPos.z = GameManager.instance.player.transform.position.z + GameManager.instance.player.transform.forward.z;
        targetPos.y  = GameManager.instance.player.transform.position.y;
    }





}
