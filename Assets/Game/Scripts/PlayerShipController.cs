using UnityEngine;

public class PlayerShipController : MonoBehaviour {
    private float deltaTime;
    public float duration = 1000;
    public GameObject endVectorObject;

    // Start is called before the first frame update
    void Start() {
        deltaTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        transform.up = endVectorObject.transform.position - transform.position; //this allows the ship to face the direction it's traveling
        transform.position = Vector3.Lerp(transform.position, endVectorObject.transform.position, (Time.time - deltaTime) / duration);
    }
}
