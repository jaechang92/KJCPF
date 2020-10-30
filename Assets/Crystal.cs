using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    
    void Update()
    {
        ClosedCrystal();
    }

    private void ClosedCrystal()
    {
        if (GameManager.instance.player == null)
        {
            return;
        }

        if (DISTANCETOPLAYER <= 3.0f)
        {
            GameManager.instance.ClosedCrystal = this;
        }
        else if (GameManager.instance.ClosedCrystal == this)
        {
            GameManager.instance.ClosedCrystal = null;
        }


    }

    public float DISTANCETOPLAYER
    {
        get
        {
            return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
        }
    }

    public void Action()
    {
        // 플레이어의 모든 HP MP를 회복하고 공격력 20%상승 버프를 부여합니다.
        Debug.Log("버프부여");

        UIManager.instance.chatText.SetText("플레이어의 모든 HP MP를 회복하고 공격력 20%상승 버프를 부여합니다.");
        UIManager.instance.userInfo.uStats.Hp = GameManager.instance.userStats.uStats.MaxHp;
        UIManager.instance.userInfo.uStats.Mp = GameManager.instance.userStats.uStats.MaxMp;
        UIManager.instance.userInfo.uStats.Attack = GameManager.instance.userStats.uStats.Attack * 1.2f;
        UIManager.instance.SetHpMpUI();



    }
}
