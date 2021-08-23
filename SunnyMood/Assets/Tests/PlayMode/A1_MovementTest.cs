using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using WindowsInput;
using WindowsInput.Native;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class A1_MovementTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;

    [UnityTest, Order(0)]
    public IEnumerator NotMovingWithoutInputCheck()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        GameObject.Destroy(GameObject.Find("LevelEnd"));
        Time.timeScale = 10;
        yield return null;
        player = PMHelper.Exist("Player");
        yield return new WaitForSeconds(1f);
        Vector3 posStart = player.transform.position;
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(posStart, player.transform.position,
            "Player's position should not be changed while there is no input, or maybe character is falling down to the void!");
    }
    [UnityTest, Order(1)]
    public IEnumerator MovementLeftCheck()
    {
        yield return null;
        Vector3 posStart = player.transform.position;
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        yield return new WaitForSeconds(0.5f);
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        yield return null;
        Vector3 posEnd = player.transform.position;
        bool ok = posEnd.x < posStart.x;
        Assert.True(ok,
            "Player's movement to the left is not working properly, X-axis should decrease!");
        yield return null;
    }
    
    [UnityTest, Order(2)]
    public IEnumerator MovementRightCheck()
    {
        yield return null;
        Vector3 posStart = player.transform.position;
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(0.5f);
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        yield return null;
        Vector3 posEnd = player.transform.position;
        bool ok = posEnd.x > posStart.x;
        Assert.True(ok,
            "Player's movement to the right is not working properly, X-axis should increase!");
        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator JumpCheck()
    {
        yield return null;
        Vector3 startpos = player.transform.position;
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return new WaitForSeconds(2f);
        Assert.True(Mathf.Abs(player.transform.position.y-startpos.y)<0.1f,
            "Player should finish his jump in less than 2 seconds (Y-axis of start position should be equal to Y-axis of end position)!");
        
        List<float> height = new List<float>();
        yield return new WaitForSeconds(1f);
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        
        yield return new WaitForSeconds(0.1f);
        height.Add(player.transform.position.y);
        yield return null;
        while (height[height.Count - 1] < player.transform.position.y)
        {
            height.Add(player.transform.position.y);
            yield return null;
        }
        PMHelper.ymax = height[height.Count-1];
        int maxi = height.Count - 1;

        Assert.GreaterOrEqual(PMHelper.ymax-startpos.y,player.transform.localScale.y*2,
            "Player should jump at least twice as his Y-axis scale!");
        
        yield return new WaitForSeconds(0.1f);
        while (height[height.Count - 1] > startpos.y)
        {
            height.Add(player.transform.position.y);
            yield return null;
        }
        
        Assert.Less(Mathf.Abs(height[maxi]-height[maxi-1]),Mathf.Abs(height[0]-height[1]),
            "Player's velocity at the highest point of his jump should be less than velocity after jump!");
        Assert.Greater(Mathf.Abs(height[height.Count-1]-height[height.Count-2]),Mathf.Abs(height[maxi+1]-height[maxi+2]),
            "Player's velocity in the end of his jump should be greater than velocity at the highest point of his jump!");
    }

    [UnityTest, Order(4)]
    public IEnumerator DoubleJumpCheck()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            yield return new WaitForSeconds(0.1f);
        }
        Assert.Less(player.transform.position.y,PMHelper.ymax,"Player should not be able to jump while it's in air!");
    }
    
    
}
