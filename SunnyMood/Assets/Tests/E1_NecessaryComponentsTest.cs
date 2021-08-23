using System.Collections;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using WindowsInput;
using WindowsInput.Native;
using Scene = UnityEditor.SearchService.Scene;

public class E1_NecessaryComponentsTest
{
    private GameObject canvas, chooselevel, play, exit, panel, close, level1, level2, level3;
    [UnityTest, Order(0)]
    public IEnumerator CanvasNecessary()
    {
        yield return null;
        PlayerPrefs.DeleteAll();
        yield return null;
        SceneManager.LoadScene("Main Menu");
        yield return null;
        canvas = PMHelper.Exist("Canvas");
        if (canvas == null)
        {
            Assert.Fail("There should be canvas on scene named \"Canvas\"");
        }
        if (PMHelper.Exist<RectTransform>(canvas) == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Canvas\" object");
        }
        if (PMHelper.Exist<Canvas>(canvas) == null)
        {
            Assert.Fail("There should be <Canvas> component on \"Canvas\" object");
        }
        if (PMHelper.Exist<CanvasScaler>(canvas) == null)
        {
            Assert.Fail("There should be <CanvasScaler> component on \"Canvas\" object");
        }
        if (PMHelper.Exist<GraphicRaycaster>(canvas) == null)
        {
            Assert.Fail("There should be <GraphicRaycaster  > component on \"Canvas\" object");
        }
    }
    
    [UnityTest, Order(1)]
    public IEnumerator ChooseLevelNecessary()
    {
        yield return null;
        chooselevel = PMHelper.Exist("ChooseLevel");
        if (chooselevel == null)
        {
            Assert.Fail("There should be button on scene named \"ChooseLevel\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(chooselevel);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"ChooseLevel\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"ChooseLevel\" object");
        }
        if (PMHelper.Exist<Button>(chooselevel) == null)
        {
            Assert.Fail("There should be <Button> component on \"ChooseLevel\" object");
        }
    }
    
    [UnityTest, Order(2)]
    public IEnumerator PlayNecessary()
    {
        yield return null;
        play = PMHelper.Exist("Play");
        if (play == null)
        {
            Assert.Fail("There should be button on scene named \"Play\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(play);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Play\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Play\" object");
        }
        if (PMHelper.Exist<Button>(play) == null)
        {
            Assert.Fail("There should be <Button> component on \"Play\" object");
        }
    }
    [UnityTest, Order(3)]
    public IEnumerator ExitNecessary()
    {
        yield return null;
        exit = PMHelper.Exist("Exit");
        if (exit == null)
        {
            Assert.Fail("There should be button on scene named \"Exit\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(exit);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Exit\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Exit\" object");
        }
        if (PMHelper.Exist<Button>(exit) == null)
        {
            Assert.Fail("There should be <Button> component on \"Exit\" object");
        }
    }

    [UnityTest, Order(4)]
    public IEnumerator PanelNecessary()
    {
        yield return null;
        chooselevel.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        panel = PMHelper.Exist("Panel");
        if (panel == null)
        {
            Assert.Fail("After pressing \"ChooseLevel\" button \"Panel\" object should become active!");
        }
        RectTransform rt = PMHelper.Exist<RectTransform>(panel);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Panel\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Panel\" object");
        }
    }
    
    [UnityTest, Order(5)]
    public IEnumerator CloseNecessary()
    {
        yield return null;
        close = PMHelper.Exist("Close");
        if (close == null)
        {
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Close\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(close);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Close\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Close\" object");
        }
        if (PMHelper.Exist<Button>(close) == null)
        {
            Assert.Fail("There should be <Button> component on \"Close\" object");
        }
    }
    
    [UnityTest, Order(6)]
    public IEnumerator Level1Necessary()
    {
        yield return null;
        level1 = PMHelper.Exist("Level 1");
        if (level1 == null)
        {
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 1\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(level1);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Level 1\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Level 1\" object");
        }
        if (PMHelper.Exist<Button>(level1) == null)
        {
            Assert.Fail("There should be <Button> component on \"Level 1\" object");
        }
    }
    
    [UnityTest, Order(7)]
    public IEnumerator Level2Necessary()
    {
        yield return null;
        level2 = PMHelper.Exist("Level 2");
        if (level2 == null)
        {
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 2\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(level2);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Level 2\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Level 2\" object");
        }
        if (PMHelper.Exist<Button>(level2) == null)
        {
            Assert.Fail("There should be <Button> component on \"Level 2\" object");
        }
    }

    [UnityTest, Order(8)]
    public IEnumerator Level3Necessary()
    {
        yield return null;
        level3 = PMHelper.Exist("Level 3");
        if (level3 == null)
        {
            Assert.Fail("There should be button on scene after pressing \"ChooseLevel\" button, named \"Level 3\"");
        }

        RectTransform rt = PMHelper.Exist<RectTransform>(level3);
        if (rt == null)
        {
            Assert.Fail("There should be <RectTransform> component on \"Level 3\" object");
        }

        bool ok = rt.anchorMin.x > 0 && rt.anchorMin.x < rt.anchorMax.x && rt.anchorMax.x < 1 &&
                  rt.anchorMin.y > 0 && rt.anchorMin.y < rt.anchorMax.y && rt.anchorMax.y < 1;
        if (!ok)
        {
            Assert.Fail("There is uncorrected settings in <RectTransform> under \"Level 3\" object");
        }

        if (PMHelper.Exist<Button>(level3) == null)
        {
            Assert.Fail("There should be <Button> component on \"Level 3\" object");
        }
    }

    [UnityTest, Order(9)]
    public IEnumerator CheckChildren()
    {
        yield return null;
        if (!PMHelper.Child(close, panel)) Assert.Fail("\"Close\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level1, panel)) Assert.Fail("\"Level 1\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level2, panel)) Assert.Fail("\"Level 2\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(level3, panel)) Assert.Fail("\"Level 3\" object should be a child of \"Panel\" object");
        if (!PMHelper.Child(panel, canvas)) Assert.Fail("\"Panel\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(play, canvas)) Assert.Fail("\"Play\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(exit, canvas)) Assert.Fail("\"Exit\" object should be a child of \"Canvas\" object");
        if (!PMHelper.Child(chooselevel, canvas)) Assert.Fail("\"Choose Level\" object should be a child of \"Canvas\" object");
    }

    [UnityTest, Order(10)]
    public IEnumerator PlayerTest()
    {
        Time.timeScale = 3;
        yield return null;
        bool correctButtonLevels = level1.GetComponent<Button>().interactable &&
                                   !level2.GetComponent<Button>().interactable &&
                                   !level3.GetComponent<Button>().interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("When player started a game first time - he should be able to choose only first level," +
                        " others should not be interactable");
        }
        close.GetComponent<Button>().onClick.Invoke();
        panel = PMHelper.Exist("Panel");
        if (panel != null) Assert.Fail("After pressing \"Close\" button - panel should be set as non active");
        yield return null;
        play.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            Assert.Fail("When player started a game first time - \"Play\" button should transfer him to Level 1");
        }

        InputSimulator IS = new InputSimulator();
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to Main Menu");
        }
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        correctButtonLevels = GameObject.Find("Level 1").GetComponent<Button>().interactable &&
                                   !GameObject.Find("Level 2").GetComponent<Button>().interactable &&
                                   !GameObject.Find("Level 3").GetComponent<Button>().interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("When player still had not passed first level - he should not be able to " +
                        "play any other levels, chosen from \"Main Menu\"'s \"ChooseLevel\" panel");
        }
        GameObject.Find("Close").GetComponent<Button>().onClick.Invoke();
        yield return null;
        GameObject.Find("Play").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }

        GameObject.Find("Player").transform.position =
            GameObject.Find("LevelEnd").GetComponent<Collider2D>().bounds.center;

        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            Assert.Fail("After passing first level, second one should be loaded");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to Main Menu");
        }
        
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        correctButtonLevels = GameObject.Find("Level 1").GetComponent<Button>().interactable &&
                              GameObject.Find("Level 2").GetComponent<Button>().interactable &&
                              !GameObject.Find("Level 3").GetComponent<Button>().interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
        GameObject.Find("Close").GetComponent<Button>().onClick.Invoke();
        yield return null;
        GameObject.Find("Play").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            Assert.Fail("When player left level without having it passed," +
                        " \"Play\" button should transfer him to same level");
        }

        GameObject.Find("Player").transform.position =
            GameObject.Find("LevelEnd").GetComponent<Collider2D>().bounds.center;

        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Level 3"))
        {
            Assert.Fail("After passing second level, third one should be loaded");
        }
        
        IS.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            Assert.Fail("When any level is loaded - Escape key should transfer player to Main Menu");
        }
        
        GameObject.Find("ChooseLevel").GetComponent<Button>().onClick.Invoke();
        yield return null;
        correctButtonLevels = GameObject.Find("Level 1").GetComponent<Button>().interactable &&
                              GameObject.Find("Level 2").GetComponent<Button>().interactable &&
                              GameObject.Find("Level 3").GetComponent<Button>().interactable;
        if (!correctButtonLevels)
        {
            Assert.Fail("From \"Main Menu\"'s ChooseLevel panel, player should be able to choose between levels," +
                        " that he had already passed and the one, that is next");
        }
    }
}
