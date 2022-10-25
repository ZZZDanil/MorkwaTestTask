using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Noise : MonoBehaviour
{
    enum NOISE_STATUS {STOP, INCREASE, REDUCE }
    public TextMeshProUGUI value;
    private float noise = 0;
    private NOISE_STATUS noiseStatus = NOISE_STATUS.STOP;
    private bool isPlayerHuntingTurnOn = false;
    private Transform playerTransform;
    private void Awake()
    {
        playerTransform = transform.parent;
    }

    private void Update()
    {
        if (Global.isPause == false)
        {
            //TODO call evety n mills
            if (isPlayerHuntingTurnOn == false)
            {
                if (noiseStatus != NOISE_STATUS.STOP)
                {
                    if (noiseStatus == NOISE_STATUS.INCREASE)
                    {
                        noise += GameSettings.noiseIncreaseInSec * Time.deltaTime;
                    }
                    else if (noiseStatus == NOISE_STATUS.REDUCE)
                    {
                        noise -= GameSettings.noiseIncreaseInSec * Time.deltaTime;
                        if (noise < 0)
                        {
                            noise = 0;
                            noiseStatus = NOISE_STATUS.STOP;
                        }
                    }
                    if (noise >= GameSettings.maxNoise)
                    {
                        isPlayerHuntingTurnOn = true;

                    }
                }
                value.text = ((int)noise).ToString();
            }
            else
            {
                CallEnemies();
            }
        }
    }
    public void StartIncreaseNoise()
    {
        if(noiseStatus != NOISE_STATUS.INCREASE)
            noiseStatus = NOISE_STATUS.INCREASE;
    }
    public void StopIncreaseNoise()
    {
        if(noiseStatus != NOISE_STATUS.STOP)
            noiseStatus = NOISE_STATUS.REDUCE;
    }
    private void CallEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 playerPos = playerTransform.position;
        foreach (GameObject enemie in enemies)
        {
            enemie.GetComponent<Enemy>().GoToPlayer(playerPos);
        }
    }
}
