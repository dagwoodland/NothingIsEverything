using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableForLevelOne : MonoBehaviour {

    public GameObject levelNegativeMusic;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.GetInt("CurrentLevel") != 1) {
            gameObject.SetActive(false);
        } else {
            levelNegativeMusic.SetActive(false);
        }
    }
}
