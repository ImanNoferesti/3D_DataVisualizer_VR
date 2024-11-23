using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataPlotter))]
public class GUI_Editor : Editor
{
    
    public override void OnInspectorGUI()
    {
        // Get a reference to the target object
        DataPlotter dataPlotter = (DataPlotter)target;

        dataPlotter.inputfile = EditorGUILayout.TextField("File Name (String)", dataPlotter.inputfile);

        // Draw the useColumnIndices toggle
        dataPlotter.useColumnIndices = EditorGUILayout.Toggle("Choose Columns By Index", dataPlotter.useColumnIndices);

        // If enabled, show box dimensions
        if (dataPlotter.useColumnIndices)
        {
            dataPlotter.columnX = EditorGUILayout.IntField("X-Axis Column Index", dataPlotter.columnX);
            dataPlotter.columnY = EditorGUILayout.IntField("Y-Axis Column Index", dataPlotter.columnY);
            dataPlotter.columnZ = EditorGUILayout.IntField("Z-Axis Column Index", dataPlotter.columnZ);
        }
        else
        {
            dataPlotter.xName = EditorGUILayout.TextField("X-Axis Column Name (String)", dataPlotter.xName);
            dataPlotter.yName = EditorGUILayout.TextField("Y-Axis Column Name (String)", dataPlotter.yName);
            dataPlotter.zName = EditorGUILayout.TextField("Z-Axis Column Name (String)", dataPlotter.zName);
        }

        // Draw the addColorDimension toggle
        dataPlotter.addColorDimension = EditorGUILayout.Toggle("Add Color Dimension (Binary 0-1 Columns)", dataPlotter.addColorDimension);
        if (dataPlotter.addColorDimension)
        {
            dataPlotter.columnE = EditorGUILayout.IntField("Color Column Index", dataPlotter.columnE);
            EditorGUILayout.LabelField("Color Column Name", dataPlotter.eName);
        }

        // Draw the enableBoxThreshold toggle
        dataPlotter.enableBoxThreshold = EditorGUILayout.Toggle("Enable Box Threshold", dataPlotter.enableBoxThreshold);

        // If enabled, show box dimensions
        if (dataPlotter.enableBoxThreshold)
        {
            dataPlotter.x_ThresholdSize = EditorGUILayout.FloatField("Box X-Min", dataPlotter.x_ThresholdSize);
            dataPlotter.y_ThresholdSize = EditorGUILayout.FloatField("Box Y-Min", dataPlotter.y_ThresholdSize);
            dataPlotter.z_ThresholdSize = EditorGUILayout.FloatField("Box Z-Min", dataPlotter.z_ThresholdSize);
            dataPlotter.maxSize = EditorGUILayout.FloatField("Box Edge-Length", dataPlotter.maxSize);

            dataPlotter.columnX_THold = EditorGUILayout.IntField("X-Threshold Col Index", dataPlotter.columnX_THold);
            dataPlotter.columnY_THold = EditorGUILayout.IntField("Y-Threshold Col Index", dataPlotter.columnY_THold);
            dataPlotter.columnZ_THold = EditorGUILayout.IntField("Z-Threshold Col Index", dataPlotter.columnZ_THold);
            EditorGUILayout.LabelField("X-Threshold Col Name", dataPlotter.x_Threshold);
            EditorGUILayout.LabelField("Y-Threshold Col Name", dataPlotter.y_Threshold);
            EditorGUILayout.LabelField("Z-Threshold Col Name", dataPlotter.z_Threshold);
        }


        // Draw the adjustPlot toggle
        dataPlotter.adjustPlot = EditorGUILayout.Toggle("Adjust the Plot", dataPlotter.adjustPlot);

        if (dataPlotter.adjustPlot)
        {
            dataPlotter.plotScale = EditorGUILayout.FloatField("Plot Scale", dataPlotter.plotScale);
            dataPlotter.radius2 = EditorGUILayout.FloatField("Secondary Sphere Radius", dataPlotter.radius2);
        }

            

        // Apply changes to the serialized object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(dataPlotter);
        }
    }

}
