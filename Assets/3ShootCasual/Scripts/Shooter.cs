using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShootCasualBullet bulletPrefab;

    [SerializeField] private float shootSpan;
    private float startTime;

    public int attack = 1;
    
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShootCasualGameStatus.isGameover) return;

        
        if (Time.time - startTime >= shootSpan)
        {
            startTime = Time.time;

            var bu = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bu.attack = attack;
        }
    }
}
