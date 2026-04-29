using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{

    public float spellRange = 2;
    //技能范围



    public void FinishSpell()
    //完成攻击
    {
        Destroy(gameObject, 0.1f);
    }


    private void OnDrawGizmosSelected()
    //游戏内攻击辅助线显示
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,spellRange );
    }


}



