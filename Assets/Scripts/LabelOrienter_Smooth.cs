﻿using UnityEngine;
using System.Collections;

public class LabelOrienter_Smooth : MonoBehaviour {

    public PointRenderer point;

    /*
     * This script finds objects with an appropriate tag, and makes them rotate according to the camera
     * 1. This script does no have to be placed on a particular object (finds them using tags)
     * 2. The tags must be added to the desired game objects in the Editor
     * 3. The tag must be defined in the inspector of this script
     * 4. Remember to have an active camera!       
     */

    public bool faceCamera = true;
    
	private GameObject[] labels;  // Array, stores all GameObjects that should be kept aligned with camera
    private GameObject[] labels3;

    // The tag of the target object, the ones that will track the camera
    public string targetTag; // 
    public string targetTag2; // 
    public string targetTag3;

    public float distanceFromCamera = 3.0f;

    //Vector3 targetPosition;

    // Use this for initialization
    void Start ()
    {
        //populates the array "labels" with gameobjects that have the correct tag, defined in inspector
        labels = GameObject.FindGameObjectsWithTag(targetTag);
        labels3 = GameObject.FindGameObjectsWithTag(targetTag3);

    }
	
	// Update is called once per frame
	void Update () {

      	orientLables ();  // remove if instead you are calling orientLables directly, whenever the camera has moved to make save processing time
        //uiLables();
        uiLookat();
        string cek = point.getmyString2();
        if(cek!=null)
        {
            gempalables();
        }
    }

        // Method definition
	public void orientLables()
    {
        

		// go through "labels" array and aligns each object to the Camera.main (built-in) position and orientation
		foreach (GameObject go in labels) {

            // create new position Vector 3 so that object does not rotate around y axis
            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x,
                                                 go.transform.position.y,
                                                 Camera.main.transform.position.z);
            

            // Reverse transform or not
            if (faceCamera == true)           
            {
                // Here the internal math reverses the direction so 3D text faces the correct way
                go.transform.LookAt(2 * go.transform.position - targetPosition);
            }
            else
            {
                //LookAt makes the object face the camera
                go.transform.LookAt(targetPosition);
            }
            

        }
    }


    public void uiLookat()
    {


        // go through "labels" array and aligns each object to the Camera.main (built-in) position and orientation
        foreach (GameObject go in labels3)
        {

            // create new position Vector 3 so that object does not rotate around y axis
            Vector3 targetPosition = new Vector3(Camera.main.transform.position.x,
                                                 go.transform.position.y,
                                                 Camera.main.transform.position.z);


            // Reverse transform or not
            if (faceCamera == true)
            {
                // Here the internal math reverses the direction so 3D text faces the correct way
                go.transform.LookAt(2 * go.transform.position - targetPosition);
            }
            else
            {
                //LookAt makes the object face the camera
                go.transform.LookAt(targetPosition);
            }


        }
    }

    public void gempalables()
    {
        GameObject[] labels2;
        labels2 = GameObject.FindGameObjectsWithTag(targetTag2); 

		// go through "labels" array and aligns each object to the Camera.main (built-in) position and orientation
		foreach (GameObject go in labels2) {

            // create new position Vector 3 so that object does not rotate around y axis
            Vector3 targetPosition2 = new Vector3(Camera.main.transform.position.x,
                                                 go.transform.position.y,
                                                 Camera.main.transform.position.z);
            

            // Reverse transform or not
            if (faceCamera == true)           
            {
                // Here the internal math reverses the direction so 3D text faces the correct way
                go.transform.LookAt(2 * go.transform.position - targetPosition2);
            }
            else
            {
                //LookAt makes the object face the camera
                go.transform.LookAt(targetPosition2);
            }
            

        }
    }

    public void uiLables()
    {
        // Get the camera's position and forward direction
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;

        // Go through "labels" array and align each object to the camera's position and orientation
        foreach (GameObject go in labels3)
        {
            // Calculate the target position based on the camera's position and forward direction
            Vector3 targetPosition = cameraPosition + cameraForward * distanceFromCamera;

            // Calculate the direction from the object to the target position
            //Vector3 directionToTarget = targetPosition - go.transform.position;

            // Reverse transform or not
            if (faceCamera)
            {
                // Make the object always face the target position
                //go.transform.rotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

                // Set the position of the object to the target position
                go.transform.position = targetPosition;
            }
            else
            {
                // LookAt makes the object face the camera
                go.transform.LookAt(cameraPosition);
            }
        }
    }


}
