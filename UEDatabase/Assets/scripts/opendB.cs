using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Data;


public class opendB : MonoBehaviour
{
    private string description;

    public InputField ncreate, pwcreate, nlogin, pwlogin;
    public Text resultado;
    dbAccess db;

    private IDataReader reader;

    // Use this for initialization
    void Start()
    {
        resultado.text = " ";
        // Retrieve next word from database
        description = "something went wrong with the database";

        db = GetComponent<dbAccess>();

        if (!System.IO.File.Exists(Application.persistentDataPath + "/" + "InGame.db"))
        {
            db.OpenDB("InGame.db");

        }
        else {

            db.OpenDB("InGame.db");
     
        }

        //reader = db.BasicQuery("drop table if exists users");
        reader = db.BasicQuery("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'users';");


        string[] col = { "kp", "nombre", "pw" };
        string[] colType = { "integer primary key autoincrement", "text", "text" };

        if (!db.CreateTable("users", col, colType))
            Debug.Log("Error creating table");
   
        db.CloseDB();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void create()
    {
        db.OpenDB("InGame.db");
        int n = db.CheckValue("users", "pw", "nombre", "=", "'" + ncreate.text + "'");
        //para evitar repetir un usuario
       if (n > 0) {
            db.OpenDB("InGame.db");
            string[] inscol = { "nombre", "pw" };
            string[] inscolvalue = { "'" + ncreate.text + "'", "'" + pwcreate.text + "'" };

            db.InsertIntoSpecific("users", inscol, inscolvalue);
            db.CloseDB();
            resultado.text = "usuario creado"; 
            }
        else
        {
            resultado.text = "usuario ya existe";
        }
    }

    public void login()
    {
        db.OpenDB("InGame.db");

        bool result = db.PwCompare("users", "pw", "nombre", "=", "'" + nlogin.text + "'",pwlogin.text);
 

        db.CloseDB();
        if (result) resultado.text = "login exitoso";
        else resultado.text = "Clave errada";
    }
}
