using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubutuShougiGameStateManager
{
    public DoubutuShougiGameStateManager(DoubutuShougiMap map)
    {
        this.map = map;

        shougiState = new SkySideKomaSelectState();
    }

    private DoubutuShougiMap map;
    
    private IShougiState shougiState;

    public void OnSuareSelected((int x, int y) pos)
    {
        bool isNextState = shougiState.OnSquareSelected(pos, map);

        if (isNextState)
        {
            shougiState = shougiState.GetNextState();
        }
    }

}
