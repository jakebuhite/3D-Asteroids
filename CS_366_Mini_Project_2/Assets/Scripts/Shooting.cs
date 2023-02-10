using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float cooldownTime;

    private bool inCooldown;

    // Start is called before the first frame update
    void Start()
    {
        inCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !inCooldown)
        {
            Shoot();
            StartCoroutine(StartCooldown());
        }
    }

    void Shoot()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // Debug
            Debug.Log(hit.transform.tag);
            // Debug.DrawRay(this.transform.position, hit.transform.position, Color.red, 3);
        }
    }

    IEnumerator StartCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }

    private void DestroyAsteroid()
    {
        // Check if asteroid is level 0
            // if level 0, destroy game object
        // If not 0, split asteroids and send them in different directions
    }
}
