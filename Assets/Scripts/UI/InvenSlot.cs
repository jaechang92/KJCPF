using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct InvenOption
{
    public string slotName;
    public string slotType;
    public string plusHp;
    public string plusMp;

}

public class InvenSlot : MonoBehaviour
{
    public InvenOption option = new InvenOption();
    
    
}
