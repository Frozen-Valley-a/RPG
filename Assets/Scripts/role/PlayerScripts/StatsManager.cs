using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

    public static StatsManager Instance;

    public TMP_Text healthText;

    [Header("战斗数值")]


    public int damage;            //伤害

    public float weaponRange;     //攻击范围

    public float knockbackForce;  //击退力度

    public float knockbackTime;   //击退时间

    public float stunTime;        //眩晕时间




    [Header("移动数值")]


    public int speed;             //速度



    [Header("生命数值")]

    public int maxHealth;         //最大生命值

    public int currentHealth;     //当前生命值

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP:" + currentHealth + "/" + maxHealth;
    }





}
