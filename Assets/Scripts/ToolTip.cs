using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject toolTip;

    // Start is called before the first frame update
    void Start()
    {
        toolTip.SetActive(false);
    }

   
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }
}
