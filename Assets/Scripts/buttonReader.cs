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

    int index;
    string baseFileName;

    void Awake()
    {
        fileName = point.inputFile();

        // Split the fileName into index and baseFileName
        int underscoreIndex = fileName.LastIndexOf('_');
        if (underscoreIndex >= 0)
        {
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

    public void ReadNextCSVFile()
    {
        index++; // Increment the file index for the next CSV file
        if(index==20)
        {
            index=0;
        }
        fileName = baseFileName + index.ToString();
        dataList = CSVReader.Read(fileName);

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
        if (index == 0)
        {
            index = 20;
        }
        index--; // Decrement the file index to move to the previous CSV file

        fileName = baseFileName + index.ToString();

        dataList = CSVReader.Read(fileName);
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
        string periode;
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
