using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBetweenTwoPoints : MonoBehaviour {

    public float tilingHeight = 1.59f;

    public void UpdatePosition(Vector2 posA, Vector2 posB) {
        // Position
        transform.position = (posA + posB) / 2;

        // Rotatation
        float xDistance = Mathf.Abs(posA.x - posB.x);
        float yDistance = Mathf.Abs(posA.y - posB.y);
        if (System.Math.Abs(xDistance) < Mathf.Epsilon) {
            return;
        }
        float angle = Mathf.Rad2Deg * Mathf.Atan(yDistance / xDistance);
        if (posA.x < posB.x ^ posA.y < posB.y) {
            angle = -angle;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Tiling
        float totalDisance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
        GetComponent<SpriteRenderer>().size = new Vector2(totalDisance * 5f, tilingHeight);
    }

}
