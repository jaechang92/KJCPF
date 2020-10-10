using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragTitle : MonoBehaviour, IDragHandler, IBeginDragHandler,IPointerUpHandler
{
    Vector3 startPosition;

    
    Transform originParent;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(transform.parent.name);
        originParent = transform.parent;
        transform.parent = transform.parent.parent;
        startPosition = Input.mousePosition - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.parent = originParent;
        transform.position = Input.mousePosition - startPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
