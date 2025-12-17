using PurrNet;
using PurrNet.Transports;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChooser : NetworkBehaviour
{

    public NetworkBehaviour rivera;
    public NetworkBehaviour rizal;


    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
        {
            NetworkManager.main.onPlayerJoinedScene += SomeoneJoined;
        }
    }

    private void SomeoneJoined(PlayerID player, SceneID scene, bool asServer)
    {
        print("burat");
        if (!asServer)
        {
            Debug.Log("not a server");
            return;
        }
        print("this is server");
        if (NetworkManager.main.playerCount == 1)
        {
            rizal.GiveOwnership(player);
        }

        if (NetworkManager.main.playerCount == 2)
        {
            rivera.GiveOwnership(player);
        }

    }

    protected override void OnDespawned(bool asServer)
    {
        base.OnDespawned();
        if (asServer)
        {
            NetworkManager.main.onPlayerJoinedScene -= SomeoneJoined;
        }
    }
}
