using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOption : MonoBehaviour
{
    private void OnDisable()
    {
        PoolingSystem.instance.Push(gameObject);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
