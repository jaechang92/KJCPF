using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    /*
     * 리소스를 로드하고 관리
     * 
     */

    public static ResourceManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }









}
