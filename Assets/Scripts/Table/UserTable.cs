using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유저의 정보
 * 현재 스테이지 진행도
 * 골드, 유저의 레벨
 * 
 */

public struct UserInfo
{
    public int lv;
    public float hp;
    public float mp;
    public float attack;
    public float defense;
    public float exp;
    public float maxExp;
    public int gold;
    public string EquipHead;
    public string EquipArmor;
    public string EquipBoots;
    public string EquipWeapon;

    public UserInfo(string _lv, string _hp, string _mp, string _attack, string _defense, string _exp, string _maxExp, string _gold, string _equipHead, string _equipArmor, string _equipBoots, string _equipWeapon)
    {
        lv = int.Parse(_lv);
        hp = float.Parse(_hp);
        mp = float.Parse(_mp);
        attack = float.Parse(_attack);
        defense = float.Parse(_defense);
        exp = float.Parse(_exp);
        maxExp = float.Parse(_maxExp);
        gold = int.Parse(_gold);
        EquipHead = _equipHead;
        EquipArmor = _equipArmor;
        EquipBoots = _equipBoots;
        EquipWeapon = _equipWeapon;
    }


}


public class UserTable
{
    public UserInfo userInfo;


    public void LoadTable(string strName)
    {
        char[] chMark = { '\r', '\n' };
        string[] strInfo = strName.Split(chMark);
        for (int i = 1; i < strInfo.Length; i++)
        {
            if (strInfo[i] != string.Empty)
            {

                string[] strTmp = strInfo[i].Split(',');

                userInfo = new UserInfo(strTmp[0], strTmp[1], strTmp[2], strTmp[3], strTmp[4], strTmp[5], strTmp[6], strTmp[7], strTmp[8], strTmp[9], strTmp[10], strTmp[11]);
                

                
            }
        }

    }


}
