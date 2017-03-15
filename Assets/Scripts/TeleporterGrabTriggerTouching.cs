using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterGrabTriggerTouching : MonoBehaviour {

    //This class is here to set a bool to true if the user has put the cube in the vicinity of the teleporter

    bool isTouching;

	// Use this for initialization
	void Start ()
    {
        isTouching = false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoopCube") == true)
        {
            isTouching = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        isTouching = false;
    }

    public bool getIsTouching()
    {
        return isTouching;
    }
}
