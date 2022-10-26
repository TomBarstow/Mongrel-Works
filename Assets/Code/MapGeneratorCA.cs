using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGeneratorCA : MonoBehaviour
{
    public int width;
    public int height;
    public int fillPercentage = 50;

    public string seed;
    //public bool useRandomSeed;

    int[,] map;



    void Start()
    {
        GenerateMap();
        OnDrawGizmos();
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFill();
    }

    void RandomFill()
    {
        System.Random randomNumGen = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    if (randomNumGen.Next(0, 100) > fillPercentage)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = 0;
                    }
                }
                
            }
        }

    }

    void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }


}