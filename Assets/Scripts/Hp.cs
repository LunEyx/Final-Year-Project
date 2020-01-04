using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public event Action<float> On99 = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            currentHealth -= 10;
            float currentHealthPct = (float) currentHealth / (float)maxHealth;
            On99(currentHealthPct);
        }
            
    }
}
