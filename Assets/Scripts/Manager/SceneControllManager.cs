using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SceneControllManager : MonoBehaviour
{
    public static SceneControllManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        nowScene = SceneManager.GetActiveScene().name;
    }

    public string nowScene;

    TextAsset txtAsset;
    public float loadingGauge = 0;
    float currentWaitTime = 0;

    public bool TestMode;

    public string FirstLoadScene;
    private void Start()
    {
        StartCoroutine(WaitLoadingScene(FirstLoadScene));
        if (TestMode)
        {
            return;
        }
    }


    public IEnumerator CreateScene(string sceneName)
    {
        loadingGauge = 0;

        if (TestMode)
        {
            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "Player");
            if (txtAsset != null)
            {
                StartCoroutine(LoadObejct(txtAsset.text, "PlayerData"));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "NPC");
            if (txtAsset != null)
            {
                StartCoroutine(LoadObejct(txtAsset.text, "NPCData"));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "Monster");
            if (txtAsset != null)
            {
                StartCoroutine(LoadObejct(txtAsset.text, "MonsterData"));
            }

            txtAsset = Resources.Load<TextAsset>("TableData/" + "SpawnTable");
            if (txtAsset != null)
            {
                StartCoroutine(LoadSpawn(txtAsset.text, "MonsterData", sceneName));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName);
            if (txtAsset != null)
            {
                StartCoroutine(LoadObejct(txtAsset.text, "MapData"));
            }

        }
        else
        {

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "Player");
            if (txtAsset != null)
            {
                yield return StartCoroutine(LoadObejct(txtAsset.text, "PlayerData"));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "NPC");
            if (txtAsset != null)
            {
                yield return StartCoroutine(LoadObejct(txtAsset.text, "NPCData"));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName + "Monster");
            if (txtAsset != null)
            {
                yield return StartCoroutine(LoadObejct(txtAsset.text, "MonsterData"));
            }
            txtAsset = Resources.Load<TextAsset>("TableData/" + "SpawnTable");
            if (txtAsset != null)
            {
                yield return StartCoroutine(LoadSpawn(txtAsset.text, "MonsterData", sceneName));
            }

            txtAsset = Resources.Load<TextAsset>("SaveData/" + sceneName);
            if (txtAsset != null)
            {
                yield return StartCoroutine(LoadObejct(txtAsset.text, "MapData"));
            }
        }
        
        yield return null;
        GameManager.instance.player.CanControl = true;
    }

    IEnumerator LoadSpawn(string data, string resourcePath,string nowScene)
    {
        loadingGauge = 0;

        
        
        Debug.Log("몬스터풀시스템");
        char[] chMark = { '\r', '\n' };
        string[] strInfo = data.Split(chMark);
        for (int i = 1; i < strInfo.Length; i++)
        {
            loadingGauge += 1f / strInfo.Length;
            GameManager.instance.SetLoadingText(loadingGauge);
            //Debug.Log(loadingGauge);
            if (strInfo[i] != string.Empty)
            {
                string[] strTmp = strInfo[i].Split(',');
                if (strTmp[1] != nowScene)
                {

                }
                else
                {
                    GameObject obj = new GameObject();
                    obj.name = strTmp[1] + strTmp[5] + "Pool";
                    obj.transform.position = new Vector3(float.Parse(strTmp[2]), float.Parse(strTmp[3]), float.Parse(strTmp[4]));
                    MonsterPoolingSystem MPSystem = obj.AddComponent<MonsterPoolingSystem>();
                    
                    MPSystem.monsterPrefab = Resources.Load<GameObject>(resourcePath + "/" + strTmp[5]);
                    MPSystem.maxCount = int.Parse(strTmp[6]);
                }
            }
            currentWaitTime += Time.deltaTime;
            if (!TestMode)
            {
                yield return null;
            }
        }
    }

    //
    IEnumerator LoadObejct(string data, string resourcePath)
    {

        
        loadingGauge = 0;
        GameObject obj = new GameObject();
        obj.name = resourcePath;

        char[] chMark = { '\r', '\n' };
        string[] strInfo = data.Split(chMark);
        for (int i = 0; i < strInfo.Length; i++)
        {
            loadingGauge += 1f / strInfo.Length;
            GameManager.instance.SetLoadingText(loadingGauge);

            if (strInfo[i] != string.Empty)
            {

                string[] strTmp = strInfo[i].Split(',');

                Vector3 position = new Vector3(float.Parse(strTmp[3]), float.Parse(strTmp[4]), float.Parse(strTmp[5]));
                
                GameObject resource = Resources.Load<GameObject>(resourcePath +"/" + strTmp[2]);

                GameObject instanceObj = Instantiate(resource, position, Quaternion.identity, obj.transform);
                instanceObj.name = strTmp[2];

                instanceObj.transform.eulerAngles = new Vector3(float.Parse(strTmp[6]), float.Parse(strTmp[7]), float.Parse(strTmp[8]));
                instanceObj.transform.localScale = new Vector3(float.Parse(strTmp[9]), float.Parse(strTmp[10]), float.Parse(strTmp[11]));

                if (strTmp[1] == "BossDragonRed")
                {
                    GameManager.instance.boss = instanceObj;
                    Monster monsterScript;
                    if (instanceObj.GetComponent<Monster>() == null)
                    {
                        monsterScript = instanceObj.AddComponent<Monster>();
                    }
                    else
                    {
                        monsterScript = instanceObj.GetComponent<Monster>();
                    }
                    
                    GameManager.instance.thisSceneMonsterList.Add(monsterScript);
                }

                switch (resourcePath)
                {
                    case "MonsterData":
                        // 몬스터 폴링 시스템을 생성해준다.


                        //Monster mob = instanceObj.AddComponent<Monster>();

                        //GameManager.instance.thisSceneMonsterList.Add(mob);
                        


                        break;
                    case "PlayerData":
                        CharacterInput player = GetComponent<CharacterInput>();
                        
                        GameManager.instance.player = player;
                        break;
                    case "NPCData":
                        NPCController NPC = instanceObj.GetComponent<NPCController>();
                        if (NPC == null)
                        {
                            NPC = instanceObj.AddComponent<NPCController>();
                        }
                        
                        if (NPC.name == "Smith")
                        {
                            NPC.isShop = true;
                        }

                        GameManager.instance.NPCInit(NPC);
                        
                        break;
                    default:
                        break;
                }

            }
            currentWaitTime += Time.deltaTime;
            if (!TestMode)
            {
                yield return null;
            }
        }
            
    }

    AsyncOperation asyncLoad;
    public void NextScene(string sceneName)
    {
        StartCoroutine(WaitLoadingScene(sceneName));
    }

    IEnumerator WaitLoadingScene(string sceneName)
    {
        //yield return new WaitForSeconds(1.5f);
        GameManager.instance.LoadingCanvas.gameObject.SetActive(true);
        if (GameManager.instance.player != null)
        {
            GameManager.instance.player.isLoadingComplete = false;
        }

        currentWaitTime = 0;
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        

        
        yield return StartCoroutine(CreateScene(sceneName));

        Debug.Log(currentWaitTime);
        if (currentWaitTime < 1.5f)
        {
            Debug.Log(currentWaitTime);
            Debug.Log(3.0f - currentWaitTime);
            yield return GameManager.instance.MinimumWaitTime(1.5f - currentWaitTime);
        }

        
        nowScene = SceneManager.GetActiveScene().name;
        UIManager.instance.InitScene(nowScene);


        //CamManager.instance.MainCam.SetTarget();

        yield return new WaitForSeconds(0.3f);
        GameManager.instance.LoadingCanvas.gameObject.SetActive(false);
        GameManager.instance.DataLoadingAll();
        
        

    }





}
