using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitButton : MonoBehaviour//控制游玩界面得不消失内容
{
    private GameObject lastSelect;

    // Start is called before the first frame update
    void Start()
    {
        lastSelect = new GameObject();

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)//如果没有，就默认是最后一个内容
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject; 
        }
    }
}
