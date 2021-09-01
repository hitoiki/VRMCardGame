using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour, ICardPrinted, ICursolable
{
    //Hand,持ち札として動かせるカード
    //KeyPadで動かせる。また、離した場所に応じてなんかする
    public Card card;
    public Vector3 anchor = Vector3.zero;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
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
        spriteRenderer.sprite = c.mainData.iconSprite;
    }

    public void Active(bool b)
    {
        this.gameObject.SetActive(b);
    }
    public void Click(Vector3 pos, ContactMode mode)
    {
        if (mode == ContactMode.Exit)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 0.01f, false);
            RaycastHit hit_info = new RaycastHit();
            if (Physics.Raycast(ray, out hit_info, 100f))
            {
                IHandPuttable[] puttable = hit_info.collider.gameObject.GetComponents<IHandPuttable>();
                if (puttable != null)
                {
                    foreach (IHandPuttable p in puttable)
                    {
                        p.HandPut(this);
                    }
                }
            }
            this.transform.position = anchor;
        }
    }
    public void Cursol(Vector3 pos)
    {
        vrmPrinted.Print(card);
    }
}
