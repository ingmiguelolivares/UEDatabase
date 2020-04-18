﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class login : MonoBehaviour
{
    public InputField Nombre, Password;

    public Button LoginButton;

    public void CallLogin()
    {
        StartCoroutine(GetRequest("http://192.168.1.120:8888/sqlconnect/login.php"));

    }

    private void Update()
    {
        VerifyInputs();
    }

    IEnumerator GetRequest(string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Nombre.text);
        form.AddField("password", Password.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {

            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
           
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0] == "0")
                {
                    DBManager.username = Nombre.text;
                    DBManager.level = int.Parse(webRequest.downloadHandler.text.Split('\t')[1]);
                    DBManager.score = int.Parse(webRequest.downloadHandler.text.Split('\t')[2]);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);

                }
                else
                {
                    print("login no se pudo realizar. Error #" + webRequest.downloadHandler.text);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        LoginButton.interactable = (Nombre.text.Length >= 8 && Password.text.Length >= 8);

    }
}
