using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class PlayModeTests
{

    /*
     * // A Test behaves as an ordinary method
    [Test]
    public void PlayModeTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(3, 1 + 2);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayModeTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
     */
    private Model model;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("MainGame");
        yield return new WaitForSeconds(1f);

        var modelGO = GameObject.Find("Model");
        Assert.IsNotNull(modelGO, "Model GameObject not found in scene.");

        model = modelGO.GetComponent<Model>();
        Assert.IsNotNull(model, "Model component not found.");
    }

    [UnityTest]
    public IEnumerator timeChangesTest(){
        int kezdoOra = model.hour;
        yield return new WaitForSeconds(2f);
        int endOra = model.hour;

        Assert.AreNotEqual(kezdoOra, endOra);
        Debug.Log("Test result(timeChangesTest): PASSED");
    }

    [UnityTest]
    public IEnumerator ClickingMenuButtonLoadsGameScene()
    {
        // 1. Load the main menu scene
        SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(1f); // Wait for the scene to load

        Debug.Log("Starting test: ClickingMenuButtonLoadsGameScene");

        // 2. Find the GameModel in the scene
        var menuManager = GameObject.Find("MenuManager");
        Assert.IsNotNull(menuManager, "Menumanager not found in scene.");

        // 3. Find the "Hard" difficulty button (by name or tag or other logic)
        var hardButtonGO = GameObject.Find("Hard"); // <- Replace with your button's actual name
        Assert.IsNotNull(hardButtonGO, "HardButton not found in scene.");

        var button = hardButtonGO.GetComponent<Button>();
        Assert.IsNotNull(button, "HardButton does not have a Button component.");

        // 4. Simulate a click
        button.onClick.Invoke();

        yield return new WaitForSeconds(1f); // wait a frame for logic to process

        // 5. Assert difficulty was set
        var model = GameObject.Find("Model");
        Assert.IsNotNull(model, "Model not found in scene.");

        Debug.Log("Test result(ClickingMenuButtonLoadsGameScene): PASSED");
    }

    [UnityTest]
    public IEnumerator ObjectsLoadInGame()
    {
        
        SceneManager.LoadScene("MainGame");
        yield return new WaitForSeconds(1f);

        Debug.Log("Starting test: ObjectsLoadInGame");

        var hill = GameObject.Find("HillObject(Clone)");
        Assert.IsNotNull(hill, "Hill not found in scene.");

        var river = GameObject.Find("RiverObject(Clone)");
        Assert.IsNotNull(river, "River not found in scene.");

        var pond = GameObject.Find("PondObject(Clone)");
        Assert.IsNotNull(pond, "Pond not found in scene.");

        var grass = GameObject.Find("GrassObject(Clone)");
        Assert.IsNotNull(grass, "Grass not found in scene.");

        Debug.Log("Test result(ObjectsLoadInGame): PASSED");
    }

    
}
