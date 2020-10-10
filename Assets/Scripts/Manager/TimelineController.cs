using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
    public static TimelineController instance;

    public PlayableDirector PD;
    TimelineCheck tc;

    public List<GameObject> objList;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        InitPD();
        for (int i = 0; i < objList.Count; i++)
        {
            if(objList[i].activeSelf)
            {
                objList[i].SetActive(false);
            }
        }
    }


    private void InitPD()
    {
        PD = GetComponent<PlayableDirector>();

    }



    public void StartTimeline(TimelineCheck check)
    {

        GameManager.instance.player.isTalk = true;
        tc = check;
        GameManager.instance.player.model.gameObject.SetActive(false);
        GameManager.instance.boss.SetActive(false);
        PD.Play();
    }

    public void EndTimeline()
    {
        GameManager.instance.player.isTalk = false;
        GameManager.instance.player.model.gameObject.SetActive(true);

        GameManager.instance.boss.SetActive(true);
        Debug.Log("???????????");
        tc.TeleportEnd();
        gameObject.SetActive(false);
        UIManager.instance.CHDTO.gameObject.SetActive(true);

    }

    


}
