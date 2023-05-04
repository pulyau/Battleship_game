using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public bool first_time = true;
    private Button button;
    private GameObject tip_gui;
    PlaceButtonScript place_button;
    RotateButtonScript rotate_button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        tip_gui = GameObject.Find("use_arrows_ip");
        button.onClick.AddListener(closeTab);
        place_button = GameObject.Find("place_button").GetComponent<PlaceButtonScript>();
        rotate_button = GameObject.Find("rotate_button").GetComponent<RotateButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void closeTab()
    {
        button.interactable = false;
        this.gameObject.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
        tip_gui.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 0); ;
        Destroy(GameObject.Find("place_button_ghost"));
        place_button.turn_interactability();
        rotate_button.turn_interactability();

    }
}
