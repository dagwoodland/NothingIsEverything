using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimateStory : MonoBehaviour {

    public float farLeftAmount;
    //public float farRightAmount;
    public float lineSize;
    public float initialOffset;
    public float animationTime;

    private bool paused;

    private int currentLine;
    private float lineTime;
    private RectTransform rect;

    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        rect.SetTop(initialOffset);
    }

    // Update is called once per frame
    void Update() {
        if (!paused) {
            lineTime += Time.deltaTime;
            float lineProgress = 1 - lineTime / animationTime * 2;
            rect.SetRight(farLeftAmount * lineProgress);

            if (lineProgress < -1) {
                lineTime = 0;
                currentLine += 1;
                rect.SetTop(initialOffset + lineSize * currentLine);
                rect.SetBottom(-initialOffset - lineSize * currentLine);
                paused = true;
                rect.SetRight(farLeftAmount);
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                paused = false;
                if (currentLine == 8) {
                    SceneManager.LoadScene("LevelSelect");
                }
            }
        }
    }
}
