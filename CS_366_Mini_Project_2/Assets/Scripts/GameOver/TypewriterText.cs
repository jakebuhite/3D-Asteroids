using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour
{
    public float InitialDelay = 0.1f;
    public float DelayPerChar = 0.1f;

    public enum TextType { title, score, highscore };
    public TextType textType;

    private string TxtTemp; // temporarily stores text
    private TMP_Text Txt;

    // Start is called before the first frame update
    void Start()
    {
        Txt = this.GetComponent<TMP_Text>();
        switch (textType)
        {
            case TextType.highscore:
                TxtTemp = "Highscore: " + PlayerPrefs.GetInt("high_score");
                break;
            case TextType.score:
                TxtTemp = "Score: " + PlayerPrefs.GetInt("recent_score");
                break;
            case TextType.title:
                TxtTemp = "Game Over";
                break;
            default:
                break;
        }
        StartCoroutine(TypeWriter(TxtTemp));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeWriter(string Temp)
    {
        yield return new WaitForSeconds(InitialDelay);
        foreach (char c in Temp)
        {
            Txt.text += c;
            yield return new WaitForSeconds(DelayPerChar);
        }
    }

}
