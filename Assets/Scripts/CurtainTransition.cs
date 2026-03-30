using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainTransition : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;
    [SerializeField] private float Duration = 0.5f;
    [Header("Debug")]
    [SerializeField] private bool Refresh = true;
    [SerializeField] private bool Open;
    [SerializeField] private bool Close;

    private bool isOpen;
    private float ScreenWidth;

    private void Start()
    {
        if (canvas==null)
        {
            canvas = GetComponent<Canvas>();
        }
    }

    private void Update()
    {
        if (Refresh)
        {
            ScreenWidth = canvas.pixelRect.width;
            Refresh = false;
        }

        if (Open)
        {
            OpenCurtain();
            Open = false;
        }

        if (Close)
        {
            CloseCurtain();
            Close = false;
        }
    }


    public void OpenCurtain()
    {
        if (!isOpen)
        {
            LeanTween.cancel(Left);
            LeanTween.cancel(Right);
            Left.transform.LeanMoveLocalX(-(ScreenWidth-(ScreenWidth/4)), Duration).setEaseOutQuint();
            Right.transform.LeanMoveLocalX((ScreenWidth-(ScreenWidth/4)), Duration).setEaseOutQuint().setOnComplete(() =>
            {
                Left.SetActive(false);
                Right.SetActive(false);
            });
            isOpen = true;
        }
    }

    public void CloseCurtain()
    {
        if (isOpen)
        {
            Left.SetActive(true);
            Right.SetActive(true);
            LeanTween.cancel(Left);
            LeanTween.cancel(Right);
            Left.transform.LeanMoveLocalX(-(ScreenWidth/4), Duration).setEaseOutQuint();
            Right.transform.LeanMoveLocalX((ScreenWidth/4), Duration).setEaseOutQuint();
            isOpen = false;
        }
    }
}
