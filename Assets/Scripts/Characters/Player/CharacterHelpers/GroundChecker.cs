using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            EventsHandler.PlayerEvents.OnPlayerNotGroundedHandler();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            EventsHandler.PlayerEvents.OnPlayerGroundedHandler();
        }
    }
}
