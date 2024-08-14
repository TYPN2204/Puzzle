using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class CombineCardGameManager : MonoBehaviour
{
    public Button restart;
    private List<CombineCardData> _selectedCards;

    //effect smoke + score
    public GameObject smokeEffect;
    public Transform theParentGameObject;
    public GameObject scoreText;

    //vung chua va 4 o trong
    public PlaceHolderData dragZone;
    [SerializeField]
    private List<PlaceHolderData> brokenLine;
    public List<PlaceHolderData> BrokenLine
    {
        get
        {
            return brokenLine;
        }
        set
        {
            brokenLine = value;
            Debug.Log("...");
        }
    }

    public GameObject cardLogo;
    //the duoc di chuyen va vung di chuyen toi
    private CombineCardData _cardChosen;
    private PlaceHolderData _zoneChosen;

    void Start()
    {
        _selectedCards = new List<CombineCardData>();
        restart.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Combined Cards", LoadSceneMode.Single);
    }

    public void CheckClosetLine(CombineCardData card)
    {
        // return;
        //reset tinh khoang cach 
        PlaceHolderData closestPositionZone = null;
        float minDistance = 100;
        List<PlaceHolderData> holderPosition = new List<PlaceHolderData>();
        holderPosition.Add(dragZone);
        foreach (var line in brokenLine)
        {
            holderPosition.Add(line);
        }
        
        //tim o co vi tri gan nhat
        foreach (var holder in holderPosition)
        {
            //reset mau cua cac o de chi hien thi o mau xanh
            holder.GetComponent<SpriteRenderer>().color = Color.white;
            float distance = Vector2.Distance(card.transform.position, holder.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPositionZone = holder; //tra ve o co vi tri gan nhat
            }
        }

        if (closestPositionZone != null)
        {
            closestPositionZone.GetComponent<SpriteRenderer>().color = Color.green;
        }

        _cardChosen = card;
        _zoneChosen = closestPositionZone;
    }

    public bool cardFromDragZone = true;

    void Update()
    {
        bool movecard = false;
        // return;
        // di chuyển thẻ khi nhả chuột
        if (Input.GetMouseButtonUp(0) && _zoneChosen != null && _cardChosen != null) //check null
        {
            movecard = true;
            //kiểm tra xem thẻ có đến từ vùng chứa không
            //
            // if (_cardChosen.holderOfThisCard == dragZone)
            // {
            //     cardFromDragZone = false;
            // }
            
            foreach (var line in brokenLine)
            {
                if (line.cardInThisHolder == _cardChosen)
                {
                    cardFromDragZone = false;
                }
            }
            //TH1: Di chuyển thẻ từ vùng chứa tới ô trống ko có thẻ
            if (_zoneChosen.cardInThisHolder == null && cardFromDragZone)
            {
                if(_zoneChosen==dragZone)
                {
                    _cardChosen.CardReturn(); //TH1.1: Thẻ ở vùng chứa vẫn ở vùng chứa + TH3: Thẻ di chuyển về vùng chứa
                    Debug.Log("TH1.1 + TH3");
                }
                else
                {
                    Vector2 moveTo = _zoneChosen.transform.position;
                    _cardChosen.transform.DOMove(moveTo, 0.5f);
                    _zoneChosen.cardInThisHolder = _cardChosen; //gắn thẻ được chọn cho ô trống
                    Debug.Log("TH1");
                }
                _cardChosen.holderOfThisCard = _zoneChosen;
            } 
            //TH2: Di chuyển thẻ từ vùng chứa tới ô trống đã chứa thẻ
            else if(_zoneChosen.cardInThisHolder != null && cardFromDragZone)
            {
                bool allHolderFull = true;
                //TH2.1: Thẻ trong ô di chuyển tới 3 ô còn lại nếu trống
                foreach (var line in brokenLine)
                {
                    if (line.cardInThisHolder == null) // nếu ô nào trống thì di chuyển tới ô đó
                    {
                        Vector2 moveTo = line.transform.position;
                        _cardChosen.transform.DOMove(moveTo, 0.5f);
                        line.cardInThisHolder = _cardChosen;
                        _cardChosen.holderOfThisCard = line;
                        allHolderFull = false;
                        Debug.Log("TH2.1");
                        break;
                    }
                }                    
                //TH2.2: Nếu không có ô nào trống
                if (allHolderFull)
                {
                    // thẻ trong ô được chọn quay lại vùng chứa
                    _zoneChosen.cardInThisHolder.CardReturn();
                    Vector2 moveTo = _zoneChosen.transform.position;
                    _cardChosen.transform.DOMove(moveTo, 0.5f);
                    //gắn thẻ được chọn cho ô này
                    _zoneChosen.cardInThisHolder = _cardChosen;
                    _cardChosen.holderOfThisCard = _zoneChosen;
                    Debug.Log("TH2.2");
                }
            }
            //TH4: Thẻ di chuyển từ ô trống sang ô trống khác
            else if (_zoneChosen.cardInThisHolder == null && !cardFromDragZone)
            {
                foreach (var line in brokenLine)
                {
                    if (line.cardInThisHolder == _cardChosen)
                    {
                        line.cardInThisHolder = null;
                        Vector2 moveTo = _zoneChosen.transform.position;
                        _cardChosen.transform.DOMove(moveTo, 0.5f);
                        _zoneChosen.cardInThisHolder = _cardChosen;
                        _cardChosen.holderOfThisCard = _zoneChosen;
                        Debug.Log("TH4");
                        break;
                    }
                }
            }
            //TH5: Hoán đổi vị trí 2 thẻ của ô trống
            else if (_zoneChosen.cardInThisHolder != null && !cardFromDragZone)
            {
                // // hoán đổi vị trí 2 thẻ
                // Debug.Log("zone bi di chuyen id: " +_zoneChosen.idPlace);
                // Debug.Log("zone di chuyen id: " +_cardChosen.holderOfThisCard.idPlace);
                //khoi tao thong tin the       //xu ly xong thi set null
                CombineCardData cardMuonDichuyen = new CombineCardData();
                cardMuonDichuyen = _cardChosen;
                CombineCardData cardBiDiChuyen = new CombineCardData();
                cardBiDiChuyen = _zoneChosen.cardInThisHolder;
                int IdHolderTheBiChuyen = cardBiDiChuyen.holderOfThisCard.idPlace;
                int IdHolderMuonDiChuyen = cardMuonDichuyen.holderOfThisCard.idPlace;


                Vector3 zonePos = _zoneChosen.transform.position;
                zonePos.z = 0;
                _zoneChosen.transform.position = zonePos;                
                
                Vector3 cardPos = _cardChosen.holderOfThisCard.transform.position;
                cardPos.z = 0;
                _cardChosen.holderOfThisCard.transform.position = cardPos;
                
                
                Transform zoneChosenPos =_zoneChosen.transform;
                Transform cardChosenPos =_cardChosen.holderOfThisCard.transform;

                
                _zoneChosen.cardInThisHolder.transform.DOMove(cardChosenPos.position, 0.25f);
                _cardChosen.transform.DOMove(zoneChosenPos.position, 0.25f);
                
                
                zonePos.z = 5.12f;
                _zoneChosen.transform.position = zonePos;  
                
                cardPos.z = 5.12f;
                _cardChosen.holderOfThisCard.transform.position = cardPos;

                zoneChosenPos = null;
                cardChosenPos = null;
                //hoán đổi thông tin ô chứa của 2 thẻ

                cardMuonDichuyen.holderOfThisCard = BrokenLine[IdHolderTheBiChuyen];
                cardBiDiChuyen.holderOfThisCard = BrokenLine[IdHolderMuonDiChuyen];
                // Debug.Log("card muon di chuyen place holder id: " + cardMuonDichuyen.holderOfThisCard.idPlace);
                // Debug.Log("card bi di chuyen place holder id: " + cardBiDiChuyen.holderOfThisCard.idPlace);
                //
                // Debug.Log("card muon di chuyen: " + cardMuonDichuyen.gameObject.name);
                BrokenLine[IdHolderTheBiChuyen].cardInThisHolder = cardMuonDichuyen;
                BrokenLine[IdHolderMuonDiChuyen].cardInThisHolder = cardBiDiChuyen;

                // hoán đổi thông tin chứa thẻ của 2 ô

                //Debug.Log("TH5");
            }
            //reset
            dragZone.cardInThisHolder = null;
            _zoneChosen.GetComponent<SpriteRenderer>().color = Color.white;
            cardFromDragZone = true;
            _zoneChosen = null;
            _cardChosen = null;


        }
        if (movecard)
        {
            StartCoroutine(CheckFullCard());
        }
    }

    public bool canCombineCard;

    public bool canCheck;

    public bool canGetCard;
    IEnumerator CheckFullCard()
    {
         canCombineCard = false;

         canCheck = true;

         canGetCard = true;

        foreach (var line in brokenLine)
        {
            if (line.cardInThisHolder == null)
            {
                canCheck = false;
                break;
            }
        }

        _selectedCards.Clear();
        List<PlaceHolderData> sortedHolderData = new List<PlaceHolderData>();
        sortedHolderData.Clear();
        List<CombineCardData> sortedCardList = new List<CombineCardData>();
        sortedCardList.Clear();
        yield return new WaitForSeconds(0.25f);
        if (canCheck)
        {
            //Debug.Log("Can check");
            foreach (var line in brokenLine)
            {
                _selectedCards.Add(line.cardInThisHolder);
            }
            sortedHolderData= brokenLine.ToList(); //id 0->3
            sortedCardList = _selectedCards.ToList(); //placeInHolder 0->3
            //dieu kien cung loai the
            for (int i = 1; i < sortedCardList.Count; i++)
            {
                if (sortedCardList[0].Id != sortedCardList[i].Id)
                {
                    //Debug.Log("Different Id");
                    //Debug.Log("Id card 0: " + sortedCardList[0].Id + ", id card " + sortedCardList[i].name + " : " + sortedCardList[i].Id);
                    canGetCard = false;
                    break;
                }
                //else Debug.Log("id card 0 = card "+i);
            }
            yield return new WaitForSeconds(0.25f);

            //dieu kien dung vi tri
            if (canGetCard)
            {
                //Debug.Log("Can get card");
                canCombineCard = true;

                    for (int n = 0; n < sortedCardList.Count; n++)
                    {
                        if (sortedCardList[n].placeInHolder != sortedHolderData[n].idPlace)
                        {
                            //Debug.Log("Different position: " +sortedCardList[n].name + "-" + sortedCardList[n].placeInHolder + sortedHolderData[n].name + "-" + sortedHolderData[n].idPlace );
                            canCombineCard = false;
                            break;
                        }
                        //else Debug.Log( "card match position: "+sortedCardList[n].name + "-" + sortedCardList[n].placeInHolder + sortedHolderData[n].name + "-" + sortedHolderData[n].idPlace);
                    }
                
            }
        }
        yield return new WaitForSeconds(0.25f);

        if (canCombineCard)
        {
            //Debug.Log("Can check card");
            StartCoroutine(CompleteCards());
        }
    }
    // if (_selectedCards[0].id == _selectedCards[1].id&&_selectedCards[0].id == _selectedCards[2].id &&_selectedCards[0].id == _selectedCards[3].id &&
    //     _selectedCards[1].id == _selectedCards[2].id && _selectedCards[1].id == _selectedCards[3].id &&
    //     _selectedCards[2].id == _selectedCards[3].id)
    //         {
    //             //if(SortedCardList[0].placeInHolder==)
    //            // StartCoroutine(CompleteCards());
    //         }
    //     }
    // }


    //
    
    IEnumerator CompleteCards()
    {
        // card effect
        foreach (var line in brokenLine)
        {
            var cardInHolder = line.cardInThisHolder;
            cardInHolder.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.3f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                cardInHolder.transform.DOScale(Vector3.zero, 1.25f);
                cardInHolder.transform.DORotate(new Vector3(0, 0, 180), 1f).SetEase(Ease.InOutQuad);
                cardInHolder.transform.DOJump(cardLogo.transform.position, 4, 1, 1f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    cardLogo.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 4);
                });
            });
            yield return new WaitForSeconds(0.5f);
            Destroy(cardInHolder,2);
        }
        yield return new WaitForSeconds(1);
        Vector3 smokePos = new Vector3(-3,0,0);
        GameObject smoke = Instantiate(smokeEffect, smokePos, Quaternion.identity);
        Destroy(smoke,2f);
        Vector2 smokePosScreen = Camera.main.WorldToScreenPoint(smokePos);
        GameObject score = Instantiate(scoreText, smokePosScreen, Quaternion.identity, theParentGameObject);
        score.transform.DOMove(smokePosScreen + new Vector2(0,100), 1f);
        score.GetComponent<Text>().DOFade(0, 2);
        Destroy(score,4);
    }
}
        

