using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGeneration : MonoBehaviour
{
    [SerializeField] GameObject mapEntity;
    static readonly Vector3Int MapSize = new Vector3Int(16, 16, 16);
    void Start()
    {
        StartCoroutine(GenerateCaves());
    }

    private IEnumerator GenerateCaves()
    {
        INoise noise = new PerlinNoise((int)UnixEpoch, 5);
        FractalNoise fractal = new FractalNoise(noise, 4, 1f);

        float[,,] arr = new float[MapSize.x, MapSize.y, MapSize.z];

        yield return null;

        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int z = 0; z < MapSize.z; z++)
                {
                    float fx = x / (MapSize.x - 1.0f);
                    float fy = y / (MapSize.y - 1.0f);
                    float fz = z / (MapSize.z - 1.0f);

                    arr[x, y, z] = fractal.Sample3D(fx, fy, fz);

                    Debug.Log($"Generated noise value -> X {x} Y {y} Z {z}");
                }
            }
        }

        yield return null;

        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int z = 0; z < MapSize.z; z++)
                {
                    float noiseValue = arr[x, y, z];
                    Debug.Log($"X {x} Y {y} Z {z} NOISE: {noiseValue}");

                    if (noiseValue > 0.15f) Instantiate(mapEntity, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }

        arr = null;
    }

    private static uint UnixEpoch
    {
        get
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return (uint)(System.DateTime.UtcNow - epochStart).TotalSeconds; // Unix epoch
        }
    }

    private void NormalizeArray(float[,,] arr)
    {
        float min = float.PositiveInfinity;
        float max = float.NegativeInfinity;

        for (int z = 0; z < MapSize.z; z++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    float v = arr[x, y, z];
                    if (v < min) min = v;
                    if (v > max) max = v;
                }
            }
        }

        for (int z = 0; z < MapSize.z; z++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    float v = arr[x, y, z];
                    arr[x, y, z] = (v - min) / (max - min);
                }
            }
        }

    }
}
