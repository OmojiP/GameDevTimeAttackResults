using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCasualEventArea : MonoBehaviour
{
    [SerializeField] private ShootEventType shootEventType;
    
    [SerializeField] private int startValue = 10;
    [SerializeField] private int value;
    public int Value => value;
    
    public event Action<int> onValueChanged;
    
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        value = startValue;
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetAreaStatus(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            
            SetAreaStatus(other.GetComponent<ShootCasualBullet>().attack);
            other.gameObject.SetActive(false);
        }
    }

    void SetAreaStatus(int attack)
    {
        value += (int)shootEventType * attack.ToString().Length;

        if (value >= 0)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
        
        onValueChanged?.Invoke(value);
    }
    
}

public enum ShootEventType
{
    IncreaseShooter = 1,
    DecreaseShooter = -1
}
