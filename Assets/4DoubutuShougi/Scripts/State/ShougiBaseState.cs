using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShougiState
{
    /// <summary>
    /// 操作が有効ならbool true
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public bool OnSquareSelected((int x , int y) pos, DoubutuShougiMap map);
    
    public IShougiState GetNextState();
}
