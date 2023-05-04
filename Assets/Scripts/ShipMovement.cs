using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    Game controller;
    ButtonScript button;
    PlaceButtonScript place_button;
    

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
        button = GameObject.Find("okay_button").GetComponent<ButtonScript>();
        place_button = GameObject.Find("place_button").GetComponent<PlaceButtonScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)

            {
                if (controller.player == "blue")
                {
                    if (controller.blue_number[hit.collider.gameObject.name] > 0)
                    {
                        place_button.get_ship(hit.collider.gameObject.name);
                        controller.rotation = "horizontal";
                        if (controller.already_highlited == true)
                        {
                            controller.clear_highlight();
                        }
                        controller.Highlight_cells(hit.collider.gameObject.name);
                        controller.already_highlited = true;

                        if (button.first_time)
                        {
                            controller.show_tip();
                        }
                        button.first_time = false;
                    }
                }
                else
                {
                    if (controller.red_number[hit.collider.gameObject.name] > 0)
                    {
                        place_button.get_ship(hit.collider.gameObject.name);
                        controller.rotation = "horizontal";
                        if (controller.already_highlited == true)
                        {
                            controller.clear_highlight();
                        }
                        controller.Highlight_cells(hit.collider.gameObject.name);
                        controller.already_highlited = true;

                        if (button.first_time)
                        {
                            controller.show_tip();
                        }
                        button.first_time = false;
                    }
                }
                
                
            }
        }

    }
}
