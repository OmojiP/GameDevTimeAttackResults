using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActionAdventureEnemy : MonoBehaviour
{
    private int hp = 5;

    private float walkSpan;
    private float timeStart;

    private void Start()
    {
        walkSpan = 1f;
        timeStart = Time.time;
    }

    private void Update()
    {
        if (Time.time - timeStart > walkSpan)
        {
            RandomWolk();
            timeStart = Time.time;
        }
    }

    void RandomWolk()
    {
        Vector2[] dirs = GetMovableDirection();

        // Debug.Log($"Moce Check {dirs.Length}");
        
        if (dirs.Length > 0)
        {
            var dir = dirs[Random.Range(0, dirs.Length)];
            transform.position += (Vector3)dir;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("EnterTrigger" + other.gameObject.name);
        
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log($"Playerの攻撃が当たった {hp} -> {hp -1}");
            hp--;

            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }


    private Vector2[] GetMovableDirection()
    {
        GameObject[] objs;
        List<Vector2> movableDir = new List<Vector2>();
        
        if (!ObjectChecker.TryGetNearestObject(transform.position, Vector2.up, out objs))
        {
            movableDir.Add(Vector2.up);
        }
        if (!ObjectChecker.TryGetNearestObject(transform.position, Vector2.down, out objs))
        {
            movableDir.Add(Vector2.down);
        }
        if (!ObjectChecker.TryGetNearestObject(transform.position, Vector2.left, out objs))
        {
            movableDir.Add(Vector2.left);
        }
        if (!ObjectChecker.TryGetNearestObject(transform.position, Vector2.right, out objs))
        {
            movableDir.Add(Vector2.right);
        }

        return movableDir.ToArray();
    }
}
