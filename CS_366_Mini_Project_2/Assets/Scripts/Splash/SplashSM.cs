using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSM : MonoBehaviour
{
    public int speed;
    public Vector3 direction;

    public GameObject shipPrefab;

    private GameObject ship;
    new private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        ship = Instantiate(shipPrefab);
        renderer = ship.GetComponent<Renderer>();
        Cursor.visible = true;
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
                StartCoroutine(SpawnShip());
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator SpawnShip() {
        yield return new WaitForSeconds(1);
        ship = Instantiate(shipPrefab);
        renderer = ship.GetComponent<Renderer>();
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
