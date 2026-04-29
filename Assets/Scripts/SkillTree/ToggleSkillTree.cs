using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开关技能树界面
public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillCanves;

    private bool skillTreeOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                //游戏时间启用
                skillCanves.alpha = 0;
                //技能树界面关闭(透明度设为0)
                skillCanves.blocksRaycasts = false;
                //阻挡射线投射关闭
                skillTreeOpen = false;
                //技能树界面状态设为关闭
            }
            else
            {
                Time.timeScale = 0;
                skillCanves.alpha = 1;
                skillCanves.blocksRaycasts = true;
                skillTreeOpen = true;
                
            }
        }


    }


}
