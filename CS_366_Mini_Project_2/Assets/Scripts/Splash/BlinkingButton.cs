using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    public float speed;

    private Color BtnColor;
    private Image BtnImg;
    private bool isFading = true;

    // Start is called before the first frame update
    void Start()
    {
        BtnImg = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color BtnColor = BtnImg.color;
        if (isFading)
        {
            BtnColor.a -= speed * Time.deltaTime;
            if (BtnColor.a <= 0.5)
            {
                isFading = false;
            }
        } else
        {
            BtnColor.a += speed * Time.deltaTime;
            if (BtnColor.a >= 1)
            {
                isFading = true;
            }
        }
        BtnImg.color = BtnColor;
    }
}
