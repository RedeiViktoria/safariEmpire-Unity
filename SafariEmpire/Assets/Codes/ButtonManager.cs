using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using UnityEngine.EventSystems;
using System;

public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Model model;
    public Button shopBtn;
    public Button safariBtn;
    public Button exitBtn;
    public GameObject layout;
    public GameObject timeButtons;
    public GameObject shop;
    public GameObject safari;
    public GameObject menu;
    public GameObject poachers;
    public GameObject security;
    public GameObject priceInput;
    private bool isPlacing;
    private string toBuy;
    void Start()
    {
        //Adding listeners to open the shop and the safari menu, and close if something else.
        shopBtn.onClick.AddListener(OnShopClicked);
        safariBtn.onClick.AddListener(OnSafariClicked);
        exitBtn.onClick.AddListener(OnPlacementExitClicked);
        layout.GetComponent<Button>().onClick.AddListener(OnLayoutClicked);

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

        //Adding listeners to poacher target buttons
        foreach (Transform parent in poachers.transform)
        {
            foreach (Transform child in parent)
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OnPoacherTargetClicked());
                }
            }
        }

        //Adding listeners to security path buttons
        foreach (Transform parent in security.transform)
        {
            foreach (Transform child in parent)
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OnSecurityPathClicked());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // Get the mouse position in screen coordinates
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    model.buy(toBuy, mousePosition);
                }
                //DetectObjectUnderMouse2D(); good for detecting 2d objects with colliders
            }
        }
    }
    void DetectObjectUnderMouse2D()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Clicked on: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("No object clicked");
        }
    }

    void OnShopClicked()
    {
        shop.SetActive(!shop.activeSelf);
        safari.SetActive(false);
    }
    void OnSetPriceClicked()
    {
        int price = int.Parse(priceInput.GetComponent<TMP_InputField>().text);
        model.setTicketPrice(price);
    }
    void OnSafariClicked()
    {
        safari.SetActive(!safari.activeSelf);
        priceInput.GetComponent<TMP_InputField>().text = model.getTicketPrice().ToString();
        shop.SetActive(false);
    }
    void OnLayoutClicked()
    {
        safari.SetActive(false);
        shop.SetActive(false);
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
            poachers.SetActive(false);
            security.SetActive(false);
        }
        else if (id == 1)
        {
            menu.SetActive(false);
            poachers.SetActive(true);
            security.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
            poachers.SetActive(false);
            security.SetActive(true);
        }
    }
    void OnShopButtonClicked(string name)
    {
        if (model.canBuy(name))
        {
            isPlacing = true;
            toBuy = name;
            shop.SetActive(false);
            exitBtn.gameObject.SetActive(true);
            layout.GetComponent<Image>().raycastTarget = false;
        }
    }

    void OnPoacherTargetClicked()
    {
        Debug.Log("Poacher clicked");
    }

    void OnSecurityPathClicked()
    {
        Debug.Log("Security path clicked");
    }

    void OnPlacementExitClicked()
    {
        layout.GetComponent<Image>().raycastTarget = true;
        isPlacing = false;
        exitBtn.gameObject.SetActive(false);
    }
}
