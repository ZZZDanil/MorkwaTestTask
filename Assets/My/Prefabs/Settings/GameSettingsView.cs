using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsView : MonoBehaviour
{
    [Range(5, 20)]
    public int levelWidth = 10;
    [Range(5, 20)]
    public int levelHeight = 10;
    [Range(0, 5)]
    public int enemies = 2;
    [Range(1.0f, 10.0f)]
    public float playerSpeed = 5.0f;
    [Range(1.0f, 10.0f)]
    public float enemiesSpeed = 5.0f;
    [Range(0.0f, 10.0f)]
    public float noiseIncreaseInSec = 3.0f;
    [Range(0.0f, 10.0f)]
    public float noiseReductionInSec = 2.0f;
    [Range(0.0f, 100.0f)]
    public float maxNoise = 10.0f;
    [Range(0.0f, 80.0f)]
    public float blocksCountInPercent = 30.0f;

    private void Awake()
    {

        GameSettings.levelWidth = levelWidth;
        GameSettings.levelHeight = levelHeight;
        GameSettings.enemies = enemies;
        GameSettings.playerSpeed = playerSpeed;
        GameSettings.enemiesSpeed = enemiesSpeed;
        GameSettings.noiseIncreaseInSec = noiseIncreaseInSec;
        GameSettings.noiseReductionInSec = noiseReductionInSec;
        GameSettings.maxNoise = maxNoise;
        GameSettings.blocksCountInPercent = blocksCountInPercent;
}
}
