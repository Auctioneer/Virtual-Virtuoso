using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeleporterTrigger : MonoBehaviour {

    public GameObject manager;
    Vector3 eyeCameraTransPosition;
    public GameObject eyeCamera;
    public float offset;

    // Use this for initialization
    void Start ()
    {
        //manager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Coroutine for changing scene
    IEnumerator beginSceneTransition()
    {
        //Wait a few seconds
        yield return new WaitForSeconds(1.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();

    }

    //Walking into the teleporter trigger initiates scene transition
    public void teleportTriggered()
    {
        //Get current position of user's head on the y axis
        eyeCameraTransPosition = eyeCamera.transform.position;
        float eyeCameraYPosition = eyeCameraTransPosition.y;

        //Set that position in Game Manager
        manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - offset);

        //TO DO: Add sound to confirm they're in the teleporter

        //Call method to move to next scene
        StartCoroutine(beginSceneTransition());

    }
}
