using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject pawnPrefab;

    [SerializeField]
    GameObject rookPrefab;

    [SerializeField]
    GameObject queenPrefab;

    [SerializeField]
    GameObject knightPrefab;

    [SerializeField]
    GameObject bishopPrefab;

    [SerializeField]
    Transform drawSpot;

    public List<GameObject> cards;

    void Start()
    {
        //instatiateCards(pawnPrefab, 1);
        // shuffleCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void instatiateCards(GameObject cardPrefab, int numCard)
    {
        for (int x = 1; x <= numCard; x++) 
        {
            GameObject card = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        }
    }


    public void Draw()
    {
        //GameObject cardToDraw = cards[0];
        //cards.RemoveAt(0);
        instatiateCards(knightPrefab, 1);
    }
}
