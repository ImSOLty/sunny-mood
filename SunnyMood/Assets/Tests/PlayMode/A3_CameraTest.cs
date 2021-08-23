using System.Collections;
using WindowsInput;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using WindowsInput;
using WindowsInput.Native;

public class A3_CameraTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;

    [UnityTest, Order(1)]
    public IEnumerator CameraFollowCheck()
    {
        Time.timeScale = 40;
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        GameObject.Destroy(GameObject.Find("LevelEnd"));
        player = GameObject.Find("Player");
        yield return null;
        SpriteRenderer playerSR = player.GetComponent<SpriteRenderer>();
        Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(3);
        Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        yield return new WaitForSeconds(3);
        Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        yield return new WaitForSeconds(3);
        Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        yield return new WaitForSeconds(3);
        Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Assert.True(playerSR.isVisible,"Player should be always visible by camera!");
        }
    }
}