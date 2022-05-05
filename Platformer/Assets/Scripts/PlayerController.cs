using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private InputActionsAsset inputActions;
    [SerializeField]
    private int jumpHeight;
    [SerializeField]
    private int runSpeed;
    private int currJumpVelocity;
    private int currRunVelocity;
    private bool isMovingRight;
    private bool isMovingLeft;
    public int score;
    public int lives;
    public event Action OnGemPickup;
    public event Action OnEnemyHit;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("player_score", 0);
        lives = PlayerPrefs.GetInt("player_lives", 3);
        playerAnimator = GetComponent<Animator>();
        
        inputActions = new InputActionsAsset();
        inputActions.PlayerActions.Runright.performed += ctx => goRight();
        inputActions.PlayerActions.Runright.canceled += ctx => stopMovingRight();
        inputActions.PlayerActions.Runleft.performed += ctx => goLeft();
        inputActions.PlayerActions.Runleft.canceled += ctx => stopMovingLeft();
        inputActions.PlayerActions.Jump.performed += ctx => StartCoroutine(jump());
        inputActions.PlayerActions.Enable();
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(currRunVelocity, currJumpVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingLeft && !isMovingRight) {
            currRunVelocity = -1 * runSpeed;
            playerAnimator.SetInteger("action", 1);
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (isMovingRight && !isMovingLeft) {
            currRunVelocity = runSpeed;
            playerAnimator.SetInteger("action", 1);
            GetComponent<SpriteRenderer>().flipX = false;
        } else {
            currRunVelocity = 0;
            playerAnimator.SetInteger("action", 0);
        }
    }

    void OnDestroy() {
        StopAllCoroutines();
        inputActions.PlayerActions.Jump.performed -= ctx => StartCoroutine(jump());
        inputActions.PlayerActions.Disable();
    }

    IEnumerator jump() {

        if (this != null) { 
        currJumpVelocity = jumpHeight;
        yield return new WaitForSeconds(0.2f);
        currJumpVelocity = 0;
        }
    }

    void goRight() {
        isMovingRight = true;
    }

    void goLeft() {
        isMovingLeft = true;
    }

    void stopMovingRight() {
        isMovingRight = false;
    }

    void stopMovingLeft() {
        isMovingLeft = false;
    }

    void playStompSound() {
        GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.tag == "Enemy") {
            lives -= 1;
            OnEnemyHit();
            if (lives == 0) {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Level-1");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D obj) {
        if (obj.tag == "Gem") {
            score += 1;
            OnGemPickup();
            Destroy(obj.gameObject);
        }
    }
}
