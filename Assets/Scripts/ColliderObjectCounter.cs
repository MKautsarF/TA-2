using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObjectCounter : MonoBehaviour
{
    public Collider countingCollider; // Assign the collider in the Inspector
    public GameObject spawnPrefab; // Assign the prefab to spawn in the Inspector
    public GameObject spawnPrefab2; // Assign the prefab to spawn in the Inspector
    public GameObject spawnPrefab3; // Assign the prefab to spawn in the Inspector
    public GameObject sphere;
    public List<int> data;

    public buttonReader button;

    private bool hasSpawned = false;
    float mean = 107.3096234f;

    void Update()
    {
        // Get all colliders within the countingCollider bounds
        Collider[] collidersInside = Physics.OverlapBox(
            countingCollider.bounds.center, 
            countingCollider.bounds.extents, 
            Quaternion.identity
        );
        int count = 0;

        // Loop through the collidersInside array to count the game objects
        foreach (Collider collider in collidersInside)
        {
            // Check if the collider belongs to a game object you want to count
            if (collider.gameObject.CompareTag("collide")) // Change "YourTag" to the appropriate tag
            {
                count++;
            }
        }
        // if (count!=0){
            Debug.Log("Total objects inside collider "+countingCollider+": " + count);
            // }
        float SD = stdev();
        Debug.Log("standar deviasi "+countingCollider+": "+SD);
        float anomalyValue = anomaly(count, SD);
        Debug.Log("anomaly value "+countingCollider+": "+anomalyValue);

        if(count>=1 && hasSpawned == false)
        {
            if(anomalyValue>-1.061363f && anomalyValue<=(-0.5866115f)){ //1-48 atau 0<x<50
                sphere = Instantiate(spawnPrefab3, countingCollider.transform.position, Quaternion.identity);}
            else if(anomalyValue>(-0.5866115f) && anomalyValue<=(-0.1810943f)){//48-89 atau 50<x<100
                sphere = Instantiate(spawnPrefab2, countingCollider.transform.position, Quaternion.identity);}
            else if(anomalyValue>(-0.1810943f)){//>89 atau x>100
                sphere = Instantiate(spawnPrefab, countingCollider.transform.position, Quaternion.identity);}
            hasSpawned = true;
        }
        if(count<1 && hasSpawned == true)
        {
            hasSpawned = false;
            Destroy(sphere);
        }
    }

    

    float stdev()
    {
        // float mean = 107.3096234f;
        float sumOfSquaredDifferences = 0;
        int indexNow = button.getIndex();
        List<int> dataNow = makeData(indexNow);

        foreach (int x in dataNow)
        {
            float squaredDifference = Mathf.Pow(x - mean, 2);
            sumOfSquaredDifferences += squaredDifference;
        }

        float variance = sumOfSquaredDifferences / 108;
        float standardDeviation = Mathf.Sqrt(variance);

        return standardDeviation;
    }

    float anomaly(int y, float z)
    {
        float anomalyValue = (y-mean)/z;
        return anomalyValue;
    }

    List<int> makeData(int a)
    {
        switch (a)
        {
            case 0:
                data = new List<int> { 2,15,5,6,2,10,4,9,5,2,1,23,2,31,2,2,2,2,43,10,1,2,6,11,1,1,1,4,85,1,12,3,10,6,1,2,1,1,1 };
                break; //39
            case 1:
                data = new List<int> { 19,19,3,20,5,2,9,22,2,1,39,20,2,7,2,24,1,60,12,20,3,3,3,1 };
                break; //24
            case 2:
                data = new List<int> { 24,1,1,2,5,22,7,7,21,4,1,1,3,6,1,4,65,20,2,7,2 };
                break; //21
            case 3:
                data = new List<int> { 8,1,5,1,5,1,5,3,34,6,7,4,1,9,1,6,46,7,4,1,3 };
                break; //21
            case 4:
                data = new List<int> { 7,7,2,1,2,3,4,3,2,2,2,4,2,4,8,37,5,4,1,1,2,89,48,4,2,8,16 };
                break; //27
            case 5:
                data = new List<int> { 1,31,2,1,2,2,2,7,2,6,2,2,34,2,1,4,2,1,109,2,84,2,3,3,11 };
                break; //25
            case 6:
                data = new List<int> { 8,121,3,2,11,1,5,3,3,10,1,3,1,1,1,1,152,1,2,1,1,7,1,2,2,7,238,5,63,1 };
                break; //30
            case 7:
                data = new List<int> { 16,38,8,2,1,4,81,6,9,2,1,84,60,3,3 };
                break; //15
            case 8:
                data = new List<int> { 1,26,5,1,1,2,7,1,1,32,2,8,8,1,2,49,1,54 };
                break; //18
            case 9:
                data = new List<int> { 11,37,4,6,2,1,3,3,6,5,14,2,46,1,1,2,3,1,1,2,3,1,54,1,42,4,12 };
                break; //27
            case 10:
                data = new List<int> { 10,27,5,3,3,2,7,2,3,2,5,31,9,2,8,2,3,63,8,1,10,2,2 };
                break; //23
            case 11:
                data = new List<int> { 11,32,7,1,3,1,7,16,5,3,1,1,2,12,1,1,7,24,2,1,1,55,1,74,3,7,4,3,2 };
                break; //29
            case 12:
                data = new List<int> { 13,27,1,4,3,3,7,4,2,13,4,1,5,13,3,3,46,1,2,3,1,15,25,7,1,3,101,4,2,1,1,2,1,1,1 };
                break; //35
            case 13:
                data = new List<int> { 19,72,5,1,2,1,12,3,1,1,12,1,3,14,5,3,18,7,2,67,1,1,6,1,1,1,3,6,9,1,5,2,99,5,1,1,7,1,5 };
                break; //39
            case 14:
                data = new List<int> { 11,62,1,4,1,4,8,4,1,6,1,6,2,10,24,3,4,12,3,11,66,4,4,5,2,3,3,3,1,44,2,17,1,2,5,1,2,194,2,3,1,4,3,1,1,4,5,2,1,1,6,4 };
                break; //52
            case 15:
                data = new List<int> { 6,57,3,1,5,3,12,1,4,6,1,7,2,2,26,3,3,9,1,1,4,3,109,6,1,12,2,7,2,18,70,7,1,3,5,7,4,183,1,2,3,3,1,1,5,2,4,1,3,1,3,1 };
                break; //52
            case 16:
                data = new List<int> { 13,81,3,1,3,1,1,8,2,1,7,12,3,3,4,3,2,1,1,40,14,2,1,8,1,13,2,5,2,134,5,3,7,4,7,14,1,33,1,8,1,5,2,1,5,4,113,2,5,1,4,3,7,6,3,5,2,5,4,1,14,5 };
                break; //62
            case 17:
                data = new List<int> { 34,58,5,1,2,8,1,3,3,4,18,4,4,7,1,1,8,7,2,2,8,2,1,6,1,3,14,2,27,7,5,4,11,13,5,1,13,1,1,1,120,2,2,2,4,2,3,6,4,3,1,14,1,46,4,9,17,3,1,3,197,1,4,4,1,11,7,8,1,9,1,5,1,2,3,4,2,2,1,10,1 };
                break; //81
            case 18:
                data = new List<int> { 29,120,1,3,2,17,7,1,27,8,4,4,1,2,5,3,9,9,2,2,2,1,1,17,4,1,12,3,1,2,1,1,1,42,1,1,4,1,2,17,10,3,161,1,6,1,2,4,8,6,1,3,9,1,1,43,1,2,109,10,3,200,2,2,1,4,19,1,3,1,6,3,9,4,2,1,1,3,2,1,7,10,3,9,3 };
                break; //85
            case 19:
                data = new List<int> { 39,152,1,1,1,1,1,2,3,8,2,2,20,25,1,6,1,3,2,1,21,9,2,1,2,2,2,1,8,2,3,31,7,3,2,6,6,3,76,3,3,1,3,4,23,3,40,2,196,2,2,2,1,2,2,4,3,8,8,4,9,3,2,11,3,36,1,2,2,8,4,2,2,1,3,8,7,3,2,220,2,3,2,3,3,1,2,4,1,6,1,2,3,5,5,5,4,20,1,2,2,1,5 };
                break; //103
            case 20:
                data = new List<int> { 31,64,1,1,1,5,14,1,18,42,6,11,9,2,2,2,19,2,4,2,7,5,3,10,9,1,5,34,1,4,4,9,4,3,2,2,37,67,166,7,2,4,7,1,1,5,6,1,4,2,2,2,2,1,5,14,10,1,3,4,58,7,1,13,7,2,1,3,3,2,2,304,2,1,1,6,5,5,1,1,1,1,4,3,5,1,2,2,3,2,1,5,16,3,6,8,8 };
                break; //97
            case 21:
                data = new List<int> { 21,92,1,1,11,3,2,4,19,1,1,6,1,3,5,12,8,4,1,3,6,12,1,2,2,4,2,2,31,1,2,47,9,1,1,1,5,36,1,39,15,4,7,181,12,2,10,15,7,1,13,6,1,6,5,9,1,22,2,4,27,1,1,1,1,3,24,4,1,6,1,6,1,1,6,354,6,1,1,2,2,2,11,9,5,8,10,1,1,14,2,6,1,6,7,12,1,12,1,1 };
                break; //100
            case 22:
                data = new List<int> { 28,68,1,2,1,2,2,5,26,2,2,8,1,1,15,34,4,5,2,11,1,3,3,10,2,43,4,2,1,1,26,2,10,1,3,28,1,3,127,8,2,3,2,12,10,5,2,2,10,1,14,3,6,28,3,3,42,4,1,7,8,2,4,6,2,1,3,7,3,1,181,4,3,6,3,9,3,6,3,12,10,2,3,1,5,1,12,15,2,10 };
                break; //90
            case 23:
                data = new List<int> { 14,68,4,12,1,1,2,1,1,21,1,6,9,9,6,1,4,8,4,3,4,7,8,2,2,1,1,3,1,16,5,1,1,2,3,47,1,2,2,5,10,2,1,3,16,6,4,2,2,194,1,5,2,13,13,1,1,2,1,1,2,3,8,11,2,30,1,2,7,2,13,1,6,2,4,4,2,3,2,267,1,5,2,4,3,3,2,1,1,1,1,1,20,9,1,1,2,1,3,7,14,1,1,3 };
                break; //104
            case 24:
                data = new List<int> { 38,86,4,9,5,39,2,3,5,2,3,2,15,1,6,2,6,1,8,9,3,1,2,3,2,49,6,1,1,39,2,4,5,5,2,2,1,47,19,24,1,2,317,2,7,8,5,6,8,3,3,5,3,4,3,4,26,10,9,4,1,5,1,1,3,2,3,2,6,4,381,2,7,7,2,7,7,4,5,2,2,29,2,6,4,14,9,3,1,1,1,15,5,1,3,2 };
                break; //96
            case 25:
                data = new List<int> { 42,119,3,2,9,3,2,4,56,13,12,4,1,2,1,6,1,1,8,5,10,2,4,1,2,2,1,2,60,1,2,2,4,40,1,1,3,6,15,6,3,39,5,10,4,4,4,232,11,1,1,1,8,3,8,7,12,6,6,3,8,12,8,1,16,1,3,22,1,2,3,13,9,1,15,3,3,2,4,1,5,2,261,8,2,10,10,1,4,8,1,1,3,11,6,2,12,3,1,1,1,22,2,1,2,15 };
                break; //106
            case 26:
                data = new List<int> { 37,90,10,6,5,1,3,30,2,7,3,2,3,7,1,22,5,6,2,2,1,42,1,1,1,4,33,4,2,6,3,1,32,3,3,1,2,3,237,16,2,3,11,5,7,2,10,2,32,1,20,7,1,28,29,3,1,7,5,1,2,2,10,7,2,281,1,1,3,5,4,1,7,6,2,9,7,9,3,2,2,1,6,4,3,2,4,13,2,9 };
                break; //99
            case 27:
                data = new List<int> { 46,113,1,1,9,5,3,9,4,1,28,4,3,1,11,1,1,2,19,2,1,1,1,1,2,58,1,1,1,89,4,2,2,5,2,3,29,1,14,6,152,5,5,2,2,3,2,2,2,3,1,3,1,2,1,5,16,2,5,2,48,2,1,23,6,6,4,2,8,1,1,1,2,3,4,2,6,5,1,2,274,1,1,8,7,10,3,6,5,1,1,8,5,2,1,1,1,2,2,1,1,24,2,7,2,1,1 };
                break; // 108
            case 28:
                data = new List<int> { 53,92,1,3,3,8,4,3,13,2,3,1,1,9,8,11,4,2,2,1,1,5,3,1,1,48,4,1,71,7,1,13,2,2,1,3,1,47,42,2,8,361,1,3,2,3,2,1,2,1,2,1,6,4,7,1,4,20,2,35,2,2,16,8,1,2,1,9,16,2,1,1,1,1,244,3,9,4,4,7,6,4,7,6,3,17,1,2,2,1,1,14,1,2 };
                break; //94
            case 29:
                data = new List<int> { 32,97,1,2,2,4,1,1,1,4,2,3,1,2,9,3,2,2,3,8,1,2,3,6,1,1,1,8,1,19,5,2,1,56,2,8,5,1,3,25,37,1,9,1,2,225,2,3,1,2,2,6,3,1,3,7,13,15,5,2,3,25,3,2,1,2,9,2,1,7,1,2,1,2,5,2,1,223,3,6,5,3,2,1,7,2,1,2,4,2,4,1,8,2,3,1,1,2,2,1,25,4,6,1 };
                break; //104
            case 30:
                data = new List<int> { 22,54,1,6,1,4,6,1,1,2,7,4,1,5,2,2,34,2,5,3,3,10,4,29,4,2,4,177,1,2,2,2,2,4,1,1,1,1,2,1,2,3,2,11,4,2,20,1,2,43,8,2,5,1,4,1,1,166,2,7,1,2,3,1,4,3,8,3,2,2,1,1,1,2,4,36,7,1 };
                break; //79
            case 31:
                data = new List<int> { 37,33,5,2,3,1,6,1,1,1,3,1,1,8,1,1,1,32,2,3,18,10,3,1,119,5,8,5,1,1,4,1,1,1,1,7,1,1,12,4,3,7,1,8,3,1,1,107,3,1,5,1,1,12,15,1,1 };
                break; //57
            case 32:
                data = new List<int> { 17,31,1,2,1,6,5,5,2,1,2,2,3,1,1,18,2,3,4,3,13,13,3,1,1,2,107,2,1,2,1,1,3,2,5,1,8,4,5,1,7,1,1,1,98,4,1,5,1,1,2,9,3,2 };
                break; //55
            case 33:
                data = new List<int> { 9,44,1,2,4,2,8,2,5,1,15,2,1,2,2,2,7,1,2,18,2,3,1,5,4,1,2,1,104,2,2,2,2,5,1,2,1,14,2,1,5,2,4,1,5,114,1,2,2,2,4,1,1,2,2,6,5,11,4 };
                break; //59
            case 34:
                data = new List<int> { 9,31,5,1,1,2,1,6,16,4,2,1,3,1,4,9,2,4,9,1,2,1,1,130,1,1,2,2,1,3,11,6,1,3,3,2,2,117,1,1,6,1,1,1,2,1,2 };
                break; //47
            case 35:
                data = new List<int> { 4,13,1,2,1,1,2,1,2,1,14,1,1,7,1,1,6,4,1,74,3,1,3,1,1,1,1,1,9,1,3,4,1,1,1,1,1,78,1,1,1,1,1,2 };
                break; //44
            case 36:
                data = new List<int> { 9,23,1,1,1,1,2,1,1,1,1,1,1,5,1,1,1,11,1,17,3,1,7,1,56,4,1,1,1,2,1,2,5,2,2,57,2,1,1,1,1,7,1 };
                break; //43
            case 37:
                data = new List<int> { 10,25,1,1,1,1,1,2,3,1,10,1,4,4,9,26,2,1,1,1,2,3,1,1,1,1,1,1,24,1,1,1,1,1,3,1,1 };
                break; //37
            case 38:
                data = new List<int> { 6,12,1,1,5,2,1,2,1,6,1,4,1,2,4,2,1,27,1,2,1,2,2,1,1,6,2,1,28,1,1,1,1,3 };
                break; //34
            case 39:
                data = new List<int> { 3,15,1,2,2,1,3,2,1,5,2,1,1,1,18,1,2,1,2,1,3,1,21,1,1,1,2,1,2 };
                break; //29
            default:
                Debug.Log("Invalid choice.");
                data = new List<int>(); // Provide a default value or handle the case
                break;
        }

        return data;
    }

}
