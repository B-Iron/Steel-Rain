using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTestControls : MonoBehaviour
{
    public TerrainDestruction terrainDestruction;
    public Texture2D destroyCircle;
    public Texture2D outlineCircle;
    public int circleSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            terrainDestruction.ApplyDestruction(mousePosition, circleSize, destroyCircle, outlineCircle);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<TerrainReset>().ResetMap();
        }
    }
}
