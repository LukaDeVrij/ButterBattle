using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int currentRound = 1;
    public float timeLeftUntilNextRound = 30.0f;
    int gold = 0;
    public int goldPerRoundStart = 100;
    public GameObject enemySpawnLocation;
    public GameObject UICanvas;
    public Text goldText;

    //Enemy prefab references
    public GameObject AI1;
    


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
            gold += goldPerRoundStart;
            callRound();
            timeLeftUntilNextRound = 30f; //Reset round timer
            
        }
    }
    public void callRound()
    {
        
        Debug.Log("Current round:" + currentRound);
        Debug.Log(gold);
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
                AI1clone.transform.position = AI1clone.transform.position + new Vector3(Random.value, Random.value, 0);
                gold -= AI1clone.GetComponent<EnemyAI>().cost;
                cost = AI1clone.GetComponent<EnemyAI>().cost;
                goldText.text = "Gold: " + gold;
                Debug.Log(gold);

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
}
