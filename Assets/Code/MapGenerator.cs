using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public int gridx;
    public int gridz;
    public float gridSpacing = 1f;
    public Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < gridx; x++)
        {
            for (int z = 0; z < gridz; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, tiles.Length);
        GameObject clone = Instantiate(tiles[randomIndex], positionToSpawn, rotationToSpawn);
    }


}
