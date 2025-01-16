using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    // Name of the input file, no extension
    public string inputfile;

    // Indices for columns to be assigned
    public bool useColumnIndices=true;
    public bool addColorDimension;
    public int columnX;
    public int columnY;
    public int columnZ;
    public int columnE;
 
    // Full column names
    public string xName;
    public string yName;
    public string zName;
    public string eName;

    //Box info
    //Selected columns for box threshold
    public bool enableBoxThreshold;
    public int columnX_THold;
    public int columnY_THold;
    public int columnZ_THold;

    //Box variables Full names
    public string x_Threshold;
    public string y_Threshold;
    public string z_Threshold;

    //Threshold sizes
    public float x_ThresholdSize = 4;
    public float y_ThresholdSize = 5;
    public float z_ThresholdSize = 5;
    public float maxSize = 9;

    //Threshold box size
    float xEdge;
    float yEdge;
    float zEdge;
    
    //For scaling the data
    float xMax;
    float yMax;
    float zMax;
    float xMin;
    float yMin;
    float zMin;
    
    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;

    // The prefab for the data points that will be instantiated
    public GameObject PointHolder;
    

    //The prefab for the info of data points that will be instantiated
    public GameObject PointInfo;
    public GameObject PointInfoHolder;

    // The prefab for the box
    public GameObject BoxPrefab;

    // Plot adjustments
    public bool adjustPlot;
    public float plotScale = 2;
    public enum RoundingOptions
    {
        NoChange,
        Ceil,
        Round,
        Floor
    }
    public RoundingOptions roundingOptions;

    // variables used for checking if a sphere is already created
    float radius = 0.01f;
    Vector3 temp;
    int counter;
    List<Vector3> duplicates = new List<Vector3>();
    List<Vector3> distinct = new List<Vector3>();
    List<int> counts = new List<int>();

    //Variables for column E in raw data
    List<Vector3> colE = new List<Vector3>();
    List<Vector3> colEDistinct = new List<Vector3>();
    List<int> colorCounts = new List<int>();
    int colorCounter;
    int countdown;
    bool colorChange;


    //Variables for equiidistant points
    int numPoints;
    public float radius2 = 0.03f;
    public GameObject spherePrefab;
    private Vector3[] spherePoints;
    private GameObject[] sphereObjects;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;




 
 // Use this for initialization
 void Start () {
 
    PointInfoHolder = GameObject.Find("Point Info Holder");
    // Set pointlist to results of function Reader with argument inputfile
    pointList = CSVReader.Read(inputfile);
 
    //Log to console
    Debug.Log(pointList);

    // Declare list of strings, fill with keys (column names)
    List<string> columnList = new List<string>(pointList[1].Keys);
 
    // Print number of keys (using .count)
    Debug.Log("There are " + columnList.Count + " columns in CSV");
 
    foreach (string key in columnList)
        Debug.Log("Column name is " + key);

    
    // Assign column name from columnList to Name variables
    xName = columnList[columnX];
    yName = columnList[columnY];
    zName = columnList[columnZ];

    try
    {
        eName = columnList[columnE];
        // Assign column name from columnList to Threshold variables
        x_Threshold = columnList[columnX_THold];
        y_Threshold = columnList[columnY_THold];
        z_Threshold = columnList[columnZ_THold];
    }

    catch (Exception ex)
    {

    }

    //Loop through Pointlist
    for (var i = 0; i < pointList.Count; i++)
    {

        // Get maxes of each axis
        xMax = FindMaxValue(xName);
        yMax = FindMaxValue(yName);
        zMax = FindMaxValue(zName);
 
        // Get minimums of each axis
        xMin = FindMinValue(xName);
        yMin = FindMinValue(yName);
        zMin = FindMinValue(zName);

        // Get value in poinList at ith "row", in "column" Name, Round it up/down    
        float x_val, y_val, z_val;
        if(roundingOptions == RoundingOptions.Ceil)
        {
            x_val = (float) Math.Ceiling(System.Convert.ToSingle(pointList[i][xName]));
            y_val = (float) Math.Ceiling(System.Convert.ToSingle(pointList[i][yName]));
            z_val = (float) Math.Ceiling(System.Convert.ToSingle(pointList[i][zName]));
        }
        else if(roundingOptions == RoundingOptions.Round)
        {
            x_val = (float) Math.Round(System.Convert.ToSingle(pointList[i][xName]));
            y_val = (float) Math.Round(System.Convert.ToSingle(pointList[i][yName]));
            z_val = (float) Math.Round(System.Convert.ToSingle(pointList[i][zName]));
        }
        else if(roundingOptions == RoundingOptions.Floor)
        {
            x_val = (float) Math.Floor(System.Convert.ToSingle(pointList[i][xName]));
            y_val = (float) Math.Floor(System.Convert.ToSingle(pointList[i][yName]));
            z_val = (float) Math.Floor(System.Convert.ToSingle(pointList[i][zName]));
        }
        else
        {
            x_val = System.Convert.ToSingle(pointList[i][xName]);
            y_val = System.Convert.ToSingle(pointList[i][yName]);
            z_val = System.Convert.ToSingle(pointList[i][zName]);
        }

        // normalize
        float x = 
            (x_val - xMin) / (xMax - xMin);
 
        float y = 
            (y_val - yMin) / (yMax - yMin);
 
        float z = 
            (z_val - zMin) / (zMax - zMin);
        
        float e=0;
        try
        {
            e = (System.Convert.ToSingle(pointList[i][eName]));
        }
        catch (Exception ex)
        {

        }
        

        temp = new Vector3(x,y,z) * plotScale;

        //Checks to see if there exist a sphere in "temp" location.
        //if so, it adds that position to the "duplicates" list.
        if (roundingOptions != RoundingOptions.NoChange && Physics.CheckSphere(temp, radius))
        {   
            duplicates.Add(temp);

            //If the value in the column e is equal to 1 add it to the list
            if(addColorDimension && e == 1)
            {
                colE.Add(temp);
            }
        }

       
        else 
        {
            
            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                PointPrefab,
                new Vector3(x, y, z) * plotScale,
                Quaternion.identity);
            
 
            // Make child of PointHolder object, to keep points within container in hierarchy
            dataPoint.transform.parent = PointHolder.transform;
 
            // Assigns original values to dataPointName
            string dataPointName =
                Convert.ToDouble(pointList[i][xName]).ToString("F0") + "," +
                Convert.ToDouble(pointList[i][yName]).ToString("F0") + "," +
                Convert.ToDouble(pointList[i][zName]).ToString("F0");
 
            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;

            // Gets material color and sets it to a new RGBA color we define
            dataPoint.GetComponent<Renderer>().material.color = 
            new Color(0.5f,0.5f,0.5f,1.0f);

            //If the value in the column e is equal to 1 add it to the list
            if(addColorDimension && e == 1)
            {
                colE.Add(temp);
            }

            if(roundingOptions != RoundingOptions.NoChange || (roundingOptions == RoundingOptions.NoChange && x_val % 1 == 0 & y_val % 1 == 0 && z_val % 1 == 0))
            {
                //Instantiate text info
                GameObject dataPointsInfo = Instantiate(
                    PointInfo, new Vector3(x-0.01f, y -0.02f, z) * plotScale,
                    Quaternion.identity);

                dataPointsInfo.GetComponent<TextMesh>().text = dataPointName;
                // Make child of PointInfo object, to keep points within container in hierarchy
                dataPointsInfo.transform.parent = PointInfoHolder.transform;
            }

        
        }
 
    }   

        //x,y,z axis names
        GameObject.Find("X-Title").GetComponent<TextMesh>().text = xName;
        GameObject.Find("Y-Title").GetComponent<TextMesh>().text = yName;
        GameObject.Find("Z-Title").GetComponent<TextMesh>().text = zName;
        

        //Threshold Box
        xEdge = maxSize - x_ThresholdSize;
        yEdge = maxSize - y_ThresholdSize;
        zEdge = maxSize - z_ThresholdSize;

        float xAdjust = (x_ThresholdSize + xEdge / 2 - xMin) / (xMax - xMin);
        float yAdjust = (y_ThresholdSize + yEdge / 2 - yMin) / (yMax - yMin);
        float zAdjust = (z_ThresholdSize + zEdge / 2 - zMin) / (zMax - zMin);

        float xScale = (xEdge - xMin) / (xMax - xMin);
        float yScale = (yEdge - yMin) / (yMax - yMin);
        float zScale = (zEdge - zMin) / (zMax - zMin);

        BoxPrefab.gameObject.transform.localScale = new Vector3(xScale, yScale, zScale) * plotScale;
        if(enableBoxThreshold)
        {
            Instantiate(BoxPrefab, new Vector3(xAdjust, yAdjust, zAdjust) * plotScale, Quaternion.identity);
        }
        



        //Creates a list of "distinct" positions based on "duplicates" list.
        distinct = Distinct(duplicates);

        //Creates a list of distinct positions based on "colE" list.
        colEDistinct = Distinct(colE);

        //Changes the color of main sphere to magenta
        for(int i = 0; i < colEDistinct.Count; i++)
        {
            Vector3 targetPosition = colEDistinct[i];
            Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 0);
            if (hitColliders.Length > 0) 
            {
                GameObject selectedObject = hitColliders[0].gameObject;
                if(addColorDimension)
                {
                    selectedObject.GetComponent<Renderer>().material.color = new Color(1,0,1,1);
                }
                
            }
        }

        //Removes spheres that we changed their color in above code from the list "colE"
        for(int i = 0; i < colEDistinct.Count; i++)
        {
            for(int j = 0; j < colE.Count; j++)
            {
                if(colEDistinct[i] == colE[j])
                {
                    colE.RemoveAt(j);
                    break;
                }
            }
        }

        //Gets the number of duplicated spheres in each position. 
        for(int i = 0; i < distinct.Count; i++)
        {
            for(int j = 0; j < duplicates.Count; j ++)
            {
                if(distinct[i] == duplicates[j])
                {
                    counter++;
                }
            }

            //Add number of duplicated spheres to the list "counts"
            counts.Add(counter);
            counter = 0;

            //Gets the number of spheres that are needed to have a different color
            for(int q = 0; q < colE.Count; q++)
            {
                if(distinct[i] == colE[q])
                {
                    colorCounter++;
                }
            }
            //Add number of spheres to the list "colorCounts"
            colorCounts.Add(colorCounter);
            colorCounter = 0;

            //Calculating equiidistant points and apply color change to designated spheres
            numPoints = counts[i];
            countdown = colorCounts[i];
            spherePoints = new Vector3[numPoints];
            sphereObjects = new GameObject[numPoints];
            double inc = Math.PI * (3.0 - Math.Sqrt(5.0));
            double off = 2.0 / numPoints;
            for(int k = 0; k < numPoints; k++)
            {
                double y = k * off - 1 + (off /2);
                double r = Math.Sqrt(1.0 - y * y);
                double phi = k * inc;
                spherePoints[k] = new Vector3((float)(Math.Cos(phi) * r), (float)y, (float)(Math.Sin(phi) * r));
                spherePoints[k] *= radius2;
                
                //Instantiate equidistant points relative to its reference object
                sphereObjects[k] = Instantiate(spherePrefab, distinct[i] + spherePoints[k], Quaternion.identity, transform);

                //change the color of spheres around the center point to magenta
                if(countdown >= 1)
                {
                    colorChange = true;
                }
                while(colorChange)
                {
                    sphereObjects[k].GetComponent<Renderer>().material.color = new Color(1,0,1,1);                   
                    colorChange = false;
                }
                countdown--;
            }

        }

 }


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


private float FindMinValue(string columnName)
   {
 
       float minValue = Convert.ToSingle(pointList[0][columnName]);
 
       //Loop through Dictionary, overwrite existing minValue if new value is smaller
       for (var i = 0; i < pointList.Count; i++)
       {
           if (Convert.ToSingle(pointList[i][columnName]) < minValue)
               minValue = Convert.ToSingle(pointList[i][columnName]);
       }
 
       return minValue;
   }


private List<Vector3> Distinct(List<Vector3> a)
{   
    List<Vector3> b = new List<Vector3>(a);
    for(int i=0; i < b.Count; i++)
    {
        for(int j=i+1; j < b.Count; j++)
        {
            if(b[i] == b[j])
            {
                b.RemoveAt(j);
            }
        }
    }
    return(b);
}

}
