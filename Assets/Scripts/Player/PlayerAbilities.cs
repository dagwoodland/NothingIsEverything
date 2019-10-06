using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    // All, None, Dash, Grapple, Laser, Jetpack
    private int dashMinLevel = 2;
    private int grappleMinLevel = 3;
    private int laserMinLevel = 4;
    private int jetpackMinLevel = 5;

    [Header("Dash")]
    public float dashLength;
    public float dashMaxCooldown;
    public GameObject dashPrefab;

    [Header("Laser")]
    public SpriteBetweenTwoPoints laser;
    public float laserBackgroundTime;
    public GameObject laserPrefab;

    [Header("Grappling Hook")]
    public SpriteBetweenTwoPoints grapple;
    public float grappleSpeed;

    private float dashCooldown;
    private float timeSinceBackgroundLaser;

    private Vector2 grapplePoint;
    private bool grappleEnabled;
    private bool isGrappledToSolid;
    private float grappleTime;
    private bool isGrappleReturning;
    private Vector2 oldGrapplePlayerPos;
    private int currentLevel;

    void Start() {
        grapple.tilingHeight = 0.67f;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    void Update() {
        // Dash
        if (currentLevel == -1 || currentLevel >= 2) {
            if (dashCooldown <= 0) {
                int layerMask = 1 << 9 | 1 << 10 | 1 << 11;
                layerMask = ~layerMask;
                if (Input.GetMouseButtonDown(0)) {
                    GetComponent<AudioSource>().Play();
                    dashCooldown = dashMaxCooldown;
                    float dashAmount = dashLength * GetComponent<PlayerMovement>().direction;

                    if (!Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right * GetComponent<PlayerMovement>().direction), dashLength, layerMask)) {
                        transform.position += new Vector3(dashAmount, 0);
                    }
                    GameObject dashObject = Instantiate(dashPrefab, transform.position - new Vector3(dashAmount / 2, 0), Quaternion.identity);
                    if (dashAmount > 0) {
                        dashObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
            } else {
                dashCooldown -= Time.deltaTime;
            }
        }


        // Laser
        if (currentLevel == -1 || currentLevel >= 4) {
            if (Input.GetMouseButton(1)) {
                int layerMask = 1 << 9 | 1 << 10;
                layerMask = ~layerMask;
                laser.gameObject.SetActive(true);
                var pos = Input.mousePosition;
                pos.z = -Camera.main.transform.position.z;
                pos = Camera.main.ScreenToWorldPoint(pos);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, pos - transform.position, Vector2.Distance(transform.position, pos), layerMask);
                if (hit) {
                    pos = hit.point;
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                        Destroy(hit.collider.gameObject);
                    }
                }
                laser.UpdatePosition(transform.position, pos);

                timeSinceBackgroundLaser += Time.deltaTime;
                if (timeSinceBackgroundLaser > laserBackgroundTime) {
                    timeSinceBackgroundLaser = 0;
                    GameObject backgroundLaser = Instantiate(laserPrefab);
                    backgroundLaser.GetComponent<SpriteBetweenTwoPoints>().UpdatePosition(transform.position, pos);
                    backgroundLaser.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
            } else {
                laser.gameObject.SetActive(false);
            }
        } else {
            laser.gameObject.SetActive(false);
        }

        // Grapple
        if (currentLevel == -1 || currentLevel >= 3) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                int layerMask = 1 << 9 | 1 << 10;
                layerMask = ~layerMask;

                grapple.gameObject.SetActive(true);
                var pos = Input.mousePosition;
                pos.z = -Camera.main.transform.position.z;
                pos = Camera.main.ScreenToWorldPoint(pos);

                var distance = Vector3.Distance(transform.position, pos) + 5f;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, pos - transform.position, distance, layerMask);
                if (hit) {
                    grapplePoint = hit.point;
                    isGrappledToSolid = true;
                } else {
                    grapplePoint = Vector2.MoveTowards(transform.position, pos, distance);
                }
                grapple.UpdatePosition(transform.position, pos);
                grappleEnabled = true;
                grapple.gameObject.SetActive(true);
            }

            if (grappleEnabled) {
                grappleTime += Time.deltaTime;
                var time = grappleTime;


                time /= grappleSpeed;

                if (isGrappleReturning) {
                    time = (2 - grappleTime / grappleSpeed);
                }

                if (isGrappleReturning && isGrappledToSolid) {
                    transform.position = Vector2.Lerp(oldGrapplePlayerPos, grapplePoint - (Vector2.MoveTowards(oldGrapplePlayerPos, grapplePoint, 0.5f) - oldGrapplePlayerPos), 1 - time);
                    grapple.UpdatePosition(transform.position, grapplePoint);
                } else {
                    grapple.UpdatePosition(transform.position, Vector2.Lerp(transform.position, grapplePoint, time));
                }

                if (grappleTime / grappleSpeed > 1) {
                    isGrappleReturning = true;

                    if (isGrappledToSolid) {
                        oldGrapplePlayerPos = transform.position;
                    }
                }

                if (Vector2.Distance(oldGrapplePlayerPos, grapplePoint) < 0.1f) {
                    grappleEnabled = false;
                    isGrappledToSolid = false;
                    grappleTime = 0;
                    isGrappleReturning = false;
                    grapple.gameObject.SetActive(false);
                }

                if (grappleTime / grappleSpeed > 2) {
                    grappleEnabled = false;
                    isGrappledToSolid = false;
                    grappleTime = 0;
                    isGrappleReturning = false;
                    grapple.gameObject.SetActive(false);
                }
            }
        }
    }
}