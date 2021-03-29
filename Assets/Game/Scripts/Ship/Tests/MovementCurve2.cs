using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MovementCurve2 : MonoBehaviour {
    //float objectT = 2.5f; //timer for that object
    public Transform movementMarker;
    public float h; //desired parabola height
    Vector2 startVector, endVector; //Vector positions for start and end
    public int plotPoints = 20; // list will be +1, since [0] is the starting point
    private LineRenderer lineRenderer;

    void Start() {
        // initialize the line
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = plotPoints + 1;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
    }

    void Update() {
        startVector = transform.position; //Get vectors from the transforms
        endVector = movementMarker.transform.position;

        //Shows how to animate something following a parabola
        //objectT = Time.time % 1; //completes the parabola trip in one second
        //transform.position = SampleParabola(startVector, endVector, h, objectT);

        // calculate line
        List<Vector2> plotList = PlotMovePoints(55, 3);

        for (int i = 0; i < plotList.Count; i++) {
            lineRenderer.SetPosition(i, plotList[i]);
        }
    }

    private Vector2 SampleParabola(Vector2 start, Vector2 end, float height, float t) {
        //start and end are roughly level, pretend they are - simpler solution with less steps
        Vector2 travelDirection = end - start;
        Vector2 result = start + t * travelDirection;
        result.y += Mathf.Sin(t * Mathf.PI) * height;
        return result;
    }

    private List<Vector2> PlotMovePoints(float angle, float distance) {
        // initialize variables
        List<Vector2> plot = new List<Vector2>(plotPoints + 1);
        Vector2 fwd = transform.up;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle / plotPoints);
        float distancePerStep = distance / plotPoints;

        // start the curve at the startingPoint
        plot.Add(startVector);

        for (int i = 0; i < plotPoints; i++) {
            Vector2 p = SampleParabola(startVector, endVector, h, i / plotPoints);
            //fwd = (rotation * fwd).normalized;
            //startVector += fwd * distancePerStep;
            plot.Add(p);
        }

        // end the curve at the movement reticule
        endVector = plot[plotPoints];

        return plot;
    }
}
