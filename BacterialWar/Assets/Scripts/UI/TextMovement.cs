using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;

    private float _fadeSpeed = 0.75f;

    [SerializeField]
    private Vector3 _moveOffset = new Vector3(0, 0, 1);

    [SerializeField]
    private float _maxDistance = 2;

    private Vector3 _realOffset;
    private Vector3 _startPosition;
    private float _startFadeOutDistance;

    private TextMesh _textMesh;

    private void Awake()
    {
        _realOffset = _moveOffset * _speed;
        _startPosition = gameObject.transform.position;
        _textMesh = GetComponent<TextMesh>();
        _startFadeOutDistance = _maxDistance / 2;
    }

    private void Update()
    {
        Move();

        var passedDistance = Vector3.Distance(_startPosition, gameObject.transform.position);

        if (passedDistance > _startFadeOutDistance)
        {
            FadeOut();
        }

        if (passedDistance > _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        gameObject.transform.position += _realOffset * Time.deltaTime;        
    }

    private void FadeOut()
    {
        var oldColor = GetComponent<TextMesh>().color;
        _textMesh.color = new Color(
            oldColor.r, oldColor.g, oldColor.b,
            oldColor.a - _fadeSpeed * Time.deltaTime);
    }
}
