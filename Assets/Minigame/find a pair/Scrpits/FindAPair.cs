using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindAPair : MonoBehaviour {
    public GameObject card;
    public GameObject parent;
    public GameObject firstCard;
    public GameObject backgorund;
    public GameObject text;

    public int row;
    public int col;
    private float backgorunWidth;
    private float backgroundHeight;
    public float gapX;
    public float gapY;
    private int cont;
    private List<int> numbers;


    public int cardsShowed;
    private bool showingCard;

    // Use this for initialization
    void Start()
    {
        // -------------------------
        // set a value all variables
        // -------------------------
        backgorunWidth = backgorund.GetComponent<RectTransform>().sizeDelta.x;
        backgroundHeight = backgorund.GetComponent<RectTransform>().sizeDelta.y;
        //Debug.Log("Size of backgorund: " + backgorunWidth + " x " + backgroundHeight);
        // there must be at least 2 
        // rows and twocolumns and 
        // be an array of letters
        if (row < 1)
            row = 4;
        if (col < 1)
            col = 4;
        if ((row * col) % 2 != 0)
        {
            row = 4;
            col = 4;
        }
        if (gapY < 0.5 || gapY > backgroundHeight / 4)
            gapY = 100f;
        if (gapX < 50 || gapX > backgorunWidth / 3)
            gapX = 300f;
        numbers = new List<int>();
        for (int i = 0; i < col * row; i++)
        {
            numbers.Add(Mathf.FloorToInt((i / 2) + 1));
            //Debug.Log("Metemos en la lista el " + Mathf.FloorToInt(i / 2 + 1));
        }
        cardsShowed = 0;
        showingCard = false;
        // Instanciate all buttons 
        // where may be stay
        // ------------------------     
        float CardSizeX = (backgorunWidth - gapX*2) / col;
        float CardSizeY = (backgroundHeight - gapY*2) / row;
        card.GetComponent<RectTransform>().sizeDelta = new Vector2(CardSizeX, CardSizeY);
        //Debug.Log("Size of the card : " + CardSizeX + " x " + CardSizeY );
        float cardPositionStartX = (backgorunWidth / 2) - gapX;
        float cardPositionStartY = (backgroundHeight / 2) - gapY;
        //Debug.Log("Position of the first card: (" + cardPositionStartX + " ," + cardPositionStartY + ")");
        parent.GetComponent<RectTransform>().localPosition = new Vector2(-cardPositionStartX, cardPositionStartY);
        //Debug.Log(cardsShowed);
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
            {
                Vector2 positionCard = new Vector2(CardSizeX * i, CardSizeY * -j);           
                //Debug.Log("(" + positionCard.x + " ," + positionCard.y + ")");
                GameObject a = Instantiate(card, Vector2.zero, Quaternion.identity, parent.GetComponent<Transform>());
                a.GetComponent<RectTransform>().localPosition = positionCard;
                GetValue(a);                
            }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    public void FlippedCard(GameObject c)
    {
        //Debug.Log(firstCard);
        if (showingCard) return;
        if (firstCard == null)
        {
            firstCard = c;
            c.GetComponent<Card>().Flip();
        }
        else
        {
            c.GetComponent<Card>().Flip();
            if (CompareCards(firstCard, c))
            {
                firstCard.GetComponent<Card>().findPair = true;
                c.GetComponent<Card>().findPair = true;
                cardsShowed += 2;
                //Debug.Log("PAREJA ENCONTRADA");
            }
            else
            {
                showingCard = true;
                c.GetComponent<Card>().FlipInTime();
                firstCard.GetComponent<Card>().FlipInTime();
                //Debug.Log("Mala Pareja");
            }
            firstCard = null;
        }
        Debug.Log(cardsShowed);
        if(cardsShowed == row * col)
        {
            Destroy(parent, .5f);
            Instantiate(text, Vector2.zero, Quaternion.identity, parent.GetComponent<Transform>());
        }

    }
    private bool CompareCards(GameObject c1, GameObject c2)
    {
        return c1.GetComponent<Card>().value == c2.GetComponent<Card>().value;
    }
    private void GetValue(GameObject card)
    {
        int rng = Random.Range(0,numbers.Count - 1);
        //Debug.Log("Sacaremos de la lista el numero" + rng);
        card.GetComponent<Card>().value = numbers[rng];
        //Debug.Log("Se ha sacado el numero " + numbers[rng]);
        numbers.RemoveAt(rng);
    }
    public void SetShowingCard(bool param)
    {
        showingCard = param;
    }
}
