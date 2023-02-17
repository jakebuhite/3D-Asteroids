using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public enum AIType { attack, none };
    public AIType aiType = AIType.none;

    private GameObject player;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dir = ProcessAI();
        this.transform.position += speed * Time.deltaTime * dir;
    }

    Vector3 ProcessAI()
    {
        Vector3 playerDir = player.transform.position - this.transform.position;

        // Function that checks if player is within enemy sites
        Vector3 returnDir = CheckPlayerView();
        switch (aiType)
        {
            case AIType.none:
                returnDir = dir;
                break;
            case AIType.attack:
                returnDir = VectorTrack(playerDir);
                break;
            default:
                break;
        }
        if (Mathf.Abs(returnDir.x) < 0.1)
        {
            returnDir.x = 0;
        }
        if (Mathf.Abs(returnDir.y) < 0.1)
        {
            returnDir.y = 0;
        }
        if (Mathf.Abs(returnDir.z) < 0.1)
        {
            returnDir.z = 0;
        }
        return returnDir;
    }

    private Vector3 VectorTrack(Vector3 rawDirection)
    {
        Vector3 temp = new Vector3(rawDirection.x, rawDirection.y, rawDirection.z);
        temp.Normalize();
        return temp;
    }

    private Vector3 CheckPlayerView()
    {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist <= 50.0f)
        {
            aiType = AIType.attack;
        }

        if (aiType == AIType.attack && dist > 50.0f)
        {
            aiType = AIType.none;
        }
        return Vector3.zero;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Avoid boundary
        switch (collider.gameObject.tag) {
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

        // Destroy Asteroids in the way
        if (collider.gameObject.tag == "LargeAsteroid" || collider.gameObject.tag == "MediumAsteroid" || collider.gameObject.tag == "SmallAsteroid")
        {
            Destroy(collider.gameObject);
            //Vector3 direction = collider.transform.position - transform.position;
        }

        if (collider.gameObject.tag == "Player")
        {
            // TODO
                // Take player life
        }
    }
}
