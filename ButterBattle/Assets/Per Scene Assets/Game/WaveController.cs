using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public static void callRound()
    {
        // TODO: Instantiate enemies based on AI on field
        // For now: just instantiate x enemies per round based on gold amount

        //System for choosing which troops to call and amount of enemy troops 
        //
        enemyWave();
        playerWave();

    }

    static void enemyWave()
    {
        int iterations = 0;
        while (iterations < 100)
        {


        }
    }

    static void playerWave()
    {

    }

    IEnumerator spawnTroop(int troopType, int cost, int currentGold)
    {
        if (currentGold - cost < 0)
        {
            GameObject AI1clone = Instantiate(AI0, enemySpawnLocation.transform.position, Quaternion.identity);


            AI1clone.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Minimap");
            AI1clone.transform.position = AI1clone.transform.position + new Vector3(Random.Range(-1f, 0f), Random.Range(-0.5f, 0.5f), 0);

            currentGold -= cost;
        }




        yield return new WaitForSeconds(1f);
    }
}
