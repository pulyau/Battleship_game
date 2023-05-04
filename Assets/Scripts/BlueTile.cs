using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTile : MonoBehaviour
{
    public bool has_ship = false;
    public string what_ship = "";
    public bool already_hit = false;
    public int xPos;
    public int yPos;

    GameScene game_scene;
    // Start is called before the first frame update
    void Start()
    {
        game_scene = GameObject.Find("GameController").GetComponent<GameScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_scene.turn == "red")
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    game_scene.hit_system(hit.collider.gameObject);
                }
            }
        }
        
    }
}
