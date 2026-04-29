using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人运动
public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    //移动速度

    public float attackRange = 2;
    //攻击状态范围

    public float attackCooldown = 1;
    //攻击冷却

    public float playerDatectRange = 4;
    //玩家检测距离

    public Transform detectionPoint;
    //检测点

    public LayerMask playerLayer;
    //玩家层级

    private float attackCooldownTimer;
    //攻击冷却计时器


    private int facingDirection = -1;
    //朝向，左是-1，右是1

    private EnemyState enemyState;
    //枚举敌人状态

    private Rigidbody2D rb;
    //刚体引用

    private Transform player;
    //获取玩家的位置组件

    private Animator anim;
    //调用动画器

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //获取自己身上的刚体组件赋值给rb

        anim = GetComponent<Animator>();
        //获取自己身上的动画器组件赋值给anim

        ChangeState(EnemyState.Idle);
        //更改状态为闲置

    }


    void Update()
    {

        if (enemyState != EnemyState.Knockback)
            //如果不是处于被击退状态执行下面代码
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }



            if (enemyState == EnemyState.Chasing)
            //当前是否处于追击状态
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            //当前状态是否为攻击
            {
                rb.velocity = Vector2.zero;
                //攻击时速度设为0
            }
        }

    }



    void Chase()
        //追击状态逻辑
    {
        
        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        //如果玩家在敌人右边,敌人朝向左边,...左边...朝向右边时,需要反转
        {
            Flip();
            //调用反转方法
        }

        Vector2 direction = (player.position - transform.position).normalized;
        //玩家位置减敌人位置,获取到的向量归一化(去除距离影响,得到一个距离为1的方向)

        rb.velocity = direction * speed;
        //向量乘速度赋值给刚体的速度
    }






    void Flip()
        //反转方法
    {
        facingDirection *= -1;
        //使敌人朝向标记反转
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
        //反转敌人图形


    }


    private void CheckForPlayer()
        //如果有碰撞体进入范围
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            detectionPoint.position,
            playerDatectRange,
            playerLayer);
        //获取检测点为原点玩家检测距离为半径,检测玩家层级

        if (hits.Length > 0)
        //如果检测到玩家
        {
            player = hits[0].transform;
            //获取玩家


            if (Vector2.Distance(transform.position, player.position) < attackRange &&
                attackCooldownTimer <= 0)
            //如果与玩家的距离小于攻击距离并且
            //攻击冷却时间小于等于0
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
                //变为攻击状态
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange
                &&enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
                //动画状态改为追击状态
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            //把速度设为0

            ChangeState(EnemyState.Idle);
            //变为空闲状态
        }

        
    }



    public void ChangeState(EnemyState newState)
        //更新状态
    {
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        //退出当前动画



        enemyState = newState;
        //获取新动画

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        //进入新动画

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDatectRange);
    }


}

public enum EnemyState
//敌人状态枚举
{

    Idle,
    //闲置
    Chasing,
    //追击
    Attacking,
    //攻击
    Knockback,
    //被击退


}
