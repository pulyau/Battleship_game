using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private Button reset_button;
    Game controller;
    ready_script ready_button;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
        reset_button = this.gameObject.GetComponent<Button>();
        reset_button.onClick.AddListener(reset_board);
        ready_button = GameObject.Find("ready_button").GetComponent<ready_script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void reset_board()
    {
        controller.blue_number = new Dictionary<string, int>()
    {
        {"blue_carrier", 1 },
        {"blue_battleship", 1 },
        {"blue_submarine", 1 },
        {"blue_destroyer", 1 },
        {"blue_patrolboat", 1 }
    };
        controller.red_number = new Dictionary<string, int>()
    {
        {"red_carrier", 1 },
        {"red_battleship", 1 },
        {"red_submarine", 1 },
        {"red_destroyer", 1 },
        {"red_patrolboat", 1 }
    };
        controller.number_ships_left = 5;
        ready_button.turn_off_interactability();

        for (int i=0; i < 10; i++)
        {
            for (int j=0; j < 10; j++)
            {
                GameObject tile = controller.tiles[i, j];
                tile.GetComponent<Tile>().is_highlited = false;
                tile.GetComponent<Tile>().is_occupied = false;
                tile.GetComponent<Tile>().has_ship = false;
                tile.GetComponent<Tile>().what_ship = "";
                tile.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
            }
        }
        controller.tiles_to_be_highlighted.Clear();
    }
}
