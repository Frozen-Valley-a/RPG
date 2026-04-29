using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapClick : MonoBehaviour
{

    [Header("关联UI（拖入即可）")]
    public RectTransform minimapRect; // 小地图的RectTransform（拖入小地图Image/RawImage）
    public RectTransform frameRect;   // 边框的RectTransform（拖入白色边框Image）

    private float originalZ;           // 原始 Z 轴位置
    // 小地图边缘（中心0,0）
    private float mapLeft;
    private float mapRight;
    private float mapBottom;
    private float mapTop;

    void Start()
    {
        // 记录摄像机初始Z轴，保持不变
        originalZ = transform.position.z;

    }

    void Update()
    {
        // 每次Update都重新获取最新宽高（应对UI缩放/动态调整）
        UpdateMapAndFrameBounds();
        Vector3 moveDir = Vector3.zero;
    }

    // 自动计算小地图边缘（从RectTransform获取真实宽高）
    void UpdateMapAndFrameBounds()
    {
        if (minimapRect == null) return;

        // 小地图总宽高（从RectTransform获取真实显示尺寸）
        float mapWidth = minimapRect.rect.width;
        float mapHeight = minimapRect.rect.height;

        // 计算小地图边缘（中心0,0）
        mapLeft = -mapWidth / 2;    // 小地图左边缘 = 中心0 - 半宽
        mapRight = mapWidth / 2;    // 小地图右边缘 = 中心0 + 半宽
        mapBottom = -mapHeight / 2; // 小地图下边缘 = 中心0 - 半高
        mapTop = mapHeight / 2;     // 小地图上边缘 = 中心0 + 半高
    }



}

