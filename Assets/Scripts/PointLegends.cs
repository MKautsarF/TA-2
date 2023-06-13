using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointLegends : MonoBehaviour
{
    public bool renderHeatmapPrefabs = true;
    public bool renderHeatmapColor = true;
    public float heatmapScale;
    public GameObject heatmapPrefab;
    public GameObject infoPoint;
    public GameObject infoLabel;
    public int heatMapScale = 1;
    public float miniScale = 0.1f;
    public PointRenderer point;
    public List<int> listCount2 = new List<int>();
    public List<GameObject> listLabel2 = new List<GameObject>();
    GameObject labelObject2;
    TextMeshPro label2;
    int testcount;


    int i;

    void Awake()
    {       
        getListCount();

    }

    // Start is called before the first frame update
    void Start()
    {

        if (renderHeatmapPrefabs == true)
        {            
            PlaceHeatmapPoints();
            PlaceLabels();
        }
    }

    public List<int> getListCount()
    {
        listCount2 = point.getCount();
        return listCount2;
    }

    private void PlaceHeatmapPoints()
    {
        bool found = false;
        float t = 0;

        float x = 0;
        float y = 0;
        float z = 0;
        for (i=0; i<10;i++)
        {
            // buat bikin pointnya
            Vector3 positionH = new Vector3 (x,y,z) * heatmapScale;
            GameObject heatmapPoint = Instantiate (heatmapPrefab, Vector3.zero, Quaternion.identity);
            
            string heatmapPointName = i.ToString();
            heatmapPoint.transform.name = heatmapPointName;

            heatmapPoint.transform.parent = infoPoint.transform;
            heatmapPoint.transform.localPosition = positionH;
            heatmapPoint.transform.localScale = new Vector3(heatMapScale, heatMapScale, heatMapScale);

            // buat warnain pointnya
            if(renderHeatmapColor==true)
            {
                Color color = Color.Lerp(Color.red, Color.green, t);
                heatmapPoint.GetComponent<Renderer>().material.color = color;
                heatmapPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                heatmapPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            }
            
            // buat ganti t sama i
            if(found==false)
            {
                if(i==3)
                {
                    t = t + 0.25f;
                    found = true;
                }
                else
                {
                    t = t + 0.1f;
                }
            }
            else if((found==true)&(i==4))
            {
                t = t + 0.05f;
                found = false;
            }
            x = x + 2;
        }
    }

    public void PlaceLabels2(List<int> listC)
    {    
        float x = 2.25f;
        float y = 2.5f;
        float z = 4;
        float labelWidth = 25;
        for (int ix = 0; ix < listC.Count; ix++)
        {
            // Create a new label GameObject
            labelObject2 = new GameObject("Label");

            listLabel2.Add(labelObject2);

            // Add a TextMeshPro component to the label GameObject
            label2 = labelObject2.AddComponent<TextMeshPro>();

            // Set the text content of the label
            label2.text = "Jumlah: " + listC[ix];

            // Set the position of the label
            Vector3 position = new Vector3(x, y, z) * heatmapScale;
            label2.transform.position = position;

            // Set the parent of the label GameObject
            label2.transform.SetParent(infoLabel.transform);

            // Customize the scale
            Vector3 scale = new Vector3(miniScale, miniScale, miniScale); 

            // Set the scale of the label
            label2.transform.localScale = scale;

            // Set the width of the label
            label2.rectTransform.sizeDelta = new Vector2(labelWidth, label2.rectTransform.sizeDelta.y);
            
            // Increment the position values
            x += 2;


        }
        testcount = testcount + 1;
    }

    private void PlaceLabels()
    {
        float x = 2.25f;
        float y = 4;
        float z = 4;
        float jarakGempa;
        float labelWidth = 25;

        for (int i = 0; i < 10; i++)
        {
            // Create a new label GameObject
            GameObject labelObject = new GameObject("Label");

            // Add a TextMeshPro component to the label GameObject
            TextMeshPro label = labelObject.AddComponent<TextMeshPro>();

            jarakGempa = distanceGempa(i);
            // Set the text content of the label
            label.text = "<= " + jarakGempa + " km";

            // Set the position of the label
            Vector3 position = new Vector3(x, y, z) * heatmapScale;
            label.transform.position = position;

            // Set the parent of the label GameObject
            label.transform.SetParent(infoLabel.transform);

            // Customize the scale
            Vector3 scale = new Vector3(miniScale, miniScale, miniScale); 

            // Set the scale of the label
            label.transform.localScale = scale;

            // Set the width of the label
            label.rectTransform.sizeDelta = new Vector2(labelWidth, label.rectTransform.sizeDelta.y);
            
            // Increment the position values
            x += 2;
        }
    }

    public float distanceGempa(int number)
    {
        float distance;
        switch (number)
        {
            case 0:
                distance = 0;
                break;
            case 1:
                distance = 12.361f;
                break;
            case 2:
                distance = 52.823f;
                break;
            case 3:
                distance = 58.571f;
                break;
            case 4:
                distance = 96.281f;
                break;
            case 5:
                distance = 119.29f;
                break;
            case 6:
                distance = 142.923f;
                break;
            case 7:
                distance = 162.663f;
                break;
            case 8:
                distance = 175.938f;
                break;
            case 9:
                distance = 204.24f;
                break;
            default:
                distance = number;
                break;
        }

        return distance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveLabels2()
    {
        foreach(var labelObject2 in listLabel2)
        {
            Destroy(labelObject2);
        }
        listLabel2.Clear();
        
        listCount2.Clear();

    }
}
