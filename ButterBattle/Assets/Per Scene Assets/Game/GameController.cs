using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int currentRound = 1;
    public float timeLeftUntilNextRound = 30.0f;
    public int gold = 0;
    public AIController AIcontroller;
    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        checkForNextRound();

    }

    public void checkForNextRound()
    {
        timeLeftUntilNextRound -= Time.deltaTime;
        if (timeLeftUntilNextRound < 0)
        {
            currentRound++;
            gold += 100;
            callRound();
            timeLeftUntilNextRound = 30f; //Reset round timer
            
        }
    }
    public void callRound()
    {
        Debug.Log("Current round:" + currentRound);
        // TODO: Instantiate enemies based on AI on field
        // For now: just instantiate x enemies per round based on gold amount
        for (int i = 0; i < gold; i+= 10)
        {

            

            gold -= 10;
        }
        

    }
}
