using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Tile tile_script;
    public GameObject ship;
    public GameObject new_ship;
    public GameObject new_cell;
    public GameObject button;
    public string rotation = "horizontal";

    public GameObject[,] tiles = new GameObject[10, 10];
    public List<GameObject> tiles_to_be_highlighted = new List<GameObject> { };
    private string[] blue_ships = new string[] { "blue_carrier", "blue_battleship", "blue_submarine", "blue_destroyer", "blue_patrolboat" };
    private string[] red_ships = new string[] { "red_carrier", "red_battleship", "red_submarine", "red_destroyer", "red_patrolboat" };
    private Dictionary<string, int> ship_size = new Dictionary<string, int>()
    {
        {"blue_carrier", 5 }, {"red_carrier", 5},
        {"blue_battleship", 4 }, {"red_battleship", 4},
        {"blue_submarine", 3 }, {"red_submarine", 3},
        {"blue_destroyer", 3 }, {"red_destroyer", 3},
        {"blue_patrolboat", 2 }, {"red_patrolboat", 2 }
    };
    public Dictionary<string, int> blue_number = new Dictionary<string, int>()
    {
        {"blue_carrier", 1 },
        {"blue_battleship", 1 },
        {"blue_submarine", 1 },
        {"blue_destroyer", 1 },
        {"blue_patrolboat", 1 }
    };
    public Dictionary<string, int> red_number = new Dictionary<string, int>()
    {
        {"red_carrier", 1 },
        {"red_battleship", 1 },
        {"red_submarine", 1 },
        {"red_destroyer", 1 },
        {"red_patrolboat", 1 }
    };
    public int number_ships_left = 5;
    public bool already_highlited = false;
    public string player = "blue";

    // Start is called before the first frame update
    void Start()
    {
        generate_blue_ships();

        float x = -3.34f;
        float y = 3.47f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                GameObject new_tile = Instantiate(new_cell, new Vector3(x, y, -1), Quaternion.identity);
                tiles[i, j] = new_tile;
                tiles[i, j].GetComponent<Tile>().yPos = i;
                tiles[i, j].GetComponent<Tile>().xPos = j;
                x += 0.812f;
            }

            x = -3.34f;
            y -= 0.81f;
        }

    }
    void generate_blue_ships()
    {
        float y = 3;
        for (int i = 0; i < blue_ships.Length; i++)
        {
            new_ship = Instantiate(ship, new Vector3(-7, y, -1), Quaternion.identity);
            new_ship.name = blue_ships[i];
            new_ship.GetComponent<Ship>().Activate();
            y -= 2;
        }

    }
    public void change_to_red_ships()
    {
        foreach (string name in blue_ships)
        {
            GameObject blue_s = GameObject.Find(name);
            Destroy(blue_s);
        }
        float y = 3;
        for (int i = 0; i < red_ships.Length; i++)
        {
            new_ship = Instantiate(ship, new Vector3(-7, y, -1), Quaternion.identity);
            new_ship.name = red_ships[i];
            new_ship.GetComponent<Ship>().Activate();
            y -= 2;
        }
    }
    
    public void show_tip()
    {
        GameObject tip_panel = GameObject.Find("use_arrows_ip");
        tip_panel.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
        button.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        button.GetComponent<Button>().interactable = true;
    }

    public void Highlight_cells(string ship)
    {
        tiles_to_be_highlighted.Clear();
        int size = ship_size[ship];
        int ready_to_highlight = 0;

        for (int i = 0; i < 10; i++)
        {
            ready_to_highlight = 0;
            tiles_to_be_highlighted.Clear();
            for (int j = 0; j < 10; j++)
            {
                if (tiles[i, j].GetComponent<Tile>().is_occupied != false)
                {
                    ready_to_highlight = 0;
                    tiles_to_be_highlighted.Clear();
                }
                else
                {
                    ready_to_highlight += 1;
                    tiles_to_be_highlighted.Add(tiles[i, j]);
                }

                if (ready_to_highlight == size)
                {
                    highlight(tiles_to_be_highlighted);
                    break;
                }
            }
            if (ready_to_highlight == size)
            {
                break;
            }


        }
        Debug.Log(tiles_to_be_highlighted.Count);
    }

    public void clear_highlight()
    {
        foreach (GameObject cell in tiles_to_be_highlighted)
        {
            cell.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
            cell.GetComponent<Tile>().is_highlited = false;
        }
    }
    public void highlight(List<GameObject> tiles_to_highlist)
    {
        foreach (GameObject t in tiles_to_highlist)
        {
            t.GetComponent<SpriteRenderer>().color = new Vector4(0, 255, 0, 255);
            t.GetComponent<Tile>().is_highlited = true;

        }
    }
    public void highlight_everythin()
    {
        for (int i=0; i < 10; i++)
        {
            for (int j=0; j< 10; j++)
            {
                if (tiles[i, j].GetComponent<Tile>().is_occupied)
                {
                    if (tiles[i, j].GetComponent<Tile>().has_ship)
                    {
                        tiles[i, j].GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 255, 255);
                    }
                    else
                    {
                        tiles[i, j].GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 255);
                    }
                }
                else if (tiles[i, j].GetComponent<Tile>().is_highlited)
                {
                    tiles[i, j].GetComponent<SpriteRenderer>().color = new Vector4(0, 255, 0, 255);
                }
            }
        }
    }
    public void clear_one_cell(GameObject cell)
    {
        cell.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
        cell.GetComponent<Tile>().is_highlited = false;
    }
 
}
