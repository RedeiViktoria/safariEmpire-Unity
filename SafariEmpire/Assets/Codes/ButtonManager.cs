using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using UnityEngine.EventSystems;
using System;
using Codes.animal;
using JetBrains.Annotations;

public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Model model;
    public Button shopBtn;
    public Button safariBtn;
    public Button exitBtn;
    public Button WinButton;
    public Button buyDrone;
    public Button buyAirBalloon;
    public Button setDrone;
    public Button setAirBalloon;
    public GameObject layout;
    public GameObject timeButtons;
    public GameObject shop;
    public GameObject safari;
    public GameObject menu;
    public GameObject rangers;
    public GameObject security;
    public GameObject priceInput;
    public GameObject winConditions;
    public GameObject animalStats;
    public TextMeshProUGUI animalTypeTEXT;
    public TextMeshProUGUI femaleCountTEXT;
    public TextMeshProUGUI maleCountTEXT;
    public TextMeshProUGUI currentActivityTEXT;
    private bool isPlacing;
    private string toBuy;
    private bool isAnimalCheckable;
    private bool shouldIgnoreNextLayoutClick = false;
    private bool animalStatsOpen;
    private AnimalGroup lastGroup;

    private bool hasDrone = false;
    private bool hasAirBalloon = false;
    void Start()
    {
        //Adding listeners to open the shop and the safari menu, and close if something else.
        shopBtn.onClick.AddListener(OnShopClicked);
        safariBtn.onClick.AddListener(OnSafariClicked);
        exitBtn.onClick.AddListener(OnPlacementExitClicked);
        layout.GetComponent<Button>().onClick.AddListener(OnLayoutClicked);
        WinButton.onClick.AddListener(OnWinClicked);
        this.isAnimalCheckable = true;
        this.animalStatsOpen = false;
        buyDrone.onClick.AddListener(OnBuyDroneClicked);
        buyAirBalloon.onClick.AddListener(OnAirBalloonClicked);
        setDrone.onClick.AddListener(OnSetDroneClicked);
        setAirBalloon.onClick.AddListener(OnSetAirBalloonClicked);

        //Adding listeners to the time buttons in the mainUI
        foreach (Transform child in timeButtons.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                string text = button.GetComponentInChildren<TextMeshProUGUI>().text;
                if (text == "Óra")
                {
                    button.onClick.AddListener(() => OnTimeClicked(1));
                }
                else if (text == "Nap")
                {
                    button.onClick.AddListener(() => OnTimeClicked(2));
                }
                else if (text == "Hét")
                {
                    button.onClick.AddListener(() => OnTimeClicked(3));
                }

            }
        }

        //Security system
        


        //Adding listeners to switch buttons in safari menu
        Transform switchButtons = safari.transform.Find("Buttons");
        int i = 0;
        foreach (Transform child in switchButtons.transform)
        {
            int btnIndex = i;
            Button button = child.GetComponent<Button>();
            button.onClick.AddListener(() => OnSwitchClicked(btnIndex));
            i += 1;
        }

        //Adding listeners to shop buttons
        foreach (Transform parent in shop.transform)
        {
            foreach (Transform child in parent)
            {
                Button button = child.GetComponent<Button>();

                if (button != null)
                {
                    Button capturedButton = button;
                    capturedButton.onClick.AddListener(() => OnShopButtonClicked(parent.name));
                }
            }
        }

        //Adding listener to ticket price setter button
        foreach (Transform child in menu.transform)
        {
            Button priceButton = child.GetComponent<Button>();
            if (priceButton != null)
            {
                priceButton.onClick.AddListener(OnSetPriceClicked);
            }
        }

        //Adding listeners to ranger target buttons
        foreach (Transform parent in rangers.transform)
        {
            Button targetButton = null; // ide mentjük a target gombot

            foreach (Transform child in parent)
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    if (button.name == "target")
                    {
                        button.GetComponent<Image>().color = Color.gray;
                        targetButton = button; // elmentjük a target gombot

                        button.onClick.AddListener(() =>
                        {
                            int felirat = model.rangerTargetChange(parent.name);
                            switch (felirat)
                            {
                                case 0: button.GetComponentInChildren<TextMeshProUGUI>().text = "Orvvadász"; break;
                                case 1: button.GetComponentInChildren<TextMeshProUGUI>().text = "Gepárd"; break;
                                case 2: button.GetComponentInChildren<TextMeshProUGUI>().text = "Krokodil"; break;
                            }
                        });
                    }
                    else
                    {
                        button.GetComponentInChildren<TextMeshProUGUI>().text = "Megvesz";

                        // másik gombra (nem target), viszont targetButton kelleni fog
                        Button thisButton = button; // fontos a closure miatt

                        button.onClick.AddListener(() =>
                        {
                            var textComponent = thisButton.GetComponentInChildren<TextMeshProUGUI>();

                            if (textComponent.text == "Megvesz")
                            {
                                model.buyRanger(parent.name);
                                textComponent.text = "Eladás";
                                button.GetComponent<Image>().color = new Color(0.8553459f, 0.5890589f, 0.6054696f);

                                if (targetButton != null)
                                {
                                    targetButton.GetComponentInChildren<TextMeshProUGUI>().text = "Orvvadász";
                                    targetButton.GetComponent<Image>().color = Color.white;
                                }
                            }
                            else
                            {
                                model.sellRanger(parent.name);
                                textComponent.text = "Megvesz";
                                button.GetComponent<Image>().color = Color.green;

                                if (targetButton != null)
                                {
                                    targetButton.GetComponentInChildren<TextMeshProUGUI>().text = "Célpont";
                                    targetButton.GetComponent<Image>().color = Color.gray;
                                }
                            }
                        });
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacing)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // Get the mouse position in screen coordinates
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    model.buy(toBuy, mousePosition);
                }
            } else if(this.isAnimalCheckable)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                AnimalGroup group = model.detectAnyAnimal(mousePosition);
                if (group!=null)
                {
                    animalStats.SetActive(true);
                    switch (group.animalType)
                    {
                        case AnimalType.Gepard:  this.animalTypeTEXT.text = "Gepárdok"; break;
                        case AnimalType.Hippo:  this.animalTypeTEXT.text = "Vízilovak"; break;
                        case AnimalType.Gazella:  this.animalTypeTEXT.text = "Gazellák"; break;
                        case AnimalType.Crocodile:  this.animalTypeTEXT.text = "Krokodilok"; break;
                    }
                    this.femaleCountTEXT.text = "Nõstények: " + group.femaleCount + " db";
                    this.maleCountTEXT.text = "Hímek: " + group.maleCount + " db";
                    this.currentActivityTEXT.text = group.currentActivity;
                    shouldIgnoreNextLayoutClick = true;
                    this.animalStatsOpen = true;
                    this.lastGroup = group;
                }
            }
        }
        if (animalStatsOpen)
        {
            if (this.lastGroup != null)
            {
                this.femaleCountTEXT.text = "Nõstények: " + this.lastGroup.femaleCount + " db";
                this.maleCountTEXT.text = "Hímek: " + this.lastGroup.maleCount + " db";
                this.currentActivityTEXT.text = this.lastGroup.currentActivity;
            }
        }

    }
    //SECURITY SYSTEM
    void OnBuyDroneClicked()
    {
        model.buy("drone", new Vector2(0, 1));
        TextMeshProUGUI droneText = buyDrone.GetComponentInChildren<TextMeshProUGUI>();
        droneText.text = "Megvéve";
        droneText.color = Color.green;
        buyDrone.GetComponent<Image>().color = Color.black;
        this.hasDrone = true;
    }
    void OnAirBalloonClicked()
    {
        model.buy("airballoon", new Vector2(0, 1));
        TextMeshProUGUI airballoonText = buyAirBalloon.GetComponentInChildren<TextMeshProUGUI>();
        airballoonText.text = "Megvéve";
        airballoonText.color = Color.green;
        buyAirBalloon.GetComponent<Image>().color = Color.black;
        this.hasAirBalloon = true;
    }
    void OnSetDroneClicked()
    {
        if (this.hasDrone)
        {
            model.setDronePath();
            TextMeshProUGUI droneText = setDrone.GetComponentInChildren<TextMeshProUGUI>();
            switch (droneText.text)
            {
                case "1. Útvonal": droneText.text = "2. Útvonal"; break;
                case "2. Útvonal": droneText.text = "3. Útvonal"; break;
                case "3. Útvonal": droneText.text = "1. Útvonal"; break;
            }
        }
    }
    void OnSetAirBalloonClicked()
    {
        if (this.hasAirBalloon)
        {
            model.setAirBalloonPath();
            TextMeshProUGUI airBalloonText = setAirBalloon.GetComponentInChildren<TextMeshProUGUI>();
            switch (airBalloonText.text)
            {
                case "1. Útvonal": airBalloonText.text = "2. Útvonal"; break;
                case "2. Útvonal": airBalloonText.text = "3. Útvonal"; break;
                case "3. Útvonal": airBalloonText.text = "1. Útvonal"; break;
            }
        }
    }

    void OnShopClicked()
    {
        shop.SetActive(true);
        safari.SetActive(false);
        winConditions.SetActive(false);
        animalStats.SetActive(false);
        this.animalStatsOpen = false;
        this.isAnimalCheckable = false;
        OnPlacementExitClicked();
    }
    void OnSetPriceClicked()
    {
        int price = int.Parse(priceInput.GetComponent<TMP_InputField>().text);
        model.setTicketPrice(price);
    }
    void OnSafariClicked()
    {
        safari.SetActive(true);
        priceInput.GetComponent<TMP_InputField>().text = model.getTicketPrice().ToString();
        shop.SetActive(false);
        winConditions.SetActive(false);
        animalStats.SetActive(false);
        this.animalStatsOpen = false;
        this.isAnimalCheckable = false;
        OnPlacementExitClicked();
    }
    void OnLayoutClicked()
    {
        if (shouldIgnoreNextLayoutClick)
        {
            shouldIgnoreNextLayoutClick = false; // lenyeltük az elsõ kattintást
            return;
        }
        safari.SetActive(false);
        shop.SetActive(false);
        winConditions.SetActive(false);
        animalStats.SetActive(false);
        this.animalStatsOpen = false;
        this.isAnimalCheckable = true;
    }
    void OnWinClicked()
    {
        winConditions.SetActive(true);
        safari.SetActive(false);
        shop.SetActive(false);
        animalStats.SetActive(false);
        this.isAnimalCheckable = false;
        OnPlacementExitClicked();
    }
    void OnTimeClicked(int timeSpeed)
    {
        model.setTimeSpeed(timeSpeed);
    }
    void OnSwitchClicked(int id)
    {
        Transform btns = safari.transform.Find("Buttons");
        for (int i = 0; i < 3; i++)
        {
            if (id == i)
            {
                btns.GetChild(i).GetComponent<Image>().color = Color.green;
            }
            else
            {
                btns.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }


        if (id == 0)
        {
            menu.SetActive(true);
            rangers.SetActive(false);
            security.SetActive(false);
        }
        else if (id == 1)
        {
            menu.SetActive(false);
            rangers.SetActive(true);
            security.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
            rangers.SetActive(false);
            security.SetActive(true);
        }
    }
    void OnShopButtonClicked(string name)
    {
        if (name == "jeep" && model.canBuy(name))
        {
            model.buy(name, new Vector2(0,0));
            return;
        }
        if (model.canBuy(name))
        {
            isPlacing = true;
            toBuy = name;
            shop.SetActive(false);
            exitBtn.gameObject.SetActive(true);
            layout.GetComponent<Image>().raycastTarget = false;
        }
    }

    void OnPlacementExitClicked()
    {
        layout.GetComponent<Image>().raycastTarget = true;
        isPlacing = false;
        exitBtn.gameObject.SetActive(false);
    }
}
