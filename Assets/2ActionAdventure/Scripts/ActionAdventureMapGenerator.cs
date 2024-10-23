using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAdventureMapGenerator : MonoBehaviour
{
    [SerializeField] GameObject mapPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMaps();
    }

    void GenerateMaps()
    {
        Vector3 offset;
        
        int generateSizeW = 2;
        int generateSizeH = 2;
        
        
        
        for (int x = -generateSizeW; x < generateSizeW; x++)
        {
            for (int y = -generateSizeH; y < generateSizeH; y++)
            {
                Generate1Map(x, y);
            }
        }
    }
    
    void Generate1Map(int w, int h)
    {
        Vector3 offset = new Vector3(
            w * CameraManager.MAP_SIZE_W - CameraManager.MAP_SIZE_W / 2,
            h * CameraManager.MAP_SIZE_H - CameraManager.MAP_SIZE_H / 2,
            0);
        bool isDark = (w + h) % 2 == 0;
        
        
        for (int x = 0; x < CameraManager.MAP_SIZE_W; x++)
        {
            for (int y = 0; y < CameraManager.MAP_SIZE_H; y++)
            {
                SpriteRenderer sp = Instantiate(mapPrefabs, new Vector3(x, y, 0) + offset, Quaternion.identity, transform).GetComponent<SpriteRenderer>();
                
                if (isDark)
                {
                    sp.color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                else
                {
                    sp.color = Color.white;
                }
            }
        }
    }
}
