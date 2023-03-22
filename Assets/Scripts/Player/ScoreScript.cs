using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour,IDataPersistence
{
    // Start is called before the first frame update
    public static int scoreValue = 0;
    Text score;

    public void LoadData(GameData gameData)
    {
        scoreValue = gameData.scoreValue;
    }

    public void SaveData(ref GameData gameData)
    {
         gameData.scoreValue = scoreValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
    }
}
