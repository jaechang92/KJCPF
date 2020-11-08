using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMan : MonoBehaviour
{
    public SkinnedMeshRenderer smRmderer;
    public Material myMaterial;

    private Color origin;
    private Color cColor;

    public float targetTo = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        smRmderer = GetComponentInChildren<SkinnedMeshRenderer>();

        myMaterial = smRmderer.material;
        
        cColor = new Color(1, 0.684f, 0.684f);
        origin = myMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        dist();



    }

    private void dist()
    {
        if (Vector3.Distance(GameManager.instance.player.transform.position, this.gameObject.transform.position) < targetTo)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HitEffect();
            }

            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    HitEffect();
            //}

        }
    }

    private void HitEffect()
    {
        StartCoroutine(Co_ChageColor());
    }

    IEnumerator Co_ChageColor()
    {
        yield return new WaitForSeconds(0.15f);
        myMaterial.color = cColor;
        yield return new WaitForSeconds(0.25f);
        myMaterial.color = origin;

        if (GameManager.instance.userInvenSlotInfo.questList[0].questName == "기본공격을 알려주지")
        {
            QuestInfo q = GameManager.instance.userInvenSlotInfo.questList[0];
            q.checkClear = true;
            GameManager.instance.userInvenSlotInfo.questList[0] = q;
            //GameManager.instance.userInvenSlotInfo.QuestClear(GameManager.instance.userInvenSlotInfo.questList[0]);
        }

    }



}
