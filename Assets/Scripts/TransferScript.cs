using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferScript : MonoBehaviour
{
    public DataHolder transfer_lists;

    public void get_blue_tiles(List<List<(bool, string)>> the_2dlist)
    {

        transfer_lists.blue_tiles = the_2dlist;
        Debug.Log(transfer_lists.blue_tiles.Count);
    }
    public void get_red_tiles(List<List<(bool, string)>> the_2dlist)
    {
        transfer_lists.red_tiles = the_2dlist;
    }
}
