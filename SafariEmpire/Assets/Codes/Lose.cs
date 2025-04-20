using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lose : MonoBehaviour
{
    public Button newGameBtn;
    public Button exit;
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int weeks = PlayerPrefs.GetInt("weeks");
        int days = PlayerPrefs.GetInt("days");
        int hours = PlayerPrefs.GetInt("hours");
        this.text.text = weeks + " hét\n" + days + " nap\n" + hours + " óra";
        newGameBtn.onClick.AddListener(() => { StartGame(); });
        exit.onClick.AddListener(() => { Application.Quit(); });
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
