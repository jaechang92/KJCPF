using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;
    public FollowCam _mainCam;
    private SecondCamPos _secondCam;

    public FollowCam MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCam>();
            }
            return _mainCam;
        }

        set
        {
            _mainCam = value;
        }

    }
    
    public SecondCamPos SecondCam
    {
        get
        {
            if (_secondCam == null)
            {
                _secondCam = GameObject.FindGameObjectWithTag("SecondCam").GetComponent<SecondCamPos>();
            }
            return _secondCam;
        }

        set
        {
            _secondCam = value;
        }


    }

    Camera test;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Init();



    }

    /*
     *  카메라 범위제한(맵밖을 비추지 않게)
     *  카메라 확대축소
     *  카메라 이동
     *  
     */


    void Init()
    {

        _mainCam = MainCam;
        _secondCam = SecondCam;
        _secondCam.gameObject.SetActive(false);
        if (GameManager.instance.player == null)
        {
            return;
        }
        
        SetCamParent(_mainCam.transform, GameManager.instance.player.transform);
        SetCamParent(_secondCam.transform, this.transform);


        _secondCam.gameObject.SetActive(false);
    }


    void SetCamParent(Transform target,Transform parent)
    {
        target.parent = parent;
    }





    

}
