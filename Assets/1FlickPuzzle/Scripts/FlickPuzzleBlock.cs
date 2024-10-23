using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class FlickPuzzleBlock : MonoBehaviour
{
    [SerializeField] public FlickPuzzleBlockType blockType;

    [SerializeField] private float dropStartPosY;
    

    public async UniTask Drop(Vector2 vector2, bool isAppeardBlock)
    {
        if (isAppeardBlock)
        {
            
        }
        else
        {
            // 画面外から指定位置まで落とす
            transform.position = new Vector2(vector2.x * 300, dropStartPosY);
        }

        await UniTask.Delay(300);
        
        // 落とす
        transform.position = vector2 * 300;
    }
    
    public void Broke()
    {
        // 壊す
        Destroy(gameObject);
    }
}
