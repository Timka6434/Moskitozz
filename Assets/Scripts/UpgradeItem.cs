using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private UIParameters uIParameters;
    [SerializeField] private UpgradesList upgradesList;
    [SerializeField] private float startPrice;
    [SerializeField] private Image[] imagesLvl;
    [SerializeField] private Image imagesType;
    [SerializeField] private uint currentLevel;
    [SerializeField] private uint maxLevel;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private float modifier = 1.0f;
    [SerializeField] private GameObject ButtonBuy, PanelPrice, PanelNoMoreUpgrades;
    //[SerializeField] private int upgradableParametr;
    private float upgradeCost;
    
    public enum UpgradeTypes
    {
        nothink,
        MoreHealthMoskit,
        MoreDamage,
        MoreHealthHouse,
        MoreBloodCollect,
        MaxBloodCollecteble,
    }
    public UpgradeTypes currentType;

    private void Start()
    {
        upgradesList = GetComponent<UpgradesList>();
        modifier = 1.0f;
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        priceText.text = (startPrice * modifier).ToString("00");
    }
    
    public void Upgrade()
    {
        upgradeCost = startPrice * modifier; // Определяем стоимость улучшения
        Debug.Log("Current type is -  " + currentType);
        switch (currentType)
        {
            case UpgradeTypes.MoreHealthMoskit:
                Debug.Log("UPGRADE " + currentType);
                upgradesList.MaxBloodCollecteble();
                break;
            case UpgradeTypes.MoreHealthHouse:
                Debug.Log("UPGRADE " + currentType);
                upgradesList.HealthHouse();
                break;
        }
        if (uIParameters.BloodCounter >= upgradeCost && currentLevel < maxLevel)
        {

            imagesLvl[currentLevel].sprite = imagesType.sprite; // Устанавливаем изображение для уровня
            modifier *= 1.2f;
            currentLevel++; // Увеличиваем уровень после улучшения
            UpdatePriceText(); // Обновляем текст цены

            // Уменьшаем значение BloodCounter на стоимость улучшения
            uIParameters.BloodCounter -= upgradeCost;
        }

        if (currentLevel == maxLevel)
        {
            ButtonBuy.gameObject.SetActive(false);
            PanelPrice.gameObject.SetActive(false);
            PanelNoMoreUpgrades.gameObject.SetActive(true);
        }
    }




}
