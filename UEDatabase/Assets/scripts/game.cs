using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class game : MonoBehaviour
{
    public Text Nombre, Puntaje, Nivel;



    public void CallLogin()
    {
        StartCoroutine(GetRequest("http://192.168.1.120:8888/sqlconnect/setvalues.php"));

    }

    private void Update()
    {
        Nombre.text = "Usuario: " + DBManager.username;
        Puntaje.text = "Puntaje: " + DBManager.score;
        Nivel.text = "Nivel: " + DBManager.level;

    }

    public void ScoreChange() {
        DBManager.score += 1;
    }

    public void LevelChange()
    {
        DBManager.level += 1;
    }

    IEnumerator GetRequest(string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("level", DBManager.username);
        form.AddField("level", DBManager.level);
        form.AddField("score", DBManager.score);
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

                if (webRequest.downloadHandler.text == "0")
                {
                   
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);

                }
                else
                {
                    print("no se pudo guardar los datos. Error #" + webRequest.downloadHandler.text);
                }
            }
        }
    }

}
