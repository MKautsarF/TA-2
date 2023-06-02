using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script gets values from CSVReader script
// It instantiates points and particles according to values read

public class PointRenderer : MonoBehaviour {

    //********Public Variables********

    // Bools for editor options
    public bool renderPointPrefabs = true;
    public bool renderParticles =  true;
    public bool renderPrefabsWithColor = true;

    // Name of the input file, no extension
    public string inputfile;

    // Indices for columns to be assigned
    public int column1 = 0;
    public int column2 = 1;
    public int column3 = 2;
    public int column4 = 3; //input kolom identifier gempa

    float a;
    float b;
    float c;
    float x;
    float y;
    float z;

    float a1;
    float b1;
    float c1;
    float x1;
    float y1;
    float z1;

    // container buat maid = datagempa
    public string magicWords; 
    string myString;
    string myString2;
     
    // Full column names from CSV (as Dictionary Keys)
    public string xColumnName;
    public string yColumnName;
    public string zColumnName;
    public string aColumnName; // placeholder kolom gempa

    // Scale of particlePoints within graph, WARNING: Does not scale with graph frame
    private float plotScale = 100; // defaultnya 10
    
    // Scale of particlePoints within graph, WARNING: Does not scale with graph frame
    public float gempaScale = 100; // defaultnya 10

    // Scale of the prefab particlePoints
    [Range(0.0f, 0.5f)]
    public float pointScale = 0.25f;
    
    // Scale of the prefab particlePoints
    [Range(0.0f, 0.5f)]
    public float pointGempaScale = 0.25f;


    // Changes size of particles generated
    [Range(0.0f, 2.0f)]
    public float particleScale = 2.0f;

    // The prefab for the data particlePoints that will be instantiated
    public GameObject PointPrefab;
    public GameObject GempaPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;
    public GameObject GempaHolder;
    public GameObject dataPoint;
    public GameObject gempaPoint;
    Vector3 position;
    Vector3 center;
    Vector3 center2;
    Vector3 position2;
    Vector3 panjang;
    Vector3 center3;
    Color color;
    // float maxDistance = 0f;

    // Color for the glow around the particlePoints
    private Color GlowColor; 
    
    //********Private Variables********
        // Minimum and maximum values of columns
    private float xMin;
    private float yMin;
    private float zMin;

    private float xMax;
    private float yMax;
    private float zMax;

    private float pMax;
    private float pMin;

    private float z1Max;
    private float z1Min;

    // Number of rows
    private int rowCount;

    // List for holding data from CSV reader
    public List<Dictionary<string, object>> pointList;
    public List<GameObject> dPoints = new List<GameObject>();
    public List<GameObject> dGempa = new List<GameObject>();

    // Particle system for holding point particles
    private ParticleSystem.Particle[] particlePoints; 

    // manggil current location
    public logicManager logic;
    public string posisi;
    public testTrigger trigger;
    public float tinggiY;

    public buttonReader button;


    //********Methods********
    public void UpdateVisualization(string file)
    {
        pointList = CSVReader.Read(file);
    }

    void Awake()
    {
        //Run CSV Reader
        // pointList = CSVReader.Read(inputfile);
        UpdateVisualization(inputfile);
    }

    // Use this for initialization
    void Start () 
	{
        
        //Debug.Log(pointList);
        // Store dictionary keys (column names in CSV) in a list
        List<string> columnList = new List<string>(pointList[1].Keys);

        Debug.Log("There are " + columnList.Count + " columns in the CSV");
        //Debug.Log(columnList);

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // string myString = "Sepal.Length";
        // string myString2 = "Sepal.Length";

        // if (myString == myString2) 
        // {
        //     Debug.Log("The string is 'Hello'.");
        // } 
        // else if (myString == "World") 
        // {
        //     Debug.Log("The string is 'World'.");
        // } 
        // else 
        // {
        //     Debug.Log("The string is something else.");
        // }


        // Assign column names according to index indicated in columnList
        xColumnName = columnList[column1];
        yColumnName = columnList[column2];
        zColumnName = columnList[column3];
        aColumnName = columnList[column4];
        
        // Get maxes of each axis, using FindMaxValue method defined below
        // xMax = FindMaxValue(xColumnName);
        // yMax = FindMaxValue(yColumnName);
        // zMax = FindMaxValue(zColumnName);

        // // Get minimums of each axis, using FindMinValue method defined below
        // xMin = FindMinValue(xColumnName);
        // yMin = FindMinValue(yColumnName);
        // zMin = FindMinValue(zColumnName);
        // z1Max = -(zMin);
        // z1Min = -(zMax);

        xMax = 130.885f;
        yMax = 10;
        xMin = 127.8626f;
        yMin = 1;
        z1Max = 3.878415f;
        z1Min = 2.787903f;

        pMax = FindMaxBgt();
        pMin = FindMinBgt();

        // karena tau semua altitude minus makan nilai yang menyimpan mereka dirubah 
        // note kalau misalnya altitude bukan di sumbu z lagi maka mohon disesuaikan
        // Debug.Log("xMax: " + xMax);
        // Debug.Log("xMin: " + xMin);
        // Debug.Log("z1Max: " + z1Max);
        // Debug.Log("z1Min: " + z1Min);

        // Debug.Log(pMax);
        // Debug.Log(pMin);
            
        // Debug.Log(xMin + " " + yMin + " " + zMin); // Write to console

        AssignLabels();

        if (renderPointPrefabs == true)
        {
            // int cek = 0;
            // bool found = false;
            // Call PlacePoint methods defined below
            PlacePrefabPoints();
            // Debug.Log(dPoints);
            if (myString2 != null)
            {
                PlacePrefabGempa();
                // Debug.Log(dGempa.Count);
            }
            // for (var i = 0; i < pointList.Count; i++)
            // {
                // if ((pointList[i][aColumnName] == magicWords))
                // {
                    // PlacePrefabGempa();
                    // found = true;
                    // cek=cek+1
                // }
                    // while( found != true )
                    // {
                    //     if (pointList[i][aColumnName] == magicWords)
                    //     {
                    //         // PlacePrefabGempa();
                    //         found = true;
                    //         cek=cek+1;   
                    //     }
                    //     // found = false;
                    // }
                    
                // PlacePrefabPoints();
                // cek=cek+1;   
            // }

        }            
                    

        // If statement to turn particles on and off
        if ( renderParticles == true)
        {
            // Call CreateParticles() for particle system
            CreateParticles();

            // Set particle system, for point glow- depends on CreateParticles()
            GetComponent<ParticleSystem>().SetParticles(particlePoints, particlePoints.Length);
        }
                                
    }
    
		
	// Update is called once per frame
	void Update ()
    {
        // Debug.Log("masuk ganti csv-1");
        if(Input.GetKey(KeyCode.UpArrow))
        {
            button.ReadNextCSVFile();
        }
        
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            button.ReadPreviousCSVFile();
        }

        if(inputfile != button.updateCSV())
        {
            // pointList = CSVReader.Read(button.updateCSV());
            inputfile = button.updateCSV();
            UpdateVisualization(inputfile);
            GameObject.Find("Dataset_Label").GetComponent<TextMesh>().text = inputfile;
            GameObject.Find("Point_Count").GetComponent<TextMesh>().text = pointList.Count.ToString("0");
            
            // panggil fungsi hapus visual lama
            // Destroy(dataPoint);
            // for (var ii = 0; ii < rowCount; ii++)
            // {
            //     // Destroy(GameObject.FindWithTag("datapoint"));
            //     GameObject obj = GameObject.FindWithTag("datapoint");
            //     if (obj)
            //     {
            //         Destroy(obj);
            //     }
            // }
            // for (int index = 0; index < dPoints.Count; index++)
            // {
            //     // var gObj = ;
            //     Destroy(dPoints[index]);
            //     dPoints.Remove(dataPoint);
            //     // gObj++;
            //     // Destroy(dGempa[index]);
            // }
            // for (int indexG = 0; indexG < dGempa.Count; indexG++)
            // {
            //     // dPoints.Remove(index);
            //     // Destroy(dPoints[index]);
            //     Destroy(dGempa[indexG]);
            //     dGempa.Remove(gempaPoint);
            // }
            foreach (var dataPoint in dPoints)
            {
                Destroy(dataPoint);
            }
            dPoints.Clear();
            Debug.Log(myString2);

            foreach (var gempaPoint in dGempa)
            {
                Destroy(gempaPoint);
            }
            dGempa.Clear();

            // panggil fungsi bikin visual baru
            PlacePrefabPoints();
            if (myString2 != null)
            {
                PlacePrefabGempa();
            }
            // Debug.Log("masuk ganti csv-2");
        }
        //Activate Particle System
       //GetComponent<ParticleSystem>().SetParticles(particlePoints, particlePoints.Length);

    }

    // visualisasi data gempanya sendiri
    private void PlacePrefabGempa() 
    {
        position2 = center * gempaScale;
        // Debug.Log(position2);
        gempaPoint = Instantiate (GempaPrefab, Vector3.zero, Quaternion.identity);
        dGempa.Add(gempaPoint);

        // Debug.Log(center);// Converts index to string to name the point the index number
        string gempaPointName = "Titik Gempa";

        // Assigns name to the prefab
        gempaPoint.transform.name = gempaPointName;

        // Make child of PointHolder object, to keep particlePoints within container in hiearchy
        gempaPoint.transform.parent = GempaHolder.transform;

        // Position point at relative to parent
        gempaPoint.transform.localPosition = position2;

        gempaPoint.transform.localScale = new Vector3(pointGempaScale, pointGempaScale, pointGempaScale);

        // warna
        if (renderPrefabsWithColor == true)
            {
                // Sets color according to x/y/z value
                // Color color = GetGradientColor(x, y, z); // Call the method to get gradient color
                Color color = Color.red;
                gempaPoint.GetComponent<Renderer>().material.color = color;

                // Activate emission color keyword so we can modify emission color
                gempaPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                gempaPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            }
    }

    // Places the prefabs according to values read in
	private void PlacePrefabPoints()
	{
        bool found = false;
        rowCount = pointList.Count;
        // bool found2 = false;
        // Get count (number of rows in table)
        Debug.Log(rowCount);

                for (var i = 0; i < rowCount; i++)
        {
            myString = (pointList[i][aColumnName]).ToString();
            // myString2 = myString;
            // Debug.Log(myString);
            // Debug.Log(magicWords);
            
            //bikin if statement buat print without maid=datagempa
            // if (pointList[i][aColumnName].Equals(magicWords),StringComparison.InvariantCultureIgnoreCase)
            if (myString==magicWords)
            // if (i==1)
            {
                Debug.Log("masuk ke mystring2");
                
                myString2 = myString;
                // center = new Vector3(Convert.ToSingle(pointList[i][xColumnName]), Convert.ToSingle(pointList[i][yColumnName]), Convert.ToSingle(pointList[i][zColumnName]));
                // a = (Convert.ToSingle(pointList[i][xColumnName])- xMin) / (xMax - xMin);
                // b = (Convert.ToSingle(pointList[i][yColumnName])- yMin) / (yMax - yMin);
                // c = (Convert.ToSingle(pointList[i][zColumnName])- zMin) / (zMax - zMin);
                a = (Convert.ToSingle(pointList[i][xColumnName]));
                b = (Convert.ToSingle(pointList[i][yColumnName]));
                c = (Convert.ToSingle(pointList[i][zColumnName]));
                
                if (a < 0)
                    a = -a;
                if (b < 0)
                    b = -b;
                if (c < 0)
                    c = -c;

                // // Normalize the transformed values
                // a = (a - pMin) / (pMax - pMin);
                // b = (b - pMin) / (pMax - pMin);
                // c = (c - zMin) / (zMax - zMin);

                // Normalize the transformed values
                a = (a - xMin) / (xMax - xMin);
                b = (b - yMin) / (yMax - yMin);
                // c = (c - zMin) / (zMax - zMin);
                c = (c - z1Min) / (z1Max - z1Min);
                // a1 = (Convert.ToSingle(pointList[i][xColumnName]));
                // b1 = (Convert.ToSingle(pointList[i][yColumnName]));
                // c1 = (Convert.ToSingle(pointList[i][zColumnName]));
                center = new Vector3(a, b, c);
                found = true;
                // Debug.Log("ini a: "+a);
                // Debug.Log(center);
                // Debug.Log(myString2);
                // Debug.Log("ketemu nih"); 
                i++;
            }
            if((i == rowCount-1)&(found==false))
            {
                myString2 = null;
            }
            // Debug.Log(i);
            
            // kalau misalnya data gempa row terakhir
            if ((found == true)&((i==rowCount)))
            {
                // i = i-1; // error kalau magicwords nya row pertama krn kayanya infinity loop
                // Debug.Log(i);
                break;
            }
            // Debug.Log(myString);
            // Debug.Log(myString2);
            // Debug.Log(a);
            // Debug.Log(xMax);

            // Set x/y/z, standardized to between 0-1
            x = (Convert.ToSingle(pointList[i][xColumnName]));
            y = (Convert.ToSingle(pointList[i][yColumnName]));
            z = (Convert.ToSingle(pointList[i][zColumnName]));

            // Transform the values from negative to positive if necessary
            if (x < 0)
                x = -x;
            if (y < 0)
                y = -y;
            if (z < 0)
                z = -z;

            // // Normalize the transformed values
            // x = (x - pMin) / (pMax - pMin);
            // y = (y - pMin) / (pMax - pMin);
            // z = (z - zMin) / (zMax - zMin);

            // Debug.Log("(prefab) ini xMin: "+xMin+" dan ini xMax: "+xMax);
            // Debug.Log("(prefab) ini yMin: "+yMin+" dan ini yMax: "+yMax);
            // Debug.Log("(prefab) ini z1Min: "+z1Min+" dan ini z1Max: "+z1Max);

            // Normalize the transformed values
            x = (x - xMin) / (xMax - xMin);
            y = (y - yMin) / (yMax - yMin);
            // z = (z - zMin) / (zMax - zMin);
            z = (z - z1Min) / (z1Max - z1Min);

            // x = (Convert.ToSingle(pointList[i][xColumnName]) - xMin) / (xMax - xMin);
            // y = (Convert.ToSingle(pointList[i][yColumnName]) - yMin) / (yMax - yMin);
            // z = (Convert.ToSingle(pointList[i][zColumnName]) - zMin) / (zMax - zMin);
            // x1 = (Convert.ToSingle(pointList[i][xColumnName]));
            // y1 = (Convert.ToSingle(pointList[i][yColumnName]));
            // z1 = (Convert.ToSingle(pointList[i][zColumnName]));
                
            // Create vector 3 for positioning particlePoints
            position = new Vector3 (x, y, z) * plotScale;
            // Debug.Log(position);

            //instantiate as gameobject variable so that it can be manipulated within loop
            dataPoint = Instantiate (PointPrefab, Vector3.zero, Quaternion.identity);
            dPoints.Add(dataPoint);
            // dataPoint = Instantiate (PointPrefab, position, Quaternion.identity);


            // if((i==(rowCount-1))&(((pointList[i][aColumnName]).ToString())!=magicWords))
            // {
            //     dataPoint = Instantiate (PointPrefab, Vector3.zero, Quaternion.identity);
            // }

            
            // Make child of PointHolder object, to keep particlePoints within container in hiearchy
            dataPoint.transform.parent = PointHolder.transform;

            // Position point at relative to parent
            dataPoint.transform.localPosition = position;

            dataPoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);

            // Converts index to string to name the point the index number
            string dataPointName = i.ToString();

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;
            // Debug.Log("ini bola ke: "+dataPointName+" dan ini posisi y nya: "+y);

            // if (renderPrefabsWithColor == true)
            // {

                // Color color = Color.white;
                // // Sets color according to x/y/z value
                // dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, z, 10.0f);

                // // Activate emission color keyword so we can modify emission color
                // dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                // dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

                // dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(x, y, z, 1.0f));
                
                // dataPoint.GetComponent<Renderer>().material.color = Color.white;

                // // Activate emission color keyword so we can modify emission color
                // dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                // dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
            // }
            if (renderPrefabsWithColor == true)
            {
                // Warnain gradasi
                // float radius = 8.858f;
                // float radius = 7.079f;
                float radius = 7.171842176271939f;
                // float radius = 2.578f;
                // float distance = 10;
                // for ( int j = 0; j < pointList.Count; j++ )
                // {
                //      // Get the position of the data point
                //     panjang = dataPoint.transform.position;
                // }
                // Debug.Log(position);
                // Debug.Log(panjang);
                
                // Debug.Log("ini a: "+a);
                float a1 = a * 10;
                // float a1 = 7.210207f;
                // Debug.Log("ini a1: "+a1);
                float b1 = b * 10;
                float x1 = x * 10;
                float y1 = y * 10;
                float c1 = c * 10;
                // float c1 = 7.694039f;
                // Debug.Log("ini c1: "+c1);
                float z1 = z * 10;
                    // Debug.Log(z);
                    // Debug.Log(position);
                    // Debug.Log(center);
                // Debug.Log(panjang);
                // Debug.Log(panjang);
                center2 = new Vector3(a1, y, c1);
                // Debug.Log(center2);
                // center3 = dataPoint.transform.position;
                    // Debug.Log(center3);

                    // Calculate the distance from the red center (point c)
                // float distance = Vector3.Distance(position, center2);
                Debug.Log("ini bola yang ke-"+i+"(color) ini x1: "+x1);
                Debug.Log("(color) ini a1: "+a1);
                Debug.Log("ini bola yang ke-"+i+"(color) ini z1: "+z1);
                Debug.Log("(color) ini c1: "+c1);
                float distance = Mathf.Sqrt( Mathf.Pow((x1-a1),2) + Mathf.Pow((z1-c1),2) ); 
                Debug.Log("ini bola yang ke-"+i+" ini distancenya: "+distance);
                // Debug.Log("ini bola ke-"+i+ "nilai distancenya: "+distance);

                // if (distance > maxDistance){
                //     maxDistance = distance;
                // }
                // float radius = maxDistance;
                // Debug.Log(radius);
                     // Determine the color based on the distance from the red center
                float t = Mathf.Clamp01(distance / radius);
                Debug.Log("ini t yang ke-"+i+ "nilai beforenya: "+t);
                // if (t>0.35)
                // {
                //     // nilai t dari 3.5 jump ke >0.55
                //     t = t + 0.2f;
                // }

                if(((Math.Abs(x1-a1))>1.4978129999999998)|((Math.Abs(z1-c1))>2.476534))
                {
                    // nilai t dari 3.5 jump ke >0.55
                    t = t + 0.2f;
                }

                // bikin pembulatan t disini
                // 0 <= t < 0.1 --> 0
                if( (t>=0) & (t<0.1f) )
                {
                    t = 0;
                }

                // 0.1 <= t < 0.2 --> 0.1
                else if( (t>=0.1f) & (t<0.2f) )
                {
                    t = 0.1f;
                }

                // 0.2f <= t < 0.3 --> 0.2f
                else if( (t>=0.2f) & (t<0.3f) )
                {
                    t = 0.2f;
                }

                // 0.3f <= t <= 0.35 --> 0.3f
                else if( (t>=0.3f) & (t<=0.35f) )
                {
                    t = 0.3f;
                }

                // batas zona merah

                // 0.55 <= t < 0.6 --> 0.55
                else if( (t>=0.55f) & (t<0.6f) )
                {
                    t = 0.55f;
                }

                // 0.6f <= t < 0.7 --> 0.6f
                else if( (t>=0.6f) & (t<0.7f) )
                {
                    t = 0.6f;
                }

                // 0.7f <= t < 0.8 --> 0.7f
                else if( (t>=0.7f) & (t<0.8f) )
                {
                    t = 0.7f;
                }

                // 0.8f <= t < 0.9 --> 0.8f
                else if( (t>=0.8f) & (t<0.9f) )
                {
                    t = 0.8f;
                }

                // 0.9f <= t < 1 --> 9f
                else if( (t>=0.9f) & (t<1) )
                {
                    t = 0.9f;
                }

                // t = 1 --> 1
                else if( (t>=1) )
                {
                    t = 1;
                }
                Debug.Log("ini t yang ke-"+i+ " nilai afternya: "+t);

                // Debug.Log(t + " ini bola ke: "+i);
                int index = button.getIndex();
                // if ((index>=13)&(index<=19))
                if (found==true) 
                {
                    Debug.Log("berhasil masuk");
                    color = Color.Lerp(Color.red, Color.green, t);
                    // tinggiY = getPointY();
                    // Debug.Log("ini bola ke: "+dataPointName+" dan ini posisi nya: "+tinggiY);
                    // Debug.Log(distance);
                    // Debug.Log(t + " ini bola ke: "+dataPointName);
                    // trigger.OnTriggerStay(dataPoint.GetComponent<Collider>());
                    // posisi = logic.getLokasi();
                    // Debug.Log("ini bola ke: "+dataPointName+" dengan posisi di: "+posisi);
                    
                }
                else if(found==false)
                {
                    color = Color.green;
                }
                    
                    // Sets color according to x/y/z value
                    // Color color = GetGradientColor(x, y, z); // Call the method to get gradient color
                    // Color color = Color.white;
                dataPoint.GetComponent<Renderer>().material.color = color;

                    // Activate emission color keyword so we can modify emission color
                dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

                
            }

            // Method to get gradient color based on x, y, and z values
            // Color GetGradientColor(float x, float y, float z)
            // {
            //     Gradient gradient = new Gradient();
            //     GradientColorKey[] colorKeys = new GradientColorKey[2];
            //     GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

            //     // Define color keys
            //     colorKeys[0].color = Color.green; // go to green
            //     colorKeys[0].time = 0.0f;
            //     colorKeys[1].color = Color.red; // finally end with red
            //     colorKeys[1].time = 1.0f;

            //     // Define alpha keys
            //     alphaKeys[0].alpha = 1.0f;
            //     alphaKeys[0].time = 0.0f;
            //     alphaKeys[1].alpha = 1.0f;
            //     alphaKeys[1].time = 1.0f;

            //     // Set color and alpha keys to the gradient
            //     gradient.SetKeys(colorKeys, alphaKeys);

            //     // Evaluate the gradient at a given position (x, y, z)
            //     // Debug.Log(gradient.Evaluate((x + y + z) / 1.0f));
            //     return gradient.Evaluate((x + y + z) / 1.0f); // You can change this to any other calculation
            // }

                                  						
		}
	}

    // creates particlePoints in the Particle System game object
    // 
    // 
    private void CreateParticles()
    {
        //pointList = CSVReader.Read(inputfile);

        rowCount = pointList.Count;
       // Debug.Log("Row Count is " + rowCount);

        particlePoints = new ParticleSystem.Particle[rowCount];

        for (int i = 0; i < pointList.Count; i++)
        {
            // Convert object from list into float
            float x = (Convert.ToSingle(pointList[i][xColumnName]) - xMin) / (xMax - xMin);
            float y = (Convert.ToSingle(pointList[i][yColumnName]) - yMin) / (yMax - yMin);
            float z = (Convert.ToSingle(pointList[i][zColumnName]) - zMin) / (zMax - zMin);

            // Debug.Log("Position is " + x + y + z);

            // Set point location
			particlePoints[i].position = new Vector3(x, y, z) * plotScale;
          
            // GlowColor = 
            // Set point color
            // particlePoints[i].startColor = new Color(x, y, z, 1.0f);
            particlePoints[i].startSize = particleScale; 
        }
                
    }

    // Finds labels named in scene, assigns values to their text meshes
    // WARNING: game objects need to be named within scene
    private void AssignLabels()
    {
        // Update point counter
        GameObject.Find("Point_Count").GetComponent<TextMesh>().text = pointList.Count.ToString("0");

        // Update title according to inputfile name
        GameObject.Find("Dataset_Label").GetComponent<TextMesh>().text = inputfile;

        // Update axis titles to ColumnNames
        GameObject.Find("X_Title").GetComponent<TextMesh>().text = xColumnName;
        GameObject.Find("Y_Title").GetComponent<TextMesh>().text = yColumnName;
        GameObject.Find("Z_Title").GetComponent<TextMesh>().text = zColumnName;

        // Set x Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        GameObject.Find("X_Min_Lab").GetComponent<TextMesh>().text = xMin.ToString("0.0");
        GameObject.Find("X_Mid_Lab").GetComponent<TextMesh>().text = (xMin + (xMax - xMin) / 2f).ToString("0.0");
        GameObject.Find("X_Max_Lab").GetComponent<TextMesh>().text = xMax.ToString("0.0");

        // Set y Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        GameObject.Find("Y_Min_Lab").GetComponent<TextMesh>().text = yMin.ToString("0.0");
        GameObject.Find("Y_Mid_Lab").GetComponent<TextMesh>().text = (yMin + (yMax - yMin) / 2f).ToString("0.0");
        GameObject.Find("Y_Max_Lab").GetComponent<TextMesh>().text = yMax.ToString("0.0");

        // Set z Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        // GameObject.Find("Z_Min_Lab").GetComponent<TextMesh>().text = zMin.ToString("0.0");
        // GameObject.Find("Z_Mid_Lab").GetComponent<TextMesh>().text = (zMin + (zMax - zMin) / 2f).ToString("0.0");
        // GameObject.Find("Z_Max_Lab").GetComponent<TextMesh>().text = zMax.ToString("0.0");
        GameObject.Find("Z_Min_Lab").GetComponent<TextMesh>().text = ("-3.801801");
        GameObject.Find("Z_Mid_Lab").GetComponent<TextMesh>().text = ("-3.294.852");
        GameObject.Find("Z_Max_Lab").GetComponent<TextMesh>().text = ("-2.787903");
        // z1Max = 3.801801f;
        // z1Min = 2.787903f;
                
    }

    //Method for finding max value, assumes PointList is generated
    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    //Method for finding minimum value, assumes PointList is generated
    private float FindMinValue(string columnName)
    {
        //set initial value to first value
        float minValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }

        return minValue;
    }

    private float FindMinBgt()
    {
        //set initial value to first value
        float minBgt = xMin;

        // if (yMin < minBgt)
        // {
        //     minBgt = yMin;
        // }
        if (zMin < minBgt)
        {
            minBgt = zMin;
        }

        return minBgt;
    }

    private float FindMaxBgt()
    {
        //set initial value to first value
        float maxBgt = xMax;

        // if (yMax > maxBgt)
        // {
        //     maxBgt = yMax;
        // }
        if (zMax > maxBgt)
        {
            maxBgt = zMax;
        }

        return maxBgt;
    }

    public float getPointY()
    {
        return dataPoint.transform.position[1];
    }

    public List<Dictionary<string, object>> getPointList()
    {
        return pointList;
    }

    public string inputFile()
    {
        return inputfile;
    }

}


