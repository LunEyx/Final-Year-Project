using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_bar : MonoBehaviour
{
    private Image hp;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Hp>().On99 += ChangeToPct;
    }

    private void ChangeToPct(float pct)
    {
        hp.rectTransform.sizeDelta = new Vector2(90 * pct, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
