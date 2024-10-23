using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FlickPuzzleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FlickPuzzleModel puzzleModel = new FlickPuzzleModel();

        puzzleModel.CreateNewPuzzle().Forget();
    }
}
