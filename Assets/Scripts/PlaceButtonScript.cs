using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceButtonScript : MonoBehaviour
{
    private Button place_button;
    ready_script ready_button;
    public GameObject text_object;
    Game controller;
    string the_ship;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
        place_button = this.gameObject.GetComponent<Button>();
        place_button.onClick.AddListener(place_ship);
        ready_button = GameObject.Find("ready_button").GetComponent<ready_script>();

    }
    public void get_ship(string ship)
    {
        the_ship = ship;
    }
    public void turn_interactability()
    {
        this.GetComponent<Button>().interactable = true;
    }
    public void turn_visibility_on()
    {
        this.GetComponent<Image>().color = new Vector4(33, 177, 77, 255);
        text_object.SetActive(true);
    }
    void place_ship()
    {
        List<GameObject> tiles_to_be_highlighted = controller.tiles_to_be_highlighted;
        int length = tiles_to_be_highlighted.Count;
        for (int a = 0; a < length; a++)
        {
            tiles_to_be_highlighted[0].GetComponent<Tile>().is_highlited = false;
            tiles_to_be_highlighted[0].GetComponent<Tile>().is_occupied = true;
            tiles_to_be_highlighted[0].GetComponent<Tile>().has_ship = true;
            tiles_to_be_highlighted[0].GetComponent<Tile>().what_ship = the_ship;
            border_creator(tiles_to_be_highlighted[0]);
            tiles_to_be_highlighted.Remove(tiles_to_be_highlighted[0]);
            controller.highlight_everythin();
        }
        if (controller.player == "blue")
        {
            controller.blue_number[the_ship] -= 1;
        }
        else
        {
            controller.red_number[the_ship] -= 1;
        }
        
        controller.number_ships_left -= 1;
        if (controller.number_ships_left == 0)
        {
            ready_button.turn_interactability();
        }
    }
    void border_creator(GameObject tile)
    {
        int x_pos = tile.GetComponent<Tile>().xPos;
        int y_pos = tile.GetComponent<Tile>().yPos;
        int x_pos_to_check = x_pos - 1;
        int y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >=0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        x_pos_to_check = x_pos + 1;
        y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            controller.tiles[y_pos_to_check, x_pos_to_check].GetComponent<Tile>().is_occupied = true;
        }
    }
}
