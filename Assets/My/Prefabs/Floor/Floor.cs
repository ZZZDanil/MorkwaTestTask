using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public enum FLOOR_TYPE { BASIC, PLAYER_START, PLAYER_FINISH }
    public Material basic;
    public Material playerStart;
    public Material playerEnd;
    [HideInInspector]
    public FLOOR_TYPE floorType = FLOOR_TYPE.BASIC;
    public void UpdateFloor(FLOOR_TYPE floorType)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        switch (floorType)
        {
            case FLOOR_TYPE.BASIC:
                break;
            case FLOOR_TYPE.PLAYER_START:
                meshRenderer.material = playerStart;
                break;
            case FLOOR_TYPE.PLAYER_FINISH:
                meshRenderer.material = playerEnd;
                break;
        }
    }
}
