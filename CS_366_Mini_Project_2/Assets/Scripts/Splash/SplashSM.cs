using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashSM : MonoBehaviour
{
    public int speed;
    public Vector3 direction;

    public GameObject shipPrefab;
    public GameObject ufoPrefab;

    private GameObject ship;
    private GameObject ufo;
    new private Renderer renderer;

    public Button PlayBtn;
    public float DelayTime = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        // Instantiate Ship
        StartCoroutine(SpawnObject("ship"));

        // Insantiate UFO
        StartCoroutine(SpawnObject("ufo"));

        // Button
        PlayBtn.gameObject.SetActive(false);
        StartCoroutine(DelayButton());
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            ship.transform.position += new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, speed * direction.z * Time.deltaTime);
            if (!renderer.isVisible)
            {
                Destroy(ship);
                StartCoroutine(SpawnObject("ship"));
            }
        }
        if (ufo != null)
        {
            ufo.transform.position += new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, speed * direction.z * Time.deltaTime);
            if (!renderer.isVisible)
            {
                Destroy(ufo);
                StartCoroutine(SpawnObject("ufo"));
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator SpawnObject(string type)
    {
        if (type == "ship")
        {
            yield return new WaitForSeconds(1);
            ship = Instantiate(shipPrefab);
            renderer = ship.GetComponent<Renderer>();
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            ufo = Instantiate(ufoPrefab);
            renderer = ufo.GetComponent<Renderer>();
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    IEnumerator DelayButton()
    {
        yield return new WaitForSeconds(DelayTime);
        PlayBtn.gameObject.SetActive(true);
    }
}
