using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainReset : MonoBehaviour
{
    public TerrainPixel primaryLayer;     // Main terrain layer
    public TerrainPixel secondaryLayer;   // Outline layer
    private Color[] originalTerrainPixels; // Store original terrain colors for res

    // Start is called before the first frame update
    void Start()
    {
        originalTerrainPixels = primaryLayer.terrainTexture.GetPixels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetMap()
    {
        // Reset main terrain texture
        primaryLayer.terrainTexture.SetPixels(originalTerrainPixels);
        primaryLayer.terrainTexture.Apply();

        // Reset outline layer if present
        if (secondaryLayer != null && secondaryLayer.outlineTexture != null)
        {
            Color[] clearColors = new Color[secondaryLayer.outlineTexture.width * secondaryLayer.outlineTexture.height];
            secondaryLayer.outlineTexture.SetPixels(clearColors);
            secondaryLayer.outlineTexture.Apply();
        }

        primaryLayer.UpdateCollider();
    }
}
