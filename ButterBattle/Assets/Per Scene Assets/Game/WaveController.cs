using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    int iterations = 0;
    int playerGold;
    int enemyGold;

    public void syncGold(GameController gameController)
    {
        playerGold = gameController.gold;
        enemyGold = gameController.enemyGold;
    }

    public void callRound(GameController gameController)
    {
        // TODO: Instantiate enemies based on AI on field
        // For now: just instantiate x enemies per round based on gold amount

        //System for choosing which troops to call and amount of enemy troops 

        syncGold(gameController);
        enemyWave(gameController.enemyAIMap, gameController.enemySpawnLocation);
        playerWave();
        

    }

    void enemyWave(Dictionary<int, GameObject> AImap, GameObject spawnLoc)
    {
        
        int troopToBeSpawned = Random.Range(0, 10);

        StartCoroutine(spawnEnemyTroop(troopToBeSpawned, TroopProperties.costOf[troopToBeSpawned], AImap[troopToBeSpawned], spawnLoc));

    }

    static void playerWave()
    {
        int troopSelected = TroopProperties.troopSelected;
        //Do stuff

    }
  

    IEnumerator spawnEnemyTroop(int troopType, int cost, GameObject AI, GameObject spawnLocation)
    {
        while (iterations < 100)
        {
            Debug.Log(enemyGold - cost);
            if (enemyGold - cost > 0)
            {

                GameObject AI1clone = Instantiate(AI, spawnLocation.transform.position, Quaternion.identity);


                AI1clone.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Minimap");
                AI1clone.transform.position = AI1clone.transform.position + new Vector3(Random.Range(-1f, 0f), Random.Range(-0.5f, 0.5f), 0);

                enemyGold -= cost;
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                StopAllCoroutines();
            }
            iterations++;
        }
        
    }
}
