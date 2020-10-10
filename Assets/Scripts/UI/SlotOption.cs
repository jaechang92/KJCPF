using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Option
{
    public int plusHp, plusMp, plusAttack, plusDefense;
    public string itemName;
    public bool isEquip;
    public bool isSpell;
    public float coolTime;
}

public class SlotOption : MonoBehaviour
{
    public Option option = new Option();


    public void InitOption()
    {
        // 옵션 초기화

        option.plusHp = 0;
        option.plusMp = 0;
        option.plusAttack = 0;
        option.plusDefense = 0;
        option.itemName = "";
        option.isEquip = false;
        option.isSpell = false;
        option.coolTime = 0;
        
    }
}
