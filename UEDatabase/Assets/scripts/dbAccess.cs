using UnityEngine;
using System;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;

public class dbAccess : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
    private IDataRecord record;
	private StringBuilder builder;

	// Use this for initialization
	void Start () {
		
	}
	
	
	public void OpenDB(string p)
	{
		connection = "URI=file:" + Application.persistentDataPath + "/" + p; // we set the connection to our database
		//connection = "URI=file:" + p; // like this will NOT work on android
		Debug.Log(connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}
	
	public void CloseDB(){
		reader.Close(); // clean everything up
  	 	reader = null;
   		dbcmd.Dispose();
   		dbcmd = null;
   		dbcon.Close();
   		dbcon = null;
	}
	
	public IDataReader BasicQuery(string query){ // run a basic Sqlite query
		dbcmd = dbcon.CreateCommand(); // create empty command
		dbcmd.CommandText = query; // fill the command
		reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		return reader; // return the reader
	
	}
	
	
	public bool CreateTable(string name,string[] col, string[] colType){ // Create a table, name, column array, column type array
		string query;
		query  = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
		for(var i=1; i< col.Length; i++){
			query += ", " + col[i] + " " + colType[i];
		}
		query += ")";
		try{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return false;
		}
		return true;
	}
	
	public int InsertIntoSingle(string tableName, string colName , string value ){ // single insert
		string query;
		query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
		try
		{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public int InsertIntoSpecific(string tableName, string[] col, string[] values){ // Specific insert with col and values
		string query;
		query = "INSERT INTO " + tableName + "(" + col[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + col[i];
		}
		query += ") VALUES (" + values[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		Debug.Log(query);
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public int InsertInto(string tableName , string[] values ){ // basic Insert with just values
		string query;
		query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for(int i=1; i< values.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public string[,] SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;
        print(query);
        dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		string[,] readArray = new string[reader.RecordsAffected+1, reader.FieldCount];
        reader.Read();
        var read = reader.GetString(0);

        int i=0;
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				readArray[i,j]=reader.GetString(j); // Fill array with all matches
				j++;
			}
			i++;
		}
        foreach (var n in readArray) {
            print(n);
        }
        return readArray; // return matches
	}


    public bool PwCompare(string tableName, string itemToSelect, string wCol, string wPar, string wValue, string pw)
    { // Selects a single Item
        string query;
        query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;
        print(query);
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        reader.Read();
        var read = reader.GetString(0);

        return string.Equals(read, pw); // return matches
    }

    public int CheckValue(string tableName, string itemToSelect, string wCol, string wPar, string wValue)
    { // Selects a single Item
        string query;
        query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;

        try
        {
            dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
        }
        catch (Exception e)
        {

            Debug.Log(e);
            //return 0;
        }

        int val;
        if (reader.Read())
        {
            val = 0;

        }
        else
        {
            val = 1;
           
        }
        return val; // return matches
    }


}