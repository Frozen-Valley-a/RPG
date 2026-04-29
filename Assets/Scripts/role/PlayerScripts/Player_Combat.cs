using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    public Transform attackPoint;
    //攻击点

    public LayerMask enemyLayer;
    //层级引用,选择敌人层级


    public Animator anim;

    public float cooldown = 0.5f;
    //攻击冷却时间

    private float timer;
    //冷却时间计时器


    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            //如果冷却时间大于0那么冷却时间开始运行
        }

    }



    public void Attack()
    {
        if (timer <=0)
            //冷却时间小于等于0
        {
            anim.SetBool("isAttacking", true);
            //攻击动画设为true

            
            timer = cooldown;
            //重置冷却时间计时器

        }
    }

    public void DealDamage()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
                attackPoint.position,
                StatsManager.Instance.weaponRange,
                enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(StatsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(
                transform,
                StatsManager.Instance.knockbackForce,
                StatsManager.Instance.knockbackTime,
                StatsManager.Instance.stunTime);
        }

    }



    public void FinishAttacking()
        //完成攻击(攻击动画结束)
    {
        anim.SetBool("isAttacking", false);
        //攻击动画设为false
    }


    private void OnDrawGizmosSelected()
        //游戏内攻击辅助线显示
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

}
