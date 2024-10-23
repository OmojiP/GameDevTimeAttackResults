using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubutuShougiKoma
{
    public DoubutuShougiKomaType komaType;
    public DoubutuShougiPlaySideType playSide;

    public DoubutuShougiKoma(DoubutuShougiKomaType komaType, DoubutuShougiPlaySideType playSide)
    {
        this.komaType = komaType;
        this.playSide = playSide;
    }
}

