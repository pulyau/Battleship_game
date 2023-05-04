using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Holder", menuName = "Data Holder")]
public class DataHolder : ScriptableObject
{
    public List<List<(bool, string)>> blue_tiles;
    public List<List<(bool, string)>> red_tiles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
