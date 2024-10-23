using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCasualAreaMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        if(ShootCasualGameStatus.isGameover) return;

        
        transform.position += Vector3.down * (moveSpeed * Time.deltaTime);
    }
}
