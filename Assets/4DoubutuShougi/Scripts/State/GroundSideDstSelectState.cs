using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSideDstSelectState : IShougiState
{
    public GroundSideDstSelectState((int x , int y) komaPos, DoubutuShougiKomaType komaType)
    {
        this.komaPos = komaPos;
        this.komaType = komaType;
    }
    
    //選択状態の駒の情報
    private (int x, int y) komaPos;
    DoubutuShougiKomaType komaType;
    
    public bool OnSquareSelected((int x, int y) pos, DoubutuShougiMap map)
    {
        // 選択した場所が移動可能な方向かつ、敵or空きマスなら移動
        if (DoubutuShougiLogicUtility.CheckMovableDirection(DoubutuShougiPlaySideType.Ground, komaType, komaPos, pos))
        {
            Debug.Log($"指定座標には移動できます {pos.x},{pos.y}");
            
            // 移動ロジックを書く
            // 進化とかとりごまとか含めて

            map.MoveKoma(komaPos, pos);
            
            
            return true;
        }

        Debug.Log($"指定座標には移動できません {pos.x},{pos.y}");
        
        return false;
    }

    public IShougiState GetNextState()
    {
        return new SkySideKomaSelectState();
    }
}
