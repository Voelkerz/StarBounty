using UnityEngine;

public class MovementCurve3 : MonoBehaviour {
    public GameObject movementTarget;
    //public float modifier;
    private float speed;
    private Vector3 start;
    private Vector3 end;
    private Vector3 mod;
    private LineRenderer lineRenderer;
    public GameObject curveModifierObject;
    public int numPoints;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        // initialize the line
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;

        // initialize rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        speed = rb.velocity.magnitude;
        curveModifierObject.transform.position = transform.position + transform.TransformDirection(new Vector3(0, transform.localScale.y + 1 * speed, 0));
        start = transform.position;
        end = movementTarget.transform.position;
        mod = curveModifierObject.transform.position;
        
        for (int i = 0; i < numPoints; i++) {
            lineRenderer.SetPosition(i, GetBezierPosition((float) i / numPoints));
        }
    }

    // parameter t ranges from 0f to 1f
    // this code might not compile!
    private Vector3 GetBezierPosition(float t) {
        // Vector3 p0 = transformBegin.position;
        // Vector3 p1 = p0 + transformBegin.forward;
        // Vector3 p3 = transformEnd.position;
        // Vector3 p2 = p3 - (-transformEnd.up);
        Vector3 p0 = start;
        Vector3 p2 = end;
        //Vector3 p1 = p0 + (transform.forward * modifier);
        Vector3 p1 = mod;

        // P = (1−t)2P1 + 2(1−t)tP2 + t2P3
        Vector3 p = Mathf.Pow(1 - t, 2f) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2f) * p2;
        return p;

        // here is where the magic happens!
        //return Mathf.Pow(1f - t, 3f) * p0 + 3f * Mathf.Pow(1f - t, 2f) * t * p1 + 3f * (1f - t) * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;
    }
}
