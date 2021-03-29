using UnityEditor;
using UnityEngine;

public class BezierCurveTest : MonoBehaviour {
    public Transform startPosition;
    public Transform endPosition;
    private Vector3 startTangent;
    private Vector3 endTangent;
    public Color color;
    public Texture2D texture;
    public float width;

    // Start is called before the first frame update
    void Start() {
        startTangent = startPosition.position;
        endTangent = startPosition.position + (startPosition.forward * 10);


    }

    // Update is called once per frame
    void Update() {
        Handles.DrawBezier(startPosition.position, endPosition.position, startTangent, endTangent, color, texture, width);
    }
}
