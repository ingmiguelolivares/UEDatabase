using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registro : MonoBehaviour
{
    public InputField Nombre, Password;

    public Button RegistrarButton;

    public void CallRegistro() {
        StartCoroutine(GetRequest("http://192.168.1.120:8888/sqlconnect/registro.php"));
        //StartCoroutine(GetRequest("https://www.google.com/"));
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
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text == "0")
                {
                    print("usuario creado exitosamente");
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);        
                }
                else {
                    print("usuario no pudo ser creado. Error #" + webRequest.downloadHandler.text);
                }
            }
        }
    }

    public void VerifyInputs() 
    {
        RegistrarButton.interactable = (Nombre.text.Length >= 8 && Password.text.Length >= 8);
    
    }

}
