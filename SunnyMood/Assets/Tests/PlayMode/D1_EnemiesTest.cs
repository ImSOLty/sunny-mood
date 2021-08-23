using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class D1_EnemiesTest
{
    private GameObject[] enemies;
    private GameObject player;

    private GameObject rat;
    private Animator anim;
    private AnimationClip[] aclips;
    
    [UnityTest, Order(0)]
    public IEnumerator CheckSpawn()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        GameObject.Destroy(player);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Assert.Fail("Enemies were not added to a scene or their tag misspelled");
        }
    }
    
    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        yield return null;
        foreach (GameObject rat in enemies)
        {
            SpriteRenderer sr = PMHelper.Exist<SpriteRenderer>(rat);
            if (sr == null)
            {
                Assert.Fail("There should be a <SpriteRenderer> component on enemies' objects");
            }

            Collider2D col = PMHelper.Exist<Collider2D>(rat);
            if (col == null)
            {
                Assert.Fail("There should be a <Collider2D> component on enemies' objects");
            }
        }
        yield return null;
    }
    
    [UnityTest, Order(2)]
    public IEnumerator AnimationCheck()
    {
        rat = GameObject.FindWithTag("Enemy");
        yield return null;
        anim = PMHelper.Exist<Animator>(rat);
        if (anim == null)
        {
            Assert.Fail("There should be an <Animator> component on enemies' objects");
        }
        Assert.NotNull(anim.runtimeAnimatorController,
            "There should be created controller, attached to <Animator> component!");
        aclips = anim.runtimeAnimatorController.animationClips;
        yield return null;
        if (aclips.Length != 1)
        {
            Assert.Fail("There should be added 1 clip to enemies' animator: EnemyWalk");
        }

        AnimationClip walk = Array.Find(aclips, clip => clip.name.Equals("EnemyWalk"));
    
        if (walk == null) Assert.Fail("There should be a clip in enemies' animator, called \"EnemyWalk\"");
        if (walk.legacy) Assert.Fail("\"EnemyWalk\" clip should be animated by animator");
        if (walk.empty) Assert.Fail("\"EnemyWalk\" clip in enemies' animator should have animation keys");
        if (!walk.isLooping) Assert.Fail("\"EnemyWalk\" clip in enemies' animator should be looped"); 
    }
    
    [UnityTest, Order(3)]
    public IEnumerator CheckTransitionsIdle()
    {
        Time.timeScale = 10;
        yield return new WaitForSeconds(0.2f);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "EnemyWalk")
        {
            Assert.Fail("\"EnemyWalk\" clip should be played by default");
        }
    }
    
    [UnityTest, Order(4)]
    public IEnumerator CorrectPlacementAndMovement()
    {
        RaycastHit2D[] hit;
        Collider2D ratCL = rat.GetComponent<Collider2D>();
        hit = Physics2D.RaycastAll(rat.transform.position, Vector2.down, 2);
        bool found = false;
        foreach (var h in hit)
        {
            if(h.collider.gameObject.CompareTag("Platform"))
            {
                found = true;
            }
        }

        if (!found)
        {
            Assert.Fail("Enemies should be spawned on platforms, right above them");
        }
        

        SpriteRenderer sr = rat.GetComponent<SpriteRenderer>();
        bool rotated = sr.flipX;
        
        Vector2 firstPos = rat.transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector2 secondPos = rat.transform.position;
        if (firstPos == secondPos)
        {
            Assert.Fail("Enemies should have movement");
        }
        bool movingLeft = firstPos.x < secondPos.x;
        bool next;

        int i;
        for (i = 0; i < 20; i++)
        {
            firstPos = rat.transform.position;
            yield return new WaitForSeconds(0.1f);
            secondPos = rat.transform.position;
            next = firstPos.x < secondPos.x;
            if (next != movingLeft)
            {
                break;
            }
        }

        if (i == 19)
        {
            Assert.Fail("Enemies should rotate their x-movement in 2 seconds maximum to cycle from point to point");
        }

        if (rotated == sr.flipX)
        {
            Assert.Fail("Enemies' sprite should be flipped after rotation");
        }
        
        yield return null;
    }
    
    [UnityTest, Order(5)]
    public IEnumerator CollisionCheck()
    {
        Time.timeScale = 10;
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        rat = GameObject.FindWithTag("Enemy");
        GameObject gem = GameObject.FindWithTag("Gem");
        gem.transform.position = rat.transform.position;
        if (gem == null)
        {
            Assert.Fail("There should be no collisions with enemies, except the player one");
        }

        Scene cur = SceneManager.GetActiveScene();
        player.transform.position = rat.transform.position;
        yield return new WaitForSeconds(1);
        if (cur == SceneManager.GetActiveScene())
        {
            Assert.Fail("Scene should be changed after enemies' collision with player");
        }
        yield return null;
    }
}
