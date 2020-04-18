using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class dBselector : MonoBehaviour
{

    public void externaldB() {
        SceneManager.LoadScene(2);
    }

    public void internaldB()
    {
        SceneManager.LoadScene(1);
    }

}
