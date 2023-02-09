using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float strafeSpeed = 7f;
    public float upSpeed = 7;

    private float aForward, aStrafe, aUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aForward = Input.GetAxis("Vertical") * forwardSpeed;
        aStrafe = Input.GetAxis("Mouse X") * strafeSpeed;
        aUp = Input.GetAxis("Mouse Y") * upSpeed;

        this.transform.position += transform.forward * aForward * Time.deltaTime;
        this.transform.position += (transform.right * aStrafe * Time.deltaTime) + (transform.up * aUp * Time.deltaTime);
    }


}
