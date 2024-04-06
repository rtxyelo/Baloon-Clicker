using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaloonController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Button _button;

    private float _duration = 10f;

    private float BaloonDuration { get => _duration; set => _duration = value; }

    private BaloonType _type;

    [HideInInspector]
    public UnityEvent<BaloonType> BaloonBurst;

    private void Awake()
    {
        _type = BaloonTagToType(gameObject.tag);

        //_duration = Random.Range(15f, 25f);
        _duration = 2.2f;

        BaloonBurst = new UnityEvent<BaloonType>();

        //_button.onClick.AddListener(() => 
        //{
        //    BaloonBurst.Invoke(_type);
        //    Destroy(gameObject, 0.05f);
        //});
    }

    private void OnMouseDown()
    {
        BaloonBurst.Invoke(_type);
        Destroy(gameObject, 0.05f);
    }

    private void Start()
    {
        //_rb.AddForce(Vector2.up * _duration);
        _rb.velocity = Vector2.up * _duration;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private BaloonType BaloonTagToType(string tag)
    {
        switch (tag)
        {
            case "BaloonRed":
                return BaloonType.Red;

            case "BaloonBlue":
                return BaloonType.Blue;

            case "BaloonGreen":
                return BaloonType.Green;

            default:
                return BaloonType.None; 
        }
    }
}
