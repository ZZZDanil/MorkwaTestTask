using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Camera camera;
    public GameObject endUI;
    private void Awake()
    {
        Global.game = this;
        Global.isPause = false;
    }
    private void Start()
    {
        camera.transform.position = camera.transform.position
            + new Vector3(GameSettings.levelHeight / 2, Mathf.Max(GameSettings.levelWidth, GameSettings.levelHeight), GameSettings.levelWidth / 2);
    }
    public void ShowEnd(bool isWin)
    {
        Global.isPause = true;
        endUI.SetActive(true);
        endUI.GetComponent<EndUI>().DrawResult(isWin);

    }
    private void OnDestroy()
    {
        Global.game = null;
    }
}
