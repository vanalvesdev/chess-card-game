﻿using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
 
    [SerializeField]
    List<GameObject> cards;

    [SerializeField]
    Deck deck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Draw()
    {
        deck.Draw();
    }

}
