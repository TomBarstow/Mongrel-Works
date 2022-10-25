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
    public Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        GenerateMap();
        GenerateBoundary();
    }

    void GenerateMap()
    {
        gridx = gridx / 2;
        gridz = gridz / 2;
        int startx = -(gridx);
        int startz = -(gridz);

        for (int x = startx; x <= gridx; x++)
        {
            for (int z = startz; z <= gridz; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;

                if (-spawnArea <= spawnPosition.x && spawnPosition.x <= spawnArea)
                {
                    if (-spawnArea <= spawnPosition.z && spawnPosition.z <= spawnArea)
                    {
                        SpawnAreaSpawn(spawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        PickAndSpawn(spawnPosition, Quaternion.identity);
                    }
                }
                else
                {
                    PickAndSpawn(spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    void GenerateBoundary()
    {
        int boundaryx = gridx + 2;
        int boundaryz = gridz + 2;

        int mapAreax = gridx * gridSpacing;
        int mapAreaz = gridz * gridSpacing;

        int startx = -(boundaryx - 1);
        int startz = -(boundaryz - 1);

        for (int x = startx; x < boundaryx; x++)
        {
            for (int z = startz; z < boundaryz; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;
                if (-mapAreaz <= spawnPosition.z && spawnPosition.z <= mapAreaz)
                {
                    if (-mapAreax <= spawnPosition.x && spawnPosition.x <= mapAreax)
                    {
                        
                    }
                    else
                    {
                        BoundarySpawn(spawnPosition, Quaternion.identity);
                    }
                }
                else
                {
                    BoundarySpawn(spawnPosition, Quaternion.identity);
                }
            }
        }
    }


    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, tiles.Length);
        GameObject clone = Instantiate(tiles[randomIndex], positionToSpawn, rotationToSpawn);
    }

    void SpawnAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[0], positionToSpawn, rotationToSpawn);
    }

    void BoundarySpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[1], positionToSpawn, rotationToSpawn);
    }


}
