using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSector : MonoBehaviour
{
    public float length;
    public float nearestLength;
    public float angle;

    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3 viewVector = new Vector3(0, 0, 1);
    private GameObject focusedPlayer;
    private Enemy enemy;

    private void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = ViewSectorVertices(viewVector, length, nearestLength, angle);
        mesh.uv = ViewSectorUi(6);
        mesh.triangles = ViewSectorTriangles();


        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        if (focusedPlayer != null)
        {
            enemy.GoToPlayer(focusedPlayer.transform.position);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("Player") == true)
        {
            if(focusedPlayer == null)
            {
                focusedPlayer = collider.gameObject;
            }
        }
    }

    private Vector3[] ViewSectorVertices(Vector3 viewVector, float length, float nearestLength, float angle)
    {
        Vector3 viewRow = viewVector.normalized;

        Vector3 nearestViewRow = viewRow * nearestLength;
        Vector3 farViewRow = viewRow * length;
        
        Vector3[] vectors = new Vector3[]
        {
            nearestViewRow,
            farViewRow,
            Quaternion.Euler(0, angle / 2, 0) * farViewRow,
            Quaternion.Euler(0, angle / 2, 0) * nearestViewRow,
            Quaternion.Euler(0, -angle / 2, 0) * nearestViewRow,
            Quaternion.Euler(0, -angle / 2, 0) * farViewRow,
        };
        return vectors;
    }
    private int[] ViewSectorTriangles()
    {
        int[] vectors = new int[]
        {
            0,1,2,
            2,3,0,
            0,4,5,
            5,1,0,
        };
        return vectors;
    }
    private Vector2[] ViewSectorUi(int size)
    {
        Vector2[] uies = new Vector2[size];
        for (int i = 0; i < size; i++)
        {
            uies[i] = new Vector2(0,1);
        }
        return uies;
    }
}
