using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gainer : MonoBehaviour
{

    float value = 0;
    float multiplier = 1;
    float price = 2;
    [SerializeField]
    float multiplierRaise = 0.2f;
    [SerializeField]
    float priceRaise = 0.2f;
    [SerializeField]
    TextMeshProUGUI valueText;
    [SerializeField]
    TextMeshProUGUI priceUpgradeText;
    [SerializeField]
    TextMeshProUGUI gainText;
    // Start is called before the first frame update
    void Start()
    {
        valueText.text = value.ToString();
        gainText.text = multiplier.ToString();
        priceUpgradeText.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        value += Time.deltaTime * multiplier;
        valueText.text = System.Math.Round(value, 2).ToString();
    }

    public void Upgrade()
    {
        if (value >= price)
        {
            value -= price;
            multiplier += multiplierRaise;
            price *= priceRaise;
            gainText.text = multiplier.ToString();
            priceUpgradeText.text = price.ToString();
        }

    }
}
