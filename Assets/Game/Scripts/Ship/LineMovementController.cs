using UnityEngine;
using UnityEditor;

public class LineMovementController : MonoBehaviour {
    public GameObject movementLinePrefab;
    public GameObject movementCursorPrefab;
    private GameObject movementLine;
    private GameObject movementCursor;
    private MoreMountains.Tools.MMBezierLineRenderer bezierLine;

    // Start is called before the first frame update
    void Start() {
        // instantiate the movement line and start it in front of ship
        movementLine = (GameObject)PrefabUtility.InstantiatePrefab(movementLinePrefab);
        movementLine.transform.position = new Vector3(transform.position.x + 2, transform.position.y, movementLine.transform.position.z);

        // instantiate the movement cursor
        movementCursor = (GameObject)PrefabUtility.InstantiatePrefab(movementCursorPrefab);
        movementCursor.transform.position = new Vector3(transform.position.x + 4, transform.position.y, movementCursor.transform.position.z);

        // bind line together with ship and cursor
        bezierLine = movementLine.GetComponent<MoreMountains.Tools.MMBezierLineRenderer>();

        bezierLine.AdjustmentHandles.SetValue(gameObject.transform, 0);
        bezierLine.AdjustmentHandles.SetValue(gameObject.transform, 1);
        bezierLine.AdjustmentHandles.SetValue(movementLine.transform, 2);
        bezierLine.AdjustmentHandles.SetValue(movementCursor.transform, 3);
    }

    // Update is called once per frame
    void Update() {

    }
}
