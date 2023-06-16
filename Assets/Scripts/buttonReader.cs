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
    int counter;

    private List<Dictionary<string, object>> dataList;
    private string fileName;

    public PointRenderer point;

    int index = 99;
    string baseFileName;
    int underscoreIndex;
    int beforeUnderscoreIndex;
    string beforeUnderscoreStr;
    string periode;
    string dateT;
    string status;
    string countdown;
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
    //int hour = mydate.Hour;
    //int minute = mydate.Minute;
    //int second = mydate.Second;

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


    public void ButtonPressed()
    {
        counter++;
        bText.text = counter + "";
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(beforeUnderscoreStr=="3"){
            switch (index)
            {
                case 0:
                    periode = "00:00:00 - 01:00:00";
                    break;
                case 1:
                    periode = "01:00:00 - 02:00:00";
                    break;
                case 2:
                    periode = "02:00:00 - 03:00:00";
                    break;
                case 3:
                    periode = "03:00:00 - 04:00:00";
                    break;
                case 4:
                    periode = "04:00:00 - 05:00:00";
                    break;
                case 5:
                    periode = "05:00:00 - 06:00:00";
                    break;
                case 6:
                    periode = "06:00:00 - 07:00:00";
                    break;
                case 7:
                    periode = "07:00:00 - 08:00:00";
                    break;
                case 8:
                    periode = "08:00:00 - 09:00:00";
                    break;
                case 9:
                    periode = "09:00:00 - 10:00:00";
                    break;
                case 10:
                    periode = "10:00:00 - 11:00:00";
                    break;
                case 11:
                    periode = "11:00:00 - 12:00:00";
                    break;
                case 12:
                    periode = "12:00:00 - 13:00:00";
                    break;
                case 13:
                    periode = "13:00:00 - 14:00:00";
                    break;
                case 14:
                    periode = "14:00:00 - 15:00:00";
                    break;
                case 15:
                    periode = "15:00:00 - 16:00:00";
                    break;
                case 16:
                    periode = "16:00:00 - 17:00:00";
                    break;
                case 17:
                    periode = "17:00:00 - 18:00:00";
                    break;
                case 18:
                    periode = "18:00:00 - 19:00:00";
                    break;
                case 19:
                    periode = "19:00:00 - 20:00:00";
                    break;
                default:
                    periode = "00:00:00 - 00:00:00";
                    break;
            }
        }
        else if(beforeUnderscoreStr=="4"){
            switch (index)
            {
                case 0:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 1:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 2:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 3:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 4:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 5:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 6:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 7:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 8:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 9:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 10:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 11:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 12:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 13:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 14:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 15:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 16:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 17:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 18:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 19:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 20:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 21:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 22:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 23:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 24:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 25:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 26:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 27:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 28:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 29:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 30:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 31:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 32:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 33:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 34:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 35:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 36:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 37:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 38:
                    evenTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                case 39:
                    oddTime(index);
                    cekStatus();
                    cekCountdown(index);
                    break;
                default:
                    periode = "00:00:00 - 00:00:00";
                    break;
            }
        }
        bText.text = periode;
        statusText.text = status;
        countdownText.text = countdown;
        if(found == false )
        {
            dateT = point.getDate();
            dateText.text = dateT;
            found = true;
        }
        datasetText.text = fileName;
        countText.text = point.pointList.Count.ToString("0");

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
            status = "Earthquake is occuring";
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
