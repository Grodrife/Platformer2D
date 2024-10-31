using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float _spriteWidth;
    private float _startXPosition;
    [SerializeField] private float _movementSpeed;
    private float _maxDistance;
    private float _movement;
    private Camera _camera;
    private void OnEnable()
    {
        _camera = FindAnyObjectByType<Camera>();
        _startXPosition = transform.position.x;
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {
        _maxDistance = _camera.transform.position.x * _movementSpeed;
        _movement = _camera.transform.position.x * (1 - _movementSpeed);
        transform.position = new Vector3(_startXPosition + _maxDistance, transform.position.y, transform.position.z);
        if (_movement > _startXPosition + _spriteWidth)
        {
            _startXPosition += _spriteWidth;
        } else if (_movement < _startXPosition - _spriteWidth)
        {
            _startXPosition -= _spriteWidth;
        }
    }
}
