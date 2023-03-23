using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    [SerializeField] private AudioSource mainSound;

    // Start is called before the first frame update
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReStart()
    {
        Time.timeScale = 1f;
        string filePath = Path.Combine(Application.persistentDataPath, "gameData");
        try
        {
                File.Delete(filePath);
        }
        catch
        {

        }
        SceneManager.LoadScene("GamePlay");
    }
    public void Save()
    {
        DataPersistencemanager.instance.SaveGame();
        Debug.Log("Game has been saved");
    }
}
