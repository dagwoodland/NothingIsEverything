using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnim : MonoBehaviour {

    public bool isAnimating;

    public Sprite[] jetpackSprites;
    public Sprite[] walkingSprites;

    public float frameGap;

    private Sprite[] currentSpriteArray;
    private int currentFrame;
    private float timeSinceLastFrame;
    private bool allowStopping = true;

    private void Start() {
        SetCurrentSpriteArray(walkingSprites, true);
    }

    public void SetCurrentSpriteArray(Sprite[] newArray, bool allowStopping) {
        if (newArray != currentSpriteArray) {
            currentFrame = 0;
            currentSpriteArray = newArray;
        }
        this.allowStopping = allowStopping;
    }

    void Update() {
        if (isAnimating || !allowStopping) {
            timeSinceLastFrame += Time.deltaTime;
            if (timeSinceLastFrame > frameGap) {
                timeSinceLastFrame = 0;
                currentFrame += 1;
                if (currentFrame >= currentSpriteArray.Length) {
                    currentFrame = 0;
                }

                GetComponent<SpriteRenderer>().sprite = currentSpriteArray[currentFrame];
            }
        } else {
            GetComponent<SpriteRenderer>().sprite = currentSpriteArray[0];
        }
    }
}
