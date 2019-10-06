using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSpaceBarContinue : MonoBehaviour {

    public float blinkTime;

    private float timeSoFar;

    void Update() {
        timeSoFar += Time.deltaTime;

        if (timeSoFar > blinkTime) {
            timeSoFar = 0;
            GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
        }
    }
}
