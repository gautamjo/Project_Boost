using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

    // Setup default reaction controll system (rcs)
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;


    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // Fetch RigidBody from the GameObject
        rigidBody = GetComponent<Rigidbody>();
        // Fetch AudioSource from the GameObject
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Friendly") {
            print("You're Ok");
        }

        else {
            print("You're Dead");
        }
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            { // so the audio dosen't layer on 
              //top of eachother.
                audioSource.Play();
            }
        }

        else
        {
            audioSource.Pause();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
