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
    [Range(0.0f, 5.5f)]
    public float pointGempaScale = 0.25f;


    // Changes size of particles generated
    [Range(0.0f, 2.0f)]
    public float particleScale = 2.0f;

    // The prefab for the data particlePoints that will be instantiated
    public GameObject PointPrefab;
    public GameObject GempaPrefab;
    public GameObject HeatPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;
    public GameObject HeatHolder;
    public GameObject GempaHolder;
    public GameObject dataPoint;
    public GameObject heatPoint;
    public GameObject gempaPoint;
    Vector3 position;
    Vector3 center;
    Vector3 center2;
    Vector3 position2;
    Vector3 position3;
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
    public List<Dictionary<string, object>> pointList2;
    public List<GameObject> dPoints = new List<GameObject>();
    public List<GameObject> hPoints = new List<GameObject>();
    public List<GameObject> dGempa = new List<GameObject>();
    public List<int> countTotal = new List<int>();

    // Particle system for holding point particles
    private ParticleSystem.Particle[] particlePoints; 

    // manggil current location
    public logicManager logic;
    public string posisi;
    public testTrigger trigger;
    public float tinggiY;

    public buttonReader button;
    int count0;
    int count1;
    int count2;
    int count3;
    int count4;
    int count5;
    int count6;
    int count7;
    int count8;
    int count9;

    string dateColumnName;
    string date;

    string depth;
    string mag;
    string hour;
    string xColumn;
    string zColumn;

    string depthColumnName;
    string magColumnName;
    string hourColumnName;

    public PointLegends plegend;
    public LabelOrienter_Smooth labelO;
    public List<int> legendList = new List<int>();
    // public List<string> columnList;


    //********Methods********
    public void UpdateVisualization(string file)
    {
        pointList = CSVReader.Read(file);
    }

    public string getDepth()
    {
        depthColumnName = "Depth";
        depth = ((pointList[0][depthColumnName])).ToString();
        return depth;
    }

    public string getMag()
    {
        depthColumnName = "Magnitude";
        mag = ((pointList[0][depthColumnName])).ToString();
        return mag;
    }

    public string getHour()
    {
        hourColumnName = "Hour";
        hour = ((pointList[0][hourColumnName])).ToString();
        return hour;
    }

    public string getXColumn()
    {
        xColumn = ((pointList[0][xColumnName])).ToString();
        return xColumn;
    }

    public string getZColumn()
    {
        zColumn = ((pointList[0][zColumnName])).ToString();
        return zColumn;
    }

    void Awake()
    {
        //Run CSV Reader
        UpdateVisualization(inputfile);
        List<string> columnList = new List<string>(pointList[1].Keys);

        Debug.Log("There are " + columnList.Count + " columns in the CSV");
        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column names according to index indicated in columnList
        xColumnName = columnList[column1];
        yColumnName = columnList[column2];
        zColumnName = columnList[column3];
        aColumnName = columnList[column4];

        Debug.Log(aColumnName);
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

        AssignLabels();

        if (renderPointPrefabs == true)
        {
            PlacePrefabPoints();
            if (myString2 != null)
            {
                PlacePrefabGempa();
            }
            addBalls();
            //countBalls();

        }            

        // If statement to turn particles on and off
        if ( renderParticles == true)
        {
            // Call CreateParticles() for particle system
            CreateParticles();

            // Set particle system, for point glow- depends on CreateParticles()
            GetComponent<ParticleSystem>().SetParticles(particlePoints, particlePoints.Length);
        }
        
        Heatmap();
        
        
    }

    // Use this for initialization
    void Start () 
	{
        plegend.PlaceLabels2(countTotal);
                                
    }


    void addBalls()
    {
        countTotal.Add(count0);
        countTotal.Add(count1);
        countTotal.Add(count2);
        countTotal.Add(count3);
        countTotal.Add(count4);
        countTotal.Add(count5);
        countTotal.Add(count6);
        countTotal.Add(count7);
        countTotal.Add(count8);
        countTotal.Add(count9);
    }

    public List<int> getCount()
    {
        return countTotal;

    }

    void countBalls()
    {
        int totalC = count0 + count1 + count2 + count3 + count4 + count5 + count6 + count7 + count8 + count9; 
        // buat ngecek value masing2 count
        List<int> CEK = getCount();
        for (int ix = 0; ix < CEK.Count; ix++)
        {
            Debug.Log("Count"+ix+": "+CEK[ix]);
        }
    }

    void Reset()
    {
        count0 = 0;
        count1 = 0;
        count2 = 0;
        count3 = 0;
        count4 = 0;
        count5 = 0;
        count6 = 0;
        count7 = 0;
        count8 = 0;
        count9 = 0;
    }
    
    void Heatmap()
    {
        //button.ReadPreviousCSVFile();
        button.getfilename();
        int indexH = button.index;
        // int indexH = 100;
        string basefilenameH = button.baseFileName;
        Debug.Log("basefilename: "+basefilenameH+" index: "+indexH);
        string fileNameH = basefilenameH + indexH.ToString();
        pointList2 = CSVReader.Read(fileNameH);

        int rowCount2 = pointList2.Count;
        // Debug.Log(rowCount);
                for (var i = 0; i < rowCount2; i++)
        {
            // Set x/y/z, standardized to between 0-1
            x = (Convert.ToSingle(pointList2[i][xColumnName]));
            y = (Convert.ToSingle(pointList2[i][yColumnName]));
            z = (Convert.ToSingle(pointList2[i][zColumnName]));

            // Transform the values from negative to positive if necessary
            if (x < 0)
                x = -x;
            if (y < 0)
                y = -y;
            if (z < 0)
                z = -z;

            // Normalize the transformed values
            x = (x - xMin) / (xMax - xMin);
            y = (y - yMin) / (yMax - yMin);
            // z = (z - zMin) / (zMax - zMin);
            z = (z - z1Min) / (z1Max - z1Min);

            // Create vector 3 for positioning particlePoints
            position3 = new Vector3 (x, y, z) * plotScale;

            //instantiate as gameobject variable so that it can be manipulated within loop
            heatPoint = Instantiate (HeatPrefab, Vector3.zero, Quaternion.identity);
            hPoints.Add(heatPoint);

            // Make child of PointHolder object, to keep particlePoints within container in hiearchy
            heatPoint.transform.parent = HeatHolder.transform;

            // Position point at relative to parent
            heatPoint.transform.localPosition = position3;

            heatPoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);

            // Converts index to string to name the point the index number
            string heatPointName = i.ToString();

            // Assigns name to the prefab
            heatPoint.transform.name = heatPointName;

        }
        // hPoints = new List<GameObject>(dPoints);
        // Debug.Log("hPoints[1]: "+hPoints[1]);



    }
		
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            button.ReadNextCSVFile();
        }
        
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            button.ReadPreviousCSVFile();
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            labelO.uiLables();
        }

        if (inputfile != button.updateCSV())
        {
            
            // int a = button.getIndex();
            // Debug.Log("index sekarang: "+a);
            foreach (var heatPoint in hPoints)
            {
                Destroy(heatPoint);
            }
            hPoints.Clear();
            Heatmap();
            Reset();
            inputfile = button.updateCSV();
            UpdateVisualization(inputfile);
            GameObject.Find("Dataset_Label").GetComponent<TextMesh>().text = inputfile;
            GameObject.Find("Point_Count").GetComponent<TextMesh>().text = pointList.Count.ToString("0");
            
            foreach (var dataPoint in dPoints)
            {
                Destroy(dataPoint);
            }
            dPoints.Clear();

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
            plegend.RemoveLabels2();
            addBalls();
            //countBalls();
            plegend.PlaceLabels2(countTotal);
        }

    }

    // visualisasi data gempanya sendiri
    private void PlacePrefabGempa() 
    {
        position2 = center * gempaScale;
        gempaPoint = Instantiate (GempaPrefab, Vector3.zero, Quaternion.identity);
        dGempa.Add(gempaPoint);

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
        Debug.Log(rowCount);
        
                for (var i = 0; i < rowCount; i++)
        {
            myString = (pointList[i][aColumnName]).ToString();
            
            //bikin if statement buat print without maid=datagempa
            if (myString==magicWords)
            {
                
                myString2 = myString;
                a = (Convert.ToSingle(pointList[i][xColumnName]));
                b = (Convert.ToSingle(pointList[i][yColumnName]));
                c = (Convert.ToSingle(pointList[i][zColumnName]));
                
                if (a < 0)
                    a = -a;
                if (b < 0)
                    b = -b;
                if (c < 0)
                    c = -c;

                // Normalize the transformed values
                a = (a - xMin) / (xMax - xMin);
                b = (b - yMin) / (yMax - yMin);
                // c = (c - zMin) / (zMax - zMin);
                c = (c - z1Min) / (z1Max - z1Min);
                center = new Vector3(a, b, c);
                found = true;
                i++;
                count0++;
            }
            if((i == rowCount-1)&(found==false))
            {
                myString2 = null;
            }
            
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

            // Normalize the transformed values
            x = (x - xMin) / (xMax - xMin);
            y = (y - yMin) / (yMax - yMin);
            // z = (z - zMin) / (zMax - zMin);
            z = (z - z1Min) / (z1Max - z1Min);
                
            // Create vector 3 for positioning particlePoints
            position = new Vector3 (x, y, z) * plotScale;

            //instantiate as gameobject variable so that it can be manipulated within loop
            dataPoint = Instantiate (PointPrefab, Vector3.zero, Quaternion.identity);
            dPoints.Add(dataPoint);

            // Make child of PointHolder object, to keep particlePoints within container in hiearchy
            dataPoint.transform.parent = PointHolder.transform;

            // Position point at relative to parent
            dataPoint.transform.localPosition = position;

            dataPoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);

            // Converts index to string to name the point the index number
            string dataPointName = i.ToString();

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;

            Debug.Log("Point: "+ dataPointName+ ", x: "+x+ ", x: "+y+ ", x: "+z);

            if (renderPrefabsWithColor == true)
            {
                // Warnain gradasi
                // float radius = 8.858f;
                float radius = 7.171842176271939f;
                float a1 = a * 10;
                float b1 = b * 10;
                float x1 = x * 10;
                float y1 = y * 10;
                float c1 = c * 10;
                float z1 = z * 10;
                center2 = new Vector3(a1, y, c1);

                // Calculate the distance from the red center (point c)
                float distance = Mathf.Sqrt( Mathf.Pow((x1-a1),2) + Mathf.Pow((z1-c1),2) ); 
                float t = Mathf.Clamp01(distance / radius);

                if((((Math.Abs(x1-a1))>1.4978129999999998f)||((Math.Abs(z1-c1))>2.4765354f)))
                {
                    float testing = (Math.Abs(z1-c1));
                    // nilai t dari 3.5 jump ke >0.55
                    t = t + 0.2f;
                }

                // bikin pembulatan t disini
                // 0 <= t < 0.1 --> 0
                if( (t>=0) & (t<0.1f) )
                {
                    t = 0;
                    count0++;
                }

                // 0.1 <= t < 0.2 --> 0.1 - 1
                else if( (t>=0.1f) & (t<0.2f) )
                {
                    t = 0.1f;
                    count1++;
                }

                // 0.2f <= t < 0.3 --> 0.2f - 2
                else if( (t>=0.2f) & (t<0.3f) )
                {
                    t = 0.2f;
                    count2++;
                }

                // 0.3f <= t <= 0.35 --> 0.3f - 3
                else if( (t>=0.3f) & (t<=0.35f) )
                {
                    t = 0.3f;
                    count3++;
                }

                // batas zona merah

                // 0.55 <= t < 0.6 --> 0.55 -4
                else if( (t>=0.35f) & (t<0.6f) )
                {
                    t = 0.55f;
                    count4++;
                }

                // 0.6f <= t < 0.7 --> 0.6f -5
                else if( (t>=0.6f) & (t<0.7f) )
                {
                    t = 0.6f;
                    count5++;
                }

                // 0.7f <= t < 0.8 --> 0.7f -6
                else if( (t>=0.7f) & (t<0.8f) )
                {
                    t = 0.7f;
                    count6++;
                }

                // 0.8f <= t < 0.9 --> 0.8f
                else if( (t>=0.8f) & (t<0.9f) )
                {
                    t = 0.8f;
                    count7++;
                }

                // 0.9f <= t < 1 --> 9f
                else if( (t>=0.9f) & (t<1) )
                {
                    t = 0.9f;
                    count8++;
                }

                // t = 1 --> 1
                else if( (t>=1) )
                {
                    t = 1;
                    count9++;
                }
                
                int index = button.getIndex();
                string beforeUnderscoreStr = button.getbeforeUnderscoreStr();

                if(index == 99){
                    if (found==true) 
                    {
                        color = Color.Lerp(Color.red, Color.green, t);
                        
                    }
                    else
                    {
                        color = Color.green;
                    }
                }
                else if(index != 99)
                {
                    if(beforeUnderscoreStr=="3")
                    {
                        if ((index>=13)&(index<=19))
                        {
                            color = Color.Lerp(Color.red, Color.green, t);
                        }
                    
                        else
                        {
                            color = Color.green;
                        }

                    }
                    
                    else if(beforeUnderscoreStr=="4")
                    {
                        if ((index>=27)&(index<=39))
                        {
                            color = Color.Lerp(Color.red, Color.green, t);
                        }
                    
                        else
                        {
                            color = Color.green;
                        }

                    }
                }
                    
                dataPoint.GetComponent<Renderer>().material.color = color;

                // Activate emission color keyword so we can modify emission color
                dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

                
            }
           						
		}
        

	}

    public string getDate()
    {
        dateColumnName = "Date";
        date = ((pointList[1][dateColumnName])).ToString();
        return date;
    }

    // creates particlePoints in the Particle System game object
    private void CreateParticles()
    {

        rowCount = pointList.Count;

        particlePoints = new ParticleSystem.Particle[rowCount];

        for (int i = 0; i < pointList.Count; i++)
        {
            // Convert object from list into float
            float x = (Convert.ToSingle(pointList[i][xColumnName]) - xMin) / (xMax - xMin);
            float y = (Convert.ToSingle(pointList[i][yColumnName]) - yMin) / (yMax - yMin);
            float z = (Convert.ToSingle(pointList[i][zColumnName]) - zMin) / (zMax - zMin);


            // Set point location
			particlePoints[i].position = new Vector3(x, y, z) * plotScale;
          
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
        GameObject.Find("Z_Min_Lab").GetComponent<TextMesh>().text = ("-3.801801");
        GameObject.Find("Z_Mid_Lab").GetComponent<TextMesh>().text = ("-3.294.852");
        GameObject.Find("Z_Max_Lab").GetComponent<TextMesh>().text = ("-2.787903");
                
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

    public string getmyString2()
    {
        return myString2;
    }

}


