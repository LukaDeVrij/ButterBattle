using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public Text waveText;
    public Text goldText;
    public TextMeshProUGUI waveCountdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateGameInfo(int wave, int gold)
    {
        waveText.text = wave.ToString();
        goldText.text = gold.ToString();

    }
    public void updateWaveCountdown(float timeUntilNextWave)
    {
        waveCountdownText.text = Mathf.FloorToInt(timeUntilNextWave).ToString();
    }
}
