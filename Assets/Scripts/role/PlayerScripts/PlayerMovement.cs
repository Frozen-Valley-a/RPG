using System.Collections;

using UnityEngine;

//移动
public class PlayerMovement : MonoBehaviour
{

    public int facingDirection = 1; //反转值,控制左右移动的动画

    public Rigidbody2D rb; //刚体

    public Animator anim;

    private bool isKnockedBack;   //禁用移动

    public Player_Combat player_Combat;
    //玩家战斗脚本引用


    private void Update()
    {
        if (Input.GetButtonDown("Slash") && player_Combat.enabled == true){
            player_Combat.Attack();
        }
    }




    //加上Fixed对比update--每秒运行50:每帧一次
    void FixedUpdate()
    {

        if (isKnockedBack == false) {
            //如果没有被禁用移动

            float horizontal = Input.GetAxis("Horizontal");
            //查询unity的输入管理器里的Horizontal条目负责监听左右方向键或者ad键

            float vertical = Input.GetAxis("Vertical");
            //上下方向键或者ws键 

            if (horizontal > 0 && transform.localScale.x < 0 ||
               horizontal < 0 && transform.localScale.x > 0)
            //如果按往左走,缩放x轴小于0(x=1是正常,x=-1是反转)
            //或者向右走，缩放x轴大于0
            {
                Flip();
                //调用反转玩家方法
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            // 将动画中的horizontal值改为监听水平方向的值的绝对值
            anim.SetFloat("vertical", Mathf.Abs(vertical));
            //改为垂直方向的绝对值


            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
            //获取水平垂直方向向量乘以速度,赋值给刚体
        }



    }

    void Flip()
        //反转玩家方法
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
            );
    }


    public void Knockback(Transform enemy,float force,float stunTime)
        //击退逻辑,需要敌人坐标计算击退向量,和击退等级
    {
        isKnockedBack = true;
        //禁用玩家移动
        Vector2 direction = (transform.position - enemy.position).normalized;
        //计算玩家和敌人的向量,归一化

        rb.velocity = direction * force;
        //把向量赋值给速度

        StartCoroutine(KnockbackCounter(stunTime));

    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;

    }



}











