using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField]
    private GameObject initialMenu;
    [SerializeField]
    private GameObject tutorial;
    
    public void GoToTutorial()
    {
        initialMenu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }

}
