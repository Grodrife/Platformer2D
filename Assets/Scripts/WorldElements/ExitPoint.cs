using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] private string _sceneToLoadName;
    [SerializeField] private string _spawnPointString;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player enter");
            EventsHandler.WorldElementsEvents.OnPlayerEntersExitPointHandler(_sceneToLoadName, _spawnPointString);
        }
    }
}
