using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSceneManager : MonoBehaviour {

    public GameObject cube;
    public GameObject targetZone;
    GameObject manager;

	// Use this for initialization
	void Start ()
    {
        //TO DO - SET PERMISSIONS FOR THIS SCENE

        manager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //We move on if the cube is in the target zone and is not being held by the player (i.e. it's null)
		if ((targetZone.GetComponent<TeleporterGrabTriggerTouching>().getIsTouching() == true) && (cube.transform.parent == null))
        {
            manager.GetComponent<GameManager>().nextScene();
        }
	}
}
