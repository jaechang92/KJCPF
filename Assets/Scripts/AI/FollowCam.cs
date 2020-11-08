using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Camera cam;
    public float height;
    
    public float distanceZ;
    public float distanceX;

    public float dampSpeed;
    
    public float camRotSpeed;
    public Transform target;
    public bool test;
    public Vector3 offset;
    float offsetY = 5;
    float addValue;
    public float tmp;

    public Vector3 targetPos;
    void InitCam()
    {
        //offset.x = 0.15f;
        //offset.y = offsetY;
        //offset.z = 8;
        //tmp = offset.z;
        GameManager.instance.mainCam = this;
        cam = GetComponent<Camera>();
        height = 3;
        distanceZ = 5;
        camRotSpeed = 50;
        dampSpeed = 10;

        StartCoroutine(RayCastCoroutine());
    }

    


    void Start()
    {
        InitCam();
    }


    private void OnEnable()
    {
        
        
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.player != null && !GameManager.instance.player.isTalk && GameManager.instance.player.CanControl || test)
        {
           
            ZoomCamera();
        }
        
    }

    public Vector3 lookAtPosition;
    private void LateUpdate()
    {
        if (target != null && GameManager.instance != null && GameManager.instance.player != null && !GameManager.instance.player.isTalk && GameManager.instance.player.CanControl || test)
        {
        }

        if (GameManager.instance.player == null) return;
        if (GameManager.instance.player.isTalk != false || !GameManager.instance.player.CanControl)
        {
            return;
        }
            CamRotate();
        
        //transform.LookAt(nTr);
    }

    void ZoomCamera()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cam.fieldOfView += 10;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cam.fieldOfView -= 10;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView,30,70);

    }

    public void CamDistHeight(Transform target)
    {
        //transform.eulerAngles = new Vector3(18.77f,1.718f,0);
        transform.position = target.position - target.forward * distanceZ + target.right * distanceX + Vector3.up * height;

        transform.LookAt(target.position + Vector3.up * 1.3f + Vector3.right * 0.15f);
    }
    
    Vector3 angle = Vector3.zero;
    public void CamRotate()
    {
        float h;
        float v;
        h = Input.GetAxis("Mouse X");
        v = Input.GetAxis("Mouse Y");

        h = h * Time.deltaTime * camRotSpeed;
        v = v * Time.deltaTime * camRotSpeed;

        angle.x = Mathf.Clamp(angle.x - v, -80, 40);
        angle.y += h;
        
        
        GameManager.instance.player.camTarget.rotation = Quaternion.Euler(angle);
    }
    
    public float rayMaxDistance = 100;
    int disTmp;
    public IEnumerator RayCastCoroutine()
    {
        Debug.Log("IN");
        WaitForEndOfFrame WFEF = new WaitForEndOfFrame();
        while (true)
        {
            Ray ray = new Ray(transform.position,transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo,rayMaxDistance))
            {
                disTmp = (int)Vector3.Distance(GameManager.instance.player.transform.position, hitInfo.point);
                UIManager.instance.CHDTO.distance.text = disTmp.ToString() + "m";
                GameManager.instance.rayHitTarget = hitInfo.collider.gameObject;
                //Debug.Log(hitInfo.collider.gameObject.name);
                //Debug.Log(Vector3.Distance(GameManager.instance.player.transform.position, hitInfo.point));
            }
            else
            {
                UIManager.instance.CHDTO.distance.text = "";
                GameManager.instance.rayHitTarget = null;
            }

            yield return WFEF;
        }
    }

   
}
