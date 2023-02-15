using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Player : MonoBehaviour
{

    [SerializeField]
    List<GameObject> cards;

    [SerializeField]
    Deck deck;

    int initialCardPosition = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Draw()
    {
        cards.Add(deck.Draw(cards.Count > 0 ? cards.LastOrDefault().transform : null));
    }

}
