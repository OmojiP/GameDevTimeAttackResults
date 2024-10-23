using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoubutuShougiMapView : MonoBehaviour
{
    [SerializeField] private Text statusText;
    
    [SerializeField] Transform mapParentTransform;
    [SerializeField] Transform komaParentTransform;
    
    [SerializeField] GameObject mapPrefab;
    [SerializeField] private DoubutuShougiKomaView[] komaPrefabs;

    [SerializeField] private DoubutuShougiPlayerInput playerInput;
    
    Dictionary<DoubutuShougiKomaType, DoubutuShougiKomaView> komaPrefabDictionary;

    private DoubutuShougiMap map;
    
    private DoubutuShougiKomaView[,] komaViews = new DoubutuShougiKomaView[DoubutuShougiMap.mapW, DoubutuShougiMap.mapH]; 
    
    private DoubutuShougiGameStateManager gameStateManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.FindObjectOfType<DoubutuShougiPlayerInput>();

        komaPrefabDictionary = new();

        foreach (var pre in komaPrefabs)
        {
            komaPrefabDictionary.Add(pre.komaType, pre);
        }

        map = new();
        map.onKomaMoved += OnKomaMoved;
        map.onWin += (DoubutuShougiPlaySideType p) => { statusText.text = $"{p.ToString()}の勝利!!";};

        GenerateMap(map);
        
        gameStateManager = new(map);
        playerInput.onButtonClick += gameStateManager.OnSuareSelected;

    }

    private void GenerateMap(DoubutuShougiMap map)
    {
        for (int z = 0; z < DoubutuShougiMap.mapH; z++)
        {
            for (int x = 0; x < DoubutuShougiMap.mapW; x++)
            {
                Instantiate(mapPrefab, new Vector3(x, 0, z), Quaternion.identity, mapParentTransform);

                if (map.map[x, z].komaType != DoubutuShougiKomaType.None)
                {
                    var komaview = Instantiate(komaPrefabDictionary[map.map[x,z].komaType], new Vector3(x, 0, z), Quaternion.identity, komaParentTransform);
                    komaview.SetPlaySide(map.map[x, z].playSide);
                    
                    komaViews[x, z] = komaview;
                }
            }
        }
    }

    public void OnKomaMoved((int x, int y) komaPos, (int x, int y) dstPos)
    {
        Destroy(komaViews[dstPos.x, dstPos.y]?.gameObject);
        komaViews[dstPos.x, dstPos.y] = komaViews[komaPos.x, komaPos.y];
        komaViews[komaPos.x, komaPos.y] = null;

        komaViews[dstPos.x, dstPos.y].transform.position = new Vector3(dstPos.x, 0, dstPos.y); ;
    }
}
