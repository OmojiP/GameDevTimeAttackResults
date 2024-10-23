using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubutuShougiMap
{
    public const int mapW = 3;
    public const int mapH = 4;

    public DoubutuShougiKoma[,] map;
    
    public List<DoubutuShougiKomaType> skyHandKomas = new();
    public List<DoubutuShougiKomaType> groundHandKomas = new();
    
    public event Action<(int x, int y), (int x, int y)> onKomaMoved; 
    public event Action<DoubutuShougiPlaySideType> onWin; 
    
    public DoubutuShougiMap()
    {
        map = new DoubutuShougiKoma[mapW, mapH];
        for (int y = 0; y < mapH; y++)
        {
            for (int x = 0; x < mapW; x++)
            {
                map[x, y] = new DoubutuShougiKoma(DoubutuShougiKomaType.None, DoubutuShougiPlaySideType.None);
            }
        }
        
        map[0, 0] = new DoubutuShougiKoma(DoubutuShougiKomaType.Zou, DoubutuShougiPlaySideType.Ground);
        map[1, 0] = new DoubutuShougiKoma(DoubutuShougiKomaType.Lion, DoubutuShougiPlaySideType.Ground);
        map[2, 0] = new DoubutuShougiKoma(DoubutuShougiKomaType.Kirin, DoubutuShougiPlaySideType.Ground);
        map[1, 1] = new DoubutuShougiKoma(DoubutuShougiKomaType.Hiyoko, DoubutuShougiPlaySideType.Ground);
        
        map[mapW-1, mapH-1] = new DoubutuShougiKoma(DoubutuShougiKomaType.Zou, DoubutuShougiPlaySideType.Sky);
        map[mapW-2, mapH-1] = new DoubutuShougiKoma(DoubutuShougiKomaType.Lion, DoubutuShougiPlaySideType.Sky);
        map[mapW-3, mapH-1] = new DoubutuShougiKoma(DoubutuShougiKomaType.Kirin, DoubutuShougiPlaySideType.Sky);
        map[mapW-2, mapH-2] = new DoubutuShougiKoma(DoubutuShougiKomaType.Hiyoko, DoubutuShougiPlaySideType.Sky);
    }

    public void MoveKoma((int x, int y) komaPos, (int x, int y) dstPos)
    {
        DoubutuShougiPlaySideType playSideType = map[komaPos.x, komaPos.y].playSide;
        DoubutuShougiKomaType moveKoma = map[komaPos.x, komaPos.y].komaType;
        DoubutuShougiKomaType dstKoma = map[dstPos.x, dstPos.y].komaType;
        map[dstPos.x, dstPos.y].komaType = moveKoma;
        map[dstPos.x, dstPos.y].playSide = playSideType;
        map[komaPos.x, komaPos.y].komaType = DoubutuShougiKomaType.None;
        
        // 手札に加える
        if (dstKoma != DoubutuShougiKomaType.None)
        {
            if (playSideType == DoubutuShougiPlaySideType.Sky)
            {
                skyHandKomas.Add(dstKoma);
            }
            else
            {
                groundHandKomas.Add(dstKoma);
            }
        }
        
        // 鶏進化
        if (moveKoma == DoubutuShougiKomaType.Hiyoko &&
            ((playSideType == DoubutuShougiPlaySideType.Ground && dstPos.y == DoubutuShougiMap.mapH - 1) ||
             (playSideType == DoubutuShougiPlaySideType.Sky && dstPos.y == 0)))
        {
            map[dstPos.x, dstPos.y].komaType = DoubutuShougiKomaType.Niwatori;
        }
        
        // 勝利
        if(moveKoma == DoubutuShougiKomaType.Lion &&
           ((playSideType == DoubutuShougiPlaySideType.Ground && dstPos.y == DoubutuShougiMap.mapH - 1) ||
            (playSideType == DoubutuShougiPlaySideType.Sky && dstPos.y == 0)))
        {
            Debug.Log($"{playSideType.ToString()}の勝利");
            onWin?.Invoke(playSideType);
        }
        
        onKomaMoved?.Invoke(komaPos, dstPos);
        
    }
}


