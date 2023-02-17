using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Movement : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 1;
    public SceneManager Manager;

    public Vector3 AsteroidPosition;

    // Start is called before the first frame update
    void Start()
    {
        float x;
        float y;
        float z;
        do
        {
            x = Random.Range(-2, 2);
            y = Random.Range(-2, 2);
            z = Random.Range(-2, 2);
        }
        while (x == 0 && y == 0 && z == 0);
        dir = new Vector3(x,y,z);

        if (this.tag == "LargeAsteroid")
            this.speed = 1;
        else if (this.tag == "MediumAsteroid")
            this.speed = 2;
        else if (this.tag == "SmallAsteroid")
            this.speed = 3;

    }

    // Update is called once per frame
    void Update()
    {
        move();
        AsteroidPosition = this.transform.position;
    }

    void move()
    {
        Vector3 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);


        Vector3 newPosition = new Vector3(speed * dir.x * Time.deltaTime, speed * dir.y * Time.deltaTime, speed*dir.z * Time.deltaTime);
        // Vector3 newPosition = new Vector3(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0); // could also use constructor and set each
        this.transform.position += newPosition;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Manager.RemoveLife();
        }
    }
}
