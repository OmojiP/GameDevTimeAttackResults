using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ActionAdventurePlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerAttackObj;
    
    private CameraManager cameraManager;
    
    public event Action<Vector3Int> onPlayerMoved;

    bool isMove;
    bool isAttack;
    private Vector2 moveDir;
    private Vector2 faceDir;
    public bool isPushingStone;
    public bool isPlayerAttacking;

    private void Start()
    {
        faceDir = Vector2.right;
        cameraManager = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        if(cameraManager.isCameraMoving || isPushingStone || isPlayerAttacking) return;
        
        isAttack = false;
        isMove = false;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            faceDir= Vector2.up;
            moveDir = Vector2.up;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            faceDir= Vector2.down;
            moveDir = Vector2.down;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            faceDir= Vector2.right;
            moveDir = Vector2.right;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            faceDir= Vector2.left;
            moveDir = Vector2.left;
            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttack = true;
        }
        

        if (isMove)
        {
            // 進めるか確認
            if (ObjectChecker.TryGetNearestObject(transform.position, moveDir, out GameObject[] objs))
            {
                // stoneだったら押す

                foreach (var obj in objs)
                {
                    if (obj.CompareTag("Stone"))
                    {
                        obj.GetComponent<ActionAdventureStone>().PushedStone(moveDir, this);
                    }
                }
                
            }
            else
            {
                transform.position += (Vector3)moveDir;
                onPlayerMoved?.Invoke(Vector3Int.RoundToInt(transform.position));
            }

            
        }

        if (isAttack)
        {
            // 攻撃
            PlayerAttack().Forget();
        }
    }


    async UniTask PlayerAttack()
    {
        isPlayerAttacking = true;

        playerAttackObj.transform.DOMove(transform.position + (Vector3)faceDir, 0.2f);
        await UniTask.Delay(500);
        playerAttackObj.transform.DOMove(transform.position, 0.2f);
        await UniTask.Delay(300);
        
        isPlayerAttacking = false;
    }
}
