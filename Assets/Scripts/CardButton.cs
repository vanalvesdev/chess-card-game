using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card))]
public class CardButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Card card = (Card) target;
        
        if (GUILayout.Button("Move to Left"))
        {
            card.MoveToLeft();
        }

        if (GUILayout.Button("Move to Right"))
        {
            card.MoveToRight();
        }
    }
}
