using UnityEngine;

public class Hovering : MonoBehaviour {

    [SerializeField] private float floatHeight = 0.3f;
    [SerializeField] bool rotate = false;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float floatSpeed = 1.0f;

    private float phaseOffsetRange = 2.0f * Mathf.PI;
    private Vector3 initialPosition;
    private float phaseOffset;

    private void Start() {
        initialPosition = transform.position;
        phaseOffset = Random.Range(0.0f, phaseOffsetRange);
    }
    
    private void Update() {
        // Rotate around the Y axis
        if (rotate) {
           transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed); 
        }

        // Float up and down
        float verticalOffset = Mathf.Sin((Time.time + phaseOffset) * floatSpeed) * floatHeight;
        transform.position = initialPosition + new Vector3(0.0f, verticalOffset, 0.0f);
    }
}
