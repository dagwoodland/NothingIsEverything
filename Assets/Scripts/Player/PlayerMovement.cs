using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed;
    public float jumpAmount;

    public int direction = 1;

    public GameObject jetpackAudioSource;

    private PlayerWalkAnim walkAnim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRigidbody;

    private bool isGrounded;

    private bool jetpackEnabled;
    private int currentLevel;

    void Start() {
        walkAnim = GetComponent<PlayerWalkAnim>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    void Update() {
        walkAnim.isAnimating = false;

        // Walking
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q)) {
            transform.position -= transform.right * walkSpeed * Time.deltaTime;
            walkAnim.isAnimating = true;
            spriteRenderer.flipX = true;
            direction = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.position += transform.right * walkSpeed * Time.deltaTime;
            walkAnim.isAnimating = true;
            spriteRenderer.flipX = false;
            direction = 1;
        }

        // Ground Detection
        int layerMask = 1 << 9 | 1 << 10;
        layerMask = ~layerMask;
        bool left = Physics2D.Raycast(transform.position + new Vector3(-0.2f, 0), transform.TransformDirection(Vector3.down), 1f, layerMask);
        bool right = Physics2D.Raycast(transform.position + new Vector3(0.2f, 0), transform.TransformDirection(Vector3.down), 1f, layerMask);
        isGrounded = left || right;

        // Jump
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z)) {
            if (isGrounded) {
                playerRigidbody.velocity += Vector2.up * jumpAmount;
            }
        }

        // Jetpack
        if (currentLevel == -1 || currentLevel >= 5) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z)) {
                if (!isGrounded) {
                    playerRigidbody.velocity = new Vector2();
                    playerRigidbody.gravityScale = 0;
                    walkAnim.SetCurrentSpriteArray(walkAnim.jetpackSprites, false);
                    jetpackAudioSource.SetActive(true);
                }
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)) {
                if (!isGrounded && playerRigidbody.velocity.y < 0.1 && playerRigidbody.velocity.y > -0.1) {
                    playerRigidbody.gravityScale = 0;
                    walkAnim.SetCurrentSpriteArray(walkAnim.jetpackSprites, false);
                    jetpackAudioSource.SetActive(true);
                }
            } else {
                jetpackAudioSource.SetActive(false);
                playerRigidbody.gravityScale = 1;
                walkAnim.SetCurrentSpriteArray(walkAnim.walkingSprites, true);
            }
        }

        // Fall Detection
        if (transform.position.y < -10) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
