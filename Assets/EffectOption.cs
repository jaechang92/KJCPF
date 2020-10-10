using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOption : MonoBehaviour
{
    public float durationTime;


    // Start is called before the first frame update
    void Start()
    {
        DestroyObj();
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject, durationTime);
    }

}
