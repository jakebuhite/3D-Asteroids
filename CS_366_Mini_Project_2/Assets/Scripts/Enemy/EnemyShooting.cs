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
            Debug.Log("Hit: " + hit.transform.gameObject.tag);
            if (hit.transform.gameObject.tag == "LargeAsteroid" || hit.transform.gameObject.tag == "MediumAsteroid" || hit.transform.gameObject.tag == "SmallAsteroid")
            {
                EndPoint = hit.point;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.CompareTag("Player"))
            {
                Manager.RemoveLife();
                EndPoint = hit.point;
                Debug.Log("Player: " + hit.point);
            }
        }
        LinePositions[0] = this.transform.position;
        Debug.Log("EndPoint: " + EndPoint);
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
