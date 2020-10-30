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
        mobAI = gameObject.AddComponent<MonsterAI>();
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
