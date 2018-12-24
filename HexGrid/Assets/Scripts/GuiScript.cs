using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void action(int players);

public class GuiScript
{ 
    public GameObject CreatePanel(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector3 localScale,
        Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform);
        panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>();

        panel.GetComponent<Image>().sprite = image;
        panel.GetComponent<Image>().type = Image.Type.Sliced;
        panel.GetComponent<Image>().color = color;

        panel.GetComponent<RectTransform>().anchorMin = anchorMin;
        panel.GetComponent<RectTransform>().anchorMax = anchorMax;
        panel.GetComponent<RectTransform>().localScale = localScale;
        panel.GetComponent<RectTransform>().localPosition = localPosition;
        panel.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        panel.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        return panel;
    }

    public GameObject CreateButton(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax,
       Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject button = new GameObject(name);
        button.transform.SetParent(parent.transform);
        button.AddComponent<RectTransform>();
        button.AddComponent<Image>();
        button.AddComponent<Button>();

        button.GetComponent<Image>().sprite = image;
        button.GetComponent<Image>().type = Image.Type.Sliced;
        button.GetComponent<Image>().color = color;

        button.GetComponent<RectTransform>().anchorMin = anchorMin;
        button.GetComponent<RectTransform>().anchorMax = anchorMax;
        button.GetComponent<RectTransform>().localScale = localScale;
        button.GetComponent<RectTransform>().localPosition = localPosition;
        button.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        button.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return button;
    }

    public GameObject[,] FillWithButtons(GameObject panel, int buttonCount, int rowsCount, float buttonWidth,
       float buttonHeight, Sprite s, Color32 color)
    {
        GameObject[,] buttons = new GameObject[buttonCount, rowsCount];
        float offsetx = buttonWidth / 2.0f;
        float offsety = buttonHeight / 2.0f;
        for (int j = 0; j < rowsCount; j++)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                GameObject but = CreateButton(panel, ("Button" + (j * rowsCount + i).ToString()), new Vector2(0, 1), new Vector2(0, 1),
                    new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonWidth, buttonHeight),
                    new Vector2((offsetx + i * buttonWidth), (-offsety - j * buttonHeight)), s, color);
                buttons[i, j] = but;
            }
        }
        return buttons;
    }

    public List<GameObject> SetMenuText(GameObject[,] but, string[,] names)
    {
        List<GameObject> l = new List<GameObject>();
        for (int i = 0; i < but.GetLength(0); i++)
        {
            for (int j = 0; j < but.GetLength(1); j++)
            {
                GameObject text = new GameObject("ButtonText");
                text.transform.SetParent(but[i, j].transform);
                text.AddComponent<Text>();
                text.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
                text.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
                text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                text.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                text.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
                text.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
                text.GetComponent<Text>().resizeTextForBestFit = true;
                text.GetComponent<Text>().resizeTextMaxSize = 30;
                text.GetComponent<Text>().resizeTextMinSize = 10;
                text.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
                text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                text.GetComponent<Text>().text = names[i,j];
            }
        }
        return l;
    }

    public void SetAction(GameObject button, int players, action act)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate { act.Invoke(players); });
    }

    public GameObject CreateTextField(GameObject parent, string name)
    {
        GameObject text = new GameObject(name);
        text.transform.SetParent(parent.transform);
        text.AddComponent<RectTransform>();
        text.AddComponent<Text>();

        text.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        text.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
        text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        text.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        text.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, -100.0f);
        text.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
        text.GetComponent<Text>().resizeTextForBestFit = true;
        text.GetComponent<Text>().resizeTextMaxSize = 50;
        text.GetComponent<Text>().resizeTextMinSize = 10;
        text.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
        text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
        text.GetComponent<Text>().text = "";

        return text;
    }
}
