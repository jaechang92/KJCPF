using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterInfo
{
    public int index;
    public string monsterName;
    public string modelName;
    public int hp;
    public int mp;
    public int speed;
    public int damage;
    public int defence;
    public int exp;

    public MonsterInfo(string index, string monsterName, string modelName, string hp,string mp, string speed, string damage, string defence, string exp)
    {
        this.index = int.Parse(index);
        this.monsterName = monsterName;
        this.modelName = modelName;
        this.hp = int.Parse(hp);
        this.mp = int.Parse(mp);
        this.speed = int.Parse(speed);
        this.damage = int.Parse(damage);
        this.defence = int.Parse(defence);
        this.exp = int.Parse(exp);
    }
}

public class MonsterTable
{
    
    public List<MonsterInfo> monsterList = new List<MonsterInfo>();

    public void LoadTable(string strName)
    {
        char[] chMark = { '\r', '\n' };
        string[] strInfo = strName.Split(chMark);
        for (int i = 1; i < strInfo.Length; i++)
        {
            if (strInfo[i] != string.Empty)
            {
                //Debug.Log(strInfo[i]);

                string[] strTmp = strInfo[i].Split(',');

                MonsterInfo tmp = new MonsterInfo(strTmp[0], strTmp[1], strTmp[2], strTmp[3], strTmp[4], strTmp[5], strTmp[6], strTmp[7], strTmp[8]);

                monsterList.Add(tmp);
            }
        }
    }

    public MonsterInfo? FindByMonsterName(string name)
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (name == monsterList[i].monsterName)
            {
                return monsterList[i];
            }
        }
        return null;
    }


}
