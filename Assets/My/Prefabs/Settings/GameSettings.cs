using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    [Range(5, 20)]
    public static int levelWidth = 10;
    [Range(5, 20)]
    public static int levelHeight = 10;
    [Range(0, 5)]
    public static int enemies = 2;
    [Range(1.0f, 10.0f)]
    public static float playerSpeed = 5.0f;
    [Range(1.0f, 10.0f)]
    public static float enemiesSpeed = 5.0f;
    [Range(0.0f, 10.0f)]
    public static float noiseIncreaseInSec = 3.0f;
    [Range(0.0f, 10.0f)]
    public static float noiseReductionInSec = 2.0f;
    [Range(0.0f, 100.0f)]
    public static float maxNoise= 10.0f;
    [Range(0.0f, 80.0f)]
    public static float blocksCountInPercent = 30.0f;

}
