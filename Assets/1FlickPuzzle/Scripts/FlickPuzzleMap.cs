using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FlickPuzzleMap : MonoBehaviour
{
    [SerializeField] private Image squarePrefab;
    [SerializeField] private FlickPuzzleBlock[] blockPrefabs;
    [SerializeField] private Dictionary<FlickPuzzleBlockType, FlickPuzzleBlock> blockPrefabDictionary;

    private const int W = 9;
    private const int H = 9;

    private Image[,] squareImages;
    private FlickPuzzleBlock[,] blocks;

    private Vector2 offsetVec;
    [SerializeField] private float offsetScale;
    
    FlickPuzzleModel puzzleModel;
    
    void Start()
    {
        // ブロックプレハブを辞書に登録
        blockPrefabDictionary = new();
        foreach (FlickPuzzleBlock block in blockPrefabs)
        {
            blockPrefabDictionary.Add(block.blockType, block);
        }
        
        offsetVec = new Vector2((float)W / 2, (float)H / 2);
        
        squareImages = new Image[W, H];
        blocks = new FlickPuzzleBlock[W, H];
        
        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                // マスの配置
                squareImages[x, y] = Instantiate(squarePrefab, MapPos2UiPos(x,y), Quaternion.identity, this.transform);
            }
        }
        
        
        puzzleModel = new FlickPuzzleModel();
        puzzleModel.onCreatedBlock += OnCreatedBlock;
        puzzleModel.onBrokeBlock += OnBrokeBlock;
        puzzleModel.onDropedBlock += OnDropedBlock;
        
        puzzleModel.CreateNewPuzzle().Forget();
    }

    Vector3 MapPos2UiPos(int x, int y)
    {
        return MapPos2UiPos(new Vector2(x, y));
    }
    Vector3 MapPos2UiPos(Vector2 vec2)
    {
        return (vec2 - offsetVec) * offsetScale;
    }

    void OnCreatedBlock( Vector2 vector2, FlickPuzzleBlockType blockType )
    {
        // 画面上部からブロックを落とす
        blocks[(int)vector2.x, (int)vector2.y] = Instantiate(blockPrefabDictionary[blockType], this.transform);
        
        // 落とす
        blocks[(int)vector2.x, (int)vector2.y].Drop(MapPos2UiPos(vector2), false);
    }

    void OnBrokeBlock(Vector2 vector2)
    {
        blocks[(int)vector2.x, (int)vector2.y].Broke();
        blocks[(int)vector2.x, (int)vector2.y] = null;
    }

    void OnDropedBlock(Vector2 dst, Vector2 start)
    {
        // 落とす
        blocks[(int)start.x, (int)start.y].Drop(MapPos2UiPos(dst), true);

        blocks[(int)dst.x, (int)dst.y] = blocks[(int)start.x, (int)start.y];
        blocks[(int)start.x, (int)start.y] = null;
    }

}
