using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class A2_EndLevelTest
{
    private GameObject player;
    
    [UnityTest, Order(1)]
    public IEnumerator LevelEndCheck()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        Scene cur = SceneManager.GetActiveScene();
        player = GameObject.Find("Player");
        yield return null;
        GameObject end = GameObject.Find("LevelEnd");
        Vector2 place = end.GetComponent<Collider2D>().ClosestPoint(player.transform.position);
        yield return null;
        player.transform.position = place;
        yield return new WaitForSeconds(2);
        Assert.AreNotEqual(cur, SceneManager.GetActiveScene());
    }
}
