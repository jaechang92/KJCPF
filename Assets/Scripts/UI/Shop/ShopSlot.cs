using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image image;
    public Text desc;
    public Button btn;



    void Start()
    {
        btn.onClick.AddListener(() => { BuyItem(); });
    }

    void Update()
    {
        
    }

    public void BuyItem()
    {
        Debug.Log("구매");
        for (int i = 0; i < GameManager.instance.userInvenSlotInfo.invenList.Count; i++)
        {
            if (GameManager.instance.userInvenSlotInfo.invenList[i] == "")
            {
                GameManager.instance.userInvenSlotInfo.invenList[i] = image.sprite.name;
                break;
            }

        }
        //GameManager.instance.userInvenSlotInfo.invenList.Add(image.sprite.name);
        UIManager.instance.InvenReset();
        UIManager.instance.chatText.SetText(image.sprite.name + "획득");
    }



}
