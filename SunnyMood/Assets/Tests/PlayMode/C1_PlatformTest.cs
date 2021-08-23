using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class C1_PlatformTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;
    private GameObject[] platforms;
    private GameObject grid;
    private Vector2 playerSize;
    private float jumpheight;

    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        if (platforms.Length == 0)
        {
            Assert.Fail("Platforms were not added to a scene or their tag misspelled");
        }
        
        grid = PMHelper.Exist("Grid");
        
        foreach (GameObject platform in platforms)
        {
            Tilemap platformTM = PMHelper.Exist<Tilemap>(platform);
            TilemapRenderer platformTMR = PMHelper.Exist<TilemapRenderer>(platform);
            if (platformTM == null)
            {
                Assert.Fail("There should be a <Tilemap> component on platform's object to use tilemaps!");
            }

            if (platformTMR == null)
            {
                Assert.Fail("There should be a <TilemapRenderer> component on platform's object to view created tilemaps!");
            }
            bool correctSize = platformTM.size.x > 0
                               && platformTM.size.y > 0;
            if (!correctSize)
            {
                Assert.Fail("There should be added tiles to platform's tilemap!");
            }

            BoxCollider2D platformCL = PMHelper.Exist<BoxCollider2D>(platform);
            if (platformCL == null)
            {
                Assert.Fail("There should be a <BoxCollider2D> component on platform's object!");
            }

            if (platformCL.isTrigger)
            {
                Assert.Fail("Platform's <BoxCollider2D> should not be triggerable!");
            }

            if (platformCL.sharedMaterial == null)
            {
                Assert.Fail("There should be attached PhysicsMaterial2D to a material field of BoxCollider2D");
            }
            
            if (platformCL.sharedMaterial.friction != 0)
            {
                Assert.Fail(
                    "PhysicsMaterial2D's friction of a BoxCollider2D should be decreased to zero " +
                    "in order for player not to get stuck, colliding with it's edges");
            }

            if (!(platformCL.edgeRadius > 0))
            {
                Assert.Fail("Edge radius of BoxCollider2D should be increased in order to slide off the platform");
            }
            playerSize = player.GetComponent<Collider2D>().bounds.size;
            if (playerSize.x * 2 > platformCL.size.x)
            {
                Assert.Fail("Let platforms be at least twice as wider, than player");
            }
            
            Rigidbody2D platformRB = PMHelper.Exist<Rigidbody2D>(platform);
            if (platformRB == null)
            {
                Assert.Fail("There should be a <Rigidbody2D> component on platform's object to perform collisions!");
            }
            
            if (platformRB.bodyType != RigidbodyType2D.Static)
            {
                Assert.Fail("Platform's <Rigidbody2D> type should be Static in order to perform correct collisions!");
            }
            
            bool ischild = PMHelper.Child(platform, grid);
            if (!ischild)
            {
                Assert.Fail("Platforms should be children of \"Grid\" object, because tilemap should be a child of grid!");
            }
        }
    }
    [UnityTest, Order(2)]
    public IEnumerator CheckCollisions()
    {
        Time.timeScale = 5;
        yield return null;
        GameObject platformTmp = platforms[0];
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.y > platformTmp.transform.position.y)
            {
                GameObject.Destroy(platform);
            }
            else
            {
                platformTmp = platform;
            }
        }

        yield return new WaitForSeconds(1);
        yield return null;
        
        Vector3 tmp = platformTmp.transform.position;
        tmp.x = player.transform.position.x;
        platformTmp.transform.position = tmp;
        yield return null;

        PlatformInfo info = platformTmp.AddComponent<PlatformInfo>();
        yield return null;
        if (info.playerCollides)
        {
            Assert.Fail("Let character to pass under the platform with lowest Y-axis");
        }

        Vector2 tmpPos = player.transform.position;
        tmpPos.x = info.leftBottom.x - 2*playerSize.x;
        player.transform.position = tmpPos;
        yield return new WaitForSeconds(2);
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(0.75f);
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(2f);
        if (player.transform.position.y <= info.leftUp.y)
        {
            Assert.Fail("Player should be able to jump on platform conveniently and stay still without falling down");
        }

        Vector2 startPos = player.transform.position;
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return new WaitForSeconds(0.2f);
        Vector2 endPos = player.transform.position;
        if (!(endPos.y > startPos.y))
        {
            Assert.Fail("Player should be able to jump on a platform");
        }
    }

    [UnityTest, Order(3)]
    public IEnumerator PlatformsReachableCheck()
    {
        Time.timeScale = 3;
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return new WaitForSeconds(1);
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            GameObject.Destroy(platform);
        }

        player = GameObject.Find("Player");
        Vector2 start = player.transform.position;
        float was = player.transform.position.y;
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(0.05f);
        while (was < player.transform.position.y)
        {
            was = player.transform.position.y;
            yield return new WaitForSeconds(0.05f);
        }
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);

        Vector2 end = player.transform.position;

        float jumpx = Mathf.Abs(end.x - start.x);
        float jumpy = Mathf.Abs(end.y - start.y);
        jumpheight = jumpy;
        
        
        List<PlatformInfo> infos = new List<PlatformInfo>();
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            infos.Add(platform.AddComponent<PlatformInfo>());
        }
        GameObject ground = GameObject.Find("Ground");
        PlatformInfo grInfo = ground.AddComponent<PlatformInfo>();
        grInfo.reachable = true;
        foreach (PlatformInfo info in infos)
        {
            if (Mathf.Abs(info.leftUp.y - grInfo.leftUp.y) < jumpy)
            {
                info.reachable = true;
            }
            foreach (PlatformInfo info2 in infos)
            {
                if(info==info2 || info2.reachable || info2.leftUp.y<info.leftUp.y) continue;
                if (Mathf.Abs(info2.leftUp.x - info.rightUp.x) < jumpx &&
                    Mathf.Abs(info2.leftUp.y - info.rightUp.y) < jumpy
                    ||
                    Mathf.Abs(info2.rightUp.x - info.leftUp.x) < jumpx &&
                    Mathf.Abs(info2.rightUp.y - info.leftUp.y) < jumpy
                )
                {
                    info2.reachable = true;
                    //info2.gameObject.GetComponent<Tilemap>().color=Color.green;
                }
                else
                {
                    info2.reachable = false;
                    //info2.gameObject.GetComponent<Tilemap>().color=Color.red;
                }

                yield return null;
            }
        }

        foreach (PlatformInfo info in infos)
        {
            /*if (info.reachable)
            {
                info.gameObject.GetComponent<Tilemap>().color=Color.green;
            }
            else
            {
                info.gameObject.GetComponent<Tilemap>().color=Color.red;
            }

            Time.timeScale = 1;
            yield return new WaitForSeconds(0.5f);*/
            
            if (!info.reachable)
            {
                Assert.Fail("All platforms should be conveniently reachable from lower platforms");
            }
        }
    }

    [UnityTest, Order(4)]
    public IEnumerator CorrectGemsNExit()
    {
        yield return null;
        foreach (GameObject gem in GameObject.FindGameObjectsWithTag("Gem"))
        {
            Collider2D gemColl =gem.GetComponent<Collider2D>();
            Collider2D[] colls = Physics2D.OverlapCircleAll(gemColl.bounds.center, jumpheight);
            Collider2D platform = Array.Find(colls, plat => 
                plat.gameObject.CompareTag("Platform") ||
                plat.gameObject.name.Equals("Ground"));
            if (platform == null)
            {
                Assert.Fail("It should not be hard to collect gems");
            }
        }
        
        Collider2D endColl =GameObject.Find("LevelEnd").GetComponent<Collider2D>();
        Collider2D[] colls2 = Physics2D.OverlapCircleAll(endColl.bounds.center, jumpheight);
        Collider2D platform2 = Array.Find(colls2, plat => 
            plat.gameObject.CompareTag("Platform") ||
            plat.gameObject.name.Equals("Ground"));
        if (platform2 == null)
        {
            Assert.Fail("Level end should closer to any platform or ground");
        }
    }
}
