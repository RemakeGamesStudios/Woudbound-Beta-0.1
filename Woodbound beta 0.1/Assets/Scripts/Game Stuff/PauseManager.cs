using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    private bool isUsingInventory;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject amoryPanel;
    public GameObject skillPanel;
    public bool usingPausePanel;
    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isUsingInventory = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        amoryPanel.SetActive(false);
        usingPausePanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
        if (Input.GetButtonDown("inventory"))
        {
            if (usingPausePanel)
            {
                SwitchPanels();
            }
            else
            {
                inventoryPanel.SetActive(true);
            }
            OpenInventory();
        }

    }

    private void OpenInventory()
    {
        isUsingInventory = !isUsingInventory;
        if (isUsingInventory)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            inventoryPanel.SetActive(false);
            amoryPanel.SetActive(false);
            skillPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;;
        }
        else
        {
            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            skillPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if(usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

    public void SwitchToAmoryPanel()
    {
        inventoryPanel.SetActive(false);
        amoryPanel.SetActive(true);
        skillPanel.SetActive(false);
    }

    public void SwitchToSkillPanel()
    {
        inventoryPanel.SetActive(false);
        amoryPanel.SetActive(false);
        skillPanel.SetActive(true);
    }

    public void SwitchToInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        amoryPanel.SetActive(false);
        skillPanel.SetActive(false);
    }

}
