using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDestruction : MonoBehaviour
{

    public TerrainPixel primaryLayer;     // Main terrain layer
    public TerrainPixel secondaryLayer;   // Outline layer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Applies destruction at the given impact position
    public void ApplyDestruction(Vector2 impactPosition, int circleSize, Texture2D destroyCircle, Texture2D outlineCircle)
    {
        Vector2Int pixelPos = WorldToPixelPosition(impactPosition);

        // Primary layer destruction
        primaryLayer.ApplyTextureChange(pixelPos, circleSize, destroyCircle, Color.clear);

        // Outline on secondary layer (if available)
        if (secondaryLayer != null && outlineCircle != null)
        {
            secondaryLayer.ApplyTextureChange(pixelPos, circleSize, outlineCircle, new Color(0, 0, 0, 0.75f));
        }
        
        primaryLayer.UpdateCollider(); // Update primary collider
    }

    private Vector2Int WorldToPixelPosition(Vector2 worldPosition)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        int x = Mathf.FloorToInt(localPosition.x * primaryLayer.terrainTexture.width);
        int y = Mathf.FloorToInt(localPosition.y * primaryLayer.terrainTexture.height);
        return new Vector2Int(x, y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
