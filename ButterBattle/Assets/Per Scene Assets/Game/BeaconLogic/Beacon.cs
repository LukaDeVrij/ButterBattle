using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{

    public int startHP = 1000;
    public int HP = 1000;

    public HealthBarScript healthBarScript;


    // Start is called before the first frame update
    void Start()
    {
        healthBarScript.SetMaxHealth(startHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHPDisplay()
    {
        healthBarScript.SetHealth(HP);
    }
}
