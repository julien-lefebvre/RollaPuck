using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed = 0;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverScreen;

    private Rigidbody rb;
    private PeriodicCaller periodicCaller;
    private float movementX;
    private float movementY;
    private int collectedCount;
    private int health;
    private bool allowedToMove;

    void Start() {
        rb = GetComponent<Rigidbody>();
        periodicCaller = FindObjectOfType<PeriodicCaller>();
        collectedCount = 0;
        health = 100;
        SetCountText();
        SetHealthText();
        allowedToMove = true;
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        if (allowedToMove) {
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Collectible")) {
            other.gameObject.SetActive(false);
            collectedCount++;
            periodicCaller.DecrementCollectibleCount();
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Cup" && health > 0) {
            health -= (int) other.relativeVelocity.magnitude;
            if (health <= 0) {
                EndGame();
            }
            SetHealthText();
        }
    }

    private void SetCountText() {
       countText.text = collectedCount.ToString();
       finalScoreText.text = collectedCount.ToString();
    }

    private void SetHealthText() {
       healthText.text = "HEALTH :  " + health.ToString();
    }

    private void EndGame() {
        health = 0;
        allowedToMove = false;
        periodicCaller.StopCalling();
        gameOverScreen.SetActive(true);
    }

}
