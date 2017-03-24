using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadSceneManager : MonoBehaviour
{

    //This class is for monitoring the progress of the cube scene, and telling the Game Manager to move scenes if the trigger is activated

    public GameObject padCube;
    GameObject manager;
    public GameObject leftController;
    public GameObject rightController;

    public GameObject onBox1;
    public GameObject onBox2;

    AudioSource soundPlayer;
    bool soundPlayed;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager");

        soundPlayer = GetComponent<AudioSource>();
        soundPlayed = false;

        //Get the boxes in the background playing
        onBox1.GetComponent<CubeGlow>().Activate();
        onBox2.GetComponent<CubeGlow>().Activate();
    }

    // Update is called once per frame
    void Update()
    {
        //When the success variable has been activated, we can move on
        if ((leftController.GetComponent<ControllerSoloOverride>().getPadSuccess() == true) || (rightController.GetComponent<ControllerSoloOverride>().getPadSuccess() == true))
        {
            StartCoroutine(beginTransition());
        }
    }

    IEnumerator beginTransition()
    {
        if (soundPlayed == false)
        {
            soundPlayer.Play();
            soundPlayed = true;
        }

        //Wait a few seconds
        //Bit longer than initial teleport because we wanna listen to the loop for a bit?
        yield return new WaitForSeconds(3.0f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }
}