using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class buttonReader : MonoBehaviour
{

    public Text bText;
    public Text dateText;
    public Text datasetText;
    public Text countText;
    public Text statusText;
    public Text countdownText;
    public Text testText;
    int counter;

    private List<Dictionary<string, object>> dataList;
    private string fileName;

    int index = 99;
    string baseFileName;
    int underscoreIndex;
    int beforeUnderscoreIndex;
    string beforeUnderscoreStr;
    string periode;
    string dateT;
    string status;
    string countdown;
    string depth;
    string mag;
    bool found = false;
    DateTime mydate = new DateTime(2021, 6, 16, 0, 0, 0);
    TimeSpan timeSpan;
    TimeSpan timeSpan2;
    DateTime addResult;
    DateTime addResult2;
    DateTime gempaResult;
    TimeSpan countdownResult;

    bool foundGempa;

    int normal;

    public PointLegends pLegend;
    public PointRenderer point;

    List<int> listCount = new List<int>();

    void Awake()
    {
        fileName = point.inputFile();        

        // Split the fileName into index and baseFileName
        underscoreIndex = fileName.LastIndexOf('_');
        if (underscoreIndex >= 0)
        {
            beforeUnderscoreIndex = underscoreIndex - 1;
            beforeUnderscoreStr = fileName.Substring(beforeUnderscoreIndex, 1);
            // Get the substring starting from the character after the last underscore
            string indexStr = fileName.Substring(underscoreIndex + 1);

            // Parse the index string to an integer
            if (int.TryParse(indexStr, out index))
            {
                // Get the baseFileName by removing the index string from the fileName
                baseFileName = fileName.Substring(0, underscoreIndex + 1);
            }
            else
            {
                Debug.Log("Failed to parse the index from the fileName.");
                return;
            }
        }
        else
        {
            Debug.Log("Invalid fileName format. Unable to split into index and baseFileName.");
            return;
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        placeLegendLabel();
    }


    void placeLegendLabel()
    {
        //listCount = point.getCount();
        //testText.text =  pLegend.distanceGempa(0) + " km: " + listCount[0] + "\n" + pLegend.distanceGempa(1) + " km: " + listCount[1];
        listCount = point.getCount();

        string labelText = "";

        for (int i = 0; i < listCount.Count; i++)
        {
            labelText += pLegend.distanceGempa(i) + " km: " + listCount[i] + "\n";
        }

        testText.text = labelText;
    }


    public int getIndex()
    {
        return index;
    }
    
    public string getbeforeUnderscoreStr()
    {
        return beforeUnderscoreStr;
    }

    public void ReadNextCSVFile()
    {
        // index++; // Increment the file index for the next CSV file
        if(beforeUnderscoreStr=="3"){
            index++; // Increment the file index for the next CSV file
            if(index==20)
            {
                index=0;
            }
            fileName = baseFileName + index.ToString();
            dataList = CSVReader.Read(fileName);
        }
        else if(beforeUnderscoreStr=="4"){
            index++; // Increment the file index for the next CSV file
            if(index==40)
            {
                index=0;
            }
            fileName = baseFileName + index.ToString();
            dataList = CSVReader.Read(fileName);
        }
        // Do whatever you want with the dataList, such as processing the data or displaying it
        
    }

    public void ReadPreviousCSVFile()
    {
        
        if(beforeUnderscoreStr=="3"){
            if (index == 0)
            {
                index = 20;
            }
            index--; // Decrement the file index to move to the previous CSV file

            fileName = baseFileName + index.ToString();

            dataList = CSVReader.Read(fileName);
        }
        else if(beforeUnderscoreStr=="4"){
            if (index == 0)
            {
                index = 40;
            }
            index--; // Decrement the file index to move to the previous CSV file

            fileName = baseFileName + index.ToString();

            dataList = CSVReader.Read(fileName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= 0 && index <= 39)
        {
            if (index % 2 == 0)
            {
                evenTime(index);
            }
            else
            {
                oddTime(index);
            }
            
            cekStatus();
            cekCountdown(index);
        }
        else
        {
            periode = "00:00:00 - 00:00:00";
        }
        bText.text = periode;
        statusText.text = status;
        countdownText.text = countdown;
        placeLegendLabel();
        if (found == false )
        {
            setTanggal();
        }
        datasetText.text = fileName;
        countText.text = point.pointList.Count.ToString("0");

}

    public void setTanggal()
    {
        dateT = point.getDate();
        dateText.text = dateT;
        found = true;
    }

    string cekCountdown(int index2)
    {
        DateTime baseDateTime = new DateTime(2021, 6, 16, 0, 0, 0);
        string minusplus;
        
        if(index2<=27)
        {
            countdownResult = gempaResult - addResult;
            minusplus = "-";
        }
        else
        {
            countdownResult = addResult - gempaResult;
            minusplus = "+";
        }
        if(foundGempa==true)
        {
            minusplus = "";
        }

        DateTime newDateTime = baseDateTime.Add(countdownResult);

        countdown = minusplus + newDateTime.ToString(" HH ") + "hours" + newDateTime.ToString(" mm ") + "minutes";
        return countdown;
    }

    string oddTime(int index2)
    {
        normal = index2/2;
        timeSpan = new TimeSpan(0,normal,30,0);
        addResult = mydate + timeSpan;

        timeSpan2 = new TimeSpan(0,0,30,0);
        addResult2 = addResult + timeSpan2;

        periode = addResult.ToString("HH:mm:ss - ")+addResult2.ToString("HH:mm:ss");
        return periode;

    }

    string evenTime(int index2)
    {
        normal = index2/2;
        timeSpan = new TimeSpan(0,normal,0,0);
        addResult = mydate + timeSpan;

        timeSpan2 = new TimeSpan(0,0,30,0);
        addResult2 = addResult + timeSpan2;

        periode = addResult.ToString("HH:mm:ss - ")+addResult2.ToString("HH:mm:ss");
        return periode;
        
    }

    string cekStatus()
    {
        string cekGempa = point.getmyString2();
        if(cekGempa != null)
        {
            depth = point.getDepth();
            mag = point.getMag();
            status = "Earthquake is occuring"+", depth "+depth+" km,"+"\n"+"magnitude "+mag;
            gempaResult = addResult;
            foundGempa = true;
        }
        else
        {
            status = "Earthquake is not occuring";
            foundGempa = false;
        }
        return status;
    }

    public string updateCSV()
    {
        return fileName;
    }
}
