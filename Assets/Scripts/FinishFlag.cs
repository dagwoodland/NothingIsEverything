using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishFlag : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            if (PlayerPrefs.GetInt("LevelsComplete") == 0 || currentLevel >= PlayerPrefs.GetInt("LevelsComplete"))
                PlayerPrefs.SetInt("LevelsComplete", currentLevel);
            if (currentLevel == -1) {
                SceneManager.LoadScene("Story");
            } else {
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }

}
