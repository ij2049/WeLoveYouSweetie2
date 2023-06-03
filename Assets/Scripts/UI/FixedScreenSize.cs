using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FixedScreenSize : MonoBehaviour
{
    private void Start()
        {
            SetWindowSize();
        }

        private void SetWindowSize()
        {
            // 기기의 화면 해상도 가져오기
            Resolution screenResolution = Screen.currentResolution;

            // 윈도우 창 크기 설정
            Screen.SetResolution(screenResolution.width, screenResolution.height, false);
        }

        //아래 코드는 창 크기 조절 가능 후에 버튼으로 써도 될듯
        
        /*[DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
    
        public void OnMinimizeWindow()
        {
            ShowWindow(GetActiveWindow(), 2);
        }
    
        public void OnFullWindow()
        {
            ShowWindow(GetActiveWindow(), 3);
        }*/
    
    //--------------------------------------------------------
    
    /*private int minWidth = 960;
    private int minHeight = 540;
    private int previousWidth;
    private int previousHeight;
    private int currentWidth;
    private int currentHeight;

    private void Start()
    {
        ResetInfo();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중에는 크기 조정을 하지 않음
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // 크기가 변경되었는지 확인
        if (currentWidth != previousWidth || currentHeight != previousHeight)
        {
            if (Screen.width < minWidth || Screen.height < minHeight)
            {
                SetWindowSize(minWidth, minHeight);
            }

            else
            {
                float desiredHeight = (9f / 16f) * Screen.width;
                SetWindowSize(Screen.width, Mathf.RoundToInt(desiredHeight));
            }
            
            // 현재 크기를 이전 크기로 저장
            previousWidth = currentWidth;
            previousHeight = currentHeight;
        }
    }

    private void ResetInfo()
    {
        previousWidth = Screen.width;
        previousHeight = Screen.height;
    }

    private void SetWindowSize(int width, int height)
    {
        Screen.SetResolution(width, height, false);
    }*/
}
