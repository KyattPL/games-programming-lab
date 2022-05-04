using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector2.up * Time.deltaTime * jumpForce);
        }
    }
}
