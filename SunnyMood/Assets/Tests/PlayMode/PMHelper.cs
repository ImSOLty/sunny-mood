using System;
using System.Reflection;
using UnityEngine;

public static class PMHelper
{
    public static float ymax;
    public static String curLevel="Level 3";
    public static String nextLevel="Main Menu";

    public static GameObject Exist(String s)
    {
        return GameObject.Find(s);
    }
    public static T Exist<T>(GameObject g)
    {
        return g.GetComponent<T>();
    }
    public static bool Child(GameObject g, GameObject g2)
    {
        return g.transform.IsChildOf(g2.transform);
    }
}
