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

    public GameObject prefabLangit; // ga diperlukan nih kayanya
    public float heightLangit;

    public GameObject prefabPulau;
    public Collider collider;
    public float heightPulau;

    public GameObject prefabLaut;
    public float heightLaut;

    public GameObject prefabCollider;
    public float heightCollider;

    // public float jarak;
    public bool isInsideCollider;

    void start()
    {
        // logic = GameObject.FindGameObjectWithTag("test").GetComponent<logicManager>();
        isInsideCollider = false;
        collider = prefabCollider.GetComponent<Collider>();
    }

    void update()
    {
        if (isInsideCollider)
        {
            logic.pindahLokasi(1); 
            // Character is inside the custom collider (e.g., on the land)
        }
        else
        {
            logic.pindahLokasi(2); 
            // Character is outside the custom collider (e.g., on the sea)
        }
    }

    // public void OnTriggerEnter(Collider collision)
    // {
    //     height = fps.GetCurrentYPosition();
    //     if(height>2)
    //     {
    //         logic.pindahLokasi(2);

    //     }
    //     else if(height<=2)
    //     {
    //         logic.pindahLokasi(1);
    //     }
    //     lokasiSekarang = logic.getLokasi();
    // }

    public void OnTriggerStay(Collider collision)
    {
        // height = fps.GetCurrentYPosition();
        // heightPoint = point.getPointY();
        // heightLangit = prefabLangit.transform.position[1];
        // heightPulau = prefabPulau.transform.position[1];
        // heightLaut = prefabLaut.transform.position[1];
        // heightCollider = prefabCollider.transform.position[1];

        // // jarak = Mathf.Abs(heightCollider - )
        // if(collision.CompareTag("pulau"))
        // // if(height>2)
        // {
        //     logic.pindahLokasi(2);

        // }
        // else
        // // else if(height<=2)
        // {
        //     logic.pindahLokasi(1);
        // }
        // lokasiSekarang = logic.getLokasi();
        if (collision.CompareTag("pulau")) // Replace "YourTag" with the appropriate tag of the object you want to detect
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

    // private bool IsPointInsideCollider(Vector3 point, Collider collider)
    public bool IsPointInsideCollider()
    {
        // // Create a ray from the point to check towards a known outside direction
        // Ray ray = new Ray(point, Vector3.down);

        // // Use a raycast to check if the ray intersects with the collider
        // RaycastHit hitInfo;
        // if (collider.Raycast(ray, out hitInfo, float.MaxValue))
        // {
        //     return true; // The point is inside the collider
        // }

        // return false; // The point is outside the collider
        Bounds bounds = collider.bounds;

        // Create a ray from the center of the collider's bounds.
        var ray = new Ray(bounds.center, bounds.center - transform.position);

        // Check if the ray hits anything.
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // The ray hit something inside the collider.
            // Debug.Log("Hit something!");
            return true;
        }
        else
        {
            return false; // Set the flag to false if the point is outside the custom collider
        }
    }

    // public void OnTriggerExit(Collider collision)
    // {
    //     height = fps.GetCurrentYPosition();
    //     if(height>2)
    //     {
    //         logic.pindahLokasi(2);

    //     }
    //     else if(height<=2)
    //     {
    //         logic.pindahLokasi(1);
    //     }
    //     lokasiSekarang = logic.getLokasi();
        
    // }
}
