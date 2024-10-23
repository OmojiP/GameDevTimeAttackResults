using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Shooter))]
public class ShootCasualPlayers : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private ShootCasualFollowUI followTextPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject button;
    private Transform canvasTransform;
    
    [SerializeField] private float moveLimit;
    [SerializeField] private float groupWidthLimit;
    
    private Camera mainCamera;

    public event Action<int> onValueChanged;
    
    void Start()
    {
        mainCamera = Camera.main;
        shooter = GetComponent<Shooter>();
        
        canvasTransform = canvas.transform;
        gameOverText.gameObject.SetActive(false);
        button.SetActive(false);
        
        var ft = Instantiate(followTextPrefab, canvasTransform);
        ft.FollowStart(transform, this, canvas);
        
        onValueChanged?.Invoke(shooter.attack);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShootCasualGameStatus.isGameover) return;
        
        // マウスのスクリーン座標を取得
        Vector3 mouseScreenPosition = Input.mousePosition;

        // マウスのスクリーン座標をワールド座標に変換
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, mainCamera.nearClipPlane));

        if (mouseWorldPosition.x > moveLimit)
        {
            mouseWorldPosition.x = moveLimit;
        }
        else if (mouseWorldPosition.x < -moveLimit)
        {
            mouseWorldPosition.x = -moveLimit;
        }
        
        // GameObjectの位置を更新
        transform.position = new Vector3(mouseWorldPosition.x, transform.position.y, transform.position.z);
    }

    void PopShooters(int popCount)
    {
        shooter.attack += popCount;

        if (shooter.attack <= 0)
        {
            Debug.Log("GameOver");
            gameOverText.gameObject.SetActive(true);
            button.SetActive(true);
            ShootCasualGameStatus.isGameover = true;
        }
        
        onValueChanged?.Invoke(shooter.attack);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EventArea"))
        {
            PopShooters(other.gameObject.GetComponent<ShootCasualEventArea>().Value);
            other.gameObject.SetActive(false);
        }
    }

    public void OnrestratButton()
    {
        SceneManager.LoadScene("Game");
    }
}
