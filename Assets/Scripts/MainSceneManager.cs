﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour {

    GameObject manager;
    public GameObject leftController;
    public GameObject rightController;

    // Use this for initialization
    void Start () {
        //Enable all actions
        leftController.GetComponent<EasyController>().setPermissionsFullFunctionality();
        rightController.GetComponent<EasyController>().setPermissionsFullFunctionality();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
