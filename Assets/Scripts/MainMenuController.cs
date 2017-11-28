using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public Canvas helpCanvas;

    public void StartGame()
    {
        Destroy(GameObject.Find("MenuSafeHouse").gameObject);
        SceneManager.LoadScene("_Scenes/FirstLevel");
    }

    public void WhatToDo()
    {
        helpCanvas.gameObject.SetActive(true);
    }

    public void OkayIGotIt()
    {
        helpCanvas.gameObject.SetActive(false);
    }
}
