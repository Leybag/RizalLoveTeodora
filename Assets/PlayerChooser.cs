using PurrNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooser : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerID id = owner.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
