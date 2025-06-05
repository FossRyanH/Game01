using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Private Variables
    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;
    [SerializeField] float noiseScale;
    #endregion

    #region Public Getters
    public int MapWidth { get => mapWidth; }
    public int MapHeight { get => mapHeight; }
    public float NoiseScale { get => noiseScale; }
    #endregion

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, NoiseScale);

        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
