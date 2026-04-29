using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spell/Spell")]
public class SpellSO : ScriptableObject
{
    public string spellName;
    public Sprite spellIcon;
    //精灵类型,技能图标
    public float cooldown;

    public float spellRange;

    public int damage;

    public GameObject spellScripts;

    public bool unlock = false;

}
