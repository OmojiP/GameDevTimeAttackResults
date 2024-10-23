using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoubutuShougiPlayerInput : MonoBehaviour
{
    public event Action<(int x,int y)> onButtonClick;
    
    public void OnButtonClick(int i)
    {
        onButtonClick?.Invoke(( i % DoubutuShougiMap.mapW , i / DoubutuShougiMap.mapW ));
    }
}
