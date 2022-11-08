using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public int gridx;
    public int gridz;
    public int spawnArea;
    public int gridSpacing = 10;
    //private Vector3 gridOrigin = Vector3.zero;

    public int[,] gridArray;

    //Offset for perlin noise map
    public Vector3 shift = new Vector3(237, 0, 92);
    //Zoom for perlin noise map
    public float zoom = 0.1f;

    void Start()
    {
        GenerateArray();
        GenerateMap();
    }

    void GenerateArray()
    {
        gridArray = new int[gridx, gridz];

        //TODO add code to offset spawn area to center or array and create references for logic

        for (int x = 0; x <= gridx - 1; x++)
        {
            for (int z = 0; z <= gridz - 1; z++)
            {

                Vector3 pos = zoom * (new Vector3(x, 0, z)) + shift;
                float noise = Mathf.PerlinNoise(pos.x, pos.z);

                if (-spawnArea <= x & x <= spawnArea & -spawnArea <= z & z <= spawnArea)
                {
                    gridArray[x,z] = 2;
                }
                else if (noise < 0.4 || noise > 0.6)
                {
                    gridArray[x,z] = 0;
                }
                else
                {
                    gridArray[x,z] = 1;
                }
                Debug.Log(gridArray[x,z]);
            }
        }
    }

    void GenerateMap()
    {
        Vector3 gridOrigin = new Vector3(-((gridx * gridSpacing) / 2), 0, -((gridz * gridSpacing) / 2));
        //Vector3 gridOrigin = new Vector3(0,0,0);

        for (int x = 0; x <= gridx - 1; x++)
        {
            for (int z = 0; z <= gridz - 1; z++)
            {
                //Spawn position
                Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;

                switch (gridArray[x, z])
                {
                    case 0:
                        /*
                        //TODO make looping function to check for neighbors in array
                        //This is incrementing the variables... pretty dumb ngl
                        if (x + 1 == 2 || x + 1 == 1 || x - 1 == 2 || x - 1 == 1 || z + 1 == 2 || z + 1 == 1 || z - 1 == 2 || z - 1 == 1)
                        {
                            BoundarySpawn(spawnPosition, Quaternion.identity);
                        }
                        */
                        BoundarySpawn(spawnPosition, Quaternion.identity);
                        break;

                    case 1:
                        GrassAreaSpawn(spawnPosition, Quaternion.identity);
                        break;

                    case 2:
                        SpawnAreaSpawn(spawnPosition, Quaternion.identity);
                        break;
                }

            }
        }
    }


    void SpawnAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[0], positionToSpawn, rotationToSpawn);
    }

    void BoundarySpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[1], positionToSpawn, rotationToSpawn);
    }

    void GrassAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[2], positionToSpawn, rotationToSpawn);
    }
}
