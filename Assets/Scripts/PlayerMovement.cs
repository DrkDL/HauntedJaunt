using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private Animator anim;
    public float turnSpeed = 20f;
    Quaternion rotation = Quaternion.identity; // make it default with no rotation
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // FixedUpdate is in time with physics, called 50 times per sec
    // this is also to make sure the movement vector and rotation are set in time with OnAnimatorMove
    void FixedUpdate() 
    {
        // obtain user inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // put user inputs in the vector3 movement variable
        movement = new Vector3(horizontal, 0f, vertical);
        //movement.Set(horizontal, 0f, vertical);
        //movement.Normalize();

        // make animation work if moving is detected
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        anim.SetBool("IsWalking", isWalking);

        // walking audio
        if (isWalking)
        {
            if (!audioSource.isPlaying)// make sure it this does not play every frame
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        // player rotation
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f); // smoother control
        rotation = Quaternion.LookRotation(desiredForward); // quaternion is used to store the rotation data
    }
    
    // built-in method from monobehaviour class
    void OnAnimatorMove() // allows to apply root motion, movement and rotation can be applied separately
    {
        // deltaPosition is the change in position due to root motion that would have been applied to this frame
        rb.MovePosition(rb.position + movement * anim.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
