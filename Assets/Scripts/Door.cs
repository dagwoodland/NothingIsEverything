using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject closedCollider;
    public SpriteRenderer doorRenderer;
    public Sprite[] doorSprites;
    public float timeBetweenFrames;

    private int animationDirection;
    private int currentFrame;
    private float timeSinceLastFrame;

    // Update is called once per frame
    void Update() {
        timeSinceLastFrame += Time.deltaTime;
        if (timeSinceLastFrame > timeBetweenFrames) {
            timeSinceLastFrame = 0;
            currentFrame += animationDirection;

            if (currentFrame < 0) {
                currentFrame = 0;
            }

            if (currentFrame >= doorSprites.Length) {
                currentFrame = doorSprites.Length - 1;
                closedCollider.SetActive(true);
            } else {
                closedCollider.SetActive(false);
            }

            doorRenderer.sprite = doorSprites[currentFrame];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        animationDirection = 1;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        animationDirection = -1;
    }
}
