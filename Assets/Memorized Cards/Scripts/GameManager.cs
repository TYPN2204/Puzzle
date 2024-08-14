using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private List<Card> _selectedCards;
    public Button restart;
    public GameObject smokeEffect;
    public Transform theParentGameObject;
    public GameObject scoreText;
    public enum OpenCardState
    {
        Opening,
        Done
    };

    public OpenCardState cardState;

    void Start()
    {
        _selectedCards = new List<Card>();
        restart.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Memorized Cards", LoadSceneMode.Single);
    }
    public void AddCard(Card cardSelected)
    {
        if (_selectedCards.Count < 2)
        {
            _selectedCards.Add(cardSelected);
            if (_selectedCards.Count == 2)
            {
                if (_selectedCards[0].id == _selectedCards[1].id)
                {
                    StartCoroutine(DeleteCards());
                }
                else
                {
                    StartCoroutine(CloseCards());
                }
            }
        }
    }

    IEnumerator DeleteCards() //hieu ung o day
    {
        cardState = OpenCardState.Done;
        yield return new WaitForSecondsRealtime(1);
        _selectedCards[0].gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        _selectedCards[1].gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
        Vector3 card1Pos = _selectedCards[0].gameObject.transform.position;
        Vector3 card2Pos = _selectedCards[1].gameObject.transform.position;
        Vector3 midPos = (card1Pos + card2Pos) / 2;
        yield return new WaitForSecondsRealtime(0.5f);
        _selectedCards[0].gameObject.transform.DOMove(midPos, 0.5f);
        _selectedCards[1].gameObject.transform.DOMove(midPos, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(_selectedCards[0].gameObject);
        Destroy(_selectedCards[1].gameObject);
        GameObject smoke = Instantiate(smokeEffect, midPos, Quaternion.identity);
        Destroy(smoke,2f);
        Vector2 midPosWorld = Camera.main.WorldToScreenPoint(midPos);
        GameObject score = Instantiate(scoreText, midPosWorld, Quaternion.identity, theParentGameObject);
        score.transform.DOMove(midPosWorld + new Vector2(0,100), 1f);
        score.GetComponent<Text>().DOFade(0, 2);
        Destroy(score,4);
        _selectedCards.Clear();
        cardState = OpenCardState.Opening;
    }

    IEnumerator CloseCards()
    {
        cardState = OpenCardState.Done;
        yield return new WaitForSecondsRealtime(1);
        _selectedCards[0].ClearCard();
        _selectedCards[1].ClearCard();
        _selectedCards.Clear();
        cardState = OpenCardState.Opening;
    }
}
