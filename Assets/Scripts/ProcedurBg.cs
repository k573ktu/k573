using System.Collections.Generic;
using UnityEngine;

public class ProcedurBg : MonoBehaviour
{
    public GameObject starPrefab;
    private int chunkSize = 40;
    private int starsPerChunk = 180;
    private float starSpread = 20f;
    private float activationDistance = 200f; 
    public bool drawBorders = true; 
    private Dictionary<Vector2Int, GameObject> chunkGrid = new();
    private Transform target;
    public List<Color> starColors;


    void Start()
    {
        target = Camera.main.transform;
        starColors = new List<Color>
        {
            ConvertRGBtoHSV(157, 180, 255), // O5(V)
            ConvertRGBtoHSV(162, 185, 255), // B1(V)
            ConvertRGBtoHSV(167, 188, 255), // B3(V)
            ConvertRGBtoHSV(170, 191, 255), // B5(V)
            ConvertRGBtoHSV(175, 195, 255), // B8(V)
            ConvertRGBtoHSV(186, 204, 255), // A1(V)
            ConvertRGBtoHSV(192, 209, 255), // A3(V)
            ConvertRGBtoHSV(202, 216, 255), // A5(V)
            ConvertRGBtoHSV(228, 232, 255), // F0(V)
            ConvertRGBtoHSV(237, 238, 255), // F2(V)
            ConvertRGBtoHSV(251, 248, 255), // F5(V)
            ConvertRGBtoHSV(255, 249, 249), // F8(V)
            ConvertRGBtoHSV(255, 245, 236), // G2(V)
            ConvertRGBtoHSV(255, 244, 232), // G5(V)
            ConvertRGBtoHSV(255, 241, 223), // G8(V)
            ConvertRGBtoHSV(255, 235, 209), // K0(V)
            ConvertRGBtoHSV(255, 215, 174), // K4(V)
            ConvertRGBtoHSV(255, 198, 144), // K7(V)
            ConvertRGBtoHSV(255, 190, 127), // M2(V)
            ConvertRGBtoHSV(255, 187, 123), // M4(V)
            ConvertRGBtoHSV(255, 187, 123)  // M6(V)
        };
    }

    Color ConvertRGBtoHSV(float r, float g, float b)
    {
        Color.RGBToHSV(new Color(r / 255f, g / 255f, b / 255f), out float h, out float s, out float v);
        return Color.HSVToRGB(h, s, v);
    }
    void Update(){genChunks();}
    void genChunks()
    {
        if (target == null) return;
        Vector2 targetPos = target.position;
        Vector2Int gridPos = new Vector2Int(
            Mathf.FloorToInt(targetPos.x / chunkSize),
            Mathf.FloorToInt(targetPos.y / chunkSize)
                                            );
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int chunkKey = gridPos + new Vector2Int(x, y);
                if (!chunkGrid.ContainsKey(chunkKey))
                {
                    createChunk(chunkKey);
                }
                float distanceToChunk = Vector2.Distance(targetPos, (Vector2)chunkKey * chunkSize);
                if (distanceToChunk < activationDistance)
                {
                    addStars(chunkKey);
                }
            }
        }
    }
    void createChunk(Vector2Int chunkKey)  
    {
        GameObject chunk = new GameObject($"Chunk {chunkKey.x} {chunkKey.y}");
        chunk.transform.parent = transform;
        chunk.transform.position = new Vector3(chunkKey.x * chunkSize, chunkKey.y * chunkSize, 0);
        if (drawBorders)
        {
            addBorder(chunk);
        }
        chunkGrid[chunkKey] = chunk;
    }
    void addStars(Vector2Int chunkKey)
    {
        GameObject chunk = chunkGrid[chunkKey];
        if (chunk.transform.childCount > 0) return;
        for (int i = 0; i < starsPerChunk; i++)
        {
            Vector3 starPos = new Vector3(
                Random.Range(-starSpread, starSpread),
                Random.Range(-starSpread, starSpread),
                0
            ) + chunk.transform.position;
            GameObject tempStar = starPrefab;
            float tempNumber = Random.value*0.13f;
            tempStar.transform.localScale = new Vector3(tempNumber, tempNumber, 1);
            tempStar.GetComponent<SpriteRenderer>().color = //starColors[i];
                starColors[Random.Range(0, starColors.Count)];
            Instantiate(starPrefab, starPos, Quaternion.identity, chunk.transform);
        }
    }
    void addBorder(GameObject chunk)
    {
        LineRenderer line = chunk.AddComponent<LineRenderer>();
        line.positionCount = 5;
        line.loop = true;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.green;
        line.endColor = Color.green;

        Vector3 chunkPos = chunk.transform.position;
        float halfSize = chunkSize / 2f;

        line.SetPositions(new Vector3[]
        {
            new Vector3(chunkPos.x - halfSize, chunkPos.y - halfSize, 0),
            new Vector3(chunkPos.x + halfSize, chunkPos.y - halfSize, 0),
            new Vector3(chunkPos.x + halfSize, chunkPos.y + halfSize, 0),
            new Vector3(chunkPos.x - halfSize, chunkPos.y + halfSize, 0),
            new Vector3(chunkPos.x - halfSize, chunkPos.y - halfSize, 0)
        });
    }
}
