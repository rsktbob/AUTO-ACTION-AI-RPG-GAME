using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidertest : MonoBehaviour
{
    private Collision a;
    void Update()
    {
        if(a != null)
        {
            // ����I���O
            float collisionForce = a.impulse.magnitude;

            // ���@�ǳB�z�A��p�ھڸI���OĲ�o�������ƥ�
            Debug.Log("�I���O�G" + collisionForce);
            // ����I���O����V
            Vector3 collisionDirection = a.relativeVelocity.normalized;

            // ���@�ǳB�z�A��p�ھڸI���O����V���X����
            Debug.Log("�I���O����V�G" + collisionDirection);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I���O�_�o�ͦb�w������H�W
        if (collision.gameObject.CompareTag("floor"))
        {
            a = collision;  
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        a = null;
    }
}