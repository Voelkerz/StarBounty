using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingBehavior : MonoBehaviour {
    public Transform Target;
    public float MoveSpeed = 350f;
    public float RotateSpeed = 2000f;
    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = transform.up * MoveSpeed * Time.deltaTime;

        Vector2 targetVector = Target.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * RotateSpeed * Time.deltaTime;
    }
}
