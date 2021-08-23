using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using WindowsInput;
using WindowsInput.Native;
using UnityEngine.UI;

public class F1_SoundTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player, gem;
    private AudioSource sTheme, sJump, sGem, sButton;
    [UnityTest, Order(0)]
    public IEnumerator InGameSoundCheck()
    {
        yield return null;
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return new WaitForSeconds(1);
        player = GameObject.Find("Player");
        gem = GameObject.FindWithTag("Gem");
        PMHelper.deleteEnemies();
        PMHelper.deletePlatforms();
        yield return null;
        sTheme = PMHelper.soundExist("Theme");
        if (sTheme == null) 
        {
            Assert.Fail("\"Theme\" AudioSource should exist");
        }

        if (!sTheme.isPlaying)
        {
            Assert.Fail("\"Theme\" sound should be playing unstoppable after scene was loaded");
        }

        if (!sTheme.loop)
        {
            Assert.Fail("\"Theme\" sound should be looped");
        }

        yield return null;
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return null;
        
        sJump = PMHelper.soundExist("Jump");
        if (sJump == null) 
        {
            Assert.Fail("\"Jump\" AudioSource should exist");
        }
        
        if (!sJump.isPlaying)
        {
            Assert.Fail("\"Jump\" sound should be played after player's jump is performed");
        }

        if (sJump.loop)
        {
            Assert.Fail("\"Jump\" sound should not be looped");
        }

        yield return new WaitForSeconds(2);
        if (sJump.isPlaying)
        {
            Assert.Fail("\"Jump\" sound should be played with less duration");
        }
        
        yield return null;
        player.transform.position = gem.GetComponent<Collider2D>().bounds.center;
        yield return null;
        sGem = PMHelper.soundExist("PickGem");
        if (sGem == null) 
        {
            Assert.Fail("\"PickGem\" AudioSource should exist");
        }

        yield return new WaitForSeconds(0.1f);

        if (!sGem.isPlaying)
        {
            Assert.Fail("\"PickGem\" sound should be played after player has taken gem");
        }

        if (sGem.loop)
        {
            Assert.Fail("\"PickGem\" sound should not be looped");
        }
    }

    [UnityTest, Order(0)]
    public IEnumerator SoundCheck()
    {
        yield return null;
        SceneManager.LoadScene("Main Menu");
        yield return null;
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        sButton = PMHelper.soundExist("Button");
        if (sButton == null) Assert.Fail("\"Button\" AudioSource should exist");
        yield return null;
        if (!sButton.isPlaying) Assert.Fail("\"Button\" sound should be played after player has taken gem");
        if (sButton.loop) Assert.Fail("\"Button\" sound should not be looped");
    }
}