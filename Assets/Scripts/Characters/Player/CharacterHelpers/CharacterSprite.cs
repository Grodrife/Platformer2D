using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector3 _scale;

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _scale = transform.localScale;
        EventsHandler.PlayerEvents.OnPlayerChangesDirection += ChangeSpriteDirection;
    }
    private void OnDestroy()
    {
        EventsHandler.PlayerEvents.OnPlayerChangesDirection -= ChangeSpriteDirection;
    }
    private void ChangeSpriteDirection()
    {
        transform.localScale = new Vector3(_scale.x * -1, _scale.y, _scale.z);
        _scale = transform.localScale;
    }
}
