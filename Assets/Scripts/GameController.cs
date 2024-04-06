using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    public int GameTime { get => (int)_gameSessionTime; }

    private float _gameSessionTime = 180f;

    [HideInInspector]
    public UnityEvent IsGameOver;

    private BaloonBurstBehaviour _baloonBurstBehaviour;

    private ScoreCounter _scoreCounter;

    public void Awake()
    {
        IsGameOver = new UnityEvent();
        
        _baloonBurstBehaviour = FindObjectOfType<BaloonBurstBehaviour>();
        IsGameOver.AddListener(_baloonBurstBehaviour.GameOver);

        _scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    private void Update()
    {
        _gameSessionTime -= Time.deltaTime;

        if (_gameSessionTime < 0 )
        {
            //Debug.Log("Game over!");

            IsGameOver.Invoke();

            _gameOverPanel.SetActive(true);
        }

        //Debug.Log("Current Baloon " + _baloonBurstBehaviour.TargetBaloon);
    }

    public void CheckBaloon(BaloonType baloonType)
    {
        if (_baloonBurstBehaviour.TargetBaloon == baloonType)
        {
            //Debug.Log("Right Baloon!");
            _scoreCounter.IncreaseScore(10);
        }
        else
        {
            //Debug.Log("Wrong Baloon!");
            _scoreCounter.IncreaseScore(-10);
        }

    }
}
