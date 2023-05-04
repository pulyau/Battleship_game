using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateButtonScript : MonoBehaviour
{
    private Button rotate_button;
    Game controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
        rotate_button = this.gameObject.GetComponent<Button>();
        rotate_button.onClick.AddListener(rotate_ship);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turn_interactability()
    {
        this.GetComponent<Button>().interactable = true;
    }
    void rotate_ship()
    {
        List<GameObject> tiles_to_be_highlighted = controller.tiles_to_be_highlighted;
        if (is_possible(tiles_to_be_highlighted))
        {
            int length = tiles_to_be_highlighted.Count;
            int x_pos = tiles_to_be_highlighted[0].GetComponent<Tile>().xPos;
            int y_pos = tiles_to_be_highlighted[0].GetComponent<Tile>().yPos;
            for (int x = 0; x < length; x++)
            {
                tiles_to_be_highlighted.Add(controller.tiles[y_pos, x_pos]);
                if (controller.rotation == "horizontal")
                {
                    y_pos += 1;
                }
                else
                {
                    x_pos += 1;
                }
            }
            for (int x = 0; x < length; x++)
            {
                controller.clear_one_cell(tiles_to_be_highlighted[0]);
                controller.tiles_to_be_highlighted.Remove(tiles_to_be_highlighted[0]);
            }
            controller.highlight(tiles_to_be_highlighted);
            if (controller.rotation == "horizontal")
            {
                controller.rotation = "vertical";
            }
            else
            {
                controller.rotation = "horizontal";
            }
        }
    }
    bool is_possible(List<GameObject> tiles_to_be_highlighted)
    {
        if (tiles_to_be_highlighted.Count < 1)
        {
            return false;
        }
        int x_pos = tiles_to_be_highlighted[0].GetComponent<Tile>().xPos;
        int y_pos = tiles_to_be_highlighted[0].GetComponent<Tile>().yPos;
        for (int x = 0; x < tiles_to_be_highlighted.Count; x++) {
            if (x_pos < 0 || x_pos >= 10 || y_pos <0 || y_pos >= 10)
            {
                return false;
            }
            if (controller.tiles[y_pos, x_pos].GetComponent<Tile>().is_occupied)
            {
                return false;
            }
            if (controller.rotation == "horizontal") {
                y_pos += 1;
            }
            else {
                x_pos += 1;
            }
            
        }
        return true;
    }
}
