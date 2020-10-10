using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    Transform target;
    public SpriteRenderer spriteRenderer;
    public Sprite exclamationMark;
    public Sprite questionMark;

    public bool isRight;

    void Start()
    {
        if (isRight)
        {
            Vector3 newVector3 = transform.localScale;
            newVector3.x = -newVector3.x;
            transform.localScale = newVector3;
        }
    }

    public float newY = 0;
    Vector3 newV3;
    void Update()
    {
        //target = Camera.main.transform;
        target = GameManager.instance.player.transform;

        //transform.LookAt(target);
        transform.LookAt(Camera.main.transform.position);
        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        
    }

}
