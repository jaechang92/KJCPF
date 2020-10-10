using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /*
     *  캐릭터 무브 : Move()
     *  
     *  캐릭터 공격 : Attack()
     *      사정거리에 들어온 적 공격
     *      공격받은 적 Hit()호출
     *      
     *  캐릭터 피격 : Hit()
     *      피격시 HP -= Damange;
     *      만약 HP가 0이하가 되면 DIe() 호출
     *  캐릭터 다이 : Die()
     *      PoolingSystem의 Push()호출
     * 
     */

    [SerializeField]
    [Range(0, 10)]
    float moveSpeed = 0, rotSpeed = 0;

    Vector3 target = Vector3.zero;


    Vector3 moveDir = Vector3.zero;
    public Vector3 MOVEDIR
    {
        get
        {
            
            return moveDir;
        }
    }




    public Vector3 TARGET
    {
        get { return target; }
    }


    private void Update()
    {
        Move();
        Rotate();


    }


    Vector3 newPosition;
    bool CalculateRay()
    {
        newPosition = transform.right * TARGET.x + transform.forward * TARGET.z;
        
        Ray ray = new Ray(transform.localPosition, newPosition);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.red);

        if (Physics.Raycast(ray, out hitInfo, 2.0f))
        {

            return false;
        }
        return true;
        
    }

    void Move()
    {


        int h = 0;
        int v = 0;

        if (Input.GetKey(KeyCode.D))
        {
            h = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            h = -1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            v = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            v = -1;
        }

        target.Set(h, 0, v);

        
        if(CalculateRay())
        {
            transform.Translate(target * Time.deltaTime * moveSpeed);
        }


        //target.Set(transform.position.x + 1, transform.position.y, transform.position.z + 1);


        //transform.position = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * moveSpeed);
    }

    void Rotate()
    {

        float mouseRot = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseRot * Time.deltaTime * rotSpeed);


    }



}


