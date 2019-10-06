using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    public Sprite[] sprites;
    public float frameGap;
    public float speed;
    public float chaseRadius;

    private int currentFrame;
    private float timeSinceLastFrame;
    private Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) <= chaseRadius) {

            int layerMask = 1 << 10 | 1 << 11;
            layerMask = ~layerMask;
            var hit2D = Physics2D.Raycast(transform.position, player.position - transform.position, chaseRadius, layerMask);

            if (hit2D) {
                if (hit2D.collider.gameObject.tag == "Player") {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    timeSinceLastFrame += Time.deltaTime;
                    if (timeSinceLastFrame > frameGap) {
                        timeSinceLastFrame = 0;
                        currentFrame += 1;
                        if (currentFrame >= sprites.Length) {
                            currentFrame = 0;
                        }
                    }
                    GetComponent<SpriteRenderer>().sprite = sprites[currentFrame];
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
