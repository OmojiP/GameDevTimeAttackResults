using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubutuShougiKomaView : MonoBehaviour
{
    public DoubutuShougiKomaType komaType;
    
    public void SetPlaySide(DoubutuShougiPlaySideType playSide)
    {
        if (playSide == DoubutuShougiPlaySideType.Ground)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }   
    }
}
