using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ObjectChecker
{
    /// <summary>
    /// 現在の座標から指定方向距離１マスの位置にオブジェクトがいた場合、そのオブジェクトを返す
    /// </summary>
    public static bool TryGetNearestObject(Vector2 pos, Vector2 dir, out GameObject[] gameObjects)
    {    
        gameObjects = null;
        
        // 現在位置から右方向へレイを飛ばす
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos + dir, Vector2.right, 0.1f);
        
        // レイがオブジェクトにヒットした場合
        if (hits != null && hits.Length > 0)
        {
            // Debug.Log("Found nearest object");
            gameObjects = hits.Select(x => x.collider.gameObject).ToArray();
            return true;
        }

        return false;
    }
    
}
