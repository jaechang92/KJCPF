using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolingSystem : MonoBehaviour
{
    public List<Monster> monsterList;
    public int maxCount;
    public GameObject monsterPrefab;

    void Start()
    {
        monsterList = new List<Monster>();
        

        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = Instantiate(monsterPrefab);
            obj.name = monsterPrefab.name;
            Monster mob = obj.AddComponent<Monster>();
            mob.mobAI.thisMonsterOriginName = monsterPrefab.name;
            mob.transform.SetParent(transform);
            mob.parentMonsterPoolingSystem = this;
            obj.transform.position = new Vector3(0,0,0);
            Debug.Log(mob);
            
            mob.gameObject.SetActive(false);
            //monsterList.Add(mob);
        }
        StartCoroutine(ReSpawnMonster(3.0f));
    }

    IEnumerator ReSpawnMonster(float reSpawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(reSpawnTime);
            if (monsterList.Count > 0)
            {
                Pull();
            }
        }
    }


    public void Pull()
    {
        Monster tmp = monsterList[0];
        tmp.transform.position = new Vector3(transform.position.x - Random.Range(-5, 6), 0, transform.position.z - Random.Range(-5, 6));
        tmp.mobAI.agent.enabled = true;
        tmp.gameObject.SetActive(true);
        monsterList.Remove(tmp);
        GameManager.instance.thisSceneMonsterList.Add(tmp);

    }

    public void Push(Monster obj)
    {
        monsterList.Add(obj);
        
        GameManager.instance.thisSceneMonsterList.Remove(obj);
        GameManager.instance.nowTraceMonsterList.Remove(obj.mobAI);
    }

}
