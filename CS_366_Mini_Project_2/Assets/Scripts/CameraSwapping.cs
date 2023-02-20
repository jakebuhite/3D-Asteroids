using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapping : MonoBehaviour
{

    public GameObject thirdPerson;
    public GameObject firstPerson;
    private int CamMode;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (CamMode == 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }
            StartCoroutine(camSwitching());
        }
    }

    IEnumerator camSwitching()
    {
        yield return new WaitForSeconds(0.1f);
        if (CamMode == 1)
        {
            thirdPerson.SetActive(false);
            firstPerson.SetActive(true);
        }
        if (CamMode == 0)
        {
            thirdPerson.SetActive(true);
            firstPerson.SetActive(false);
        }
    }
}
