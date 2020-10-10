using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenCamMove : MonoBehaviour
{
    private iTweenCamMove itm;
    public bool startCheck = false;
    public Transform lookTarget;

    private void Start()
    {
        itm = this;
        
        //"looktarget", lookTarget,
    }


    public void ITweenMove()
    {
        GameManager.instance.player.isTalk = true;
        startCheck = true;
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "easeType", "linear", "oncomplete", "Complete", "speed", 30f));
        
        StartCoroutine(Shake(10.0f, 0.5f));
    }


    private void LateUpdate()
    {
        if (startCheck == true)
        {
        
            Camera.main.transform.LookAt(lookTarget);
            //CamShake();
        }
    }


    private void Complete()
    {
        startCheck = false;
        Debug.Log("END");
        GameManager.instance.mainCam.CamDistHeight(GameManager.instance.player.transform);

        GameManager.instance.player.isTalk = false;
        itm.enabled = false;
    }

    private Vector3 shakeVector2Max = new Vector3(1,1);
    private Vector3 shakeVector2Min = new Vector3(-1,-1);
    int index = 0;
    float currentTime = 0;
    float delayTime = 0.2f;
    private void CamShake()
    {
        currentTime += Time.deltaTime;
        if (currentTime > delayTime)
        {
            

            switch (index)
            {
                case 0:
                    gameObject.transform.position += shakeVector2Max;
                    index = 1;
                    break;
                case 1:
                    gameObject.transform.position += shakeVector2Min;
                    index = 0;
                    break;
                default:
                    break;
            }
            currentTime = 0;
        }

    }

    IEnumerator Shake(float duration,float magnitude)
    {
        
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(Camera.main.transform.localPosition.x + x, Camera.main.transform.localPosition.y + y, Camera.main.transform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //transform.localPosition = originalPos;

    }


}
