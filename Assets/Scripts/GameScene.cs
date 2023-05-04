using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public TMP_Text main_menu_text;
    TransferScript transfer_script;
    public GameObject message;
    public Sprite miss_mark;
    public Sprite hit_mark;
    public GameObject new_blue_cell;
    public GameObject new_red_cell;
    public List<List<(bool, string)>> blue_tiles = new List<List<(bool, string)>>();
    public List<List<(bool, string)>> red_tiles = new List<List<(bool, string)>>();
    public string turn = "blue";
    public Dictionary<string, int> blue_number = new Dictionary<string, int>()
    {
        {"blue_carrier", 5 },
        {"blue_battleship", 4 },
        {"blue_submarine", 3 },
        {"blue_destroyer", 3 },
        {"blue_patrolboat", 2 }
    };
    public int total_blue_number = 17;
    public Dictionary<string, int> red_number = new Dictionary<string, int>()
    {
        {"red_carrier", 5 },
        {"red_battleship", 4 },
        {"red_submarine", 3 },
        {"red_destroyer", 3 },
        {"red_patrolboat", 2 }
    };
    public int total_red_number = 17;
    public GameObject[,] twod_blue = new GameObject[10, 10];
    public GameObject[,] twod_red = new GameObject[10, 10];
    // Start is called before the first frame update
    void Start()
    {
        transfer_script = this.GetComponent<TransferScript>();
        blue_tiles = transfer_script.transfer_lists.blue_tiles;
        red_tiles = transfer_script.transfer_lists.red_tiles;
        int y_pos = 0;
        int x_pos = 0;
        float x = 1.93f;
        float y = 2.87f;
        int count = 0;
        foreach (List<(bool, string)> tile in blue_tiles)
        {
            GameObject new_blue_tile = Instantiate(new_blue_cell, new Vector3(x, y, 0), Quaternion.identity);
            twod_blue[y_pos, x_pos] = new_blue_tile;
            new_blue_tile.GetComponent<BlueTile>().xPos = x_pos;
            new_blue_tile.GetComponent<BlueTile>().yPos = y_pos;
            new_blue_tile.GetComponent<BlueTile>().what_ship = tile[0].Item2;
            new_blue_tile.GetComponent<BlueTile>().has_ship = tile[0].Item1;
            count += 1;
            x_pos += 1;
            if (count % 10 == 0)
            {
                y_pos += 1;
                x_pos = 0;
                y -= 0.59f;
                x = 1.93f;
            }
            else
            {
                x += 0.59f;
            }

        }

        
        y_pos = 0;
        x_pos = 0;
        x = -7.27f;
        y = 2.87f;
        count = 0;
        foreach (List<(bool, string)> tile in red_tiles)
        {
            GameObject new_red_tile = Instantiate(new_red_cell, new Vector3(x, y, 0), Quaternion.identity);
            twod_red[y_pos, x_pos] = new_red_tile;
            new_red_tile.GetComponent<RedTile>().xPos = x_pos;
            new_red_tile.GetComponent<RedTile>().yPos = y_pos;
            new_red_tile.GetComponent<RedTile>().what_ship = tile[0].Item2;
            new_red_tile.GetComponent<RedTile>().has_ship = tile[0].Item1;
            count += 1;
            x_pos += 1;
            if (count % 10 == 0)
            {
                y_pos += 1;
                x_pos = 0;
                y -= 0.59f;
                x = -7.27f;
            }
            else
            {
                x += 0.59f;
            }

        }
    }


    public void get_blue_tiles(List<List<(bool, string)>> the_2dlist)
    {
        blue_tiles = the_2dlist;
    }
    public void get_red_tiles(List<List<(bool, string)>> the_2dlist)
    {
        red_tiles = the_2dlist;
    }

    public void hit_system(GameObject tile)
    {

        if (turn == "blue")
        {
            if (tile.GetComponent<RedTile>().has_ship)
            {
                if (tile.GetComponent<RedTile>().already_hit)
                {
                    turn = "red";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Hit! (again)";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());

                }
                else
                {
                    tile.GetComponent<RedTile>().already_hit = true;
                    tile.GetComponent<SpriteRenderer>().sprite = hit_mark;
                    tile.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
                    tile.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
                    red_number[tile.GetComponent<RedTile>().what_ship] -= 1;
                    total_red_number -= 1;
                    if (total_red_number == 0)
                    {
                        turn = "game_over";
                        GameObject.Find("game_over_screen").GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
                        TMP_Text who_won = GameObject.Find("player_won").GetComponent<TMP_Text>();
                        who_won.text = "Player 1 won!";
                        who_won.color = new Vector4(255, 255, 255, 255);
                        main_menu_text.color = new Vector4(255, 255, 255, 255);
                        GameObject.Find("to_main_menu").GetComponent<Button>().interactable = true;
                    }
                    if (red_number[tile.GetComponent<RedTile>().what_ship] == 0)
                    {
                        for (int a = 0; a < 10; a++)
                        {
                            for (int b = 0; b < 10; b++)
                            {
                                if (twod_red[a, b].GetComponent<RedTile>().what_ship == tile.GetComponent<RedTile>().what_ship)
                                {
                                    border_creator_red(twod_red[a, b]);
                                }
                            }
                        }
                        if (total_red_number != 0)
                        {
                            message.GetComponent<TMP_Text>().text = "Destroyed!";
                            message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                            StartCoroutine(message_show());
                        }
                        
                    }
                    else
                    {
                        message.GetComponent<TMP_Text>().text = "Hit!";
                        message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                        StartCoroutine(message_show());
                    }
                    
                }
                
            }
            else
            {
                if (tile.GetComponent<RedTile>().already_hit)
                {
                    turn = "red";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Miss! (again)";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());

                }
                else
                {
                    tile.GetComponent<RedTile>().already_hit = true;
                    tile.GetComponent<SpriteRenderer>().sprite = miss_mark;
                    tile.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
                    turn = "red";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Miss!";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());
                }
                
            }
        }
        else
        {
            if (tile.GetComponent<BlueTile>().has_ship)
            {
                if (tile.GetComponent<BlueTile>().already_hit)
                {
                    turn = "blue";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Hit! (again)";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());

                }
                else
                {
                    tile.GetComponent<BlueTile>().already_hit = true;
                    tile.GetComponent<SpriteRenderer>().sprite = hit_mark;
                    tile.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
                    tile.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
                    blue_number[tile.GetComponent<BlueTile>().what_ship] -= 1;
                    total_blue_number -= 1;
                    if (total_blue_number == 0)
                    {
                        turn = "game_over";
                        GameObject.Find("game_over_screen").GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
                        TMP_Text who_won = GameObject.Find("player_won").GetComponent<TMP_Text>();
                        who_won.text = "Player 2 won!";
                        who_won.color = new Vector4(255, 255, 255, 255);
                        main_menu_text.color = new Vector4(255, 255, 255, 255);
                        GameObject.Find("to_main_menu").GetComponent<Button>().interactable = true;
                    }
                    if (blue_number[tile.GetComponent<BlueTile>().what_ship] == 0)
                    {
                        for (int a=0; a< 10; a++)
                        {
                            for (int b=0; b< 10; b++)
                            {
                                if (twod_blue[a, b].GetComponent<BlueTile>().what_ship == tile.GetComponent<BlueTile>().what_ship)
                                {
                                    border_creator_blue(twod_blue[a, b]);
                                }
                            }
                        }
                        if (total_blue_number != 0)
                        {
                            message.GetComponent<TMP_Text>().text = "Destroyed!";
                            message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                            StartCoroutine(message_show());
                        }
                        
                    }
                    else
                    {
                        message.GetComponent<TMP_Text>().text = "Hit!";
                        message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                        StartCoroutine(message_show());
                    }

                }
            }
            else
            {
                if (tile.GetComponent<BlueTile>().already_hit)
                {
                    turn = "blue";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Miss! (again)";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());

                }
                else
                {
                    tile.GetComponent<BlueTile>().already_hit = true;
                    tile.GetComponent<SpriteRenderer>().sprite = miss_mark;
                    tile.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
                    turn = "blue";
                    change_turn_gui(turn);
                    message.GetComponent<TMP_Text>().text = "Miss!";
                    message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 255);
                    StartCoroutine(message_show());
                }
            }
        }
        
    }
    IEnumerator message_show()
    {
        yield return new WaitForSeconds(0.5f);
        message.GetComponent<TMP_Text>().color = new Vector4(255, 255, 255, 0);
    }
    void change_turn_gui(string turn)
    {
        if (turn == "red")
        {
            GameObject gui_blue = GameObject.Find("golden_rectangle_blue");
            GameObject gui_red = GameObject.Find("golden_rectangle_red");
            gui_blue.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 0);
            gui_red.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
        }
        else
        {
            GameObject gui_blue = GameObject.Find("golden_rectangle_blue");
            GameObject gui_red = GameObject.Find("golden_rectangle_red");
            gui_blue.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
            gui_red.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 0);
        }
    }

    void border_creator_blue(GameObject tile)
    {
        int x_pos = tile.GetComponent<BlueTile>().xPos;
        int y_pos = tile.GetComponent<BlueTile>().yPos;
        int x_pos_to_check = x_pos - 1;
        int y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos + 1;
        y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().has_ship)
            {
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<BlueTile>().already_hit = true;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_blue[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
    }
    void border_creator_red(GameObject tile)
    {
        int x_pos = tile.GetComponent<RedTile>().xPos;
        int y_pos = tile.GetComponent<RedTile>().yPos;
        int x_pos_to_check = x_pos - 1;
        int y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos + 1;
        y_pos_to_check = y_pos;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos + 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
        x_pos_to_check = x_pos;
        y_pos_to_check = y_pos - 1;
        if (x_pos_to_check >= 0 && x_pos_to_check < 10 && y_pos_to_check >= 0 && y_pos_to_check < 10)
        {
            if (!twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().has_ship)
            {
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<RedTile>().already_hit = true;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<SpriteRenderer>().sprite = miss_mark;
                twod_red[y_pos_to_check, x_pos_to_check].GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
    }
} 

