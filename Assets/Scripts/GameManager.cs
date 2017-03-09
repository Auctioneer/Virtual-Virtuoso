using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Height of all the cubes in the game (storing this value between scenes
    float cubeHeight;

	// Use this for initialization
	void Start ()
    {
        //Standard cube height placeholder
        cubeHeight = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCubeHeight(float firstCubeHeight)
    {
        cubeHeight = firstCubeHeight;
    }

    public float getCubeHeight()
    {
        return cubeHeight;
    }

    //Move to next scene in the game)
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //SceneManager.LoadScene(1);
    }
}
