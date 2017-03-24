using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSceneManager : MonoBehaviour {

    public GameObject cube;
    public GameObject targetZone;
    GameObject manager;

    AudioSource soundPlayer;
    bool soundPlayed;

    public GameObject leftController;
    public GameObject rightController;

    // Use this for initialization
    void Start ()
    {
        //TO DO - SET PERMISSIONS FOR THIS SCENE

        manager = GameObject.Find("GameManager");

        soundPlayer = GetComponent<AudioSource>();
        soundPlayed = false;

        leftController.GetComponent<EasyController>().setPermissionsGrabScene();
        rightController.GetComponent<EasyController>().setPermissionsGrabScene();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //We move on if the cube is in the target zone and is not being held by the player (i.e. it's null)
		if ((targetZone.GetComponent<TeleporterGrabTriggerTouching>().getIsTouching() == true) && (cube.transform.parent == null))
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
        yield return new WaitForSeconds(2.0f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }
}

