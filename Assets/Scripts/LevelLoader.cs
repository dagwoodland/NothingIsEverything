using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject messagePanel;
    public Image messageImage;
    public Sprite[] messageImages;

    private int levelNum;

    public void LoadLevel(int levelNumber) {
        levelNum = levelNumber;

        if (levelNum == -1) {
            ConfirmLevelChange();
        } else {
            messagePanel.SetActive(true);
            messageImage.sprite = messageImages[levelNum - 1];
        }
    }

    public void ConfirmLevelChange() {
        PlayerPrefs.SetInt("CurrentLevel", levelNum);
        if (levelNum == -1) {
            SceneManager.LoadScene("Level1");
        } else {
            SceneManager.LoadScene("Level" + levelNum);
        }
    }

}
