using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    public float fadeOutDuration;
    public float initialAlpha;

    private SpriteRenderer spriteRenderer;

    private float timeSoFar;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        timeSoFar += Time.deltaTime;

        var color = spriteRenderer.color;
        color.a = initialAlpha - timeSoFar / fadeOutDuration * initialAlpha;
        spriteRenderer.color = color;

        if (timeSoFar >= fadeOutDuration) {
            Destroy(gameObject);
        }
    }
}
