using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _baloonsPrefabs = new(3);    

    [SerializeField]
    private List<Transform> _spawnPositions = new(9);

    private bool SpawnAllow = true;

    [SerializeField]
    private float _spawnTime = 0.3f;

    private GameController _gameController;

    private BeatSoundBehaviour _beatSoundBehaviour;

    private void Awake()
    {
        _beatSoundBehaviour = FindObjectOfType<BeatSoundBehaviour>();
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        _gameController.IsGameOver.AddListener(GameOver);
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (SpawnAllow)
        {
            int spawnIndex = Random.Range(0, 9);
            int baloonIndex = Random.Range(0, 3);

            GameObject baloon = Instantiate(_baloonsPrefabs[baloonIndex], _spawnPositions[spawnIndex].transform.position, Quaternion.identity, transform);
            baloon.GetComponent<BaloonController>().BaloonBurst.AddListener(BaloonBurst);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private void BaloonBurst(BaloonType baloonType)
    {
        //Debug.Log("Baloon Burst!" + baloonType);

        _gameController.CheckBaloon(baloonType);
        _beatSoundBehaviour.PlayBeatSound();
    }

    private void GameOver()
    {
        SpawnAllow = false;

        gameObject.SetActive(false);
    }
}
