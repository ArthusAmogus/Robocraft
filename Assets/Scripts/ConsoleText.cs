using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private Vector2 TextPosition;
    [SerializeField] private float spacing;
    [SerializeField] private float lifespan;
    [SerializeField] private bool doAutoDelete = true;
    [SerializeField] private bool TestConsole;
    [SerializeField] private bool ClearConsole;

    private string cur_text = "";
    private float timer;
    private Canvas canvas;
    private Queue<GameObject> texts = new();

    private void OnEnable()
    {
        canvas = this.gameObject.GetComponent<Canvas>();
        ClearConsole = true;
    }

    private void Update()
    {
        if (TestConsole)
        {
            if (timer>0.5)
            {
                AddText("Testing the text console");
                timer = 0;
            }
            else timer += Time.deltaTime;
        }

        if (ClearConsole)
        {
            while (texts.Count > 0) Destroy(texts.Dequeue());
            ClearConsole = false;
        }
    }


    public void AddText(string text)
    {
        if (text!=cur_text)
        {
            MoveText(spacing);
            cur_text = text;
        }
        GameObject textObj = new("TextConsoleObject");
        textObj.transform.parent = canvas.transform;
        textObj.transform.localScale = new(1, 1, 1);
        textObj.transform.LeanSetLocalPosX(TextPosition.x * 10);
        textObj.transform.LeanSetLocalPosY(TextPosition.y * 10);
        textObj.transform.LeanSetLocalPosZ(0);
        textObj.AddComponent<Billboard>();
        textObj.AddComponent<TextMeshProUGUI>();
        textObj.GetComponent<TextMeshProUGUI>().text = text;
        textObj.GetComponent<RectTransform>().sizeDelta = new(500, textObj.GetComponent<RectTransform>().localScale.y);
        texts.Enqueue(textObj);
        if (doAutoDelete)
        {
            Coroutine doTextRemove = StartCoroutine(RemoveText(textObj));
        }
    }

    private void MoveText(float amount)
    {
        if (texts.Count > 0)
        {
            foreach (var textObjs in texts)
            {
                RectTransform rectpos = textObjs.GetComponent<RectTransform>();
                rectpos.position = new(rectpos.position.x, rectpos.position.y + (amount/10), rectpos.position.z);
            }
        }
    }

    IEnumerator RemoveText(GameObject text)
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(texts.Dequeue());
    }
}
