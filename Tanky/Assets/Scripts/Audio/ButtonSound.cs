﻿
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonSound: MonoBehaviour ,IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag.Equals("Button"))
            MusicManager.instance.Play("Menu_move");
    }
    
}
