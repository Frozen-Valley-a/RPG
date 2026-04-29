using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = -1;
    //触碰扣除生命值

    public Transform attackPoint;
    //攻击点

    public float weaponRange;
    //攻击命中范围

    public float knockbackForce;
    //击退等级

    public float stunTime;

    public LayerMask playerLayer;
    //层级引用,选择玩家层级


    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            weaponRange,
            playerLayer);
        //创建一个圆形区域,attackPoint,
        //position为原点,weaponRange为半径,寻找playerLayer层级的物体


        if (hits.Length > 0)
            //如果检测到玩家大于0
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(damage);
            //找到的第一个玩家层级的物体也就是玩家调用生命变化方法加-1伤害

            hits[0].GetComponent<PlayerMovement>().Knockback(transform,knockbackForce,stunTime);
            //获取玩家移动脚本,调用击退方法,传入敌人坐标(计算击退方向)
        }

    }


}
