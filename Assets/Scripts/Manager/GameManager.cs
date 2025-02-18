using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player {  get; private set; }
    private AreaManager areaManager;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        areaManager = GetComponentInChildren<AreaManager>();
        areaManager.Init(this);
    }


}
