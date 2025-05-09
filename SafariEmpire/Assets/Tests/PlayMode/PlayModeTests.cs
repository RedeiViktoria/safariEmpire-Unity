using System.Collections;
using Codes.animal;
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
    public IEnumerator moveTest()
    {
        model.buy("cheetah", new Vector2(0, 0));
        GameObject gepard = GameObject.Find("CheetahObject(Clone)");
        Vector2 position1 = gepard.transform.position;
        yield return new WaitForSeconds(1f);
        Vector2 position2 = gepard.transform.position;

        Assert.AreNotEqual(position2, position1);
        Debug.Log("Test result(moveTest): PASSED");
    }
    /*
    [UnityTest]
    public IEnumerator poacherVisibilityTest()
    {
        model.makePoacher();
        Poacher poacher = model.poachers[0];
        model.buy("camera", new Vector2(10, 10));
        poacher.obj.transform.position = new Vector2(10, 10);
        poacher.targetPosition = new Vector2(10.1f, 10.1f);
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(poacher.visible, true);
        Debug.Log("Test result(poacherVisibilityTest): PASSED");
    }*/
    [UnityTest]
    public IEnumerator makePoacherTest()
    {
        int count1 = model.poachers.Count;
        model.makePoacher();
        int count2 = model.poachers.Count;

        Assert.AreEqual(count1 + 1, count2, "Poacher not found");
        Debug.Log("Test result(makePoacherTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator detectRangerOrJeepTest()
    {
        model.buyRanger("id");
        Ranger ranger = model.rangers[0];
        ranger.obj.transform.position = new Vector2(10, 10);
        bool detect = model.detectRangerOrJeep(new Vector2(10, 10), 5);

        Assert.AreEqual(detect, true, "Ranger not found");
        Debug.Log("Test result(detectRangerOrJeepTest): PASSED");
        return null;
    }

    [UnityTest]
    public IEnumerator detectAnimalTest()
    {
        model.buy("crocodile", new Vector2(10, 10));
        AnimalGroup group = model.detectAnimal(Codes.animal.AnimalType.Crocodile, new Vector2(10, 10), 5);

        Assert.IsNotNull(group, "AnimalGroup not found");
        Debug.Log("Test result(detectAnimalTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator detectPoacherTest()
    {
        model.makePoacher();
        Poacher poacher = model.poachers[0];
        poacher.obj.transform.position = new Vector2(10, 10);
        Poacher detect = model.detectPoacher(new Vector2(10, 10), 5);

        Assert.IsNotNull(detect, "Poacher not found");
        Debug.Log("Test result(detectPoacherTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator detectAnyAnimalTest()
    {
        model.buy("crocodile", new Vector2(10, 10));
        AnimalGroup group = model.detectAnyAnimal(new Vector2(10, 10));

        Assert.IsNotNull(group, "AnimalGroup not found");
        Debug.Log("Test result(detectAnyAnimalTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator canBuyTest()
    {
        model.money = 90;
        bool canBuy = model.canBuy("jeep");

        Assert.AreEqual(canBuy, false);
        Debug.Log("Test result(canBuyTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator buyTest()
    {
        int treeCount1 = 0;
        foreach(Plant plant in model.plants)
        {
            if (plant.GetType() == typeof(Tree))
            {
                ++treeCount1;
            }
        }
        model.buy("tree", new Vector2(10, 10));
        int treeCount2 = 0;
        foreach (Plant plant in model.plants)
        {
            if (plant.GetType() == typeof(Tree))
            {
                ++treeCount2;
            }
        }

        Assert.AreEqual(treeCount1 + 1, treeCount2);
        Debug.Log("Test result(buyTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator buyRangerTest()
    {
        int count1 = model.rangers.Count;
        model.buyRanger("id");
        int count2 = model.rangers.Count;

        Assert.AreEqual(count1 + 1, count2);
        Debug.Log("Test result(buyRangerTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator sellRangerTest()
    {
        model.buyRanger("id");
        int count1 = model.rangers.Count;
        model.sellRanger("id");
        int count2 = model.rangers.Count;

        Assert.AreEqual(count2 + 1, count1);
        Debug.Log("Test result(sellRangerTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator rangerTargetChangeTest()
    {
        model.buyRanger("id");
        Ranger ranger = model.rangers[0];
        int target1 = ranger.target;
        model.rangerTargetChange("id");
        int target2 = ranger.target;

        Assert.AreNotEqual(target1, target2);
        Debug.Log("Test result(rangerTargetChangeTest): PASSED");
        return null;
    }
    [UnityTest]
    public IEnumerator payCheckTest()
    {
        model.buyRanger("id");
        int money1 = model.money;
        model.payCheck();
        int money2 = model.money;

        Assert.Greater(money1, money2);
        Debug.Log("Test result(payCheckTest): PASSED");
        return null;
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
