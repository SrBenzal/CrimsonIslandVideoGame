using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindAPair : MonoBehaviour {
    public GameObject card;
    public GameObject firstCard;
    public GameObject backgorund;
    public GameObject text;

    public int row;
    public int col;
	private int count;
    private float backgorunWidth;
    private float backgroundHeight;
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
		count = row * col;
		//Debug.Log (count);
        if ((count) % 2 != 0)
        {
            row = 4;
            col = 4;
			count = row * col;
        }
        /*if (gapY < 0.5 || gapY > backgroundHeight / 4)
            gapY = 100f;
        if (gapX < 50 || gapX > backgorunWidth / 3)
            gapX = 300f;*/
        numbers = new List<int>();
        for (int i = 0; i < count; i++)
        {
            numbers.Add(Mathf.FloorToInt((i / 2) + 1));
            //Debug.Log("Metemos en la lista el " + Mathf.FloorToInt(i / 2 + 1));
        }
        cardsShowed = 0;
        showingCard = false;
        // Instanciate all buttons 
        // where may be stay
        // ------------------------     
        float CardSizeX = (backgroundHeight /*- gapY*/) / row;
		float CardSizeY = CardSizeX;
        card.GetComponent<RectTransform>().sizeDelta = new Vector2(CardSizeX, CardSizeY);
        //Debug.Log("Size of the card : " + CardSizeX + " x " + CardSizeY );
        //Debug.Log(cardsShowed);
        for (int i = 0; i < col; i++)
            for (int j = 0; j < row; j++)
            {
				Vector2 positionCard = new Vector2 (backgorunWidth/(col*2) + (backgorunWidth/col) * i, (backgroundHeight/(row*2) + (backgroundHeight/row) * j) - backgroundHeight);          
                //Debug.Log("(" + positionCard.x + " ," + positionCard.y + ")");
				GameObject a = Instantiate(card, backgorund.transform);
                a.GetComponent<RectTransform>().localPosition = positionCard;
                GetValue(a);                
            }
    }

    // Update is called once per frame
    void Update ()
    {
		//Debug.Log ("hay: " + count + " cartas y se han mostrado: " + cardsShowed);
	}
    public void FlippedCard(GameObject c)
    {
		//Debug.Log("Aqui entra asi: " + cardsShowed);
        //Debug.Log("xd" + firstCard);
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
			//Debug.Log("Ha mostrdo: " + cardsShowed + " Y hay " + count + " cartas. NO me lo creo: " + (cardsShowed == count));
			if(cardsShowed == count)
			{
				foreach (Transform t in backgorund.transform) 
				{
					
					Destroy(t.gameObject);
				}
				Instantiate(text, gameObject.transform);
			}
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
	public int GetcardsShowed()
	{
		return this.cardsShowed;
	}	
}
