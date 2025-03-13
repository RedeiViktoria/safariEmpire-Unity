using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;

public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button shopBtn;
    public Button safariBtn;
    public GameObject timeButtons;
    public GameObject shop;
    public GameObject safari;
    public GameObject menu;
    public GameObject vadorok;
    public GameObject security;
    void Start()
    {
        shopBtn.onClick.AddListener(OnShopClicked);
        safariBtn.onClick.AddListener(OnSafariClicked);
        
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

        Transform switchButtons = safari.transform.Find("Buttons");
        int i = 0;
        foreach (Transform child in switchButtons.transform)
        {
            int btnIndex = i;
            Button button = child.GetComponent<Button>();
            button.onClick.AddListener(() => OnSwitchClicked(btnIndex));
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnShopClicked()
    {
        shop.SetActive(!shop.activeSelf);
        safari.SetActive(false);
    }
    void OnSafariClicked()
    {
        safari.SetActive(!safari.activeSelf);
        shop.SetActive(false);
    }
    void OnTimeClicked(int timeSpeed)
    {
        Debug.Log(timeSpeed);
    }
    void OnSwitchClicked(int id)
    {
        Transform btns = safari.transform.Find("Buttons");
        for (int i = 0; i < 3; i++)
        {
            if (id==i)
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
            vadorok.SetActive(false);
            security.SetActive(false);
        }
        else if(id == 1)
        {
            menu.SetActive(false);
            vadorok.SetActive(true);
            security.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
            vadorok.SetActive(false);
            security.SetActive(true);
        }
    }
    void OnBuyClicked()
    {
        Debug.Log("clicked2");
    }
}
