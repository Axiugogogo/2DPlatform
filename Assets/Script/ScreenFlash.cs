using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreenFlash : MonoBehaviour
{

    public Image img;
    public float time;
    public Color flashColor;

    private Color defultColor;
    // Start is called before the first frame update
    void Start()
    {
        defultColor = img.color;
    }


    public void FlashScreen()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        img.color = flashColor;
        yield return new WaitForSeconds(time);
        img.color = defultColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
