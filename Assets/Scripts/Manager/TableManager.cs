using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        txtAsset = Resources.Load<TextAsset>("TableData/MonsterTable");
        monsterTable.LoadTable(txtAsset.text);

        txtAsset = Resources.Load<TextAsset>("TableData/QuestTable");
        questTable.LoadTable(txtAsset.text);

        txtAsset = Resources.Load<TextAsset>("TableData/ItemTableAll");
        itemTable.LoadTable(txtAsset.text);

        //txtAsset = Resources.Load<TextAsset>("TableData/EquipTable");
        //equipTable.LoadTable(txtAsset.text);
        //txtAsset = Resources.Load<TextAsset>("TableData/OtherTable");
        //otherTable.LoadTable(txtAsset.text);
        //txtAsset = Resources.Load<TextAsset>("TableData/SpellTable");
        //spellTable.LoadTable(txtAsset.text);


        txtAsset = Resources.Load<TextAsset>("TableData/UserTable");
        userTable.LoadTable(txtAsset.text);
        

    }
    
    public MonsterTable monsterTable = new MonsterTable();
    public QuestTable questTable = new QuestTable();
    public SpawnTable spawnTable = new SpawnTable();
    public ItemTable itemTable = new ItemTable();
    //public ItemTable equipTable = new ItemTable();
    //public ItemTable otherTable = new ItemTable();
    //public ItemTable spellTable = new ItemTable();
    public UserTable userTable = new UserTable();
    TextAsset txtAsset;

    void Start()
    {
        
        
        //Debug.Log(userTable.userInfo.lv);
        //Debug.Log(userTable.userInfo.hp);

        //foreach (var item in itemTable.itemList)
        //{
        //    Debug.Log(item.coolTime);
        //}

        //questTable.useNameAndIndexDictionary[""][1]
        //Debug.Log(questTable.keyValuePairs["StartQuest"]);

    }

    void Update()
    {
        
    }


    public UserTable RetrunUserTable()
    {
        return userTable;
    }

    public ItemInfo FindItemTable(string name)
    {
        for (int i = 0; i < itemTable.itemList.Count; i++)
        {
            if (itemTable.itemList[i].itemName == name)
            {
                return itemTable.itemList[i];
            }
        }
        return new ItemInfo();
    }
}
