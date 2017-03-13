using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSceneManager : MonoBehaviour {

    //This class is for monitoring the progress of the cube scene, and telling the Game Manager to move scenes if the trigger is activated

    public GameObject triggerCube;
    GameObject manager;


	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("GameManager");
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
        //Wait a few seconds
        //Bit longer than initial teleport because we wanna listen to the loop for a bit
        yield return new WaitForSeconds(2.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
    }
}
