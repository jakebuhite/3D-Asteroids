using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Movement : MonoBehaviour
{
    public Vector3 dir;
    private float speed = 0;
    public SceneManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2, 2);
        float y = Random.Range(-2, 2);
        float z = Random.Range(-2, 2);
        dir = new Vector3(x,y,z);

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        Vector3 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);


        Vector3 newPosition = new Vector3(speed * dir.x * Time.deltaTime, speed * dir.y * Time.deltaTime, speed*dir.z * Time.deltaTime);
        // Vector3 newPosition = new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0); // could also use constructor and set each
        this.transform.position += newPosition;
    }
}
