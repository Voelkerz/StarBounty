using UnityEngine;

public class DirectShipControl : MonoBehaviour {
    private Rigidbody2D rb;

    public float thrustModifier = 500.0f;
    public float rotationModifier = 0.1f;
    public float defaultDrag = 0.3f;
    private float t = 0.0f;
    private bool moving = false;
    private bool engineOn = false;
    private bool stop = false;

    void Awake() {

    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            engineOn = true;
            stop = false;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            engineOn = false;
            stop = false;
        }

        if (Input.GetKey(KeyCode.R)) {
            engineOn = false;
            stop = true;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            rb.rotation -= rotationModifier;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.rotation += rotationModifier;
        } else {
            //rb.rotation = 0.0f;
        }
    }

    void FixedUpdate() {
        if (engineOn) {
            rb.drag = defaultDrag;
            rb.AddRelativeForce(Vector2.up * thrustModifier);
            moving = true;
            t = 0.0f;
        }

        if (!engineOn) {
            rb.drag = 0.0f;
            rb.velocity.Set(0.0f, 0.0f);
            moving = true;
            t = 0.0f;
        }

        if (stop) {
            rb.drag = defaultDrag;
            rb.velocity.Set(0.0f, 0.0f);
            moving = true;
            t = 0.0f;
        }

        if (moving) {
            //Debug.Log(rb.velocity);
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t = t + Time.deltaTime;
            if (t > 1.0f) {
                //Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }
    }
}
