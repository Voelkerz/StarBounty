using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour {
    
    public GameObject endTarget;
    public float maxAcceleration;

    private Rigidbody2D playerRigidbody;
    private LineRenderer line;
    private Vector2 lastVelocity;
    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 pointC;
    private Vector2 v1;
    private float v2;
    private float t;

    // Start is called before the first frame update
    void Start() {
        // retrieve ui manager
        GameObject uiManagerObject = GameObject.Find("UIManager");
        UIManager uiManagerScript = uiManagerObject.GetComponent<UIManager>();

        // retrieve rigidbody
        playerRigidbody = GetComponent<Rigidbody2D>();
        lastVelocity = playerRigidbody.velocity;
        
        // initialize the line
        line = GetComponent<LineRenderer>();
        line.positionCount = 3;
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.startWidth = 0.03f;
        line.endWidth = 0.03f;

        // initialize quadratic bezier curve variables
        v2 = maxAcceleration;
        //t = uiManagerScript.turnTimer;
        t = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        // constantly set the initial acceleration
        v1 = (playerRigidbody.velocity - lastVelocity) / t;

        // set the points
        pointA = playerRigidbody.position;
        pointB = ((t / 2) * v1) + playerRigidbody.position;
        pointC = endTarget.transform.position;

        line.SetPosition(0, pointA);
        line.SetPosition(1, pointB);
        line.SetPosition(2, pointC);

        lastVelocity = playerRigidbody.velocity;
        Debug.Log("v1: " + v1 + " pointB: " + pointB + " pointC: " + pointC);
    }
}
