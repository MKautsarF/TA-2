using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class testTrigger : MonoBehaviour
{

    public logicManager logic;
    public FirstPersonController fps;
    public float height;
    public string lokasiSekarang;

    public PointRenderer point;
    public float heightPoint;

    public GameObject prefabLangit; 
    public float heightLangit;

    public GameObject prefabPulau;
    public Collider collider1;
    public float heightPulau;

    public GameObject prefabLaut;
    public float heightLaut;

    public GameObject prefabCollider;
    public float heightCollider;

    public bool isInsideCollider;

    void start()
    {
        isInsideCollider = false;
        collider1 = prefabCollider.GetComponent<Collider>();
    }

    void update()
    {
        if (isInsideCollider)
        {
            logic.pindahLokasi(1); 
        }
        else
        {
            logic.pindahLokasi(2); 
        }
    }


    public void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("pulau")) 
        {
            Vector3 pointToCheck = collision.transform.position;

            // Check if the point is inside the custom collider
            if (IsPointInsideCollider())
            {
                isInsideCollider = true; // Set the flag to true if the point is inside the custom collider
            }
            else
            {
                isInsideCollider = false; // Set the flag to false if the point is outside the custom collider
            }
        }
    }

    public bool IsPointInsideCollider()
    {
        Bounds bounds = collider1.bounds;

        // Create a ray from the center of the collider's bounds.
        var ray = new Ray(bounds.center, bounds.center - transform.position);

        // Check if the ray hits anything.
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            return true;
        }
        else
        {
            return false; // Set the flag to false if the point is outside the custom collider
        }
    }

}
