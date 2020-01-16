using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jesus.Cards;

public class GeneralSelector : MonoBehaviour
{
    public GeneralManager gm;

    private void Awake()
    {
        gm = Object.FindObjectOfType<GeneralManager>();
    }

    [Header("General 1")]
    public GeneralSO general1;
    public Text general1NameText;
    public Image general1Image;
    public Text general1BuffText;

    [Header("General 2")]
    public GeneralSO general2;
    public Text general2NameText;
    public Image general2Image;
    public Text general2BuffText;


    private void Start()
    {
        PopulateButton(general1, general1NameText, general1Image, general1BuffText);
        PopulateButton(general2, general2NameText, general2Image, general2BuffText);

    }


    private void PopulateButton(GeneralSO info, Text name, Image image, Text buff)
    {
        name.text = info.name;
        image.sprite = info.image;
        buff.text = info.buffDescription;

    }

    public void PickGeneral(int pick)
    {
        if (pick == 0)
        {
            gm.general = general1;
            Debug.Log("Isaac");

        }
        else
        {
            gm.general = general2;
            Debug.Log("Nathan");
        }
    }



}
