using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaloonType
{
    None = -1,
    Red = 0,
    Blue = 1,
    Green = 2
}

public class BaloonBurstBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _targetsIcons = new(3);

    private bool _isActive = false;

    private float _changeCurrentBaloonTime = 10f;

    private int _currentBaloonIndex = 0;

    private bool ChangeBaloonAllow = true;

    [HideInInspector]
    public BaloonType TargetBaloon { get => (BaloonType)_currentBaloonIndex; }

    private void Awake()
    {
        ChangeCurrentBaloon();
    }

    private void Update()
    {
        StartCoroutine(CurrentBaloonChange());
    }

    public void GameOver()
    {
        ChangeBaloonAllow = false;
    }

    private IEnumerator CurrentBaloonChange()
    {
        if (_isActive)
        {
            yield break;
        }

        _isActive = true;

        while(ChangeBaloonAllow)
        {
            yield return new WaitForSeconds(_changeCurrentBaloonTime);
            ChangeCurrentBaloon();
        }
    }

    private void ChangeCurrentBaloon()
    {
        _currentBaloonIndex = Random.Range(0, 3);

        for (int i = 0; i < _targetsIcons.Count; i++)
        {
            if (_currentBaloonIndex == i)
            {
                _targetsIcons[i].SetActive(true);
            }
            else
                _targetsIcons[i].SetActive(false);
        }
    }
}
