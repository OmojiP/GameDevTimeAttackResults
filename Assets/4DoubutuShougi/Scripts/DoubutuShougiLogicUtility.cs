using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubutuShougiLogicUtility
{
    // 選択方向に移動可能かどうかを判定
    public static bool CheckMovableDirection(
        DoubutuShougiPlaySideType playSideType, 
        DoubutuShougiKomaType komaType, 
        (int x , int y) komaPos,
        (int x , int y) dstPos
        )
    {
        if (playSideType == DoubutuShougiPlaySideType.Sky)
        {
            if (komaType == DoubutuShougiKomaType.Hiyoko && komaPos.x == dstPos.x && komaPos.y -1 == dstPos.y)
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Zou && 
                    (komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && 
                    (komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y))
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Kirin &&
                    ((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) || 
                    ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x))
            {
                return true;
            }
            else if (
                komaType == DoubutuShougiKomaType.Lion && 
                (((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && (komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y)) ||
                 ((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) || ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x)))
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Niwatori &&
                    (
                        (((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) && 
                            ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x)) ||
                        ( (komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y-1 == dstPos.y)
                    )
                )
            {
                return true;
            }
        }
        else
        {
            if (komaType == DoubutuShougiKomaType.Hiyoko && komaPos.x == dstPos.x && komaPos.y +1 == dstPos.y)
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Zou && 
                    (komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && 
                    (komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y))
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Kirin &&
                    ((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) || 
                    ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x))
            {
                return true;
            }
            else if (
                ((komaType == DoubutuShougiKomaType.Lion && 
                  (((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && 
                   (komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y)) ||
                  ((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) ||
                  ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x)))))
            {
                return true;
            }
            else if(komaType == DoubutuShougiKomaType.Niwatori &&
                    (
                        (((komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y == dstPos.y) && 
                         ((komaPos.y + 1 == dstPos.y || komaPos.y - 1 == dstPos.y) && komaPos.x == dstPos.x)) ||
                        ( (komaPos.x + 1 == dstPos.x || komaPos.x - 1 == dstPos.x) && komaPos.y+1 == dstPos.y)
                    )
                   )
            {
                return true;
            }
        }

        return false;

    }
}
