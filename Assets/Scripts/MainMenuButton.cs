using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private Button menu_button;
    // Start is called before the first frame update
    void Start()
    {
        menu_button = this.gameObject.GetComponent<Button>();
        menu_button.onClick.AddListener(go_to_main_menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void go_to_main_menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
