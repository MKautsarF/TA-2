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

    public void ReadNextCSVFile()
    {
        index++; // Increment the file index for the next CSV file
        fileName = baseFileName + index.ToString();
        dataList = CSVReader.Read(fileName);

        // Do whatever you want with the dataList, such as processing the data or displaying it
        
        // bText.text = fileName;
    }

    public void ReadPreviousCSVFile()
    {
        if (index > 0)
        {
            index--; // Decrement the file index to move to the previous CSV file

            fileName = baseFileName + index.ToString();

            dataList = CSVReader.Read(fileName);

            // Do whatever you want with the dataList, such as processing the data or displaying it
        }
        else
        {
            Debug.Log("Cannot go back further. Already at the first CSV file.");
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
        bText.text = fileName;
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
