using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootCasualFollowUI : MonoBehaviour
{
    // 追従するオブジェクト
    private Transform targetObject;
    // メインカメラ
    private Camera mainCamera;

    private bool isFollowStart = false;

    private Text text;
    
    
    private Canvas canvas;
 
    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0, 0);
    

    public void FollowStart(Transform target, ShootCasualEventArea eventArea, Canvas canvas)
    {
        targetObject = target;
        isFollowStart = true;
        this.canvas = canvas;
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        
        mainCamera = Camera.main;
        text = GetComponent<Text>();
        myRectTfm = GetComponent<RectTransform>();

        eventArea.onValueChanged += OnValueChanged;
    }
    public void FollowStart(Transform target, ShootCasualPlayers players, Canvas canvas)
    {
        targetObject = target;
        isFollowStart = true;
        this.canvas = canvas;
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        
        mainCamera = Camera.main;
        text = GetComponent<Text>();
        myRectTfm = GetComponent<RectTransform>();

        players.onValueChanged += OnValueChanged;
    }
    
    
    void Update() {
        
        if(ShootCasualGameStatus.isGameover) return;

        if (!isFollowStart) return;

        if (!targetObject.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        
        // Debug.Log("追従");
        
        Vector2 pos;
         
        switch (canvas.renderMode) {
 
            case RenderMode.ScreenSpaceOverlay:
                myRectTfm.position = RectTransformUtility.WorldToScreenPoint(mainCamera, targetObject.position + offset);
 
                break;
 
            case RenderMode.ScreenSpaceCamera:
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetObject.position + offset);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTfm, screenPos, mainCamera, out pos);
                myRectTfm.localPosition = pos;
                break;
 
            case RenderMode.WorldSpace:
                myRectTfm.LookAt(mainCamera.transform);
 
                break;
        }
    }


    void OnValueChanged(int value)
    {
        text.text = value.ToString();
    }
}
