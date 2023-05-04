using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quit_button;
    // Start is called before the first frame update
    void Start()
    {
        quit_button = GameObject.Find("quit_button").GetComponent<Button>();
        quit_button.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
