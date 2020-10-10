using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonster : MonoBehaviour
{
    Animator animator;
    public MonsterAI mobAI;
    public Transform Tongue;
    void BossInit()
    {
        gameObject.AddComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.enabled = true;
        mobAI = gameObject.AddComponent<MonsterAI>();
    }

    void Start()
    {
        BossInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
