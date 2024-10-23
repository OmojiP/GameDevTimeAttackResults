using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCasualBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int attack;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }
}
