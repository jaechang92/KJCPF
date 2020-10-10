using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;



    /*
     * 유저 몬스터풀, 적 몬스터풀
     * User EnabledPool, Enemy EnabledPool, DisabledPool
     * EnabledPool의 최대 저장 가능한 오브젝트 수 40개
     * 
     * EnabledPool
     *      리스트로 구현
     *      몬스터가 활성화 되있는동안 가지고있는 풀
     *      몬스터가 죽으면 Push 함수 호출
     *      몬스터를 DisabledPool에 반환
     *      
     *      스테이지가 종료되면 모든 몬스터를 DisabledPool에 반환
     * 
     * 
     * 구현 방법(DisabledPool)
     *      리스트로 구현
     *      각 몬스터당 기본 생성 갯수 10개씩 지정
     *      몬스터가 생성될때 필요한 Pool에서 Pull 함수 호출
     *      만약 Pull의 Count가 0이면 Instantiate호출
     *      새로 만들어진 오브젝트를 반환
     *      
     * Push()
     * Pull()
     * 
     */

    public List<Vector3> enemySpawnPosition;
    
    public List<GameObject> itemPool;
    
    public GameObject itemPrefab;
    private GameObject itemObejctPool;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        itemObejctPool = new GameObject();
        itemObejctPool.transform.parent = this.gameObject.transform;
        itemObejctPool.name = "itemObejctPool";

    }
    private void Start()
    {
        
    }


    public void Push(GameObject pushObj)
    {
        // List.Push(pushObj)
        itemPool.Add(pushObj);
        
    }

    public GameObject Pull(Vector3 callPosition)
    {
        if (itemPool.Count == 0)
        {
            GameObject obj = Instantiate(itemPrefab, itemObejctPool.transform);
            Push(obj);
            obj.AddComponent<ItemOption>();
        }

        // List.Pull(pullObj)
        itemPool[0].SetActive(true);
        itemPool[0].transform.position = callPosition;
        return itemPool[0];

    }


    //// 몬스터가 죽으면 아이템을 생성하고 생성된 아이템에서 이것을 불러온다.
    //public void GetItemOption()
    //{
    //    thisItemInfo = TableManager.instance.equipTable.itemList[Random.Range(0, TableManager.instance.equipTable.itemList.Count)];
    //    image.sprite = thisItemInfo.itemSprite;
    //    //thisItemInfo = table;
    //}

    //public void GetItemOption(ItemInfo info)
    //{
    //    thisItemInfo = new ItemInfo(info.index.ToString(), info.itemName, info.plusHp.ToString(), info.plusMp.ToString(), info.plusAttack.ToString(), info.plusDefense.ToString(), info.itemSprite, info.isEquip, info.isSpell);
    //}

    public void EnemySpawnPositionSet(string sceneName)
    {
        enemySpawnPosition = new List<Vector3>();

        //TableManager.
        //enemySpawnPosition.Add();
    }

}
