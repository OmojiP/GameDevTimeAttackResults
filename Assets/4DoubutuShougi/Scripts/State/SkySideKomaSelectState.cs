using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySideKomaSelectState : IShougiState
{
    private (int x, int y) komaPos;
    private DoubutuShougiKomaType komaType;
    
    public bool OnSquareSelected((int x, int y) pos, DoubutuShougiMap map)
    {
        // 選択した座標が自分の駒なら移動先選択へ

        Debug.Log($"pos {pos.x}, {pos.y}");
        Debug.Log($"map.map {map.map}");
        
        if (map.map[(int)pos.x, (int)pos.y].playSide == DoubutuShougiPlaySideType.Sky)
        {
            komaPos = pos;
            komaType = map.map[(int)pos.x, (int)pos.y].komaType;
            
            Debug.Log($"指定座標には自分の駒があります {komaType.ToString()}, {pos.x}, {pos.y}");
            
            return true;
        }

        Debug.Log("指定座標に自分の駒がありません");
        
        return false;
    }

    public IShougiState GetNextState()
    {
        SkySideDstSelectState state = new SkySideDstSelectState(komaPos, komaType);

        return state;
    }
}
