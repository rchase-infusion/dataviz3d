﻿using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

//    public float lineDrawSpeed = 6f;

	// Use this for initialization
	void Start ()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
        lineRenderer.SetWidth(.03f, .03f);

        dist = Vector3.Distance(origin.position, destination.position);
        
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
        lineRenderer.SetWidth(.02f, .02f);

        dist = Vector3.Distance(origin.position, destination.position);


    }

    

 //       if (counter < dist)
        
//            counter += .1f / lineDrawSpeed;

//            float x = Mathf.Lerp(0, dist, counter);

  //          Vector3 pointA = origin.position;
   //         Vector3 pointB = destination.position;

//           Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
        }
//        LineRenderer.SetPosition(1, pointAlongLine);


     
	
	
