using UnityEngine;
using UnityEngine.UI;
using Sys = System;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameField;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject firstGameModeButton;
    [SerializeField]
    private GameObject secondGameModeButton;
    [SerializeField]
    private GameObject thirdGameModeButton;
    [SerializeField]
    private GameObject scoreText;
    [SerializeField]
    private GameObject restartGameButton;
    [SerializeField]
    private Sprite cardBackground;
    [SerializeField]
    private GameObject newGameButton;
    [SerializeField]
    private GameObject resultText;
    [SerializeField]
    private GameActions gameActions;
    private int chosenGameMode;
    private List<GameObject> cards;
    private Sprite[] cardImages;
    private List<int> randomizedCards;
    private bool firstClicked, secondClicked;
    private int firstSelected, secondSelected;
    private int cardsGuessed;
    private int score;

    void Start() {
        cardImages = Resources.LoadAll<Sprite>("Cards");
        gameActions = new GameActions();
        gameActions.GameActionsMap.Restart.performed += ctx => RestartGameClicked();
        gameActions.GameActionsMap.Enable();
    }

    void Update() {
        
    }

    void StartGame() {

        scoreText.SetActive(true);
        scoreText.GetComponent<Text>().text = "Score: 0";
        firstGameModeButton.SetActive(false);
        secondGameModeButton.SetActive(false);
        thirdGameModeButton.SetActive(false);
        resultText.SetActive(false);
        newGameButton.SetActive(false);
        restartGameButton.SetActive(true);
        cards = new List<GameObject>();
        randomizedCards = new List<int>();
        RandomizeImages();
        cardsGuessed = 0;
        score = 0;

        int numberOfCards = 0;

        switch (chosenGameMode) {
            case 1: numberOfCards = 4; break;
            case 2: numberOfCards = 8; break;
            case 3: numberOfCards = 16; break;
            default: break;
        }

        switch (chosenGameMode) {
            case 1: gameField.GetComponent<GridLayoutGroup>().constraintCount = 2; break;
            default: gameField.GetComponent<GridLayoutGroup>().constraintCount = 4; break;
        }

        List<int> tempPairNumbers = new List<int>();
        for (int j=0; j < 2; j++) {
            for (int i=0; i < numberOfCards / 2; i++) {
                tempPairNumbers.Add(i);
            }
        }

        int howManyToRandomize = numberOfCards;
        Sys.Random rnd = new Sys.Random();
        while(howManyToRandomize > 0) {
            int tmp = rnd.Next(0, howManyToRandomize);
            int pairNo = tempPairNumbers[tmp];
            tempPairNumbers.RemoveAt(tmp);
            randomizedCards.Add(pairNo);
            howManyToRandomize -= 1;
        }

        for (int i=0; i < numberOfCards; i++) {
            GameObject card = Instantiate(buttonPrefab);
            card.name = "Card-" + i;
            card.transform.SetParent(gameField.GetComponent<Transform>(), false);
            card.GetComponent<Image>().sprite = cardBackground;
            int temp = i;
            card.GetComponent<Button>().onClick.AddListener(delegate {HandleCardClick(temp);});
            cards.Add(card);
        }
    }

    void HandleCardClick(int cardNumber) {

        if (!firstClicked) {
            firstClicked = true;
            firstSelected = cardNumber;
            int pairNumber = randomizedCards[cardNumber];
            cards[cardNumber].GetComponent<Image>().sprite = cardImages[pairNumber];
        } else if (!secondClicked && firstSelected != cardNumber) {
            secondClicked = true;
            secondSelected = cardNumber;
            int pairNumber = randomizedCards[cardNumber];
            cards[cardNumber].GetComponent<Image>().sprite = cardImages[pairNumber];
            StartCoroutine(CheckSelection());
        }
    }

    IEnumerator CheckSelection() {
        yield return new WaitForSeconds(1f);

        if (randomizedCards[firstSelected] == randomizedCards[secondSelected]) {
            cards[firstSelected].GetComponent<Button>().interactable = false;
            cards[firstSelected].GetComponent<Button>().image.enabled = false;
            
            cards[secondSelected].GetComponent<Button>().interactable = false;
            cards[secondSelected].GetComponent<Button>().image.enabled = false;

            cardsGuessed += 1;
            score += 10;
        } else {
            cards[firstSelected].GetComponent<Image>().sprite = cardBackground;
            cards[secondSelected].GetComponent<Image>().sprite = cardBackground;
            score -=2 ;
        }
        scoreText.GetComponent<Text>().text = "Score: " + score;
        CheckGameFinished();
        firstClicked = false;
        secondClicked = false;
    }

    void CheckGameFinished() {
        if (cardsGuessed == cards.Count / 2) {
            resultText.GetComponent<Text>().text = "You won!\nYour score was: " + score;
            restartGameButton.SetActive(false);
            resultText.SetActive(true);
            newGameButton.SetActive(true);
            scoreText.SetActive(false);
        } else if (score == -6) {
            foreach (GameObject card in cards) {
                Destroy(card);
            }
            resultText.GetComponent<Text>().text = "Oh no, you lost!\nYour score was: " + score;
            restartGameButton.SetActive(false);
            resultText.SetActive(true);
            newGameButton.SetActive(true);
            scoreText.SetActive(false);
        }
    }

    public void HandleFirstGameModeChosen() {
        chosenGameMode = 1;
        StartGame();
    }

    public void HandleSecondGameModeChosen() {
        chosenGameMode = 2;
        StartGame();
    }

    public void HandleThirdGameModeChosen() {
        chosenGameMode = 3;
        StartGame();
    }

    public void RestartGameClicked() {
        
        if (!firstClicked) {
            foreach (GameObject card in cards) {
                Destroy(card);
            }

            scoreText.SetActive(false);
            firstGameModeButton.SetActive(true);
            secondGameModeButton.SetActive(true);
            thirdGameModeButton.SetActive(true);
            restartGameButton.SetActive(false);
            resultText.SetActive(false);
            newGameButton.SetActive(false);
        }
    }
    private void RandomizeImages() {
        for (int i=0; i < cardImages.Length; i++) {
            Sprite temp = cardImages[i];
            int r = Random.Range(i, cardImages.Length);
            cardImages[i] = cardImages[r];
            cardImages[r] = temp;
        }
    }
}
