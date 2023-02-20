using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float RollSpeed = 90f;

    private float aForward;
    private float aRoll;

    private float fAcceleration = 2f;
    private float rAcceleration = 3.5f;

    public float speedOfCamera;

    public AudioSource engine;

    private GameObject[] Rockets;
    private bool RocketsOn;
    private Vector2 look, screenCenter, mouseDistance;

    // Check to see if player has hit boundary
    private bool hitBoundary = false;
    public Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Rockets = GameObject.FindGameObjectsWithTag("PlayerRocket");
        RocketsOn = true;
        ToggleRockets();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !engine.isPlaying)
            engine.Play();
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W))
            ToggleRockets();
        look.x = Input.mousePosition.x;
        look.y = Input.mousePosition.y;

        mouseDistance.x = (look.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (look.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        aRoll = Mathf.Lerp(aRoll, Input.GetAxis("Horizontal"), rAcceleration * Time.deltaTime);

        transform.Rotate(mouseDistance.y * speedOfCamera * Time.deltaTime, mouseDistance.x * speedOfCamera * Time.deltaTime, aRoll * RollSpeed * Time.deltaTime);

        aForward = Mathf.Lerp(aForward, Input.GetAxis("Vertical") * forwardSpeed, fAcceleration * Time.deltaTime);

        if (!hitBoundary)
        {
            dir = transform.forward;
        }
        this.transform.position += dir * aForward * Time.deltaTime;
       
    }

    private void ToggleRockets()
    {
        if (RocketsOn)
        {
            foreach (GameObject g in Rockets)
            {
                g.GetComponent<ParticleSystem>().Stop();
                g.GetComponent<ParticleSystem>().Clear();
            }
            RocketsOn = false;
        } else
        {
            foreach (GameObject g in Rockets)
            {
                g.GetComponent<ParticleSystem>().Play();
            }
            RocketsOn = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(Bounce());
        switch (collider.gameObject.tag)
        {
            case "BoundaryX":
                dir.x *= Random.Range(-2.0f, -1.0f);
                break;
            case "BoundaryY":
                dir.y *= Random.Range(-2.0f, -1.0f);
                break;
            case "BoundaryZ":
                dir.z *= Random.Range(-2.0f, -1.0f);
                break;
            default:
                break;
        }
    }

    IEnumerator Bounce()
    {
        hitBoundary = true;
        yield return new WaitForSeconds(0.5f);
        hitBoundary = false;
    }
}
