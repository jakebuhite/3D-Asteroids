using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSM : MonoBehaviour
{

    public int speed;
    public Vector3 direction;

    public GameObject shipPrefab;

    private bool isFlying = true;
    private GameObject ship;
    new private Renderer renderer;
    private bool isInvisible;

    // Start is called before the first frame update
    void Start()
    {
        ship = Instantiate(shipPrefab);
        renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(direction.x, direction.y, 0);
        //transform.position *= WrapShip();
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator FlyingShip()
    {
        yield return new WaitForSeconds(2);
    }

    private Vector3 WrapShip()
    {
        if (renderer.isVisible)
        {
            isInvisible = false;
            return new Vector3(1, 1, 1);
        }

        if (isInvisible)
        {
            return new Vector3(1, 1, 1);
        }

        float xFix = 1;
        float yFix = 1;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1)
        {
            xFix = -1;
        }
        if (pos.y < 0 || pos.y > 1)
        {
            yFix = -1;
        }
        isInvisible = true;
        return new Vector3(xFix, yFix, 1);
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
