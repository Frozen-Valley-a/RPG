using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat combat;
    public AttackSpellManager attackspell;

    public static event Action<int> OnUnlockSpell;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;

    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "最大生命值提升":
                StatsManager.Instance.UpdateMaxHealth(1);
                break;
            case "劈砍":
                combat.enabled = true;
                break;
            case "燃烧":
                OnUnlockSpell?.Invoke(0);
                break;
            default:
                Debug.LogWarning("未知技能:" + skillName);
                break;
        }



    }

}
