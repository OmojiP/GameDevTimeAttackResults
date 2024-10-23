using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public const int MAP_SIZE_W = 16;
    public const int MAP_SIZE_H = 8;
    
    public bool isCameraMoving = false;
    
    /*
    player が 縦8 横16 の範囲で動く
    そこから出たらカメラを移動
    */
    
    private ActionAdventurePlayer player;
    
    void Start()
    {
        player = GameObject.FindObjectOfType<ActionAdventurePlayer>();
        player.onPlayerMoved += OnPlayerMoved;
    }


    private Vector3Int currentMapCenter = Vector3Int.zero;
    private Vector3Int CurrentMapCenter => currentMapCenter;
    
    // 現在のマップの中心座標

    void UpdateCurrentMapCenter(Vector3Int value)
    {
        //valueに最も近い (MAP_SIZE_W*i, MAP_SIZE_H*j) , i, jは整数 をセットする
        int i = Mathf.RoundToInt((float)(value.x + 0.05f) / MAP_SIZE_W);
        int j = Mathf.RoundToInt((float)(value.y + 0.05f) / MAP_SIZE_H);
        
        // Debug.Log($"update i :{i}, j:{j}");

        currentMapCenter = new Vector3Int(i * MAP_SIZE_W, j * MAP_SIZE_H, value.z);
    }
    
    void OnPlayerMoved(Vector3Int pos)
    {
        // CurrentMapCenter と pos の距離が遠い→マップ移動

        // Debug.Log($"CurrentMapCenter : {CurrentMapCenter}, PlayerPos : {pos}");
        // Debug.Log($"{Mathf.Abs(CurrentMapCenter.x - pos.x - 0.05f)} >= { MAP_SIZE_W/2} or {Mathf.Abs(CurrentMapCenter.y - pos.y- 0.05f)} >= {MAP_SIZE_H/2}");
        // Debug.Log($"{Mathf.Abs(CurrentMapCenter.x - pos.x - 0.05f) >= MAP_SIZE_W/2} or {Mathf.Abs(CurrentMapCenter.y - pos.y- 0.05f) >= MAP_SIZE_H/2}");
        
        if (Mathf.Abs(CurrentMapCenter.x - pos.x - 0.05f) >= MAP_SIZE_W/2
            || Mathf.Abs(CurrentMapCenter.y - pos.y  - 0.05f) >= MAP_SIZE_H/2)
        {
            // カメラの位置更新
            UpdateCurrentMapCenter(pos);
            
            //カメラ移動
            Move(CurrentMapCenter);
        }
    }

    void Move(Vector3Int pos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y, transform.position.z);
        // transform.position = newPos;
        isCameraMoving = true;
        transform.DOMove(newPos, 0.5f).OnComplete(()=>isCameraMoving=false);
    }
    
}
