using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public struct SaveDataFormet
{
    int index;
    string buildName;
    string resourceName;
    Vector3 position;
    Vector3 eulerAngles;
    Vector3 scale;

    public SaveDataFormet(int _index, string _buildName, string _resourceName, Vector3 _position, Vector3 _eulerAngles, Vector3 _scale)
    {
        index = _index;
        buildName = _buildName;
        resourceName = _resourceName;
        position = _position;
        eulerAngles = _eulerAngles;
        scale = _scale;
    }
}


public class SaveAndLoadData : MonoBehaviour
{
    

    static string strFolderPath = string.Empty;
    static string strFileName = string.Empty;
    static string loadFilePath = string.Empty;
    static GameObject[] saveObjects;
    static Sprite[] saveObjects2;

    [MenuItem("Custom/SpawnTable")]
    static void SpawnTable()
    {
        
        bool check;
        saveObjects = GameObject.FindGameObjectsWithTag("SpawnPosition");
        strFileName = EditorUtility.SaveFilePanel("SaveSpawnTable", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        strFolderPath = Application.dataPath + "/"+ "Resources" + "/" + "TableData";
        if (!Directory.Exists(strFolderPath))
        {
            Directory.CreateDirectory(strFolderPath);
            Debug.Log("파일 경로 없어서 만듬");
            
        }

        if (!File.Exists(strFileName))
        {
            check = true;
        }
        else
        {
            check = false;
        }

        

        if (check)
        {
            Debug.Log("생성해");
            SaveSpawnData(FileMode.Create);
        }
        else
        {
            Debug.Log("더해");
            SaveSpawnData( FileMode.Append);
        }
        
    }



    [MenuItem("Custom/SaveItemTable")]
    static void SaveItemTable()
    {
        saveObjects2 = Resources.LoadAll<Sprite>("ItemData");
        
        strFileName = EditorUtility.SaveFilePanel("SaveItemTable", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }

        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "TableData";
        // 세이브
        if (!Directory.Exists(strFolderPath))
        {
            Directory.CreateDirectory(strFolderPath);
            Debug.Log("파일 경로 없어서 생성");
        }
        else
        {
            Debug.Log("파일 경로 있음");
        }

        string data = "index" + "," + "itemName" + "," + "plusHp" + "," + "plusMp" + "," + "plusAttack" + "," + "plusDefense" + "," + "\r\n";
        
        
        for (int i = 0; i < saveObjects2.Length; i++)
        {
            
            data += i + ",";
            data += saveObjects2[i].name + "\r\n";
        }

        WriteData(strFileName, data);
    }


    [MenuItem("Custom/SaveMonsterTable")]
    static void SaveMonsterTable()
    {
        saveObjects = Resources.LoadAll<GameObject>("MonsterData");

        strFileName = EditorUtility.SaveFilePanel("SaveMonsterTable", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }

        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "TableData";
        // 세이브
        if (!Directory.Exists(strFolderPath))
        {
            Directory.CreateDirectory(strFolderPath);
            Debug.Log("파일 경로 없어서 생성");
        }
        else
        {
            Debug.Log("파일 경로 있음");
        }

        string data = string.Empty;
        for (int i = 0; i < saveObjects.Length; i++)
        {
            data += i + ",";
            data += saveObjects[i].name + "\r\n";
        }

        WriteData(strFileName, data);
    }



    [MenuItem("Custom/SavePlayer")]
    static void SavePlayerData()
    {
        saveObjects = GameObject.FindGameObjectsWithTag("Player");
        strFileName = EditorUtility.SaveFilePanel("PlayerInfo", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "SaveData";
        SaveData();
    }

    [MenuItem("Custom/LoadPlayer")]
    static void LoadPlayerData()
    {
        loadFilePath = EditorUtility.OpenFilePanel("OpenPlayerInfo", "", "csv");
        if (string.IsNullOrEmpty(loadFilePath))
        {
            return;
        }
        GameObject obj = new GameObject();
        obj.name = "PlayerData";
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        LoadData(loadFilePath, "PlayerData/", obj.transform, "Player");
    }




    [MenuItem("Custom/SaveMonster")]
    static void SaveMonsterData()
    {

        // 1. 씬에 올려진 건물 게임오브젝트를 찾는다.

        saveObjects = GameObject.FindGameObjectsWithTag("Monster");

        // 2. 건물별로 csv파일에 저장
        strFileName = EditorUtility.SaveFilePanel("SaveMonsterInfo", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }

        // 이름을 반환 받은 것으로 파일을 생성하고 파일을 열고 파일에 작성하고 .csv파일로 저장한다.
        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "SaveData";
        //strFileName = strFolderPath + "/" + strFileName;
        Debug.Log(strFileName);
        SaveData();

        //ReadData();

    }

    [MenuItem("Custom/LoadMonster")]
    static void LoadMonsterData()
    {
        // 1. 로드할 파일을 불러온다
        loadFilePath = EditorUtility.OpenFilePanel("OpenMonsterInfo", "", "csv");
        if (string.IsNullOrEmpty(loadFilePath))
        {
            return;
        }

        GameObject obj = new GameObject();
        obj.name = "MonsterData";
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        LoadData(loadFilePath, "MonsterData/", obj.transform, "Monster");

    }





    [MenuItem("Custom/SaveNPC")]
    static void SaveNPCData()
    {

        // 1. 씬에 올려진 건물 게임오브젝트를 찾는다.

        saveObjects = GameObject.FindGameObjectsWithTag("NPC");

        // 2. 건물별로 csv파일에 저장
        strFileName = EditorUtility.SaveFilePanel("SaveNPCInfo", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }

        // 이름을 반환 받은 것으로 파일을 생성하고 파일을 열고 파일에 작성하고 .csv파일로 저장한다.
        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "SaveData";
        //strFileName = strFolderPath + "/" + strFileName;
        Debug.Log(strFileName);
        SaveData();

        //ReadData();

    }

    [MenuItem("Custom/LoadNPC")]
    static void LoadNPCData()
    {
        // 1. 로드할 파일을 불러온다
        loadFilePath = EditorUtility.OpenFilePanel("OpenNPCInfo", "", "csv");
        if (string.IsNullOrEmpty(loadFilePath))
        {
            return;
        }

        GameObject obj = new GameObject();
        obj.name = "NPCData";
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        
        LoadData(loadFilePath, "NPCData/",obj.transform, "NPC");

    }


    [MenuItem("Custom/SaveTerrain")]
    static void SaveTerrainData()
    {

        // 1. 씬에 올려진 건물 게임오브젝트를 찾는다.

        saveObjects = GameObject.FindGameObjectsWithTag("Building");
        
        // 2. 건물별로 csv파일에 저장
        strFileName = EditorUtility.SaveFilePanel("SaveTerrainInfo", "", strFileName, "csv");
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }

        // 이름을 반환 받은 것으로 파일을 생성하고 파일을 열고 파일에 작성하고 .csv파일로 저장한다.
        strFolderPath = Application.dataPath + "/" + "Resources" + "/" + "SaveData";
        //strFileName = strFolderPath + "/" + strFileName;
        Debug.Log(strFileName);
        SaveData();

        //ReadData();

    }

    [MenuItem("Custom/LoadTerrain")]
    static void LoadTerrainData()
    {
        // 1. 로드할 파일을 불러온다
        loadFilePath = EditorUtility.OpenFilePanel("OpenTerrainInfo", "", "csv");
        if (string.IsNullOrEmpty(loadFilePath))
        {
            return;
        }

        Debug.Log(loadFilePath);
        GameObject obj = new GameObject();
        obj.name = "MapData";
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        LoadData(loadFilePath, "MapData/",obj.transform,"Building");

    }

    static void LoadData(string _path, string _resourcePath,Transform parent, string tagName)
    {
        FileStream file = new FileStream(_path, FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader(file, System.Text.Encoding.UTF8);
        string line = string.Empty;
        while (!string.IsNullOrEmpty(line = streamReader.ReadLine()))
        {
            string [] eachData = line.Split(',');


            GameObject obj = Resources.Load<GameObject>(_resourcePath + eachData[2]);
            obj = Instantiate(obj,parent);
            obj.tag = tagName;
            obj.name = eachData[2];
            obj.transform.position = new Vector3(float.Parse(eachData[3]), float.Parse(eachData[4]), float.Parse(eachData[5]));
            obj.transform.eulerAngles = new Vector3(float.Parse(eachData[6]), float.Parse(eachData[7]), float.Parse(eachData[8]));
            obj.transform.localScale = new Vector3(float.Parse(eachData[9]), float.Parse(eachData[10]), float.Parse(eachData[11]));

        }
        streamReader.ReadLine();

    }


    static void SaveData()
    {
        if (!Directory.Exists(strFolderPath))
        {
            Directory.CreateDirectory(strFolderPath);
            Debug.Log("파일 경로 없어서 생성");
        }
        else
        {
            Debug.Log("파일 경로 있음");
        }

        string data = string.Empty;

        for (int i = 0; i < saveObjects.Length; i++)
        {
            data += i + ",";
            data += saveObjects[i].name + ",";
            string [] s = saveObjects[i].name.Split(' ');
            data += s[0] + ",";
            //data += bulidingObjects[i].GetComponent<MeshFilter>().name + ",";
            data += saveObjects[i].transform.position.x + ",";
            data += saveObjects[i].transform.position.y + ",";
            data += saveObjects[i].transform.position.z + ",";
            data += saveObjects[i].transform.eulerAngles.x + ",";
            data += saveObjects[i].transform.eulerAngles.y + ",";
            data += saveObjects[i].transform.eulerAngles.z + ",";
            data += saveObjects[i].transform.localScale.x + ",";
            data += saveObjects[i].transform.localScale.y + ",";
            data += saveObjects[i].transform.localScale.z + "\r\n";
        }
        
        //SaveDataFormet data = new SaveDataFormet();
        WriteData(strFileName, data);
    }

    static void SaveSpawnData(FileMode mode)
    {
        string data = string.Empty;
        int index = 0;
        
        
        if (!File.Exists(strFileName))
        {
            Debug.Log(strFolderPath);
            Debug.Log(strFileName);

            Debug.Log("파일 경로 없어서 생성");

            data = "Index" + "," + "SceneName" + "," + "posX" + "," + "posY" + "," + "posZ" + "," + "spawnMonster" + "," + "spawnCount" + "\r\n";
        }
        else
        {
            Debug.Log(">???>?");
            ReadData(data,out index);
        }
        for (int i = 0; i < saveObjects.Length; i++)
        {
            data += i + index + ",";
            string[] s = saveObjects[i].name.Split(' ');
            data += s[0] + ",";
            
            //data += bulidingObjects[i].GetComponent<MeshFilter>().name + ",";
            data += saveObjects[i].transform.position.x + ",";
            data += saveObjects[i].transform.position.y + ",";
            data += saveObjects[i].transform.position.z + ",";
            data += s[1] + ",";
            if (i == saveObjects.Length-1)
            {
                //마지막 라인
                data += s[2];
            }
            else
            {
                data += s[2] + "\r\n";
            }

        }

        //SaveDataFormet data = new SaveDataFormet();
        WriteData(strFileName, data, mode);
    }

    static void WriteData(string _filename, string _data)
    {
        FileStream f = new FileStream(_filename, FileMode.Create, FileAccess.Write);
        
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.UTF8);
        
        writer.WriteLine(_data);
        
        writer.Close();

    }

    static void WriteData(string _filename, string _data, FileMode fileMode)
    {
        FileStream f = new FileStream(_filename, fileMode, FileAccess.Write);

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.UTF8);

        writer.WriteLine(_data);

        writer.Close();

    }


    static void ReadData()
    {
        FileStream f = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
        string line = string.Empty;
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            Debug.Log(line);
        }
        reader.Close();
    }

    static void ReadData(string data,out int index)
    {
        FileStream f = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(f, System.Text.Encoding.UTF8);
        string line = string.Empty;
        index = 0;
        while (!reader.EndOfStream)
        {
            index++;
            line = reader.ReadLine();
            data += line;
            Debug.Log(line);
        }
        reader.Close();
    }

    
}
