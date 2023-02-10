using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAsteroid : MonoBehaviour
{
    public SceneManager Manager;
    public Vector3 AsteroidPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidPosition = this.transform.position;
    }
}
