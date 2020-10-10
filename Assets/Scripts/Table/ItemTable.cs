using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아이템의 정보
 * 아이템은 게임스피트X2, 스테이지 종료후 획득골드 X2, 특수보상 획득확률 X2
 * 
 */

[System.Serializable]
public struct ItemInfo
{
    public int index;
    public int plusHp, plusMp, plusAttack, plusDefense;
    public string itemName;
    public Sprite itemSprite;
    public bool isEquip;
    public bool isSpell;
    public float coolTime;

    public ItemInfo(string _index, string _itemName, string _plusHp, string _plusMp, string _plusAttack, string _plusDefense, Sprite _itemSprite,bool _isEquip, bool _isSpell, string _coolTime)
    {
        index = int.Parse(_index);
        itemName = _itemName;
        plusHp = int.Parse(_plusHp);
        plusMp = int.Parse(_plusMp);
        plusAttack = int.Parse(_plusAttack);
        plusDefense = int.Parse(_plusDefense);
        itemSprite = _itemSprite;
        isEquip = _isEquip;
        isSpell = _isSpell;
        coolTime = float.Parse(_coolTime);
    }
}


public class ItemTable
{
    [SerializeField]
    public List<ItemInfo> itemList = new List<ItemInfo>();



    public void LoadTable(string strName)
    {
        char[] chMark = { '\r', '\n' };
        string[] strInfo = strName.Split(chMark);
        for (int i = 1; i < strInfo.Length; i++)
        {
            if (strInfo[i] != string.Empty)
            {

                string[] strTmp = strInfo[i].Split(',');

                ItemInfo tmp = new ItemInfo(strTmp[0], strTmp[1], strTmp[2], strTmp[3], strTmp[4], strTmp[5], Resources.Load<Sprite>("IconData/" + strTmp[1]), strTmp[1].Contains("Equip_"), strTmp[1].Contains("Spell_"),strTmp[6]);

                itemList.Add(tmp);
            }
        }
    }


    public ItemInfo FindItem(string name)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == name)
            {
                return itemList[i];
            }
        }
        return new ItemInfo();
    }

    
    public string RandomItem()
    {
        int ri = Random.Range(0, itemList.Count);
        return itemList[ri].itemName;
    }
    
}
