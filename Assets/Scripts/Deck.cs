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

    void instatiateCards(GameObject cardPrefab, int numCard, Transform previousCard)
    {
        for (int x = 1; x <= numCard; x++) 
        {   
            GameObject card = Instantiate(cardPrefab, transform.position, Quaternion.identity);
            card.GetComponent<Draw>().previousDrawOnHand = previousCard;
            cards.Add(card);
        }
    }


    public GameObject Draw(Transform previousCard)
    {
        //GameObject cardToDraw = cards[0];
        //cards.RemoveAt(0);
        instatiateCards(knightPrefab, 1, previousCard);
        return cards.LastOrDefault();
    }
}
