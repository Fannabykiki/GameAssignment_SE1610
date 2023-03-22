using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    private string dataDirPath = "";
    private string dataFileName = "gameData";

    // Start is called before the first frame update
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale= 0f;
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReStart()
    {
        Time.timeScale = 1f;
        string fullPath = Path.Combine(Application.persistentDataPath, dataFileName);
        if (File.Exists(fullPath))
        {
            try
            {
               File.Delete(fullPath);
            }
            catch
            {
                Debug.LogError("Errow when trying load data" + fullPath);
            }
        }
        SceneManager.LoadScene("GamePlay");
    }
   
    public void Save()
    {
        DataPersistencemanager.instance.SaveGame();
    }
}
