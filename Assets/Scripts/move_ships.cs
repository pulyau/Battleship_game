using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ships : MonoBehaviour
{
    private bool once = false;
    private bool yes = false;
    Game controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.tiles_to_be_highlighted.Count > 0)
        {
            if ((Input.GetKey(KeyCode.RightArrow) && controller.rotation == "horizontal") || (Input.GetKey(KeyCode.DownArrow) && controller.rotation == "vertical"))
            { 

                if (once == false)
                {
                    move_right(controller.rotation);
                    
                }
            }
            if ((Input.GetKeyUp(KeyCode.RightArrow) && controller.rotation == "horizontal") || (Input.GetKeyUp(KeyCode.DownArrow) && controller.rotation == "vertical"))
            {
                once = false;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) && controller.rotation == "horizontal") || (Input.GetKey(KeyCode.UpArrow) && controller.rotation == "vertical"))
            {
                if (once == false)
                {
                    move_left(controller.rotation);

                }
            }
            if ((Input.GetKeyUp(KeyCode.LeftArrow) && controller.rotation == "horizontal") || (Input.GetKeyUp(KeyCode.UpArrow) && controller.rotation == "vertical"))
            {
                once = false;
            }
            
            if ((Input.GetKey(KeyCode.DownArrow) && controller.rotation == "horizontal") || (Input.GetKey(KeyCode.RightArrow) && controller.rotation == "vertical"))
            {
                
                if (once == false)
                {
                    move_up_down(1, "down", controller.rotation);

                }
            }
            if ((Input.GetKeyUp(KeyCode.DownArrow) && controller.rotation == "horizontal") || (Input.GetKeyUp(KeyCode.RightArrow) && controller.rotation == "vertical"))
            {
                once = false;
            }



            if ((Input.GetKey(KeyCode.UpArrow) && controller.rotation == "horizontal") || (Input.GetKey(KeyCode.LeftArrow) && controller.rotation == "vertical"))
            {
                if (once == false)
                {
                    move_up_down(-1, "up", controller.rotation);

                }
            }
            if ((Input.GetKeyUp(KeyCode.UpArrow) && controller.rotation == "horizontal") || (Input.GetKeyUp(KeyCode.LeftArrow) && controller.rotation == "vertical"))
            {
                once = false;
            }
        } 
    }

    void move_right(string orientation)
    {
        int length = controller.tiles_to_be_highlighted.Count;
        int x_pos = controller.tiles_to_be_highlighted[length - 1].GetComponent<Tile>().xPos + 1;
        int y_pos = controller.tiles_to_be_highlighted[length - 1].GetComponent<Tile>().yPos;
        if (orientation == "vertical")
        {
            x_pos = controller.tiles_to_be_highlighted[length - 1].GetComponent<Tile>().xPos;
            y_pos = controller.tiles_to_be_highlighted[length - 1].GetComponent<Tile>().yPos + 1;
        }

        once = true;
        
        if (x_pos < 10 && y_pos < 10)
        {
            if (!controller.tiles[y_pos, x_pos].GetComponent<Tile>().is_occupied)
            {
                controller.clear_one_cell(controller.tiles_to_be_highlighted[0]);
                controller.tiles_to_be_highlighted.Remove(controller.tiles_to_be_highlighted[0]);

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i == y_pos && j == x_pos)
                        {
                            yes = true;
                            GameObject cell_to_add = controller.tiles[i, j];
                            controller.tiles_to_be_highlighted.Add(cell_to_add);
                            break;
                        }
                    }
                    if (yes)
                    {
                        yes = false;
                        break;
                    }

                }
                controller.highlight(controller.tiles_to_be_highlighted);
            }
            else
            {
                List<GameObject> ready_to_go = new List<GameObject> { };
                if (orientation == "vertical")
                {
                    
                    for (int y = y_pos; y < 10; y++)
                    {
                        if (controller.tiles[y, x_pos].GetComponent<Tile>().is_occupied)
                        {
                            ready_to_go.Clear();
                        }
                        else
                        {
                            ready_to_go.Add(controller.tiles[y, x_pos]);
                        }

                        if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                        {
                            controller.clear_highlight();
                            controller.tiles_to_be_highlighted.Clear();
                            controller.tiles_to_be_highlighted = ready_to_go;
                            controller.highlight(controller.tiles_to_be_highlighted);
                            break;
                        }
                    }
                }
                else
                {
                    for (int x = x_pos; x < 10; x++)
                    {
                        if (controller.tiles[y_pos, x].GetComponent<Tile>().is_occupied)
                        {
                            ready_to_go.Clear();
                        }
                        else
                        {
                            ready_to_go.Add(controller.tiles[y_pos, x]);
                        }

                        if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                        {
                            controller.clear_highlight();
                            controller.tiles_to_be_highlighted.Clear();
                            controller.tiles_to_be_highlighted = ready_to_go;
                            controller.highlight(controller.tiles_to_be_highlighted);
                            break;
                        }
                    }
                }
                
            }

        }
    }

    void move_left(string orientation)
    {
        int length = controller.tiles_to_be_highlighted.Count;
        int x_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().xPos - 1;
        int y_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().yPos;
        if (orientation == "vertical")
        {
            x_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().xPos;
            y_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().yPos - 1;
        }

        once = true;
        if (x_pos < 10 && y_pos < 10 && x_pos >= 0 && y_pos >= 0)
        {
            if (!controller.tiles[y_pos, x_pos].GetComponent<Tile>().is_occupied)
            {
                
                controller.clear_one_cell(controller.tiles_to_be_highlighted[length - 1]);
                controller.tiles_to_be_highlighted.Remove(controller.tiles_to_be_highlighted[length - 1]);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i == y_pos && j == x_pos)
                        {
                            
                            yes = true;
                            GameObject cell_to_add = controller.tiles[i, j];
                            controller.tiles_to_be_highlighted.Insert(0, cell_to_add);
                            break;
                        }
                    }
                    if (yes)
                    {
                        yes = false;
                        break;
                    }

                }
                controller.highlight(controller.tiles_to_be_highlighted);
            }
            else
            {
                List<GameObject> ready_to_go = new List<GameObject> { };
                if (orientation == "vertical")
                {
                    for (int y = y_pos; y >= 0; y--)
                    {
                        if (controller.tiles[y, x_pos].GetComponent<Tile>().is_occupied)
                        {
                            ready_to_go.Clear();
                        }
                        else
                        {
                            ready_to_go.Insert(0, controller.tiles[y, x_pos]);
                        }

                        if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                        {
                            controller.clear_highlight();
                            controller.tiles_to_be_highlighted.Clear();
                            controller.tiles_to_be_highlighted = ready_to_go;
                            controller.highlight(controller.tiles_to_be_highlighted);
                            break;
                        }
                    }
                }
                else
                {
                    for (int x = x_pos; x >= 0; x--)
                    {
                        if (controller.tiles[y_pos, x].GetComponent<Tile>().is_occupied)
                        {
                            ready_to_go.Clear();
                        }
                        else
                        {
                            ready_to_go.Insert(0, controller.tiles[y_pos, x]);
                        }

                        if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                        {
                            controller.clear_highlight();
                            controller.tiles_to_be_highlighted.Clear();
                            controller.tiles_to_be_highlighted = ready_to_go;
                            controller.highlight(controller.tiles_to_be_highlighted);
                            break;
                        }
                    }
                }
                
            }
                
        }
        
        
    }
    void move_up_down(int kuda, string up_or_down, string orientation)
    {
        int length = controller.tiles_to_be_highlighted.Count;
        int x_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().xPos;
        int y_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().yPos + kuda;
        if (orientation == "vertical")
        {
            x_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().xPos + kuda;
            y_pos = controller.tiles_to_be_highlighted[0].GetComponent<Tile>().yPos;
        }
        once = true;
        bool can_place = true;
        int temp_x = x_pos;
        int temp_y = y_pos;
        if (x_pos < 10 && y_pos < 10 && x_pos >= 0 && y_pos >= 0)
        {
            for (int a = 0; a < controller.tiles_to_be_highlighted.Count; a++)
            {
                if (controller.tiles[temp_y, temp_x].GetComponent<Tile>().is_occupied)
                {
                    can_place = false;
                    break;
                }
                else
                {
                    if (orientation == "horizontal")
                    {
                        temp_x += 1;
                    }
                    else
                    {
                        temp_y += 1;
                    }
                }
            }
            if (can_place)
            {
                for (int a = 0; a < length; a++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (i == y_pos && j == x_pos)
                            {

                                yes = true;
                                GameObject cell_to_add = controller.tiles[i, j];
                                controller.tiles_to_be_highlighted.Add(cell_to_add);
                                if (orientation == "horizontal")
                                {
                                    x_pos += 1;
                                }
                                else
                                {
                                    y_pos += 1;
                                }
                                break;
                            }

                        }
                        if (yes)
                        {
                            yes = false;
                            break;
                        }


                    }
                }
                for (int a = 0; a < length; a++)
                {
                    controller.clear_one_cell(controller.tiles_to_be_highlighted[0]);
                    controller.tiles_to_be_highlighted.Remove(controller.tiles_to_be_highlighted[0]);
                }
                controller.highlight(controller.tiles_to_be_highlighted);
            }
            else
            {
                bool end = false;
                List<GameObject> ready_to_go = new List<GameObject> { };
                if (orientation == "horizontal")
                {
                    if (up_or_down == "down")
                    {
                        for (int y = y_pos; y < 10; y++)
                        {
                            for (int x = x_pos; x < controller.tiles_to_be_highlighted.Count + x_pos; x++)
                            {
                                if (controller.tiles[y, x].GetComponent<Tile>().is_occupied)
                                {
                                    ready_to_go.Clear();
                                    break;
                                }
                                else
                                {
                                    ready_to_go.Add(controller.tiles[y, x]);
                                }
                                if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                                {
                                    end = true;
                                    controller.clear_highlight();
                                    controller.tiles_to_be_highlighted.Clear();
                                    controller.tiles_to_be_highlighted = ready_to_go;
                                    controller.highlight(controller.tiles_to_be_highlighted);
                                    break;
                                }
                            }
                            if (end)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int y = y_pos; y >= 0; y--)
                        {
                            for (int x = x_pos; x < controller.tiles_to_be_highlighted.Count + x_pos; x++)
                            {
                                if (controller.tiles[y, x].GetComponent<Tile>().is_occupied)
                                {
                                    ready_to_go.Clear();
                                    break;
                                }
                                else
                                {
                                    ready_to_go.Add(controller.tiles[y, x]);
                                }
                                if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                                {
                                    end = true;
                                    controller.clear_highlight();
                                    controller.tiles_to_be_highlighted.Clear();
                                    controller.tiles_to_be_highlighted = ready_to_go;
                                    controller.highlight(controller.tiles_to_be_highlighted);
                                    break;
                                }
                            }
                            if (end)
                            {
                                break;
                            }
                        }
                    }


                }
                else
                {
                    if (up_or_down == "down")
                    {
                        for (int x = x_pos; x < 10; x++)
                        {
                            for (int y = y_pos; y < controller.tiles_to_be_highlighted.Count + y_pos; y++)
                            {
                                if (controller.tiles[y, x].GetComponent<Tile>().is_occupied)
                                {
                                    ready_to_go.Clear();
                                    break;
                                }
                                else
                                {
                                    ready_to_go.Add(controller.tiles[y, x]);
                                }
                                if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                                {
                                    end = true;
                                    controller.clear_highlight();
                                    controller.tiles_to_be_highlighted.Clear();
                                    controller.tiles_to_be_highlighted = ready_to_go;
                                    controller.highlight(controller.tiles_to_be_highlighted);
                                    break;
                                }
                            }
                            if (end)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int x = x_pos; x >= 0; x--)
                        {
                            for (int y = y_pos; y < controller.tiles_to_be_highlighted.Count + y_pos; y++)
                            {
                                if (controller.tiles[y, x].GetComponent<Tile>().is_occupied)
                                {
                                    ready_to_go.Clear();
                                    break;
                                }
                                else
                                {
                                    ready_to_go.Add(controller.tiles[y, x]);
                                }
                                if (ready_to_go.Count == controller.tiles_to_be_highlighted.Count)
                                {
                                    end = true;
                                    controller.clear_highlight();
                                    controller.tiles_to_be_highlighted.Clear();
                                    controller.tiles_to_be_highlighted = ready_to_go;
                                    controller.highlight(controller.tiles_to_be_highlighted);
                                    break;
                                }
                            }
                            if (end)
                            {
                                break;
                            }
                        }
                    }
                }
                
            }
            

        }
        
    }
 
}
