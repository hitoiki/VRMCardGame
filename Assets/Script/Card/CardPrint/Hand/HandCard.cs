using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour, ICardPrinted, ICursolable
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かせる。また、離した場所に応じてなんかする
    public Card card;
    public Vector3 anchor = Vector3.zero;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject InitVRM = null;
    public ICardPrinted vrmPrinted;
    private void OnValidate()
    {
        if ((InitVRM != null) && InitVRM.GetComponent<ICardPrinted>() == null) InitVRM = null;
    }

    private void Start()
    {
        if (InitVRM != null) vrmPrinted = InitVRM.GetComponent<ICardPrinted>();
    }
    public void Print(Card c)
    {
        card = c;
        spriteRenderer.sprite = c.iconSprite;
    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
    public void Click(Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Exit) this.transform.position = anchor;
    }
    public void Cursol(Vector3 pos)
    {
        vrmPrinted.Print(card);
    }
}
