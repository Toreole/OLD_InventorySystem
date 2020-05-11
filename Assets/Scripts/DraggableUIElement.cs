using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableUIElement : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector3 offset;
    public RectTransform target;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector3) eventData.position - target.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        target.position = (Vector3) eventData.position - offset;
    }
}