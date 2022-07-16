using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{

    public RectTransform UI_Element;//
    public RectTransform CanvasRect;//这两个很重要

    public Transform trashBinPos;//垃圾桶的坐标
    public float xOffset;
    public float yOffset;
    public Text coinNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(trashBinPos.position);//将垃圾桶的世界坐标转换成视口坐标
        Vector2 worldObjectScreenPos = new Vector2((viewportPos.x * CanvasRect.sizeDelta.x) -
                                                    (CanvasRect.sizeDelta.x * 0.5f) + xOffset,
                                                    (viewportPos.y * CanvasRect.sizeDelta.y) - 
                                                    (CanvasRect.sizeDelta.y * 0.5f) + yOffset);
        UI_Element.anchoredPosition = worldObjectScreenPos;
    }
}
