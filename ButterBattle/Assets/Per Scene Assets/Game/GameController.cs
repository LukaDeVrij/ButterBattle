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
    public int enemyGold = 100;
    public GameObject enemySpawnLocation;
    public GameObject playerSpawnLocation;
    public GameObject UICanvas;
    public Text goldText;
    public Text waveText;
    public UIHandler uiHandler;
    public WaveController waveController;

    //Enemy prefab references
    public GameObject AI0;
    public GameObject AI1;
    public GameObject AI2;
    public GameObject AI3;
    public GameObject AI4;
    public GameObject AI5;
    public GameObject AI6;
    public GameObject AI7;
    public GameObject AI8;
    public GameObject AI9;

    //Player AI prefab references
    public GameObject AI0P;
    public GameObject AI1P;
    public GameObject AI2P;
    public GameObject AI3P;
    public GameObject AI4P;
    public GameObject AI5P;
    public GameObject AI6P;
    public GameObject AI7P;
    public GameObject AI8P;
    public GameObject AI9P;


    public GameObject BeaconL;
    public GameObject BeaconR;

    //For wavecontroller

    public Dictionary<int, GameObject> enemyAIMap;
    public Dictionary<int, GameObject> playerAIMap;


    // Start is called before the first frame update
    void Start()
    {
        enemyAIMap = new Dictionary<int, GameObject>()
        {
            { 0, AI0 },
            { 1, AI1 },
            { 2, AI2 },
            { 3, AI3 },
            { 4, AI4 },
            { 5, AI5 },
            { 6, AI6 },
            { 7, AI7 },
            { 8, AI8 },
            { 9, AI9 },
        };

        playerAIMap = new Dictionary<int, GameObject>()
        {
            { 0, AI0P },
            { 1, AI1P },
            { 2, AI2P },
            { 3, AI3P },
            { 4, AI4P },
            { 5, AI5P },
            { 6, AI6P },
            { 7, AI7P },
            { 8, AI8P },
            { 9, AI9P },
        };

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
            enemyGold = gold;
            waveController.callRound(this);
            
            
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
