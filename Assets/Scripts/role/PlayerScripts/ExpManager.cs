using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{

    public int level;
    //等级

    public int currentExp;
    //当前经验

    public int expToLevel = 10;
    //下一级所需经验

    public float expGrowthMultiplier = 1.2f;
    //下一级所需经验倍数

    public int updateSkillPoints = 1;
    //更新技能点(升级)

    public Slider exSlider;
    //滑块引用

    public TMP_Text currentLevelText;
    //当前等级文本引用


    public static event Action<int> OnLevelUp;
    //添加升级广播


    private void Start()
    {
        //更新ui
        UpdateUI();
    }

    private void Update()
    {

        //测试用代码按回车给2经验////
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
        ////////////////////////////



    }


    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
        //获得经验 时,增加当前经验,如果大于经验上限则调用升级方法,更新ui
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void LevelUp()
        //升级,等级加1,当前经验减掉当前升级所需经验,升级所需经验设为1.2倍的经验倍数的整数
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);


        OnLevelUp?.Invoke(updateSkillPoints);
        //广播升级,获得updateSkillPoints个升级点数

    }

    public void UpdateUI()
        //更新ui
    {
        exSlider.maxValue = expToLevel;
        exSlider.value = currentExp;
        currentLevelText.text = "Level:" + level;


    }

}
