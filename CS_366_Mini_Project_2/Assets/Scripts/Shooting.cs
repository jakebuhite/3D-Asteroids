using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float CooldownTime;
    public SceneManager Manager;
    public GameObject ShootPoint;

    public AudioSource Laser;
    public AudioSource Explosion;

    // Create visual effect for ray
    public float LineWidth = 0.1f;
    public float LineLength = 5f;
    private LineRenderer lineRenderer;
    private Vector3[] LinePositions = { Vector3.zero, Vector3.zero };

    private bool InCooldown;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.SetPositions(LinePositions);
        lineRenderer.startWidth = LineWidth;
        lineRenderer.endWidth = LineWidth;

        InCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !InCooldown)
        {
            Shoot();
            if(!Laser.isPlaying)
            {
            Laser.Play();
            }

            lineRenderer.enabled = true;
            StartCoroutine(StartCooldown());
        }
        else
        {
            LinePositions[0] = Vector3.zero;
            LinePositions[1] = Vector3.zero;
            lineRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new(this.transform.position, -this.transform.forward);
        Vector3 EndPoint = ray.GetPoint(100.0f);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.gameObject.CompareTag("LargeAsteroid") || hit.transform.gameObject.CompareTag("MediumAsteroid") || hit.transform.gameObject.CompareTag("SmallAsteroid"))
            {
                Manager.SplitAsteroid(hit.transform);
                EndPoint = hit.point;
                Explosion.Play();
                Destroy(hit.transform.gameObject);
            }
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Manager.UFOExplosion(hit.transform);
                Explosion.Play();
                Destroy(hit.transform.gameObject);
                Manager.UpdateScore(1000);
            }
        }
        LinePositions[0] = ShootPoint.transform.position;
        LinePositions[1] = EndPoint;
        lineRenderer.SetPositions(LinePositions);
    }

    IEnumerator StartCooldown()
    {
        InCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        InCooldown = false;
    }
}
