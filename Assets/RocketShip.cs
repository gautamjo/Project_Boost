using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour {

    // Setup default reaction controll system (rcs)
    [SerializeField] float rcsThrust = 100f;
    // setup default main thrust system
    [SerializeField] float mainThrust = 100f;
    // Setup audio clip
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        // Fetch RigidBody from the GameObject
        rigidBody = GetComponent<Rigidbody>();
        // Fetch AudioSource from the GameObject
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == State.Alive) {
            ProcessInput();
        }
	}

    void OnCollisionEnter (Collision collision)
    {
        if (state != State.Alive) {
            return;
        }

        if (collision.gameObject.tag == "Friendly") {
            //print("You're Ok");
        }

        else if (collision.gameObject.tag == "Finish")
        {
            StartSuccessSequence();
        }

        else
        {
            StartDeathSequence();
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        Invoke("LoadNextScene", 1f); // parameterize time
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        Invoke("LoadFirstLevel", 1f); // parameterize time 
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo allow for more than 2 levels
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
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
            ApplyThrust();
        }

        else
        {
            audioSource.Pause();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        { // so the audio dosen't layer on 
          //top of eachother.
            audioSource.PlayOneShot(mainEngine);
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
