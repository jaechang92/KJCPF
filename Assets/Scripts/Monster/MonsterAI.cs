using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    /*
     *  몬스터 생성 AI
     *      재화가 쌓이면 재화에 맞는 몬스터를 생성
     *      
     *      
     *  몬스터 애니메이션
     *      몬스터 State에맞는 애니메이션 실행
     *      
     *  몬스터 이동 AI
     *      
     *      
     *  몬스터 공격 AI
     *      코루틴을 사용하여 몬스터 사정거리에 적 몬스터가 들어왔는지 검출
     *          검출이 됬다면 State를 변경
     *          
     */

    
    public NavMeshAgent agent;
    public static CharacterInput target;
    public Vector3 originPosition;

    public float currnetTime = 0;
    public float randomTime = 2.0f;
    public int randomAreaSize = 3;

    private MonsterStats mobStats;

    private float stopDistance = 0.5f;
    public SkinnedMeshRenderer monsterSkinMesh;
    public Animator animator;
    public string thisMonsterOriginName;
    private void Awake()
    {
        if (agent == null)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = stopDistance;
            originPosition = transform.position;
        }
        animator = GetComponent<Animator>();

        if (target == null)
        {
            target = GameManager.instance.player;
        }

        monsterSkinMesh = GetComponentInChildren<SkinnedMeshRenderer>();

        thisMonsterInfo = TableManager.instance.monsterTable.FindByMonsterName(this.gameObject.name);
    }
    MonsterInfo? thisMonsterInfo;

    public float moveSpeed = 1.0f;
    public float monsterHp = 150;
    public float monsterMaxHp = 150;

    public float monsterMp;
    public float monsterdamage;
    public float monsterdefence;
    public float monstergetExp;

    private void MonsterInit()
    {
        if (thisMonsterInfo == null) return;

        // HP,MP등 여러가지 옵션을 초기화 한다.
        moveSpeed = thisMonsterInfo.Value.speed;
        monsterHp = thisMonsterInfo.Value.hp;
        monsterMaxHp = thisMonsterInfo.Value.hp;
        monsterMp = thisMonsterInfo.Value.mp;
        monsterdamage = thisMonsterInfo.Value.damage;
        monsterdefence = thisMonsterInfo.Value.defence;
        monstergetExp = thisMonsterInfo.Value.exp;
        

    }

    private void Start()
    {
        

    }


    private void Update()
    {
        //agent.destination = target.transform.position;
        //agent.Move(target.transform.position - transform.position);
        if (myTag != null)
        {
            MobInfoTagRectSet();
        }
    }

    public float attackDistance, walkDistance, runDistance, idleDistance, traceDistance = 5.0f, attackDelay = 2.0f;

    public float DISTANCETOTARGET
    {
        get
        {
            if (target == null)
            {
                target = GameManager.instance.player;
                return Vector3.Distance(target.transform.position, transform.position);
            }
            else
            {
                return Vector3.Distance(target.transform.position, transform.position);
            }
        }
    }


    public Vector3 RandomSize()
    {
        Vector3 tmp = new Vector3();
        Debug.Log("randomAreaSize =" + randomAreaSize);
        tmp.x = Random.Range(-randomAreaSize, randomAreaSize);
        tmp.z = Random.Range(-randomAreaSize, randomAreaSize);
        Debug.Log(tmp);
        return tmp;
    }


    public void DieMonster()
    {
        RemoveMonsterInfoTag();
        
        GameManager.instance.userStats.uStats.Exp += 100;
        Color color = monsterSkinMesh.material.color;
        color.a = 1;
        monsterSkinMesh.material.color = color;
        
        StartCoroutine(Co_Disappear());

    }

    IEnumerator Co_Disappear()
    {
        agent.enabled = false;
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        MonsterInit();
    }

    private void OnDisable()
    {
        
    }

    
    public void Hit(float damage)
    {
        if (monsterHp <= 0) return;

        if (myTag == null)
        {
            SetUpMonsterInfoTag();
        }

        if (monsterHp > 0)
        {
            UIManager.instance.monsterHpBarPool.PullText(damage.ToString(), this);
        }

        Debug.Log("Hit Damage = " + damage);
        monsterHp -= damage;
        myTag.hp.fillAmount = monsterHp / monsterMaxHp;
        if (monsterHp <= 0)
        {
            animator.SetTrigger("Die");
            
        }

        

    }

    public MobInfoTag myTag;
    public void SetUpMonsterInfoTag()
    {
        for (int i = 0; i < UIManager.instance.tagPool.Count; i++)
        {
            if (UIManager.instance.tagPool[i].gameObject.activeSelf == false)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.0f);
                if (pos.z < 0)
                {
                    return;
                }
                myTag = UIManager.instance.tagPool[i];
                myTag.transform.position = pos;
                myTag.gameObject.SetActive(true);
                

                switch (gameObject.name)
                {
                    case "WerewolfPBRDefault":
                        myTag.mobName.text = "웨어 울프";
                        break;

                    case "RatAssassinPBRDefault":
                        myTag.mobName.text = "생쥐 암살자";
                        break;
                    case "LizardWarriorPBRDefault":
                        myTag.mobName.text = "도마뱀 전사";
                        break;

                    default:
                        break;
                }









                myTag.nowObj = this.gameObject;
                Debug.Log(pos);
                return;
            }
        }
    }

    public void RemoveMonsterInfoTag()
    {
        if (myTag == null) return;
        myTag.gameObject.SetActive(false);
        myTag.mobName.text = "";
        myTag.hp.fillAmount = 1;
        myTag.transform.position = new Vector3(-2000, 0);
        myTag.nowObj = null;
        myTag = null;
    }


    public void MobInfoTagRectSet()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.0f);
        if (pos.z < 0)
        {
            return;
        }
        myTag.transform.position = pos;
        

        //Vector2 monsterWTSP = Camera.main.WorldToScreenPoint(transform.position);
        //if (UIManager.instance.CHDTO.IsInRect(pos))
        //{
        //    UIManager.instance.CHDTO.distance.text = Mathf.Abs(GameManager.instance.player.transform.position.z - transform.position.z).ToString();
        //}

    }


}
