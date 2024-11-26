using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    const int WHEAT = 100;
    const float COOLDOWNwHEAT = 20f;
    const float COOLDOWNHIRINGPEASANT = 5f;
    const float COOLDOWNLEVELPEASANT = 4f;
    const float COOLDOWNKNIGHTS = 4f;
    const float COOLDOWNLEVELKNIGHTS = 3f;
    const float COOLDOWNEATING = 22f;
    const float COOLDOWNBARARIANS = 5.9f;


    // Пшеница
    public int Wheat;
    public Text[] TextQuantityofWheat;
    public float CooldownWheat;
    private float TimerWheat;
    private bool activeTimerWheat = false;

    // Крестьяне
    public int Peasant;
    public Text[] TextPeasant;
    public float CooldownHiringPeasant;
    private float TimerHiringPeasant;
    private bool activeTimerHiringPeasant = true;

    public int LevelPeasant;
    public Text TextPeasantLevel;
    public float CooldownLevelPeasant;
    private float TimerLevelPeasant;
    private bool activeTimerLevelPeasant = true;

    // Рыцари
    public int Knights;
    public Text[] TextKnights;
    public float CooldownKnights;
    private float TimerKnights;
    private bool activeTimerKnights = true;

    public int LevelKnights;
    public Text TextLevelKnights;
    public float CooldownLevelKnights;
    private float TimerLevelKnights;
    private bool activeTimerLevelKnights = true;

    public float CooldownEating;
    private float TimerEating;
    private bool activeTimerEating = true;

    public Text[] TextPowerKnights;

    //Варвары
    public int Barbarians;
    public Text[] TextBarbarians;
    public float CooldownBarbarians;
    private float TimerBarbarians;
    private bool activeTimerBarbarians = true;
    public Text[] TextBarbariansTimer;
    public Text[] TextPowerBarbarians;

    private int CountRide = 0;
    public Text TextCountRide;
    public Text TextPanelRaid;

    // Кнопки
    public Button addPeasantButton;
    public Button ButtonLearn;
    public Button addKnightsButton;
    public Button ButtonLearnKnights;

    // Индикаторы
    public Image Stamina1;
    public Image Stamina2;
    public Image GoldStamina;
    public Image Stamina3;
    public Image Stamina4;

    // Панели для отображения
    public GameObject PanelLevelPeasant;
    public GameObject PanelEndWheat;
    public GameObject PanelRaid;
    public GameObject MainMenu;
    public GameObject Win;
    public GameObject BadEnd;
   

    // -----Инициализация-------
    void Start()
    {
        MainMenu.SetActive(true);
        RestartGame();
    }

    public void StopGame()
    {
       activeTimerWheat = false;
       activeTimerEating = false;
       activeTimerHiringPeasant = false;
       activeTimerLevelPeasant = false;
       activeTimerKnights = false;
       activeTimerLevelKnights = false;
       activeTimerBarbarians = false;
    }

        public void ResumeGame()
    {
       activeTimerWheat = true;
       activeTimerEating = true;
       activeTimerHiringPeasant = true;
       activeTimerLevelPeasant = true;
       activeTimerKnights = true;
       activeTimerLevelKnights = true;
       activeTimerBarbarians = true;
    }

    public void RestartGame()
    {
        Wheat = WHEAT;
        Peasant = 0;
        Knights = 0;
        Barbarians = 0;
        CountRide = 0;
        LevelPeasant = 0;
        LevelKnights = 0;
        CooldownWheat = COOLDOWNwHEAT;
        CooldownHiringPeasant = COOLDOWNHIRINGPEASANT;
        CooldownLevelPeasant = COOLDOWNLEVELPEASANT;
        CooldownKnights = COOLDOWNKNIGHTS;
        CooldownLevelKnights = COOLDOWNLEVELKNIGHTS;
        CooldownEating = COOLDOWNEATING;
        CooldownBarbarians = COOLDOWNBARARIANS;
        TimerWheat = CooldownWheat;
        TimerEating = CooldownEating;
        TimerHiringPeasant = 0f;
        TimerLevelPeasant = 0f;
        TimerKnights = 0f;
        TimerLevelKnights = 0f;
        TimerBarbarians = CooldownBarbarians;

        addPeasantButton.interactable = true;
        ButtonLearn.interactable = true;
        addKnightsButton.interactable = true;
        ButtonLearnKnights.interactable = true;
        Stamina1.fillAmount = 0f;
        Stamina2.fillAmount = 0f;
        Stamina3.fillAmount = 0f;
        Stamina4.fillAmount = 0f; 
        GoldStamina.fillAmount = 0f;
        ResumeGame();
        activeTimerWheat = false;
        UpdateUI();
    }

    // Конец игры
    void EndGame()
    {
        if (Wheat <= 0)
        {
            TimerWheat = 0f;
            TimerEating = 0f;
            TimerHiringPeasant = 0f;
            TimerLevelPeasant = 0f;
            TimerKnights = 0f;
            TimerLevelKnights = 0f;

            if (PanelEndWheat != null)
            {
                PanelEndWheat.SetActive(true);
                StopGame();

            }
        }
    }
    void WinGame()
    {
        if(Wheat >= 1000)
        {
            Win.SetActive(true);
            RestartGame();
        }

    }

    // -----Обновление пшеницы-------
    void UpdateWheat()
    {
        EndGame();
        if (TimerWheat > 0f && activeTimerWheat)
        {
            TimerWheat -= Time.deltaTime;

            if (TimerWheat <= 0f)
            {
                TimerWheat = CooldownWheat;
                Wheat += Peasant;

                UpdateUI();

                if (TimerHiringPeasant <= 0f)
                {
                    addPeasantButton.interactable = true;
                }
                if (TimerLevelPeasant <= 0f && NewCooldownWheat() > 1f)  
                {
                    ButtonLearn.interactable = true;
                }
            }
            else
            {
                GoldStamina.fillAmount = 1 - (TimerWheat / CooldownWheat);
            }
        }
    }

    // Уменьшение пшеницы
    void IncWheat(int CountWheat)
    {
        Wheat -= CountWheat;
        if (Wheat <= 0)
        {
            addPeasantButton.interactable = false;
            ButtonLearn.interactable = false;
            addKnightsButton.interactable = false;
            ButtonLearnKnights.interactable = false;
            Wheat = 0;
        }
    }

    // -----Добавление крестьян-------
    public void AddPeasant()
    {
        IncWheat(1);


        TimerHiringPeasant = CooldownHiringPeasant;
        addPeasantButton.interactable = false;
        activeTimerWheat = true;

        Peasant++;

        UpdateUI();
    }

    void UpdatePeasantCooldown()
    {
        if (TimerHiringPeasant > 0f && activeTimerHiringPeasant)
        {
            TimerHiringPeasant -= Time.deltaTime;

            if (TimerHiringPeasant <= 0f)
            {
                Stamina1.fillAmount = 0f;
                if (Wheat > 0)
                {
                    addPeasantButton.interactable = true;
                }
            }
            else
            {
                Stamina1.fillAmount = 1 - (TimerHiringPeasant / CooldownHiringPeasant);
            }
        }
    }

    // Откат пшеницы
    float NewCooldownWheat()
    {
        return CooldownWheat - (LevelPeasant + 1);
    }

    // -----Обновление уровня крестьян-------
    public void addLevel()
    {
        var NewCooldown = NewCooldownWheat();
        if (NewCooldown > 1f)
        {
            CooldownLevelPeasant += (LevelPeasant * 10);

            CooldownWheat = NewCooldown; 
            LevelPeasant++;

            IncWheat(1);

            TimerLevelPeasant = CooldownLevelPeasant;

            ButtonLearn.interactable = false;

            UpdateUI();
        }
        else
        {
            if (PanelLevelPeasant != null)
            {
                PanelLevelPeasant.SetActive(true);
                StopGame();
            }
            ButtonLearn.interactable = false;
        }
    }

    // Обновление таймера уровня крестьян
    void UpdateLevelPeasant()
    {
        if (TimerLevelPeasant > 0f && activeTimerLevelPeasant)
        {
            TimerLevelPeasant -= Time.deltaTime;

            if (TimerLevelPeasant <= 0f)
            {
                Stamina2.fillAmount = 0f;
                if (Wheat > 0)
                {
                    ButtonLearn.interactable = true;
                }
            }
            else
            {
                Stamina2.fillAmount = 1 - (TimerLevelPeasant / CooldownLevelPeasant);
            }
        }
    }

    // -----Добавление рыцарей-------
    public void AddKnights()
    {
        IncWheat(2);

        TimerKnights = CooldownKnights;
        addKnightsButton.interactable = false;

        Knights++;

        UpdateUI();
    }

    void UpdateKnightsCooldown()
    {
        if (TimerKnights > 0f && activeTimerKnights)
        {
            TimerKnights -= Time.deltaTime;

            if (TimerKnights <= 0f)
            {
                Stamina3.fillAmount = 0f;
                if (Wheat > 0)
                {
                    addKnightsButton.interactable = true;
                }
            }
            else
            {
                Stamina3.fillAmount = 1 - (TimerKnights / CooldownKnights);
            }
        }
    }

    void UpdateCooldownEating()
    {
        if (TimerEating > 0f && activeTimerEating)
        {
            TimerEating -= Time.deltaTime;
            if (TimerEating <= 0f)
            {
                TimerEating = CooldownEating;
                IncWheat(Knights * 2);
                UpdateUI();
            }
        }
    }

    // Обновление уровня рыцарей
    public void addLevelKnight()
    {
        CooldownLevelKnights += LevelKnights * 2;
        LevelKnights++;

        IncWheat(2);
        TimerLevelKnights = CooldownLevelKnights;
        ButtonLearnKnights.interactable = false;
        UpdateUI();
    }

    void UpdateLevelKnights()
    {
        if (TimerLevelKnights > 0f && activeTimerLevelKnights)
        {
            TimerLevelKnights -= Time.deltaTime;

            if (TimerLevelKnights <= 0f)
            {
                Stamina4.fillAmount = 0f;
                if (Wheat > 0)
                {
                    ButtonLearnKnights.interactable = true;
                }
            }
            else
            {
                Stamina4.fillAmount = 1 - (TimerLevelKnights / CooldownLevelKnights);
            }
        }
    }

    int GetPower(int Power)
    {
        return Power * 123;

    }


    void UpdateCooldownBarbarians()
    {

        if (TimerBarbarians > 0f && activeTimerBarbarians)
        {
            TimerBarbarians -= Time.deltaTime;

            if (TimerBarbarians <= 0f)
            {
                Raid();
            }
            else
            {
                foreach (Text textElement in TextBarbariansTimer)
                {
                    textElement.text = Mathf.Floor(TimerBarbarians).ToString();
                }
            }
        }
    }

    bool VS()
    {
        if (Knights * (LevelKnights/2) > Barbarians)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Raid()
    {
        if ( CountRide == 2) 
        {
            Barbarians = 4;
        }
        if (CountRide >= 3)
        {
            
            if (VS())
            {
                PanelRaid.SetActive(true);
                TextPanelRaid.text = "Враги оказались слабее рыцарей. Вы выиграли битву!";
                StopGame();
            }
            else
            {
                PanelRaid.SetActive(true);
                TextPanelRaid.text = "Враги оказались сильнее рыцарей. Вы проиграли!";
                StopGame();
            }
        }
        TimerBarbarians = CooldownBarbarians;
        CountRide++;
        CooldownBarbarians += 5f;
        UpdateUI();
    }

    public void ClickPanelRaid()
    {
        if (VS())
        {
            if( Knights - Barbarians > 0)
            {
                Knights -= Barbarians;
            } 
            else
             {
                Knights = 0;
            }
            Barbarians += 4;
            PanelRaid.SetActive(false);
            ResumeGame();
            UpdateUI();
        }
        else
        {
            BadEnd.SetActive(true);
            RestartGame();
        }
    }

    // -----Обновление UI-------
    void UpdateUI()
    {
        // Пшеница
        foreach (Text textElement in TextQuantityofWheat)
        {
            textElement.text = Wheat.ToString();
        }

        // Крестьяне
        foreach (Text textElement in TextPeasant)
        {
            textElement.text = Peasant.ToString();
        }

        TextPeasantLevel.text = LevelPeasant.ToString();

        // Рыцари
        foreach (Text textElement in TextKnights)
        {
            textElement.text = Knights.ToString();
        }

        TextLevelKnights.text = LevelKnights.ToString();
        
        foreach (Text textElement in TextPowerKnights)
        {
            textElement.text = GetPower((LevelKnights/2)*Knights).ToString();
        }
        
        foreach (Text textElement in TextBarbarians)
        {
            textElement.text = Barbarians.ToString();
        }

        TextCountRide.text = CountRide.ToString();

        foreach (Text textElement in TextPowerBarbarians)
        {
            textElement.text = GetPower(Barbarians).ToString();
        }
        
    }


    // -----Обновление------
    void Update()
    {
        UpdateWheat();
        UpdatePeasantCooldown();
        UpdateLevelPeasant();
        UpdateKnightsCooldown();
        UpdateCooldownEating();
        UpdateLevelKnights();
        UpdateCooldownBarbarians();
        WinGame();
    }
}
