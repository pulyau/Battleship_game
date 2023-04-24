using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject controller;
    public Sprite blue_battleship;
    public Sprite blue_carrier;
    public Sprite blue_destroyer;
    public Sprite blue_submarine;
    public Sprite blue_patrolboat;

    public Sprite red_battleship;
    public Sprite red_carrier;
    public Sprite red_destroyer;
    public Sprite red_submarine;
    public Sprite red_patrolboat;


    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        switch (this.name)
        {
            case "blue_battleship": this.GetComponent<SpriteRenderer>().sprite = blue_battleship;
                this.transform.localScale = new Vector3(4f, 4f, 1);
                this.transform.position = new Vector3(-7.64f, 0.58f, -1);
                break;
            case "blue_carrier": this.GetComponent<SpriteRenderer>().sprite = blue_carrier;
                this.transform.localScale = new Vector3(5.5f, 4.5f, 1);
                this.transform.position = new Vector3(-7.64f, 2.5f, -1);
                break;
            case "blue_destroyer": this.GetComponent<SpriteRenderer>().sprite = blue_destroyer;
                this.transform.localScale = new Vector3(3f, 3f, 1);
                this.transform.position = new Vector3(-7.91f, -2.36f, -1);
                break;
            case "blue_submarine": this.GetComponent<SpriteRenderer>().sprite = blue_submarine;
                this.transform.localScale = new Vector3(3.5f, 4f, 1);
                this.transform.position = new Vector3(-7.61f, -1f, -1);
                break;
            case "blue_patrolboat": this.GetComponent<SpriteRenderer>().sprite = blue_patrolboat;
                this.transform.localScale = new Vector3(4f, 3.5f, 1);
                this.transform.position = new Vector3(-6.65f, -3.37f, -1);

                break;

            case "red_battleship": this.GetComponent<SpriteRenderer>().sprite = red_battleship; break;
            case "red_carrier": this.GetComponent<SpriteRenderer>().sprite = red_carrier; break;
            case "red_desrtoyer": this.GetComponent<SpriteRenderer>().sprite = red_destroyer; break;
            case "red_submarine": this.GetComponent<SpriteRenderer>().sprite = red_submarine; break;
            case "red_patrolboat": this.GetComponent<SpriteRenderer>().sprite = red_patrolboat; break;
        }
    }

   

}
