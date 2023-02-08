using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public Vector3 position;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        position.z = 10;
        transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

    }

    private void Move()
    {
        float inputSpeed = 0;
        float inputRotation = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            inputSpeed = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            inputSpeed = -speed * Time.deltaTime;
        }
        Move(inputRotation, inputSpeed);
    }

    private void Move(float rot, float speed)
    {
        transform.Rotate(0, rot, 0);
        // does two things: rotates forward and then translates to world space
        Vector3 moveDirection = transform.TransformDirection(Vector3.forward) * speed;
        characterController.Move(moveDirection);
    }

}
