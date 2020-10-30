using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    MonsterInfo mobInfo;
    Animator animator;
    public MonsterAI mobAI;
    public MonsterPoolingSystem parentMonsterPoolingSystem;
    private void Awake()
    {
        gameObject.AddComponent<NavMeshAgent>();
        if (GetComponent<MonsterAI>() == null)
        {
            mobAI = gameObject.AddComponent<MonsterAI>();
        }
        else
        {
            mobAI = GetComponent<MonsterAI>();
        }
        animator = GetComponent<Animator>();
        animator.enabled = true;
    }

    void Start()
    {
    }

    
    void Update()
    {
        
    }

    

    private void OnDisable()
    {
        if (parentMonsterPoolingSystem == null)
        {
            return;
        }
        parentMonsterPoolingSystem.Push(this);
        
    }

    
}
