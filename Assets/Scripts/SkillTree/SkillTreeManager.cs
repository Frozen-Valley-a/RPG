using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{

    public SkillSlot[] skillSlots;
    public TMP_Text pointsText;

    public int availablePoints;
    //可用技能点


    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints;


    }



    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        //遍历技能槽按钮,批量绑定TryUpgradeSkill升级技能方法(按钮绑定升级技能)
        {
            slot.skillButton.onClick.AddListener(()=>CheckAvailablePoints(slot));
        }
        UpdateAbilityPoints(0);
    }

    private void CheckAvailablePoints(SkillSlot slot)
        //检查技能点是否足够
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }



    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
        //收到(技能点消耗)订阅触发,如果技能点大于0,则技能点减1
    {
        if (availablePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    //收到(技能点升满)订阅触发
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill()) { 


            slot.Unlock();
            }
        }
    }


    public void UpdateAbilityPoints(int amount)
        //更新技能点方法
    {
        availablePoints += amount;
        pointsText.text = "可用技能点:" + availablePoints;
    }


}
