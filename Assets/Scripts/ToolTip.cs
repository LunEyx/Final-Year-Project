using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltip;
    public string tooltipText;

    // Start is called before the first frame update
    void Start()
    {


    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.GetComponentInChildren<Text>().text = tooltipText;
        tooltip.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
