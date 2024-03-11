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
            // 獲取碰撞力
            float collisionForce = a.impulse.magnitude;

            // 做一些處理，比如根據碰撞力觸發相應的事件
            Debug.Log("碰撞力：" + collisionForce);
            // 獲取碰撞力的方向
            Vector3 collisionDirection = a.relativeVelocity.normalized;

            // 做一些處理，比如根據碰撞力的方向做出反應
            Debug.Log("碰撞力的方向：" + collisionDirection);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞是否發生在預期的對象上
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