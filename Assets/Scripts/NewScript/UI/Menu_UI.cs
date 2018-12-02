using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_UI : MonoBehaviour
{
    public GameObject setActive_Buttons;
    public GameObject setting_UI;
    public GameObject Menu;
    private bool setActive;
    private bool isMenu;
    private Scene scene;


    void Start()
    {
        Cursor.visible = true;
        setActive = false;
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Menu")
        {
            isMenu = true;
        }
        else
        {
            Cursor.visible = false;
            isMenu = false;
        }
    }
    void Update()
    {
        if (!isMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Menu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
    }
    public void Resume_Button()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void Play_Button()
    {
        SceneManager.LoadScene(isMenu ? "main" : "Menu");
    }
    public void Exit_Button()
    {
        Application.Quit();
    }
    public void Settings_Button()
    {
        Debug.Log(setActive);
        //Debug.Log(obj);
        setActive_Buttons.SetActive(setActive);
        setActive = !setActive;
        //Debug.Log(obj);
        setting_UI.SetActive(setActive);
        //SkipButton(setActive ? setting_UI : setActive_Buttons_0);
    }
    private void SkipButton(GameObject[] array_GameObject)
    {
        //for(int i=0;i<array_GameObject.Length; i++)
        //{
        //    Debug.Log(array_GameObject[i]);
        //    array_GameObject[i].SetActive(false);
        //}
        foreach (GameObject obj in array_GameObject)
        {
            Debug.Log(obj);
            obj.SetActive(false);
        }
    }
}
