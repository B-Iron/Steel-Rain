using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPixel : MonoBehaviour
{

    public Texture2D terrainTexture;      // Main terrain texture
    public Texture2D outlineTexture;      // Outline texture
    private Color[] originalTerrainPixels; // Stores original terrain pixels for resetting
    private PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
        terrainTexture = Instantiate(GetComponent<SpriteRenderer>().sprite.texture);
        originalTerrainPixels = terrainTexture.GetPixels();

        if (outlineTexture != null)
        {
            outlineTexture = Instantiate(outlineTexture);
        }

        polygonCollider = GetComponent<PolygonCollider2D>();
        UpdateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void ApplyTextureChange(Vector2Int position, int size, Texture2D texturePattern, Color fillColor)
    {
        for (int x = -size; x <= size; x++)
        {
            for (int y = -size; y <= size; y++)
            {
                Vector2Int currentPos = position + new Vector2Int(x, y);
                if (IsWithinBounds(currentPos, terrainTexture.width, terrainTexture.height))
                {
                    terrainTexture.SetPixel(currentPos.x, currentPos.y, fillColor);
                }
            }
        }
        terrainTexture.Apply();
    }

    public void UpdateCollider()
    {
        List<Vector2> colliderPoints = new List<Vector2>();

        // Update collider based on texture
        for (int x = 0; x < terrainTexture.width; x++)
        {
            for (int y = 0; y < terrainTexture.height; y++)
            {
                if (terrainTexture.GetPixel(x, y).a > 0.1f) 
                {
                    Vector2 point = new Vector2(
                        (x / (float)terrainTexture.width) - 0.5f,
                        (y / (float)terrainTexture.height) - 0.5f
                    );
                    colliderPoints.Add(point);
                }
            }
        }

        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, colliderPoints.ToArray());
    }

    private bool IsWithinBounds(Vector2Int pos, int width, int height)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < width && pos.y < height;
    }
}

