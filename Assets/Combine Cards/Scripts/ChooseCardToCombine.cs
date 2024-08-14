using UnityEngine;

public class ChooseCardToCombine : MonoBehaviour
{
    private CombineCardData _selectedCard;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && _selectedCard == null)
            {
                CombineCardData card = hit.collider.gameObject.GetComponent<CombineCardData>();
                if (card != null)
                {
                    _selectedCard = card;
                }
            }

            if (_selectedCard != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = -10;
                _selectedCard.transform.position = mousePos;
                _selectedCard.SelectCard();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _selectedCard = null; 
        }
    }
}