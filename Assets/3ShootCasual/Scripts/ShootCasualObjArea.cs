using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCasualObjArea : MonoBehaviour
{
    private int hp = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp--;

            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
