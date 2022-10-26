using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public int mapWidth;
    public int mapHeight;
    public int spawnSquareArea;
    public int gridSpacing = 10;
    public Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int gridx = mapWidth / 2;
        int gridz = mapHeight / 2;
        int startx = -(gridx);
        int startz = -(gridz);
        int mapAreax = gridx * gridSpacing;
        int mapAreaz = gridz * gridSpacing;
        int spawnArea = (spawnSquareArea * gridSpacing)/2;

        for (int x = startx; x <= gridx; x++)
        {
            for (int z = startz; z <= gridz; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;
                
                //Condition for spawn area fill
                if (-spawnArea <= spawnPosition.x && spawnPosition.x <= spawnArea && -spawnArea <= spawnPosition.z && spawnPosition.z <= spawnArea)
                {
                    SpawnAreaSpawn(spawnPosition, Quaternion.identity);
                }
                //Condition for boundary walls
                else if (spawnPosition.x == -mapAreax || spawnPosition.x == mapAreax || spawnPosition.z == -mapAreaz || spawnPosition.z == mapAreaz)
                {
                    BoundarySpawn(spawnPosition, Quaternion.identity);
                }
                //Default condition for random area fill
                else
                {
                    RandomAreaSpawn(spawnPosition, Quaternion.identity);
                }
                
            }
        }
    }

    void RandomAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
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
        //makes more taller-er
        clone.transform.localScale = new Vector3(10, 20, 10);
        clone.transform.position = new Vector3(positionToSpawn.x, positionToSpawn.y + 10f, positionToSpawn.z);
    }


}
