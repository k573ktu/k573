using System.Collections.Generic;
using UnityEngine;

public class ProceduralBg : MonoBehaviour
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
    public Material starMaterial; // Assign shader-based material in Inspector

    void Start()
    {
        target = Camera.main.transform;
        starColors = new List<Color>
        {
            ConvertRGBtoHSV(157, 180, 255), ConvertRGBtoHSV(162, 185, 255),
            ConvertRGBtoHSV(167, 188, 255), ConvertRGBtoHSV(170, 191, 255),
            ConvertRGBtoHSV(175, 195, 255), ConvertRGBtoHSV(186, 204, 255),
            ConvertRGBtoHSV(192, 209, 255), ConvertRGBtoHSV(202, 216, 255),
            ConvertRGBtoHSV(228, 232, 255), ConvertRGBtoHSV(237, 238, 255),
            ConvertRGBtoHSV(251, 248, 255), ConvertRGBtoHSV(255, 249, 249),
            ConvertRGBtoHSV(255, 245, 236), ConvertRGBtoHSV(255, 244, 232),
            ConvertRGBtoHSV(255, 241, 223), ConvertRGBtoHSV(255, 235, 209),
            ConvertRGBtoHSV(255, 215, 174), ConvertRGBtoHSV(255, 198, 144),
            ConvertRGBtoHSV(255, 190, 127), ConvertRGBtoHSV(255, 187, 123),
            ConvertRGBtoHSV(255, 187, 123)
        };

        // Apply the parallax shader material to the star prefab
        if (starPrefab != null && starMaterial != null)
        {
            starPrefab.GetComponent<SpriteRenderer>().material = starMaterial;
        }
    }

    Color ConvertRGBtoHSV(float r, float g, float b)
    {
        Color.RGBToHSV(new Color(r / 255f, g / 255f, b / 255f), out float h, out float s, out float v);
        return Color.HSVToRGB(h, s, v);
    }

    void Update()
    {
        genChunks();

        // Update parallax effect by sending camera position to the shader
        if (starMaterial != null && target != null)
        {
            starMaterial.SetVector("_CameraPos", target.position);
        }
    }

    void genChunks()
    {
        if (target == null) return;
        Vector2 targetPos = target.position;
        Vector2Int gridPos = new Vector2Int(
            Mathf.FloorToInt(targetPos.x / chunkSize),
            Mathf.FloorToInt(targetPos.y / chunkSize)
        );

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
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

            GameObject newStar = Instantiate(starPrefab, starPos, Quaternion.identity, chunk.transform);

            // Assign random size & color
            float size = Random.value * 0.13f;
            newStar.transform.localScale = new Vector3(size, size, 1);
            newStar.GetComponent<SpriteRenderer>().color = starColors[Random.Range(0, starColors.Count)];
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
