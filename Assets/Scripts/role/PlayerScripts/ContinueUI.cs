using System.Collections.Generic;
using UnityEngine;

public class ContinueUI : MonoBehaviour
{
    public static ContinueUI Instance { get; private set; }

    public CanvasGroup escCanvas;  


    private Stack<CanvasGroup> uiStack = new Stack<CanvasGroup>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (uiStack.Count > 0)
            {
                CloseTopUI();
            }
            else
            {
                OpenEscMenu();
            }
        }
    }

    public void Push(CanvasGroup canvas)
    {
        if (canvas == null) return;

        if (uiStack.Count == 0)
        {
            Time.timeScale = 0f;
        }


        uiStack.Push(canvas);
        canvas.alpha = 1;
        canvas.interactable = true;
    }


    public void CloseTopUI()
    {
        if (uiStack.Count == 0) return;

        CanvasGroup top = uiStack.Pop();
        top.alpha = 0;
        top.interactable = false;


        if (uiStack.Count > 0)
        {
            uiStack.Peek().interactable = true;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void RemoveIfTop(CanvasGroup canvas)
    {
        if (uiStack.Count > 0 && uiStack.Peek() == canvas)
        {
            CloseTopUI();
        }
        else
        {
            Debug.LogWarning($"尝试关闭非栈顶 UI: {canvas.name}");
        }
    }


    private void OpenEscMenu()
    {
        if (uiStack.Contains(escCanvas)) return;

        Push(escCanvas);
    }

}