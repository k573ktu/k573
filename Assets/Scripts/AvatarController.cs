using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Text;

public class AvatarController : MonoBehaviour
{
    [SerializeField] RectTransform avatarImage;

    [SerializeField] float avatarHiddenYPosition;
    float avatarCurrYPosition;

    [SerializeField] GameObject dialogObject;

    [SerializeField] TextMeshProUGUI dialogText;

    [SerializeField] float timeToAddLetter;

    [SerializeField] List<string> MessageList;

    public static AvatarController inst;

    Queue<string> textQueue;

    bool skip, showing, inAnimation;

    string currentText;

    StringBuilder inDialog;

    int currLetterIndex;

    float currTime, timeUntilNext;

    static HashSet<string> oneTime;

    private void Awake()
    {
        if (inst == null) inst = this;
        if(oneTime == null) oneTime = new HashSet<string>();
    }

    private void Start()
    {
        textQueue = new Queue<string>();
        inDialog = new StringBuilder();
        skip = false;
        showing = false;
        inAnimation = false;
        avatarCurrYPosition = avatarImage.anchoredPosition.y;
        avatarImage.anchoredPosition = new Vector2(avatarImage.anchoredPosition.x, avatarHiddenYPosition);
        dialogObject.SetActive(false);

        foreach (string text in MessageList)
        {
            ShowText(text);
        }
    }

    public void HideText()
    {
        showing = false;
        inAnimation = false;
        dialogObject.SetActive(false);
        avatarImage.DOKill();
        avatarImage.DOAnchorPosY(avatarHiddenYPosition, 0.7f).SetEase(Ease.InOutCubic).SetDelay(0.6f);
    }

    void DisplayTextAnimation()
    {
        currentText = textQueue.Dequeue();
        inAnimation = true;
        currTime = 0;
        timeUntilNext = 0;
        currLetterIndex = 0;
        inDialog.Clear();
    }

    void ShowDialogObject()
    {
        dialogObject.SetActive(true);
        DisplayTextAnimation();
    }

    void UpdateText()
    {
        if(skip) return;
        if (textQueue.Count == 0)
        {
            if (showing) HideText();
            return;
        }

        if (!showing)
        {
            avatarImage.DOKill();
            avatarImage.DOJumpAnchorPos(new Vector2(avatarImage.anchoredPosition.x,avatarCurrYPosition),100,1, 0.7f).SetEase(Ease.InOutCubic).OnComplete(() => { showing = true; ShowDialogObject(); });
        }
        else
        {
            ShowDialogObject();
        }
    }

    public void ShowText(string text)
    {
        textQueue.Enqueue(text);

        UpdateText();
    }

    public void ShowTextOneTime(string text, string code)
    {
        if (oneTime.Contains(code)) return;
        oneTime.Add(code);
        textQueue.Enqueue(text);

        UpdateText();
    }

    public void DialogPressed()
    {
        if (inAnimation)
        {
            currTime += 500 * timeToAddLetter;
        }
        else
        {
            UpdateText();
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    skip = true;
        //    HideText();
        //}

        if (inAnimation)
        {
            currTime += Time.deltaTime;
            if (currTime >= timeUntilNext) {
                while (currTime >= timeUntilNext)
                {
                    inDialog.Append(currentText[currLetterIndex]);
                    timeUntilNext += timeToAddLetter;
                    currLetterIndex++;
                    if (currLetterIndex == currentText.Length)
                    {
                        inAnimation = false;
                        break;
                    }
                }
                dialogText.text = inDialog.ToString();
            }
        }
    }
}
