using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTeleporterTrigger : MonoBehaviour {

    GameObject manager;
    Vector3 eyeCameraTransPosition;
    public GameObject eyeCamera;
    public float offset;

    AudioSource soundPlayer;
    bool soundPlayed;

    public string nextScene;

    // Use this for initialization
    void Start ()
    {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayed = false;

        manager = GameObject.Find("GameManager");
    }
	
    //Coroutine for changing scene
    IEnumerator beginSceneTransition()
    {
        if (soundPlayed == false)
        {
            soundPlayer.Play();
            soundPlayed = true;
        }

        //Wait a few seconds
        yield return new WaitForSeconds(1.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().changeScene(nextScene);

    }

    //Walking into the teleporter trigger initiates scene transition
    public void teleportTriggered()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
         {
            //Get current position of user's head on the y axis
            eyeCameraTransPosition = eyeCamera.transform.position;
            float eyeCameraYPosition = eyeCameraTransPosition.y;

            //Set that position in Game Manager
            manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - offset);
        }

        //Get current position of user's head on the y axis
        //eyeCameraTransPosition = eyeCamera.transform.position;
        //float eyeCameraYPosition = eyeCameraTransPosition.y;

        //Set that position in Game Manager
        //manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - offset);

        //Call method to move to next scene
        StartCoroutine(beginSceneTransition());

    }
}
