using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

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

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying) { // so the audio dosen't layer on 
                                          //top of eachother.
                audioSource.Play();
            }
        }

        else {
            audioSource.Pause();
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
        }

    }
}
