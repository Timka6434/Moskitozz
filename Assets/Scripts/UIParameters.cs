using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIParameters : MonoBehaviour
{
    [SerializeField] private MoskitoHouse moskitoHouse;

    [Header("Counter units")]
    [SerializeField] private float MoskitCounter;
    [SerializeField] private float EnemyCounter;

    [Header("Settings")]
    [SerializeField] public float MaxBloodCount;
    [SerializeField] public float BloodCounter;
    [SerializeField] private Slider BloodBar;

    [Header("Other")]
    [SerializeField] private float Timer; // время от начала игры

    [Header("Texts components")]
    [SerializeField] public TextMeshProUGUI TimerText;
    [SerializeField] public TextMeshProUGUI MoskitCounterText;
    [SerializeField] public TextMeshProUGUI EnemyCounterText;
    [SerializeField] public TextMeshProUGUI BloodCounterText;

    private void Awake()
    {
        BloodBar.value = BloodCounter;
    }

    private void FixedUpdate()
    {
        MoskitCounter = moskitoHouse.UnitContainer.Length;
        TimerText.text = "Time: " + (Timer += Time.deltaTime).ToString("00.00");
        MoskitCounterText.text = MoskitCounter.ToString();
        BloodCounterText.text = BloodCounter.ToString();
    }

    public void BloodCollect(float localCount)
    {
        if (BloodCounter < 0f)
        {
            BloodCounter = 0f;
        }
        else if (BloodCounter > MaxBloodCount)
        {
            BloodCounter = MaxBloodCount;
        }
        BloodCounter += localCount;
        BloodBar.value = BloodCounter;
    }
}
