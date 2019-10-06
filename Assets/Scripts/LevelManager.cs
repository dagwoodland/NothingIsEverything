using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public Sprite checkmarkTexture;
    public Sprite lockTexture;

    public Image[] levelImages;

    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start() {
        SetLevels();
    }

    private void SetLevels() {
        var levelsCompleted = PlayerPrefs.GetInt("LevelsComplete");

        for (int i = 0; i < levelImages.Length; i++) {
            levelImages[i].enabled = true;
        }

        if (levelsCompleted == -1) {
            levelImages[0].sprite = checkmarkTexture;
            levelImages[1].enabled = false;
        }
        if (levelsCompleted > 0) {
            levelImages[0].sprite = checkmarkTexture;
        }
        if (levelsCompleted == 0) {
            levelImages[0].enabled = false;
        }

        for (int i = 1; i < levelImages.Length; i++) {
            levelImages[i].sprite = lockTexture;
            levelButtons[i].interactable = false;

            if (i == levelsCompleted + 1) {
                levelImages[i].enabled = false;
                levelButtons[i].interactable = true;
            }

            if (i <= levelsCompleted) {
                levelImages[i].sprite = checkmarkTexture;
                levelButtons[i].interactable = true;
            }
        }

        if (levelsCompleted == 0) {
            levelImages[1].enabled = true;
            levelButtons[1].interactable = false;
        }
        if (levelsCompleted == -1) {
            levelButtons[1].interactable = true;
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Backspace) && Input.GetKeyDown(KeyCode.P)) {
            PlayerPrefs.DeleteAll();
            SetLevels();
        }
    }
}