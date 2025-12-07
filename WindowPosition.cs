using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WindowPosition : MonoBehaviour
{
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr GetActiveWindow();

    void Start()
    {
        CenterWindow();
    }

    void CenterWindow()
    {
        IntPtr windowHandle = GetActiveWindow();

        int width = Screen.width;
        int height = Screen.height;

        int screenW = Display.main.systemWidth;
        int screenH = Display.main.systemHeight;

        int posX = (screenW - width) / 2;
        int posY = (screenH - height) / 2;

        SetWindowPos(windowHandle, IntPtr.Zero, posX, posY, width, height,
            SWP_NOZORDER | SWP_NOSIZE);
    }
}