using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemHandler : MonoBehaviour
{
    [SerializeField] public static InputSystemHandler s_inputSystemHandler;

    private void Awake()
    {
        if (s_inputSystemHandler == null)
        {
            s_inputSystemHandler = this;
            DontDestroyOnLoad(gameObject);
        } else if (s_inputSystemHandler != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
