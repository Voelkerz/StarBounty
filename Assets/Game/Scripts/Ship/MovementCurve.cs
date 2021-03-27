using System.Collections.Generic;
using UnityEngine;

public class MovementCurve : MonoBehaviour {
    public int plotPoints = 20; // list will be +1, since [0] is the starting point
    private LineRenderer lineRenderer;
    public GameObject movementReticule;
    public float maxPosTurnAngle;
    public float maxNegTurnAngle;
    private Vector2 shipPos;
    private Vector2 retPos;

    // Start is called before the first frame update
    void Start() {
        // initialize the line
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = plotPoints + 1;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
    }

    // Update is called once per frame
    void Update() {



        //   Vector2 direction = (Vector2)target.position - rigidBody.position;
        //   direction.Normalize();
        //   float rotateAmount = Vector3.Cross(direction, transform.up).z;
        //   rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        //   rigidBody.velocity = transform.up * movementSpeed;




        // initial moving variables that must be updated constantly
        shipPos = transform.position;
        retPos = movementReticule.transform.position;

        // calculate distance from ship to movement reticule
        float dist = (shipPos - retPos).magnitude;

        // calculate the angle between ship and reticule
        float angle = Vector2.Angle(shipPos, retPos);

        // calculate angle direction
        float angleDir = AngleDir(shipPos, retPos);

        if (angleDir > 0) {
            angle = (-angle);
        }

        // calculate line
        List<Vector2> plotList = PlotMovePoints(angle, dist);

        for (int i = 0; i < plotList.Count; i++) {
            lineRenderer.SetPosition(i, plotList[i]);
        }

        Debug.Log("dist: " + dist + " || angle: " + angle);
    }

    public List<Vector2> PlotMovePoints(float angle, float distance) {
        // initialize variables
        List<Vector2> plot = new List<Vector2>(plotPoints + 1);
        Vector2 fwd = transform.up;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle / plotPoints);
        float distancePerStep = distance / plotPoints;

        // start the curve at the startingPoint
        plot.Add(shipPos);

        for (int i = 0; i < plotPoints; i++) {
            fwd = (rotation * fwd).normalized;
            shipPos += fwd * distancePerStep;
            plot.Add(shipPos);
        }

        // end the curve at the movement reticule
        retPos = plot[plotPoints];

        return plot;
    }

    public float AngleDir(Vector2 A, Vector2 B) {
        return (-A.x * B.y + A.y * B.x);
    }
}
