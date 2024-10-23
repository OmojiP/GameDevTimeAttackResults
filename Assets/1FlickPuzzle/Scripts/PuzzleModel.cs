using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class FlickPuzzleModel
{
    // パズルの配列を持ち、操作に応じて消す、新たにブロックを追加する等行う

    
    private const int W = 9;
    private const int H = 9;

    public event Action<Vector2, FlickPuzzleBlockType> onCreatedBlock;
    public event Action<Vector2> onBrokeBlock;
    public event Action<Vector2, Vector2> onDropedBlock;
    
    public FlickPuzzleModel()
    {
        
    }

    // 新たな盤面を生成する
    // ３つ以上同じ色が並ばないようにする
    public async UniTask CreateNewPuzzle()
    {
        blocks = new FlickPuzzleBlockType[W, H];
        
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                // ランダムなブロックをマスに配置
                int r = Random.Range(0, 3);
                blocks[x, y] = (FlickPuzzleBlockType)r;
                onCreatedBlock?.Invoke(new Vector2(x, y), blocks[x, y]);
            }
        }
        
        // 被ってる盤面がないか確認し、被っている部分を消した後、ブロックを追加する
        while (CheckBlock3Ren())
        {
            await BreakRenBlocks();
            Debug.Log("Break Block");
            DebugBlocks();
            await UniTask.Delay(1000);
            
            await DropBlocks();
            Debug.Log("Drop Block");
            DebugBlocks();
            await UniTask.Delay(1000);
            
            await FillNoneBlocks();
            Debug.Log("Fill Block");
            DebugBlocks();
            await UniTask.Delay(1000);
        }

        DebugBlocks();
    }

    // blockの状態を出力
    // 実際の配置と近い形で出力
    void DebugBlocks()
    {
        string s = String.Empty;
        for (int y = H-1; y >= 0; y--)
        {
            for (int x = 0; x < W; x++)
            {
                s += ((int)blocks[x, y]) + ", ";
                
            }

            s += "\n";
        }
        Debug.Log(s);
    }
    
    // 並んでいるブロックがあれば true
    bool CheckBlock3Ren()
    {
        FlickPuzzleBlockType preBlockType;
        int blockRenCount = 0;
        
        // 横方向に調べる
        for (int y = 0; y < H; y++)
        {
            //リセット
            blockRenCount = 0;
            preBlockType = FlickPuzzleBlockType.NONE;
            
            for (int x = 0; x < W; x++)
            {
                if (preBlockType == blocks[x, y])
                {
                    blockRenCount++;
                }
                else
                {
                    blockRenCount = 0;
                }

                if (blockRenCount >= 2)
                {
                    Debug.Log("ブロックが連続になっている");
                    return true;
                }
                
                preBlockType = blocks[x, y];
            }
        }
        
        // 縦方向に調べる
        for (int x = 0; x < W; x++)
        {
            //リセット
            blockRenCount = 0;
            preBlockType = FlickPuzzleBlockType.NONE;
            
            for (int y = 0; y < H; y++)
            {
                if (preBlockType == blocks[x, y])
                {
                    blockRenCount++;
                }
                else
                {
                    blockRenCount = 0;
                }

                if (blockRenCount >= 2)
                {
                    Debug.Log("ブロックが連続になっている");

                    return true;
                }
                
                preBlockType = blocks[x, y];
            }
        }

        Debug.Log("ブロックが連続になっていない");
        DebugBlocks();
        return false;
    }
    
    // 並んでいるブロックを同時に消す
    async UniTask BreakRenBlocks()
    {
        // 連続したブロックをメモしておき、最後に一気に消す
        
        FlickPuzzleBlockType preBlockType;
        int blockRenCount = 0;
        (int x, int y) renStartPosition = (0, 0);
        bool[,] isBreakBlocks = new bool[W, H];
        
        // 横方向に調べる
        for (int y = 0; y < H; y++)
        {
            //リセット
            blockRenCount = 0;
            preBlockType = blocks[0, y];
            renStartPosition = (-1, -1);
            
            for (int x = 1; x < W; x++)
            {
                if (preBlockType == blocks[x, y])
                {
                    if (blockRenCount == 0)
                    {
                        renStartPosition = (x-1, y);
                    }
                    
                    blockRenCount++;
                    
                }
                else
                {
                    blockRenCount = 0;
                    renStartPosition = (-1, -1);
                }

                if (blockRenCount >= 2)
                {
                    // renStartPositionから今の位置までのブロックを消すリストに追加する
                    isBreakBlocks[renStartPosition.x, renStartPosition.y] = true;
                    isBreakBlocks[renStartPosition.x + 1, renStartPosition.y] = true;
                    isBreakBlocks[x, y] = true;
                }
                
                preBlockType = blocks[x, y];
            }
        }
        
        // 縦方向に調べる
        for (int x = 0; x < W; x++)
        {
            //リセット
            blockRenCount = 0;
            preBlockType = blocks[x, 0];
            renStartPosition = (-1, -1);
            
            for (int y = 1; y < H; y++)
            {
                if (preBlockType == blocks[x, y])
                {
                    if (blockRenCount == 0)
                    {
                        renStartPosition = (x, y-1);
                    }
                    
                    blockRenCount++;
                }
                else
                {
                    blockRenCount = 0;
                    renStartPosition = (-1, -1);
                }

                if (blockRenCount >= 2)
                {
                    // renStartPositionから今の位置までのブロックを消すリストに追加する
                    isBreakBlocks[renStartPosition.x, renStartPosition.y] = true;
                    isBreakBlocks[renStartPosition.x, renStartPosition.y + 1] = true;
                    isBreakBlocks[x, y] = true;
                }
                
                preBlockType = blocks[x, y];
            }
        }
        
        // メモしたマスをNONEに変更する
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if (isBreakBlocks[x, y])
                {
                    blocks[x, y] = FlickPuzzleBlockType.NONE;
                    onBrokeBlock?.Invoke(new Vector2(x, y));
                }
            }
        }
        
    }
    
    // NONEの部分を重力で詰める
    async UniTask DropBlocks()
    {
        // 下から調べていってNoneがあったら上のブロックをそこに移動させる
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if (blocks[x, y] == FlickPuzzleBlockType.NONE)
                {
                    int dropY = y + 1; 
                    while (dropY < H)
                    {
                        // 上のブロックを調べ、NONEでないマスがあったら落とす
                        if (blocks[x, dropY] != FlickPuzzleBlockType.NONE)
                        {
                            blocks[x, y] = blocks[x, dropY];
                            blocks[x, dropY] = FlickPuzzleBlockType.NONE;
                            onDropedBlock?.Invoke(new Vector2(x, y), new Vector2(x, dropY));
                            break;
                        }
                        
                        dropY++;
                    }
                }
            }
        }
    }
    
    // NONEのブロックに新たなブロックを入れる
    async UniTask FillNoneBlocks()
    {
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                if (blocks[x, y] == FlickPuzzleBlockType.NONE)
                {
                    // ランダムなブロックをマスに配置
                    int r = Random.Range(0, 3);
                    blocks[x, y] = (FlickPuzzleBlockType)r;
                    
                    // 本当はCreateは全部画面上部から落ちてくる仕様なのでdropとまとめてもいいかも
                    onCreatedBlock?.Invoke(new Vector2(x, y), blocks[x, y]);
                }
            }
        }
    }
    
    private FlickPuzzleBlockType[,] blocks;
}

public enum FlickPuzzleBlockType
{
    RED = 0,
    BLUE = 1,
    YELLOW = 2,
    NONE = 3
}
