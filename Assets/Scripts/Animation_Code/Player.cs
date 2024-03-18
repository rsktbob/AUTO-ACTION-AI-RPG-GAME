using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed;
    public bool walking;
    public Transform playerTrans;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetTrigger("idle");
            playerAnim.ResetTrigger("walk");
            walking = false;
        }
    }
}
