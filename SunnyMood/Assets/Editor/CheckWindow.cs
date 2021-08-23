using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class CheckWindow : EditorWindow
{
    private bool ok;
    public GUIStyle red, green;
    private String icon;
    [MenuItem("Window/Check")]
    public static void ShowWindow()
    {
        GetWindow<CheckWindow>("Check");
    }
    
    void createStyles()
    {
        if (red == null)
        {
            red = new GUIStyle(EditorStyles.label);
            red.normal.textColor = Color.red;
        }

        if (green == null)
        {
            green = new GUIStyle(EditorStyles.label);
            green.normal.textColor = Color.green;
        }
    }
    private void OnGUI()
    {
        createStyles();
        GUILayout.Label("Press here to run tests:");
        if (GUILayout.Button("Check",GUILayout.Width(100)))
        {
            icon = "";
            MyEditorScript.result = "";
            MyEditorScript.RunPlayModeTests();
        }
        GUILayout.Label("\nResult:");

        GUILayout.BeginHorizontal();
        if (ok)
        {
            GUILayout.Label(icon,green,GUILayout.Width(15));
        }
        else
        {
            GUILayout.Label(icon,red,GUILayout.Width(15));
        }
        
        GUILayout.Label(MyEditorScript.result);
        GUILayout.EndHorizontal();
        
        Event e = Event.current;
        
        if (e.commandName == "FinishedWrong")
        {
            icon = "✖️";
            ok = false;
            Repaint();
        }
        if (e.commandName == "FinishedOk")
        {
            icon = "✔️";
            ok = true;
            Repaint();
        }
    }
        
}
