using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ready_script : MonoBehaviour
{
    private Button ready_button;
    Game controller;
    public TMP_Text player_text;
    ButtonScript okay_button;
    public List<List<(bool, string)>> blue_tiles = new List<List<(bool, string)>>();
    public List<List<(bool, string)>> red_tiles = new List<List<(bool, string)>>();
    TransferScript transfer_script;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
        transfer_script = GameObject.Find("GameController").GetComponent<TransferScript>();
        ready_button = this.gameObject.GetComponent<Button>();
        ready_button.onClick.AddListener(ready_go);
        okay_button = GameObject.Find("okay_button").GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turn_interactability()
    {
        ready_button.interactable = true;
    }
    public void turn_off_interactability()
    {
        ready_button.interactable = false;
    }

    void ready_go()
    {
        if (controller.player == "blue")
        {
            turn_off_interactability();
            player_text.text = "player 2";
            controller.change_to_red_ships();
            controller.player = "red";
            controller.number_ships_left = 5;
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject tile = controller.tiles[i, j];
                    if (tile.GetComponent<Tile>().has_ship)
                    {
                        blue_tiles.Add(new List<(bool, string)>());
                        blue_tiles[count].Add((true, tile.GetComponent<Tile>().what_ship));
                        count += 1;
                    }
                    else
                    {
                        blue_tiles.Add(new List<(bool, string)>());
                        blue_tiles[count].Add((false, ""));
                        count += 1;
                    }
                    tile.GetComponent<Tile>().is_highlited = false;
                    tile.GetComponent<Tile>().is_occupied = false;
                    tile.GetComponent<Tile>().has_ship = false;
                    tile.GetComponent<Tile>().what_ship = "";
                    tile.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
                }
            }
            controller.tiles_to_be_highlighted.Clear();
            okay_button.first_time = true;
            transfer_script.get_blue_tiles(blue_tiles);
        }
        else
        {
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject tile = controller.tiles[i, j];
                    if (tile.GetComponent<Tile>().has_ship)
                    {
                        red_tiles.Add(new List<(bool, string)>());
                        red_tiles[count].Add((true, tile.GetComponent<Tile>().what_ship));
                        count += 1;
                    }
                    else
                    {
                        red_tiles.Add(new List<(bool, string)>());
                        red_tiles[count].Add((false, ""));
                        count += 1;
                    }
                }
            }
            transfer_script.get_red_tiles(red_tiles);
            SceneManager.LoadScene("GameScene");
        }

        
    }
}
