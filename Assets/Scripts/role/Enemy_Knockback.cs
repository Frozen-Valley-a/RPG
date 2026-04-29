using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    //刚体

    private Enemy_Movement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //获取刚体组件

        enemy_Movement = GetComponent<Enemy_Movement>();

    }


    public void Knockback(Transform playerTransform,float knockbackForce,float knockbackTime, float stunTime)
        //被击退方法,获取玩家位置和击退力度
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);

        StartCoroutine(StunTimer(knockbackTime,stunTime));

        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockbackForce;

        Debug.Log("击退");
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }



}
