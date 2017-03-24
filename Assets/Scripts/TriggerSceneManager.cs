using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSceneManager : MonoBehaviour {

    //This class is for monitoring the progress of the cube scene, and telling the Game Manager to move scenes if the trigger is activated

    public GameObject triggerCube;
    GameObject manager;
    public GameObject leftController;
    public GameObject rightController;

    AudioSource soundPlayer;
    bool soundPlayed;

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("GameManager");

        soundPlayer = GetComponent<AudioSource>();
        soundPlayed = false;

        //Disable all actions except trigger
        leftController.GetComponent<EasyController>().setPermissionsTriggerScene();
        rightController.GetComponent<EasyController>().setPermissionsTriggerScene();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //When the cube is activated, the user has completed the task, and the game can move on
	    if (triggerCube.GetComponent<CubeGlow>().GetActive() == true)
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
        //Bit longer than initial teleport because we wanna listen to the loop for a bit
        yield return new WaitForSeconds(2.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }
}
