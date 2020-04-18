using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class DBManager 
{
    public static string username;
    public static int score, level;

    public static bool logeado { get { return username != null; } }


    public static void LogOut()
    {
        username = null;
    }
}
