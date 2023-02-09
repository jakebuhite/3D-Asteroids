using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float localSpeed = 0;
        if (Input.GetKey(KeyCode.W))
        {
            localSpeed += speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            localSpeed -= speed;
        }
        speed = Mathf.Clamp(speed, 0f, 100f);
    }
}
