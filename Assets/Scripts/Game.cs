using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject ship;
    public GameObject new_ship;
    public GameObject new_cell;

    private GameObject [,] tiles = new GameObject[10, 10];
    private string[] blue_ships = new string[] { "blue_carrier", "blue_battleship", "blue_submarine", "blue_destroyer", "blue_patrolboat" };

    // Start is called before the first frame update
    void Start()
    {
        float y = 3;
        for (int i=0; i < blue_ships.Length; i++)
        {
            Debug.Log(blue_ships[i]);
            new_ship = Instantiate(ship, new Vector3(-7, y, -1), Quaternion.identity);
            new_ship.name = blue_ships[i];
            new_ship.GetComponent<Ship>().Activate();
            y -= 2;
        }


        float  x = -3.34f;
        y = 3.47f;
        for (int i=0; i < 10; i ++)
        {
            for (int j = 0; j < 10; j++)
            {
                
                GameObject new_tile = Instantiate(new_cell, new Vector3(x, y, -1), Quaternion.identity);
                tiles[i, j] = new_tile;
                x += 0.812f;
            }

            x = -3.34f;
            y -= 0.81f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
