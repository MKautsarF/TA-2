using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class buttonReader : MonoBehaviour
{

    public Text bText;
    int counter;

    // private int fileIndex = 0;
    // private string baseFileName = "datav2_";
    private List<Dictionary<string, object>> dataList;
    private string fileName;

    public PointRenderer point;

    int index = 99;
    string baseFileName;
    int underscoreIndex;
    int beforeUnderscoreIndex;
    string beforeUnderscoreStr;
    string periode;

    void Awake()
    {
        fileName = point.inputFile();

        // Split the fileName into index and baseFileName
        underscoreIndex = fileName.LastIndexOf('_');
        // Debug.Log(beforeUnderscoreIndex);
        // Debug.Log(beforeUnderscoreStr);
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

        // Now you can use the index and baseFileName variables as needed
        // Debug.Log("index: " + index);
        // Debug.Log("baseFileName: " + baseFileName);
        // bText.text = fileName;

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
        
        // bText.text = fileName;
    }

    public void ReadPreviousCSVFile()
    {
        // if (index > 0)
        // {
        //     index--; // Decrement the file index to move to the previous CSV file

        //     fileName = baseFileName + index.ToString();

        //     dataList = CSVReader.Read(fileName);

        //     // Do whatever you want with the dataList, such as processing the data or displaying it
        // }
        // else
        // {
        //     // Debug.Log("Cannot go back further. Already at the first CSV file.");
        // }
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
        // bText.text = fileName;
    }


    public void ButtonPressed()
    {
        // var counter = 0;
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
                    periode = "00:00:00 - 00:30:00";
                    break;
                case 1:
                    periode = "00:30:00 - 01:00:00";
                    break;
                case 2:
                    periode = "01:00:00 - 01:30:00";
                    break;
                case 3:
                    periode = "01:30:00 - 02:00:00";
                    break;
                case 4:
                    periode = "02:00:00 - 02:30:00";
                    break;
                case 5:
                    periode = "02:30:00 - 03:00:00";
                    break;
                case 6:
                    periode = "03:00:00 - 03:30:00";
                    break;
                case 7:
                    periode = "03:30:00 - 04:00:00";
                    break;
                case 8:
                    periode = "04:00:00 - 04:30:00";
                    break;
                case 9:
                    periode = "04:30:00 - 05:00:00";
                    break;
                case 10:
                    periode = "05:00:00 - 05:30:00";
                    break;
                case 11:
                    periode = "05:30:00 - 06:00:00";
                    break;
                case 12:
                    periode = "06:00:00 - 06:30:00";
                    break;
                case 13:
                    periode = "06:30:00 - 07:00:00";
                    break;
                case 14:
                    periode = "07:00:00 - 07:30:00";
                    break;
                case 15:
                    periode = "07:30:00 - 08:00:00";
                    break;
                case 16:
                    periode = "08:00:00 - 08:30:00";
                    break;
                case 17:
                    periode = "08:30:00 - 09:00:00";
                    break;
                case 18:
                    periode = "09:00:00 - 09:30:00";
                    break;
                case 19:
                    periode = "09:30:00 - 10:00:00";
                    break;
                case 20:
                    periode = "10:00:00 - 10:30:00";
                    break;
                case 21:
                    periode = "10:30:00 - 11:00:00";
                    break;
                case 22:
                    periode = "11:00:00 - 11:30:00";
                    break;
                case 23:
                    periode = "11:30:00 - 12:00:00";
                    break;
                case 24:
                    periode = "12:00:00 - 12:30:00";
                    break;
                case 25:
                    periode = "12:30:00 - 13:00:00";
                    break;
                case 26:
                    periode = "13:00:00 - 13:30:00";
                    break;
                case 27:
                    periode = "13:30:00 - 14:00:00";
                    break;
                case 28:
                    periode = "14:00:00 - 14:30:00";
                    break;
                case 29:
                    periode = "14:30:00 - 15:00:00";
                    break;
                case 30:
                    periode = "15:00:00 - 15:30:00";
                    break;
                case 31:
                    periode = "15:30:00 - 16:00:00";
                    break;
                case 32:
                    periode = "16:00:00 - 16:30:00";
                    break;
                case 33:
                    periode = "16:30:00 - 17:00:00";
                    break;
                case 34:
                    periode = "17:00:00 - 17:30:00";
                    break;
                case 35:
                    periode = "17:30:00 - 18:00:00";
                    break;
                case 36:
                    periode = "18:00:00 - 18:30:00";
                    break;
                case 37:
                    periode = "18:30:00 - 19:00:00";
                    break;
                case 38:
                    periode = "19:00:00 - 19:30:00";
                    break;
                case 39:
                    periode = "19:30:00 - 20:00:00";
                    break;
                default:
                    periode = "00:00:00 - 00:00:00";
                    break;
            }
        }
        bText.text = periode;
        // point.pointList = dataList;
    }

    // public List<Dictionary<string, object>> updateCSV()
    // {
    //     return dataList;
    // }

    public string updateCSV()
    {
        return fileName;
    }
}
