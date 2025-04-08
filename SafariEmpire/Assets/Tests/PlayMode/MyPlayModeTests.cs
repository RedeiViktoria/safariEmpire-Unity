using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class MyPlayModeTests
{
    [UnityTest]
    public IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(1);
        Assert.IsTrue(true);
    }
}
