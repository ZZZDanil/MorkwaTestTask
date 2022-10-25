using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using System.Linq;
public class Level : MonoBehaviour
{
    public GameObject block;
    public GameObject floor;
    public GameObject floorPlayerStart;
    public GameObject floorPlayerFinish;
    public GameObject player;
    public GameObject enemy;

    private void Start()
    {
        GenerateLevel(GameSettings.levelWidth, GameSettings.levelHeight);

        GetComponent<NavMeshSurface>().BuildNavMesh();

    }

    private void GenerateLevel(int width, int height)
    {
        Dictionary<int, int> cells = new Dictionary<int, int>(width*height);
        for (int i = 0; i < width * height; i++)
        {
            cells.Add(i,i);
        }

        (Vector3Int playerStartPos, Vector3Int playerEndPos) = FindPlayer(ref cells, width, height);
        InitPlayer(playerStartPos, playerEndPos);

        for (int i = 0; i < GameSettings.enemies; i++)
        {
            (Vector3Int enemyStartPos, Vector3Int enemyEndPos)  = FindEnemy(ref cells, width, height);
            InitEnemy(enemyStartPos, enemyEndPos);
        }
        int cellCount = (int)(cells.Keys.Count * GameSettings.blocksCountInPercent / 100);
        for (int i = 0; i < cellCount; i++)
        {
            Vector3Int pos = FindBloc(ref cells, width, height);
            InitBlock(pos);
        }
        foreach (int cell in cells.Keys)
        {
            InitFloor(CelIdToPosition(cell, width, height), floor);
        }

    }
    /**/
    private (Vector3Int, Vector3Int) FindPlayer(ref Dictionary<int, int> cells, int width, int height)
    {
        int index = Random.Range(0, cells.Count);
        int startPlyerPosId = cells.Values.ElementAt(index);
        cells.Remove(startPlyerPosId);
        InitFloor(CelIdToPosition(startPlyerPosId, width, height), floorPlayerStart);

        index = Random.Range(0, cells.Count);
        int endPlyerPosId = cells.Values.ElementAt(index);
        cells.Remove(endPlyerPosId);
        InitFloor(CelIdToPosition(endPlyerPosId, width, height), floorPlayerFinish);


        Vector3Int playerStartPos = CelIdToPosition(startPlyerPosId, width, height);
        Vector3Int playerEndPos = CelIdToPosition(endPlyerPosId, width, height);

        Vector3Int playerNewPos = CelIdToPosition(startPlyerPosId, width, height);
        Vector3Int playerNearestNewPos = NearestPositionForTarget(playerNewPos
            , CelIdToPosition(endPlyerPosId, width, height));

        for (int i = 0; playerNearestNewPos != playerEndPos && i < 50; i++)
        {
            cells.Remove(CelPositionToId(playerNearestNewPos, width));
            InitFloor(playerNearestNewPos, floor);

            playerNewPos = playerNearestNewPos;
            playerNearestNewPos = NearestPositionForTarget(playerNewPos
                , CelIdToPosition(endPlyerPosId, width, height));
        }
        return (playerStartPos, playerEndPos);
    }

    private (Vector3Int, Vector3Int) FindEnemy(ref Dictionary<int, int> cells, int width, int height)
    {
        int index = Random.Range(0, cells.Count);
        int startEnemyPosId = cells.Values.ElementAt(index);
        cells.Remove(startEnemyPosId);
        InitFloor(CelIdToPosition(startEnemyPosId, width, height), floor);
        Vector3Int enemyStartPos = CelIdToPosition(startEnemyPosId, width, height);

        index = Random.Range(0, cells.Count);
        int endEnemyPosId = cells.Values.ElementAt(index);
        cells.Remove(endEnemyPosId);
        InitFloor(CelIdToPosition(endEnemyPosId, width, height), floor);
        Vector3Int enemyEndPos = CelIdToPosition(endEnemyPosId, width, height);

        return (enemyStartPos, enemyEndPos);
    }
    private Vector3Int FindBloc(ref Dictionary<int, int> cells, int width, int height)
    {
        int index = cells.Values.ElementAt(Random.Range(0, cells.Count));
        cells.Remove(index);
        Vector3Int pos = CelIdToPosition(index, width, height);

        return pos;
    }

    /**/
    private void InitBlock(Vector3Int pos)
    {
        Instantiate(block, pos, Quaternion.identity, transform);
    }
    private void InitFloor(Vector3Int pos, GameObject floor)
    {
        Instantiate(floor, pos, Quaternion.identity, transform);
    }
    private void InitPlayer(Vector3Int startPos, Vector3Int endPos)
    {
        GameObject g = Instantiate(player, startPos + new Vector3(0.65f, 0, 0.65f), Quaternion.identity, transform);
        Player p = g.GetComponent<Player>();
    }
    private void InitEnemy(Vector3Int startPos, Vector3Int endPos)
    {
        GameObject g = Instantiate(enemy, startPos + new Vector3(0.65f, 0, 0.65f), Quaternion.identity, transform);
        Enemy e = g.GetComponent<Enemy>();
        e.UpdatePatrolPositions(startPos, endPos);
        g.name = $"Enemy{Random.Range(0, 9)}:{Random.Range(0, 10)}";
    }
    /**/
    private Vector3Int CelIdToPosition(int cellId, int width, int height)
    {
        return new Vector3Int(cellId / width, 0, cellId % width);
    }
    private int CelPositionToId(Vector3Int pos, int width)
    {
        return (pos.x * width + pos.z);
    }
    private Vector3Int NearestPositionForTarget(Vector3Int start, Vector3Int target)
    {
        Vector3Int[] startPositions = NearestPositions(start);
        float[] lenghts = new float[4]
        {
            Vector3Int.Distance(startPositions[0], target),
            Vector3Int.Distance(startPositions[1], target),
            Vector3Int.Distance(startPositions[2], target),
            Vector3Int.Distance(startPositions[3], target)
        };
        int maxlenghtId= 0;
        for (int i = 0; i < lenghts.Length-1; i++)
        {
            for (int k = i+1; k < lenghts.Length; k++)
            {
                if (lenghts[k] < lenghts[maxlenghtId])
                    maxlenghtId = k;
            }
        }
        return startPositions[maxlenghtId];
    }
    private Vector3Int[] NearestPositions(Vector3Int start)
    {
        return new Vector3Int[]{
            start + new Vector3Int(1, 0, 0),
            start + new Vector3Int(-1, 0, 0),
            start + new Vector3Int(0, 0, 1),
            start + new Vector3Int(0, 0, -1),
        };
    }
}

