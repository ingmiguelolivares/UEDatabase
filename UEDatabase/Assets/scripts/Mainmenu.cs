using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public Text playerDisplay;

    private void Start()
    {
        if (DBManager.logeado) playerDisplay.text = "Player: " + DBManager.username;
                
    }

    public void Registrar() {
        SceneManager.LoadScene(3);
    
    }

    public void Login()
    {
        SceneManager.LoadScene(4);

    }

    public void Jugar()
    {
        SceneManager.LoadScene(5);

    }
}
