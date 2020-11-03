using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;


[Serializable]
public struct QuestInfo
{
    public int index;
    public string questName;
    public string questMobName;
    public string questMobModelname;
    public int questMobCount;
    public string npcName;
    public string description;
    public string mainDescription;
    public string reward;
    public bool checkClear;
    public string func;

    string DESCRIPTION
    {
        set
        {
            string[] strTmp = value.Split(' ');
            string strNew = string.Empty;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (strTmp[i] == "mobName")
                {
                    strNew += questMobName;
                }
                else if (strTmp[i] == "mobCount")
                {
                    strNew += questMobCount.ToString();
                }
                else
                {
                    strNew += strTmp[i];
                    strNew += " ";
                }

            }
            description = strNew;
        }

    }

    string DESCRIPTION2
    {
        set
        {
            string[] strTmp = value.Split(' ');
            string strNew = string.Empty;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (strTmp[i] == "mobName")
                {
                    strNew += questMobName;
                }
                else if (strTmp[i] == "mobCount")
                {
                    strNew += questMobCount.ToString();
                }
                else
                {
                    strNew += strTmp[i];
                    strNew += " ";
                }

            }
            mainDescription = strNew;
        }

    }


    public QuestInfo(string _index, string _questName, string _questMobName, string _questMobModelname, string _questMobCount, string _npcName, string _description, string _mainDescription,string _reward, string _checkClear,string _func)
    {
        this.index = int.Parse(_index);
        this.questName = _questName;
        this.questMobName = _questMobName;
        this.questMobModelname = _questMobModelname;
        this.questMobCount = int.Parse(_questMobCount);
        this.npcName = _npcName;
        this.description = string.Empty;
        this.mainDescription = string.Empty;
        this.reward = _reward;
        this.checkClear = false;
        if(_checkClear == "FALSE")
        {
            checkClear = false;
        }
        else if (_checkClear == "TRUE")
        {
            checkClear = true;
        }
        this.func = _func;

        DESCRIPTION = _description;
        DESCRIPTION2 = _mainDescription;
    }
}


public class QuestTable
{
    public List<QuestInfo> questList = new List<QuestInfo>();

    // 퀘스트 이름으로 받아서 퀘스트 정보를 리턴
    //Dictionary<int, QuestInfo> keyValuePairs = new Dictionary<int, QuestInfo>();
    //public Dictionary<string, Dictionary<int, QuestInfo>> useNameAndIndexDictionary = new Dictionary<string, Dictionary<int, QuestInfo>>();
    int ii = 0;

    public void LoadTable(string strName)
    {
        char[] chMark = { '\r', '\n' };
        string[] strInfo = strName.Split(chMark);
        string tmpKey = string.Empty;
        
        for (int i = 1; i < strInfo.Length; i++)
        {

            if (strInfo[i] != string.Empty)
            {
                string[] strTmp = strInfo[i].Split(',');

                QuestInfo tmp = new QuestInfo(strTmp[0], strTmp[1], strTmp[2], strTmp[3], strTmp[4], strTmp[5], strTmp[6], strTmp[7], strTmp[8], strTmp[9],strTmp[10]);

                questList.Add(tmp);
                //keyValuePairs.Add(tmp.index, tmp);

                //if (tmpKey == string.Empty)
                //{
                //    tmpKey = tmp.npcName;
                //}


                //if (tmpKey != tmp.npcName)
                //{
                //    Debug.Log(ii);
                //    ii++;
                //    useNameAndIndexDictionary.Add(tmpKey, keyValuePairs);
                //    tmpKey = tmp.npcName;
                //}
            }
        }
        //useNameAndIndexDictionary.Add(tmpKey, keyValuePairs);
    }
    

    public void Find(string questName)
    {
        
    }
}
