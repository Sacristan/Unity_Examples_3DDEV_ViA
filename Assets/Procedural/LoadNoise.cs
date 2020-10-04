using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadNoise : MonoBehaviour
{
    public enum NoiseType { Perlin, Simplex, Voronoi, Worley, Value }

    const int Width = 512;
    const int Height = 512;
    const int NoiseFrequency = 10;

    [SerializeField] NoiseType noiseType = NoiseType.Perlin;

    void Start()
    {
        Texture2D texture = new Texture2D(Width, Height);

        INoise noise = GetNoise();
        FractalNoise fractal = new FractalNoise(noise, 4, 1f);

        float[,] arr = new float[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                float fx = x / (Width - 1.0f);
                float fy = y / (Height - 1.0f);

                arr[x, y] = fractal.Sample2D(fx, fy);
            }
        }

        NormalizeArray(arr);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                float n = arr[x, y];
                texture.SetPixel(x, y, new Color(n, n, n, 1));
            }
        }

        texture.Apply();
        GetComponent<RawImage>().texture = texture;
    }

    private static uint UnixEpoch
    {
        get
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return (uint)(System.DateTime.UtcNow - epochStart).TotalSeconds; // Unix epoch
        }
    }

    private INoise GetNoise()
    {
        switch (noiseType)
        {
            case NoiseType.Perlin:
                return new PerlinNoise((int)UnixEpoch, NoiseFrequency);
            case NoiseType.Simplex:
                return new SimplexNoise((int)UnixEpoch, NoiseFrequency);
            case NoiseType.Voronoi:
                return new VoronoiNoise((int)UnixEpoch, NoiseFrequency);
            case NoiseType.Worley:
                return new WorleyNoise((int)UnixEpoch, NoiseFrequency, 1f);
            case NoiseType.Value:
                return new ValueNoise((int)UnixEpoch, NoiseFrequency);
            // case NoiseType.Fractal:
            default:
                return null;
        }
    }

    private void NormalizeArray(float[,] arr)
    {
        float min = float.PositiveInfinity;
        float max = float.NegativeInfinity;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {

                float v = arr[x, y];
                if (v < min) min = v;
                if (v > max) max = v;

            }
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                float v = arr[x, y];
                arr[x, y] = (v - min) / (max - min);
            }
        }

    }

}
