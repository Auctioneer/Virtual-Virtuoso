﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Height of all the cubes in the game (storing this value between scenes)
    float cubeHeight;

	// Use this for initialization
	void Start ()
    {
        //Standard cube height placeholder
        cubeHeight = 2;

        //Keep Game Manager between scenes
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCubeHeight(float firstCubeHeight)
    {
        cubeHeight = firstCubeHeight;
        print("Height is " + cubeHeight);
    }

    public float getCubeHeight()
    {
        return cubeHeight;
    }

    //Move to next scene in the game
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Move to a particular scene
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
