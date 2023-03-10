using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float CooldownTime;
    public SceneManager Manager;

    // Create visual effect for ray
    public float LineWidth = 0.1f;
    public float LineLength = 5f;
    private LineRenderer lineRenderer;
    private Vector3[] LinePositions = { Vector3.zero, Vector3.zero };

    public AudioSource PlayerHit;

    private bool InCooldown;
    private EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = this.GetComponent<EnemyMovement>();

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.SetPositions(LinePositions);
        lineRenderer.startWidth = LineWidth;
        lineRenderer.endWidth = LineWidth;

        InCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InCooldown && enemyMovement.aiType == EnemyMovement.AIType.attack)
        {
            StartCoroutine(Shoot());
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator Shoot()
    {
        Vector3 dir = enemyMovement.dir;
        yield return new WaitForSeconds(CooldownTime);
        RaycastHit hit = new RaycastHit();
        Ray ray = new(this.transform.position, dir);
        Vector3 EndPoint = ray.GetPoint(100.0f);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.gameObject.tag == "LargeAsteroid" || hit.transform.gameObject.tag == "MediumAsteroid" || hit.transform.gameObject.tag == "SmallAsteroid")
            {
                EndPoint = hit.point;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Player"))
            {
                PlayerHit.Play();
                Manager.RemoveLife();
                EndPoint = hit.point;
            }
        }
        StartCoroutine(ShowLaser(this.transform.position, EndPoint));
    }

    IEnumerator StartCooldown()
    {
        InCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        InCooldown = false;
    }

    IEnumerator ShowLaser(Vector3 Start, Vector3 End)
    {
        LinePositions[0] = Start;
        LinePositions[1] = End;
        lineRenderer.SetPositions(LinePositions);
        yield return new WaitForSeconds(0.1f);
        LinePositions[0] = Vector3.zero;
        LinePositions[1] = Vector3.zero;
        lineRenderer.enabled = false;
    }
}
