using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCubeYPosition : MonoBehaviour {

    //To get position of head
    public GameObject eyeCamera;
    Vector3 eyeCameraTransPosition;
    GameObject manager;

    //Can change this offset to suit
    public float offset;

    public GameObject cubeSelected;

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("GameManager");

        if (offset == 0)
        {
            offset = 0.3f;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Get current position of user's head on the y axis
        eyeCameraTransPosition = eyeCamera.transform.position;
        float eyeCameraYPosition = eyeCameraTransPosition.y;

        //Vary start cube's y position relative to the user's head
        Vector3 cubePosition = new Vector3(1.005f, eyeCameraYPosition-offset, -0.099f);
        this.gameObject.transform.position = cubePosition;
    }


    public void clickedBehaviour()
    {
        print("We're in clicked behaviour m8");

        //Set cube height with game manager
        manager.GetComponent<GameManager>().setCubeHeight(eyeCameraTransPosition.y - offset);

        //Call method to move to next scene
        StartCoroutine(beginSceneTransition());
    }
    
    //Coroutine for changing scene
    IEnumerator beginSceneTransition()
    {
        //Wait a few seconds
        yield return new WaitForSeconds(1.5f);

        //Tell GameManager to do scene change
        manager.GetComponent<GameManager>().nextScene();
        
    }

    //Highlight behaviour for cube
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") == true)
        {
            cubeSelected.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") == true)
        {
            cubeSelected.SetActive(false);
        }
    }
}
