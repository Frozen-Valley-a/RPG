using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//开关技能树界面
public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillCanves;


    private void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillCanves.alpha >=0.9)
            {
                Time.timeScale = 1;
                //游戏时间启用
                skillCanves.alpha = 0;
                //技能树界面关闭(透明度设为0)
                skillCanves.blocksRaycasts = false;
                //阻挡射线投射关闭


                ContinueUI.Instance.RemoveIfTop(skillCanves);

            }
            else
            {
                Time.timeScale = 0;
                skillCanves.alpha = 1;
                skillCanves.blocksRaycasts = true;


                ContinueUI.Instance.Push(skillCanves);

            }
        }


    }


}
