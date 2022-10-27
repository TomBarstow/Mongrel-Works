using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public int mapWidth;
    public int mapHeight;
    //public int mapFloors;
    public int spawnSquareArea;
    public int gridSpacing = 10;
    public Vector3 gridOrigin = Vector3.zero;

    //Offset for perlin noise map
    Vector3 shift = new Vector3(237, 0, 92);
    //Zoom for perlin noise map
    float zoom = 0.05f;

    void Start()
    {
        GenerateMap();
    }


    void GenerateMap()
    {
        int gridx = mapWidth / 2;
        //int gridy = mapFloors / 2;
        int gridz = mapHeight / 2;
        int startx = -(gridx);
        int startz = -(gridz);
        int mapAreax = gridx * gridSpacing;
        int mapAreaz = gridz * gridSpacing;
        int spawnArea = (spawnSquareArea * gridSpacing)/2;

        int shiftNoise = Random.Range((mapWidth + mapHeight), 100000);
        shift = new Vector3(shiftNoise, 0, shiftNoise);

        for (int x = startx; x <= gridx; x++)
            {
                for (int z = startz; z <= gridz; z++)
                {
                    //Spawn position
                    Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;
                    //Perlin noise value
                    Vector3 pos = zoom * (new Vector3(x, 0, z)) + shift;
                    float noise = Mathf.PerlinNoise(pos.x, pos.z);
                    //Debug.Log(noise);

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
                        if (noise < 0.2f)
                        {
                            WallSpawn(spawnPosition, Quaternion.identity);
                        }
                        else if (noise > 0.2f && noise < 0.35f)
                        {
                            RandomAreaSpawn(spawnPosition, Quaternion.identity);
                        }
                        else if (noise > 0.35f && noise < 0.5f)
                        {
                            GrassAreaSpawn(spawnPosition, Quaternion.identity);
                        }
                        else if (noise > 0.5f && noise < 0.62f)
                        {
                            RandomAreaSpawn(spawnPosition, Quaternion.identity);
                        }
                        else if (noise > 0.62f && noise < 0.65f)
                        {
                            PillarSpawn(spawnPosition, Quaternion.identity);
                        }
                        else if (noise > 0.65f)
                        {
                            BoundarySpawn(spawnPosition, Quaternion.identity);
                        }
                    }

                }
            }

        
    }

    void RandomAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(3, tiles.Length);
        GameObject clone = Instantiate(tiles[randomIndex], positionToSpawn, rotationToSpawn);
        clone.transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 4) * 90, 0));
    }

    void SpawnAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[0], positionToSpawn, rotationToSpawn);
    }

    void BoundarySpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[1], positionToSpawn, rotationToSpawn);
        //makes more taller-er
        clone.transform.localScale = new Vector3(1, 3, 1);
        //clone.transform.position = new Vector3(positionToSpawn.x, positionToSpawn.y + 0.0f, positionToSpawn.z);
    }

    void WallSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[1], positionToSpawn, rotationToSpawn);
    }

    void PillarSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[2], positionToSpawn, rotationToSpawn);
    }

    void GrassAreaSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(tiles[3], positionToSpawn, rotationToSpawn);
    }
}
