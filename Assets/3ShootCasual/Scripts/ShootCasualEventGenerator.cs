using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootCasualEventGenerator : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    private Canvas canvas;
    
    [SerializeField] private ShootCasualEventArea[] eventAreaIncreasePrefabs;
    [SerializeField] private ShootCasualEventArea[] eventAreaDecreasePrefabs;
    [SerializeField] private ShootCasualFollowUI followTextPrefab;

    private void Start()
    {
        ShootCasualGameStatus.isGameover = false;
        
        canvas = canvasTransform.GetComponent<Canvas>();
        
        startTime = Time.time;
        PopEventAreas();
    }

    private float generateSpan = 3f;
    private float startTime;
    
    private void Update()
    {
        if(ShootCasualGameStatus.isGameover) return;

        
        if(Time.time - startTime >= generateSpan)
        {
            Debug.Log("生成");
            
            startTime = Time.time;
            if (generateSpan >= 2f)
            {
                generateSpan *= 0.99f;
            }
            PopEventAreas();
        }
    }

    private void PopEventAreas()
    {
        bool isRightIncrease =  Random.Range(0f, 1f) >= 0.5f;
        
        PopEventAreaLeft(isRightIncrease);
        PopEventAreaRight(isRightIncrease);
    }
    private void PopEventAreaRight(bool isRightIncrease)
    {
        ShootCasualEventArea eventAreaPrefab;
        if (!isRightIncrease)
        {
            eventAreaPrefab = eventAreaDecreasePrefabs[Random.Range(0, eventAreaDecreasePrefabs.Length)];
        }
        else
        {
            eventAreaPrefab = eventAreaIncreasePrefabs[Random.Range(0, eventAreaIncreasePrefabs.Length)];
        }
        
        var ea = Instantiate(eventAreaPrefab, new Vector3(1.42f, 10, 0), Quaternion.identity);
        var ft = Instantiate(followTextPrefab, canvasTransform);
        ft.FollowStart(ea.transform, ea, canvas);
    }
    private void PopEventAreaLeft( bool isRightIncrease)
    {
        ShootCasualEventArea eventAreaPrefab;
        if (isRightIncrease)
        {
            eventAreaPrefab = eventAreaDecreasePrefabs[Random.Range(0, eventAreaDecreasePrefabs.Length)];
        }
        else
        {
            eventAreaPrefab = eventAreaIncreasePrefabs[Random.Range(0, eventAreaIncreasePrefabs.Length)];
        }
        
        var ea = Instantiate(eventAreaPrefab, new Vector3(-1.42f, 10, 0), Quaternion.identity);
        var ft = Instantiate(followTextPrefab, canvasTransform);
        ft.FollowStart(ea.transform, ea, canvas);
    }
    
    
}
