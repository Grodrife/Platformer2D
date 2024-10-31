using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEdge : MonoBehaviour
{
    private float _fallDamage = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventsHandler.WorldElementsEvents.OnPlayerEntersWorldEdgeHandler(_fallDamage);
            EventsHandler.GameEvents.OnResetLevelHandler();
        }
    }
}
