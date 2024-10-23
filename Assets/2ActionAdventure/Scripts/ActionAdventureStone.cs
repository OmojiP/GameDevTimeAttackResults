using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ActionAdventureStone : MonoBehaviour
{

    public void PushedStone(Vector2 moveDir, ActionAdventurePlayer player)
    {
        moveDir = moveDir.normalized;
        
        player.isPushingStone = true;
        transform.DOMove(transform.position + (Vector3)moveDir, 0.2f).OnComplete(() =>{player.isPushingStone = false;});
    }
    
}
