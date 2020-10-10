using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterInput : MonoBehaviour
{
    public Vector3 MOVEDIR { get; set; } = Vector3.zero;
    public Vector3 ANGLETODIRECTION
    {
        get
        {
            return transform.right * MOVEDIR.x + transform.forward * MOVEDIR.z;
        }
    }

    public Vector3 ROTANGLETODIRECTION
    {
        get
        {
            return transform.right * mouseInputY + transform.up * mouseInputX;
        }
    }




    public Transform model;
    private float moveSpeed = 5,rotSpeed = 80, gravity = -9.8f;

    public float mouseInputX;
    public float mouseInputY;
    Animator ani;
    public bool isAttack = false;
    public bool isTalk = false;
    public bool isLoadingComplete = false;
    public TrailRenderer trail;
    public Transform camTarget;
    public NavMeshAgent agent;
    public SkinnedMeshRenderer skinnedMesh;
    private void Awake()
    {
        model = GameObject.Find("Model").GetComponent<Transform>();

        GameManager.instance.mainCam.transform.SetParent(camTarget.transform);
        GameManager.instance.mainCam.transform.localEulerAngles = Vector3.zero;
        GameManager.instance.mainCam.transform.position = new Vector3(GameManager.instance.mainCam.distanceX, GameManager.instance.mainCam.height, -GameManager.instance.mainCam.distanceZ);
        skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (GameManager.instance.player == null)
        {
            GameManager.instance.player = this;
        }

        if (trail == null)
        {
            trail = GetComponentInChildren<TrailRenderer>();
        }

        trail.enabled = false;
        GameManager.instance.mainCam.CamDistHeight(transform);
        

    }

    public bool CanControl;
    void Update()
    {
        if (CanControl)
        {
            UserInput();
        }
    }



    void UserInput()
    {
        if (!isTalk && isLoadingComplete)
        {
            if (!isAttack)
            {
                MoveInput();
                Move();
                Rotate();
            }

            PressKey();
        }

        TalkNPCkey();


    }



    float h,v;

    void MoveInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");


        MOVEDIR = new Vector3(h, 0, v);
        if (MOVEDIR != Vector3.zero)
        {
            Vector3 newForward = camTarget.forward;

            newForward.y = 0;
            camTarget.transform.localEulerAngles = new Vector3(camTarget.transform.eulerAngles.x, 0, 0);
            transform.forward = newForward;
        }

    }

    void Move()
    {
        transform.Translate(MOVEDIR * Time.deltaTime * moveSpeed);
        //agent.Move(MOVEDIR  * Time.deltaTime * moveSpeed);
    }


    void Rotate()
    {
        if (ANGLETODIRECTION != Vector3.zero)
        {
            Vector3 newDir = Vector3.RotateTowards(model.forward, ANGLETODIRECTION, Time.deltaTime * 10.0f, 0.0f);
            model.rotation = Quaternion.LookRotation(newDir);
        }

    }

void PressKey()
    {
        // UI가 아닐때
        if (Input.GetMouseButtonDown(0) )
        {

            ani.SetTrigger("Attack");

        }

        if (Input.GetKeyDown(KeyCode.X) && GameManager.instance.ClosedPortal)
        {
            UIManager.instance.Portal();
        }

        if (Input.GetKeyDown(KeyCode.F) && GameManager.instance.ClosedItem)
        {
            GameManager.instance.GetItem();
        }

    }


    public void TalkNPCkey()
    {
        if (Input.GetKeyDown(KeyCode.X) && GameManager.instance.ClosedNPC)
        {
            Debug.Log("npc와 대화");
            isTalk = true;
            if (GameManager.instance.ClosedNPC.isShop)
            {
                // 상점
                UIManager.instance.OpenUI();
            }
            else
            {
                Debug.Log("대화");
                UIManager.instance.PlayerAndNpcTalkStart();
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && GameManager.instance.Interaction)
        {
            Debug.Log("상호작용");
            GameManager.instance.Interaction.StartInteraction();
        }

    }

    public void PressSkillKey()
    {

    }



}
