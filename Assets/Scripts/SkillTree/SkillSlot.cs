using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;

    public SkillSO skillSO;

    public int currentLevel;
    //当前等级

    public bool isUnlocked;
    //是否解锁

    public Image skillIcon;
    //技能图标

    public Button skillButton;

    public TMP_Text skillLevelText;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    //添加技能点消耗广播

    public static event Action<SkillSlot> OnSkillMaxed;
    //添加当前技能升满广播


    private void OnValidate()
        //游戏没开始时有任何变动就运行
        //更新ui
    {

        if(skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }

    }

    public void TryUpgradeSkill()
    //升级技能,如果技能解锁,并且当前等级小于等级上限,则
    //技能等级加一,发出广播该技能消耗技能点,如果
    //当前技能等级大于等于技能上限,则发出广播技能升满
    //更新ui
    {
        if (isUnlocked && currentLevel <skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);

            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel <slot.skillSO.maxLevel)
            {
                return false;
            }
        }

        return true;
    }


    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }



    private void UpdateUI()
        //更新ui
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked)
        {
            skillButton.interactable = true;

            skillLevelText.text = currentLevel.ToString() +
                "/" +
                skillSO.maxLevel.ToString();

            skillIcon.color = Color.white;

        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "锁定";
            skillIcon.color = Color.gray;
        }

    }


}
