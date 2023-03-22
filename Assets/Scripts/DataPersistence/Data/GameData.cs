using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
    
{
    public int currentHealth;
    public int currentwave;
    public int enemiesPerWave;
    public int currentEnemies;
    public int scoreValue;
    public int towerHealth;


    // Start is called before the first frame update
    public GameData()
    {
        this.currentHealth= 100;
        this.currentwave= 0;
        this.scoreValue= 0;
        this.currentEnemies= 3;
        this.enemiesPerWave = 3;
        this.towerHealth = 300; 
    }

}
