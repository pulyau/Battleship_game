using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private Button play_button;
    // Start is called before the first frame update
    void Start()
    {
        play_button = this.gameObject.GetComponent<Button>();
        play_button.onClick.AddListener(launch_game);
    }

    void launch_game()
    {
        SceneManager.LoadScene("Ship Placement Scene");
    }
}
