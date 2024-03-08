using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollision : MonoBehaviour
{
    public bool isTouchFloor = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I���O�_�o�ͦb�w������H�W
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isTouchFloor = false;
        }

    }
}
