using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Deck))]
public class DeckButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Deck deck = (Deck)target;

        if (GUILayout.Button("Draw a Card"))
        {
            deck.Draw();
        }
    }
}
