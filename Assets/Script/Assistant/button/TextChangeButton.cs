using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeButton : MonoBehaviour
{
    //
    [SerializeField] Text uiText;
    [SerializeField] List<string> Texts;
    [SerializeField] List<GameObject> objects;
    private int index = 0;

    private void Awake()
    {
        uiText.text = Texts[index];
        objects[index].SetActive(true);
    }
    public void OnClick()
    {
        objects[index].SetActive(false);
        index++;
        if (index >= Texts.Count) index = 0;
        uiText.text = Texts[index];
        objects[index].SetActive(true);
    }
}
