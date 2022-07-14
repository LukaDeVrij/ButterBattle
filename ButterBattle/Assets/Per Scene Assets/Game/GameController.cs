using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int currentRound = 1;
    public float timeLeftUntilNextRound = 30.0f;
    public int gold = 100;
    public int goldPerRoundStart = 100;
    public GameObject enemySpawnLocation;
    public GameObject UICanvas;
    public Text goldText;
    public Text waveText;
    public UIHandler uiHandler;
    public WaveController waveController;

    //Enemy prefab references
    public GameObject AI0;
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
