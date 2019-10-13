using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _restartGameText;

    [SerializeField]
    private Text _pauseGameText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _liveImage;

    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    private int _totalScore = 0;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();       

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager not found.");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }

        _scoreText.text = "Score: " + _totalScore;
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _pauseGameText.gameObject.SetActive(false);
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(1.0f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GameOver()
    {
        _gameManager.OnGameOver();
        _restartGameText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverRoutine());
    }

    public void UpdateScore(int score)
    {
        _totalScore += score;
        _scoreText.text = "Score: " + _totalScore;
    }

    public void UpdateLives(int currentLive)
    {
        _liveImage.sprite = _liveSprites[currentLive];
        
        if (currentLive <=0)
        {
            GameOver();
        }
    }

    public void OnGamePause(bool isPause)
    {
        _pauseGameText.gameObject.SetActive(isPause);
    }
}
