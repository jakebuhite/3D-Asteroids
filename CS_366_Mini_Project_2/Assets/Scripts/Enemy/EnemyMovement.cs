using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // General
    public Vector3 dir;
    public float speed;
    public SceneManager Manager;
    
    // AI Management 
    public enum AIType { attack, none };
    public AIType aiType = AIType.none;

    // UFO Rotation
    private Matrix4x4 rotateAxis;
    private float spinner;
    public float spinSpeed = 200;

    private MeshFilter mesh;
    private Vector3[] origVerts;
    private Vector3[] newVerts;

    // Player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // UFO rotation
        mesh = this.GetComponent<MeshFilter>();
        origVerts = mesh.mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
        spinner = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dir = ProcessAI();
        this.transform.position += speed * Time.deltaTime * dir;

        // UFO rotation
        spinner += spinSpeed * Time.deltaTime;
        rotateAxis = Matrix4x4.Rotate(Quaternion.Euler(1, 1, spinner));
        for (int i = 0; i < origVerts.Length; i++)
        {
            newVerts[i] = rotateAxis.MultiplyPoint3x4(origVerts[i]);
        }
        mesh.mesh.vertices = newVerts;
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

        if (dist <= 50.0f && !Manager.isPlayerInvicible)
        {
            aiType = AIType.attack;
        } else
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
        }

        if (collider.gameObject.tag == "Player")
        {
            Manager.RemoveLife();
        }
    }
}
