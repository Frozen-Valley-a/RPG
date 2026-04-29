using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpellManager : MonoBehaviour
{

    public List<SpellSO> spellSO;

    private Dictionary<SpellSO, float> spellCDDict = new Dictionary<SpellSO, float>();

    public LayerMask enemyLayer;
    //层级引用,选择敌人层级

    private Transform player;

    private void OnEnable()
    {
        SkillManager.OnUnlockSpell += unlock;

    }
    private void OnDisable()
    {
        SkillManager.OnUnlockSpell -= unlock;
    }



    private void Start()
    {

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("未找到玩家对象");
        }



        foreach (var spell in spellSO)
        {
            spellCDDict[spell] = 0;
            spell.unlock = false;
        }


    }

    private void Update()
    {
        TryCastSpells();
        UpdateCooldowns();
    }

    void TryCastSpells()
    {
        if (spellSO.Count >0 && Input.GetButtonDown("BurnSpell"))
        {
            CastSkill(0);
        }
    }



    public void CastSkill(int index)
    {
        if (index<0 || index >=spellSO.Count)return ;
        SpellSO spell = spellSO[index];

        if (!spell.unlock) return;
        if (spellCDDict[spell] > 0) return;
        if (player == null) return;


        

        GameObject spellObj =  Instantiate(spell.spellScripts.gameObject,
                player.position,   // 这里直接使用玩家位置
                Quaternion.identity
                );


        DealDamage(spellObj, spell.spellRange, spell.damage);

        spellCDDict[spell] = spell.cooldown;

            
         
        
    }






    public void DealDamage(GameObject spell,float spellRange,int damage)
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
                spell.transform.position,
                spellRange,
                enemyLayer);


        foreach (var col in enemies)
        {
            Enemy_Health health = col.GetComponent<Enemy_Health>();
            if (health != null)
            {
                health.ChangeHealth(damage);
            }

        }

    }



    void UpdateCooldowns()
    {

        // 所有技能CD倒计时
        List<SpellSO> keys = new List<SpellSO>(spellCDDict.Keys);
        foreach (var spell in keys)
        {
            if (spellCDDict[spell] > 0)
            {
                spellCDDict[spell] -= Time.deltaTime;

                if (spellCDDict[spell] < 0)
                    spellCDDict[spell] = 0;
            }
        }

    }


    public void unlock(int sort)
    {
        if (sort >= 0 && sort < spellSO.Count)
        {
            spellSO[sort].unlock = true;
        }


    }


}




