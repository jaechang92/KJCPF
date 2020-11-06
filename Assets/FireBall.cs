using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    float step;

    private void Update()
    {
        step = speed * Time.deltaTime;
        Vector3.MoveTowards(transform.position, transform.position + transform.forward * 5.0f, step);
    }


    private void OnDestroy()
    {
        Destroy(this.gameObject, 3.0f);
    }

}
