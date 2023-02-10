using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 15f;
    public float RollSpeed = 90f;

    private float aForward;
    private float aRoll;

    private float fAcceleration = 2f;
    private float rAcceleration = 3.5f;

    public float speedOfCamera;

    private Vector2 look, screenCenter, mouseDistance;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        look.x = Input.mousePosition.x;
        look.y = Input.mousePosition.y;

        mouseDistance.x = (look.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (look.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        aRoll = Mathf.Lerp(aRoll, Input.GetAxis("Horizontal"), rAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * speedOfCamera * Time.deltaTime, mouseDistance.x * speedOfCamera * Time.deltaTime, aRoll * RollSpeed * Time.deltaTime);

        aForward = Mathf.Lerp(aForward, Input.GetAxis("Vertical") * forwardSpeed, fAcceleration * Time.deltaTime);

        this.transform.position += transform.forward * aForward * Time.deltaTime;
       
    }


}
