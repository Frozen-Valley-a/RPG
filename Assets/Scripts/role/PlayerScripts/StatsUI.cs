using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;


    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsCanvas.alpha >= 0.9f) 
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Open()
    {
        UpdateAllStats();
        statsCanvas.alpha = 1;
        statsCanvas.interactable = true;
        ContinueUI.Instance.Push(statsCanvas);
    }

    public void Close()
    {
        statsCanvas.alpha = 0;
        statsCanvas.interactable = false;
        ContinueUI.Instance.RemoveIfTop(statsCanvas);
    }


    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "伤害" + -StatsManager.Instance.damage;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "速度" + StatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}