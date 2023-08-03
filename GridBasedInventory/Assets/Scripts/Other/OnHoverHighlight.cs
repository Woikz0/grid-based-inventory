using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Color HighLightColor;
    public Color PreviousColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PreviousColor = GetComponent<Image>().color;
        Highlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Dehighlight();
    }

    public void Highlight()
    {
        GetComponent<Image>().color = HighLightColor;
    }

    public void Dehighlight()
    {
        GetComponent<Image>().color = PreviousColor;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Highlight();
    }
}
