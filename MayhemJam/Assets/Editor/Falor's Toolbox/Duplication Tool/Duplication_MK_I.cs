using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum Dup_Axis
{
    singleAxis,
    twoAxis,
    threeAxis
}

public enum Axis
{
    X,
    Y,
    Z
}

public enum Plane
{
    XY,
    XZ,
    YZ
}
public class Duplication_MK_I : EditorWindow {

    List<List<GameObject>> previous = new List<List<GameObject>>();
    
    //Selected Duplication image and mode
    static int ImageSelection = 0;
    // Slider Min and max values for distance
    const float sliderMaxValue = 50;

    

    // Single Axis Duplication Variables
    Axis axisOptions = 0;
    float dup_distance = 0;
    int dup_amount = 0;
    int max_amount = 25;
    float max_distance = 25;

    // Double Axis Duplicaiton Variables
    Plane selectedPlane = 0;
    float dupPlaneOne_distance = 0;
    float dupPlaneTwo_distance = 0;
    int dupPlaneOne_Amount = 1;
    int dupPlaneTwo_Amount = 1;

    // Tripple Axis Duplication Variables
    float dupMatrixOne_distance = 0;
    float dupMatrixTwo_distance = 0;
    float dupMatrixThree_distance = 0;
    int dupMatrixOne_Amount = 1;
    int dupMatrixTwo_Amount = 1;
    int dupMatrixThree_Amount = 1;

    Dup_Axis dup_Axis;

    static Texture[] header;


    //Adds the Script widow to the windows Tab
    [MenuItem("Falor's Toolbox/Duplication Tool Mk I")]
    public static void ShowWindow()
    {
        // Opens the new window
        EditorWindow.GetWindow(typeof(Duplication_MK_I));
    }


    /// <summary>
    /// Creates the GUI for the Duplication Tool
    /// </summary>
    private void OnGUI()
    {
        minSize = new Vector2(400, 400);
        maxSize = new Vector2(400, 400);
        //dup_Axis = (Dup_Axis)EditorGUI.EnumPopup(new Rect(0, 0, position.width, 25), "Axis: ", dup_Axis);

        Rect selection = new Rect(0, 0, position.width, position.height / 4 + 4);
        //EditorGUI.DrawTextureAlpha(selection, header[0]);

        header = new Texture[3];
        header[0] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Falor's Toolbox/Duplication Tool/singleAxisHeader.png", typeof(Texture)) as Texture;
                                                                  
        header[1] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Falor's Toolbox/Duplication Tool/doubleAxisHeader.png", typeof(Texture)) as Texture;
                                                                                       
        header[2] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Falor's Toolbox/Duplication Tool/trippleAxisHeader.png", typeof(Texture)) as Texture;


        GUIContent img = new GUIContent(header[ImageSelection]);

        img.tooltip = "Toggle Duplication Mode";

        if (GUI.Button(selection, img))
        {
            ImageSelection = (ImageSelection + 1) % 3;
        }

        switch (ImageSelection % 3)
        {
            #region LineGeneration
            case 0:
                // UI Elements
                axisOptions = (Axis)GenerateChoiceBox(new Rect(0, position.height / 4 + 10, position.width, 20), axisOptions, (int)axisOptions, "Select Axis");
                string axisName = Enum.GetName(axisOptions.GetType(), axisOptions);
                dup_amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 30, position.width, 20), "Duplicates " + axisName, dup_amount, 1,  25 + max_amount);
                // This adds dynamic sliders. Subject to change due to clunkiness
                if (dup_amount > max_amount)
                    max_amount = dup_amount;
                dup_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 50, position.width, 20), "Distance " + axisName, dup_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dup_distance > max_distance || dup_distance < -max_distance)
                    max_distance = Mathf.Abs(dup_distance);

                // Generation Logic
                if (GUI.Button(new Rect(position.width / 2, (position.height / 6) * 5, position.width / 2, position.height / 6), "Generate"))
                {
                    List<GameObject> previouslyAdded = new List<GameObject>();
                    switch (axisOptions)
                    {
                        case Axis.X:
                            for (int i = 1; i <= dup_amount; i++)
                            {
                                foreach (GameObject tmp in Selection.gameObjects)
                                {
                                    GameObject temp = Instantiate(tmp, tmp.transform.position + new Vector3(dup_distance * i, 0, 0), Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                    temp.transform.localScale = tmp.transform.lossyScale;
                                    previouslyAdded.Add(temp);
                                }
                            }
                            break;
                        case Axis.Y:

                            for (int i = 1; i <= dup_amount; i++)
                            {
                                foreach (GameObject tmp in Selection.gameObjects)
                                {
                                    GameObject temp = Instantiate(tmp, tmp.transform.position + new Vector3(0, dup_distance * i, 0), Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                    temp.transform.localScale = tmp.transform.lossyScale;
                                    previouslyAdded.Add(temp);
                                }
                            }
                            break;
                        case Axis.Z:

                            for (int i = 1; i <= dup_amount; i++)
                            {
                                foreach (GameObject tmp in Selection.gameObjects)
                                {
                                    GameObject temp = Instantiate(tmp, tmp.transform.position + new Vector3(0, 0, dup_distance * i), Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                    temp.transform.localScale = tmp.transform.lossyScale;
                                    previouslyAdded.Add(temp);                                
                                }
                            }
                            break;
                    }
                    if (Selection.gameObjects.Length > 0)
                    {
                        GameObject root = new GameObject(Selection.gameObjects[0].name + " Group");
                        foreach (GameObject tmp in previouslyAdded)
                            tmp.transform.parent = root.transform;
                        previouslyAdded.Add(root);
                    }
                    previous.Add(previouslyAdded);
                }


                break;
            #endregion

            #region PlaneGeneration
            case 1:
                // UI Elements
                selectedPlane = (Plane)GenerateChoiceBox(new Rect(0, position.height / 4 + 10, position.width, 20), selectedPlane, (int)selectedPlane, "Select Plane");
                string planeName = Enum.GetName(selectedPlane.GetType(), selectedPlane);

                dupPlaneOne_Amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 30, position.width, 20), ("Duplicates " + planeName[0]), dupPlaneOne_Amount, 1, 25 + max_amount);
                if (dupPlaneOne_Amount > max_amount)
                    max_amount = dupPlaneOne_Amount;
                dupPlaneTwo_Amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 50, position.width, 20), ("Duplicates " + planeName[1]), dupPlaneTwo_Amount, 1, 25 + max_amount);
                if (dupPlaneTwo_Amount > max_amount)
                    max_amount = dupPlaneTwo_Amount;

                dupPlaneOne_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 70, position.width, 20), ("Distance " + planeName[0]), dupPlaneOne_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dupPlaneOne_distance > max_distance || dupPlaneOne_distance < -max_distance)
                    max_distance = Mathf.Abs(dupPlaneOne_distance);
                dupPlaneTwo_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 90, position.width, 20), ("Distance " + planeName[1]), dupPlaneTwo_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dupPlaneTwo_distance > max_distance || dupPlaneTwo_distance < -max_distance)
                    max_distance = Mathf.Abs(dupPlaneTwo_distance);



                // Generation Logic
                if (GUI.Button(new Rect(position.width / 2, (position.height / 6) * 5, position.width / 2, position.height / 6), "Generate"))
                {
                    List<GameObject> previouslyAdded = new List<GameObject>();
                    switch (selectedPlane)
                    {
                        case Plane.XY:
                            for (int i = 0; i <= dupPlaneOne_Amount; i++)
                            {
                                for (int j = 0; j < dupPlaneTwo_Amount; j++)
                                {
                                    foreach (GameObject tmp in Selection.gameObjects)
                                    {
                                        if (i != 0 ^ j != 0 || i > 0 || j > 0) {
                                            GameObject temp = Instantiate(tmp, tmp.transform.position + Vector3.right * dupPlaneOne_distance * i + Vector3.up * dupPlaneTwo_distance * j, Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                            temp.transform.localScale = tmp.transform.lossyScale;
                                            previouslyAdded.Add(temp);
                                        }
                                        
                                            
                                    }
                                }
                            }
                            break;
                        case Plane.XZ:

                            for (int i = 0; i <= dupPlaneOne_Amount; i++)
                            {
                                for (int j = 0; j < dupPlaneTwo_Amount; j++)
                                {
                                    foreach (GameObject tmp in Selection.gameObjects)
                                    {
                                        if (i != 0 ^ j != 0 || i > 0 || j > 0)
                                        {
                                            GameObject temp = Instantiate(tmp, tmp.transform.position + Vector3.forward * dupPlaneOne_distance * i + Vector3.right * dupPlaneTwo_distance * j, Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                            temp.transform.localScale = tmp.transform.lossyScale;
                                            previouslyAdded.Add(temp);
                                        }
                                            
                                    }
                                }
                            }
                            break;
                        case Plane.YZ:

                            for (int i = 0; i <= dupPlaneOne_Amount; i++)
                            {
                                for (int j = 0; j < dupPlaneTwo_Amount; j++)
                                {
                                    foreach (GameObject tmp in Selection.gameObjects)
                                    {
                                        if (i != 0 ^ j != 0 || i > 0 || j > 0)
                                        {
                                            GameObject temp = Instantiate(tmp, tmp.transform.position + Vector3.forward * dupPlaneOne_distance * i + Vector3.up * dupPlaneTwo_distance * j, Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                            temp.transform.localScale = tmp.transform.lossyScale;
                                            previouslyAdded.Add(temp);

                                        }
                                    }
                                }
                            }
                            break;
                    }

                    if (Selection.gameObjects.Length > 0)
                    {
                        GameObject root = new GameObject(Selection.gameObjects[0].name + " Group");
                        foreach (GameObject tmp in previouslyAdded)
                            tmp.transform.parent = root.transform;
                        previouslyAdded.Add(root);
                    }
                    previous.Add(previouslyAdded);
                }
                break;
            #endregion

            #region MatrixGeneration
            case 2:

                // UI Elements
                dupMatrixOne_Amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 5, position.width, 20), ("Duplicates " + "X"), dupMatrixOne_Amount, 1, 25);
                if (dupMatrixOne_Amount > max_amount)
                    max_amount = dupMatrixOne_Amount;
                dupMatrixTwo_Amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 25, position.width, 20), ("Duplicates " + "Y"), dupMatrixTwo_Amount, 1, 25);
                if (dupMatrixTwo_Amount > max_amount)
                    max_amount = dupMatrixTwo_Amount;
                dupMatrixThree_Amount = EditorGUI.IntSlider(new Rect(0, position.height / 4 + 45, position.width, 20), ("Duplicates " + "Z"), dupMatrixThree_Amount, 1, 25);
                if (dupMatrixThree_Amount > max_amount)
                    max_amount = dupMatrixThree_Amount;
                dupMatrixOne_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 65, position.width, 20), ("Distance " + "X"), dupMatrixOne_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dupMatrixOne_distance > max_distance)
                    max_distance = Mathf.Abs(dupMatrixOne_distance);
                dupMatrixTwo_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 85, position.width, 20), ("Distance " + "Y"), dupMatrixTwo_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dupMatrixTwo_distance > max_distance)
                    max_distance = Mathf.Abs(dupMatrixTwo_distance);
                dupMatrixThree_distance = EditorGUI.Slider(new Rect(0, position.height / 4 + 105, position.width, 20), ("Distance " + "Z"), dupMatrixThree_distance, -sliderMaxValue - max_distance, sliderMaxValue + max_distance);
                if (dupMatrixThree_distance > max_distance)
                    max_distance = Mathf.Abs(dupMatrixThree_distance);

                // Generation Logic
                if (GUI.Button(new Rect(position.width / 2, (position.height / 6) * 5, position.width / 2, position.height / 6), "Generate"))
                {
                    List<GameObject> previouslyAdded = new List<GameObject>();
                    for (int i = 0; i <= dupMatrixThree_Amount; i++)
                    {
                        for (int j = 0; j < dupMatrixTwo_Amount; j++)
                        {
                            for (int k = 0; k <= dupMatrixOne_Amount; k++)
                                foreach (GameObject tmp in Selection.gameObjects)
                                {
                                    if (!(i == 0 && j == 0 && k == 0))
                                    {
                                        GameObject temp = Instantiate(tmp, tmp.transform.position + Vector3.right * dupMatrixOne_distance * k + Vector3.up * dupMatrixTwo_distance * j + Vector3.forward * dupMatrixThree_distance * i, Quaternion.Euler(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y, tmp.transform.eulerAngles.z));
                                        temp.transform.localScale = tmp.transform.lossyScale;
                                        previouslyAdded.Add(temp);
                                    }
                                        
                                }
                        }
                    }
                    if (Selection.gameObjects.Length > 0)
                    {
                        GameObject root = new GameObject(Selection.gameObjects[0].name + " Group");
                        foreach (GameObject tmp in previouslyAdded)
                            tmp.transform.parent = root.transform;
                        previouslyAdded.Add(root);
                    }
                    previous.Add(previouslyAdded);
                }
                break;
        }

        if (GUI.Button(new Rect(0, (position.height / 6) * 5, position.width / 2, position.height / 6), "Undo"))
        {
            RemoveLastGeneration();
        }

        #endregion
    }
    /// <summary>
    /// Removes the last generated Obejects similar to ctrl + z
    /// </summary>
    public void RemoveLastGeneration()
    {
        if (previous.Count > 0)
        {
            foreach (GameObject tmp in previous[previous.Count - 1])
                DestroyImmediate(tmp);
            previous.RemoveAt(previous.Count - 1);
        }
    }

    /// <summary>
    /// Generates a Choicebox (Toggle group consisting of buttons)
    /// </summary>
    /// <param name="boxposition"></param>
    /// <param name="options"></param>
    /// <param name="selected"></param>
    /// <param name="label"></param>
    public int GenerateChoiceBox(Rect boxposition, Enum options, int selected, string label = null)
    {
        string[] optionNames = Enum.GetNames(options.GetType());
        
        int spacing = (int) position.width / (optionNames.Length + ((label != null || label == "")? 1 : 0));

        int i = 0;

        if (label != null || label == "")
        {    
            GUI.Label(new Rect(boxposition.position.x, boxposition.position.y, spacing, boxposition.height), label);
            i++;
        }
        
        for (; i < optionNames.Length + ((label != null || label == "") ? 1 : 0); i++)
        {
            if(GUI.Button(new Rect(boxposition.position.x + spacing * i, boxposition.position.y, spacing, boxposition.height), ((i == (selected + ((label != null || label == "") ? 1 :  0))? "(" : "") + optionNames[i - ((label != null || label == "") ? 1 : 0)] + ((i == (selected + ((label != null || label == "") ? 1 : 0)) ? ")" : ""))))){
                //Debug.Log(i);
                selected = i - ((label != null || label == "") ? 1 : 0);
            }
        }
        return selected;
    }
}
