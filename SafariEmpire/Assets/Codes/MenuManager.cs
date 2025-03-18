using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button easyBtn;
    public Button mediumBtn;
    public Button hardBtn;
    public Button exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        easyBtn.onClick.AddListener(() => { StartGame(1); });
        mediumBtn.onClick.AddListener(() => { StartGame(2); });
        hardBtn.onClick.AddListener(() => { StartGame(3); });
        exit.onClick.AddListener(() => { Application.Quit(); });
    }

    public void StartGame(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGame");
    }

}
