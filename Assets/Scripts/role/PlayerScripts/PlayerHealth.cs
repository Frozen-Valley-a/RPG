using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//玩家生命值
public class PlayerHealth : MonoBehaviour
{

    public TMP_Text healthText;
    //血量文本

    public Animator healthTextAnim;
    //动画引用

    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        //初始化生命值文本
    }



    //改变生命值方法
    public void ChangeHealth(int amount)
        //生命发生变化传入一个数值变量
    {
        StatsManager.Instance.currentHealth += amount;
        //当前生命值加变量(正数或负数),实现生命值的加减

        healthTextAnim.Play("TextUpdate");
        //播放TextUpdate动画


        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        //更新生命值文本

        if (StatsManager.Instance.currentHealth <=0)
            //如果生命值小于等于0
        {
            gameObject.SetActive(false);
            //关闭游戏对象

        }

    }

}
