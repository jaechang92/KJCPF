using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     * 캐릭터를 만들때 사용하는 최상위 부모 
     * 
     * 캐릭터의 체력, 공격력, 방어력, 공격딜레이, 공격 타입(단일 or 광역), 사정거리, 이동속도, 드랍골드량
     * 
     */


[System.Serializable]
public struct UserStats
{
    [SerializeField]
    private int _lv, _gold;
    [SerializeField]
    private float _MaxHp, _hp,_MaxMp, _mp, _attack, _defense, _exp, _maxExp;
    [SerializeField]
    private string _equipHead, _equipArmor, _equipBoots, _equipWeapon;

    public int Lv { get { return _lv; } set { _lv = value; } }
    public float AttackDistance;
    public float MaxHp { get { return _MaxHp; } set { _MaxHp = value; } }
    public float Hp { get { return _hp; } set { if (value <= 0) { _hp = 0; } else if (value >= _MaxHp) { _hp = _MaxHp; } else { _hp = value; } } }
    public float MaxMp { get { return _MaxMp; } set { _MaxMp = value; } }
    public float Mp { get { return _mp; } set { if (value <= 0) { _mp = 0; } else if (value >= _MaxMp) { _mp = _MaxMp; } else { _mp = value; } } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }

    public float Exp { get { return _exp; } set { _exp = value; } }
    public float MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public int Gold { get { return _gold = TableManager.instance.userTable.userInfo.gold; } set { UIManager.instance.goldText.text = value.ToString() + " Gold"; _gold = value; } }
    public string EquipHead { get { return _equipHead = TableManager.instance.userTable.userInfo.EquipHead; } set { _equipHead = value; } }
    public string EquipArmor { get { return _equipArmor = TableManager.instance.userTable.userInfo.EquipArmor; } set { _equipArmor = value; } }
    public string EquipBoots { get { return _equipBoots = TableManager.instance.userTable.userInfo.EquipBoots; } set { _equipBoots = value; } }
    public string EquipWeapon { get { return _equipWeapon = TableManager.instance.userTable.userInfo.EquipWeapon; } set { _equipWeapon = value; } }
}


public class Stats : MonoBehaviour
{
    
    public UserStats uStats = new UserStats();

    private void Start()
    {
        uStats.Lv = TableManager.instance.RetrunUserTable().userInfo.lv;
        uStats.MaxHp = TableManager.instance.RetrunUserTable().userInfo.hp;
        uStats.MaxMp = TableManager.instance.RetrunUserTable().userInfo.mp;
        uStats.Hp = TableManager.instance.RetrunUserTable().userInfo.hp;
        uStats.Mp = TableManager.instance.RetrunUserTable().userInfo.mp;
        uStats.Attack = TableManager.instance.RetrunUserTable().userInfo.attack;
        uStats.Defense = TableManager.instance.RetrunUserTable().userInfo.defense;
        uStats.Exp = TableManager.instance.RetrunUserTable().userInfo.exp;
        uStats.MaxExp = TableManager.instance.RetrunUserTable().userInfo.maxExp;
        uStats.Gold = TableManager.instance.RetrunUserTable().userInfo.gold;
        uStats.EquipHead = TableManager.instance.RetrunUserTable().userInfo.EquipHead;
        uStats.EquipArmor = TableManager.instance.RetrunUserTable().userInfo.EquipArmor;
        uStats.EquipBoots = TableManager.instance.RetrunUserTable().userInfo.EquipBoots;
        uStats.EquipWeapon = TableManager.instance.RetrunUserTable().userInfo.EquipWeapon;


        uStats.MaxExp = TableManager.instance.userTable.userInfo.maxExp;

        Debug.Log("---Stats---");
        Debug.Log(TableManager.instance.RetrunUserTable().userInfo.hp);
        Debug.Log(uStats.MaxHp);
        Debug.Log(uStats.Hp);
        Debug.Log("---Stats---");

    }

    private void Update()
    {
        if (uStats.Hp <= 0)
        {
            UIManager.instance.InitList[12].SetActive(true);
        }
    }


    public void PlusExp(int point)
    {
        Debug.Log(point);
        uStats.Exp += point;

        if (uStats.Exp == uStats.MaxExp)
        {
            uStats.Lv++;
            uStats.Exp = 0;
            uStats.MaxExp = uStats.MaxExp * 3;
            UIManager.instance.lv.text = uStats.Lv.ToString();
            GameManager.instance.LvUpEffect();
        }
    }

    

}
