using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeleporterTrigger : MonoBehaviour {

    GameObject manager;

	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("GameManager");
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") == true)
        {
            //TO DO: Add sound to confirm they're in the teleporter


            //Call method to move to next scene
            StartCoroutine(beginSceneTransition());
        }
    }
}
