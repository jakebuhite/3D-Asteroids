using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSM : MonoBehaviour
{

    public float DelayTime = 4.2f;
    public Button PlayAgainBtn;

    // Start is called before the first frame update
    void Start()
    {
        PlayAgainBtn.gameObject.SetActive(false);
        StartCoroutine(DelayButton());
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    IEnumerator DelayButton()
    {
        yield return new WaitForSeconds(DelayTime);
        PlayAgainBtn.gameObject.SetActive(true);
    }
}
