using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class LoadSceneOnClick : MonoBehaviour
{

    public void loadArscene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {

        //Quit the application
        Application.Quit();

    }

}