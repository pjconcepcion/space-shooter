using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private bool _isGamePause = false;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager not found.");
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene("Scenes/Game");
        }

        if (Input.GetKeyDown(KeyCode.Q) && _isGamePause)
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    public void OnGameOver()
    {
        _isGameOver = true;
    }

    public void OnPause()
    {
        _isGamePause = true;
        _uiManager.OnGamePause(true);
        _spawnManager.OnGamePause();
        Enemy[] enemy = GameObject.Find("EnemyContainer").GetComponentsInChildren<Enemy>();
        Powerups[] powerup = GameObject.Find("PowerupContainer").GetComponentsInChildren<Powerups>();
        Laser[] laser = GameObject.Find("LaserContainer").GetComponentsInChildren<Laser>();
        Asteroid[] asteroid = GameObject.Find("AsteroidContainer").GetComponentsInChildren<Asteroid>();

        if (enemy.Length > 0)
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                enemy[i].OnGamePause();
            }
        }
        else 
        {
            Debug.Log("Enemy not found.");
        }

        if (powerup.Length > 0)
        {
            for (int i = 0; i < powerup.Length; i++)
            {
                powerup[i].OnGamePause();
            }
        }
        else 
        {
            Debug.Log("Powerup not found.");
        }

        if (laser.Length > 0)
        {
            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].OnGamePause();
            }
        }
        else
        {
            Debug.Log("Laser not found.");
        }

        if (asteroid.Length > 0)
        {
            for (int i = 0; i < asteroid.Length; i++)
            {
                asteroid[i].OnGamePause();
            }
        }
        else
        {
            Debug.Log("Asteroid not found.");
        } 
    }

    public void OnUnpause()
    {
        _isGamePause = false;
        _uiManager.OnGamePause(false);
        _spawnManager.OnGameUnpause();
        Enemy[] enemy = GameObject.Find("EnemyContainer").GetComponentsInChildren<Enemy>();
        Powerups[] powerup = GameObject.Find("PowerupContainer").GetComponentsInChildren<Powerups>();
        Laser[] laser = GameObject.Find("LaserContainer").GetComponentsInChildren<Laser>();
        Asteroid[] asteroid = GameObject.Find("AsteroidContainer").GetComponentsInChildren<Asteroid>();

        if (enemy.Length > 0)
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                enemy[i].OnGameUpause();
            }
        }
        else 
        {
            Debug.Log("Enemy not found.");
        }    
        
        if (powerup.Length > 0)
        {
            for (int i = 0; i < powerup.Length; i++)
            {
                powerup[i].OnGameUnpause();
            }
        }
        else 
        {
            Debug.Log("Powerup not found.");
        }   

        if (laser.Length > 0)
        {
            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].OnGameUnpause();
            }
        }
        else
        {
            Debug.Log("Laser not found.");
        } 

        if (asteroid.Length > 0)
        {
            for (int i = 0; i < asteroid.Length; i++)
            {
                asteroid[i].OnGameUnpause();
            }
        }
        else
        {
            Debug.Log("Asteroid not found.");
        } 
    }
}
