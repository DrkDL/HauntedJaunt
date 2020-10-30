using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool IsPlayerInRange;
    public GameEnding gameEnding; // to allow to access the public method in other script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is in range of the ghost's line of sight
        if (IsPlayerInRange)
        {
            // a vector from A to B is B - A
            Vector3 direction = player.position - transform.position + Vector3.up; // Vector3.up is short for (0,1,0)

            // Ray is the line shot from a specific point, checking for Colliders along the ray is Raycast
            Ray ray = new Ray(transform.position, direction); // Ray(origin, direction)

            // RaycastHit will send info about what raycast hits
            RaycastHit raycastHit;

            // detecting player collider
            if (Physics.Raycast(ray, out raycastHit)) // out parameter gives hit info to raycastHit
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            IsPlayerInRange = false;
        }
    }
}
