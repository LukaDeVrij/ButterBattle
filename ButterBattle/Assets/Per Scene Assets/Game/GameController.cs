using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int currentRound = 1;
    public float timeLeftUntilNextRound = 30.0f;
    public int gold = 0;
    public int goldPerRoundStart = 100;
    public GameObject enemySpawnLocation;
    public GameObject UICanvas;
    public Text goldText;
    public Text waveText;
    public UIHandler uiHandler;

    //Enemy prefab references
    public GameObject AI1;
    public GameObject BeaconL;
    public GameObject BeaconR;
    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
        checkForNextRound();
        
        //UI UPDATERS
        uiHandler.updateGameInfo(currentRound, gold);
        uiHandler.updateWaveCountdown(timeLeftUntilNextRound);
    }

    public void checkForNextRound()
    {
        timeLeftUntilNextRound -= Time.deltaTime;
        if (timeLeftUntilNextRound < 0)
        {
            currentRound++;
            gold += goldPerRoundStart;
            timeLeftUntilNextRound = 30f; //Reset round timer
            callRound();
            
            
        }
    }
    public void callRound()
    {
        // TODO: Instantiate enemies based on AI on field
        // For now: just instantiate x enemies per round based on gold amount
        float nextTroopSpawnTime = 0f;
        float troopSpawnInterval = 5f;
       
        int cost = 0;

        while (gold - cost >= 0)
        {
            //if (Time.time >= nextTroopSpawnTime)
            //{
                
                GameObject AI1clone = Instantiate(AI1, enemySpawnLocation.transform.position, Quaternion.identity);
                AI1clone.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Minimap");
                AI1clone.transform.position = AI1clone.transform.position + new Vector3(Random.Range(-1f,0f), Random.Range(-0.5f, 0.5f), 0);
                gold -= AI1clone.GetComponent<EnemyAI>().cost;
                cost = AI1clone.GetComponent<EnemyAI>().cost;
                

                nextTroopSpawnTime = Time.time + troopSpawnInterval;

            //}
            //else if(Time.time <= nextTroopSpawnTime)
            //{
            //    Debug.Log("Waiting for next troop spawn...");
            //}
            //else
            //{
            //    return;
            //}
        }
    }

    public void WinLossCheck()
    {
        if (BeaconL.GetComponent<Beacon>().HP <= 0)
        {
            Debug.Log("Right wins");
        }
        if (BeaconR.GetComponent<Beacon>().HP <= 0)
        {
            Debug.Log("Left wins");
        }
    }
}
