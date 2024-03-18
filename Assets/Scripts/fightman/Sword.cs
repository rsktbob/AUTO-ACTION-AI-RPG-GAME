using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    private Collider colliderComponent;
    public bool isInBody = false;
    [Header("Event")]
    public UnityEvent<float> SwordRewardEvent;
    // Start is called before the first frame update
    void Start()
    {
        colliderComponent = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("body"))
        {
            isInBody = true;
            SwordRewardEvent.Invoke(1);//touch body

            //Get the normalized velocity vector
            Vector3 velocity = GetComponent<Rigidbody>().velocity;

            //calculate normal vectors
            Vector3 A = transform.GetChild(0).transform.position;
            Vector3 B = transform.GetChild(1).transform.position;
            Vector3 C = transform.GetChild(2).transform.position;
            Vector3 D = transform.GetChild(3).transform.position;
            Vector3 AB = B - A;
            Vector3 AC = C - A;
            Vector3 AD = D - A;
            Vector3 BA = A - B;
            Vector3 normalVector1 = Vector3.Cross(AB, AC).normalized;
            Vector3 normalVector2 = Vector3.Cross(BA, AC).normalized;
            Vector3 normalVector3 = Vector3.Cross(AB, AD).normalized;

            //calculate Dot Products
            float dotProduct = 0;
            if (Vector3.Angle(normalVector1, velocity) < 45)
            {
                dotProduct = Vector3.Dot(normalVector1, velocity);
            }
            else if (Vector3.Angle(normalVector2, velocity) < 45)
            {
                dotProduct = Vector3.Dot(normalVector2, velocity);
            }
            else if (Vector3.Angle(normalVector3, velocity) < 45)
            {
                dotProduct = Vector3.Dot(normalVector3, velocity);
            }
            else
            {
                colliderComponent.isTrigger = false;
                return;
            }
            SwordRewardEvent.Invoke(5 + dotProduct);//good angle
            if (dotProduct < 4)
            {
                colliderComponent.isTrigger = false;
                return;
            }
            SwordRewardEvent.Invoke(15 + dotProduct);//slash successful
        }
        else
        {
            if (colliderComponent != null)
            {
                colliderComponent.isTrigger = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("body"))
        {
            isInBody = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("body"))
        {
            if (colliderComponent != null)
            {
                colliderComponent.isTrigger = false;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        colliderComponent.isTrigger = true;
    }
}
