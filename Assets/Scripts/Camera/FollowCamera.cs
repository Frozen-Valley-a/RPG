using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    Vector3 distance;

    private void Start()
    {
        transform.position = player.position + transform.position;
        //把玩家设置到屏幕正中心

        distance = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = player.position + distance;
    }


}
