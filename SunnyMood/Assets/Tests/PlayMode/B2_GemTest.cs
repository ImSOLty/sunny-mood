using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class B2_GemTest
{
    private GameObject player;
    private GameObject end;
    private GameObject gem;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    private AnimationClip[] aclips;
    private AnimationClip gemClip;
    
    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        end = GameObject.Find("LevelEnd");
        gem = GameObject.FindWithTag("Gem");
        if (gem == null)
        {
            Assert.Fail("Gems were not added to a scene or their tag misspelled");
        }

        sr = PMHelper.Exist<SpriteRenderer>(gem);
        if (sr==null)
        {
            Assert.Fail("Gems are not visible, SpriteRenderer component is not attached");
        }

        col = PMHelper.Exist<Collider2D>(gem);
        if (col==null)
        {
            Assert.Fail("There is no attached <Collider2D> component to gems");
        }

        if (!col.isTrigger)
        {
            Assert.Fail("<Collider2D> component should be triggerable in order not to collide with anything");
        }

        anim = PMHelper.Exist<Animator>(gem);
        if (anim == null)
        {
            Assert.Fail("There is no attached <Animator> component to gems");
        }
        yield return null;
        aclips = anim.runtimeAnimatorController.animationClips;
    }

    [UnityTest, Order(2)]
    public IEnumerator CheckAnimationClips()
    {
        yield return null;
        if (aclips.Length != 1)
        {
            Assert.Fail("There should be added 1 clip to Gem's animator: Gem");
        }

        AnimationClip idle = Array.Find(aclips, clip => clip.name.Equals("Gem"));
        
        if (idle == null) Assert.Fail("There should be a clip in Gem's animator, called \"Gem\"");
        if (idle.legacy) Assert.Fail("\"Gem\" clip should be animated by animator");
        if (idle.empty) Assert.Fail("\"Gem\" clip in Gem's animator should have animation keys");
        if (!idle.isLooping) Assert.Fail("\"Gem\" clip in Gem's animator should be looped");
    }

    [UnityTest, Order(3)]
    public IEnumerator CheckTransitionsIdle()
    {
        Time.timeScale = 10;
        yield return new WaitForSeconds(0.2f);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Gem")
        {
            Assert.Fail("\"Gem\" clip should be played by default");
        }
    }
    
    [UnityTest, Order(4)]
    public IEnumerator CheckDestroying()
    {
        Time.timeScale = 40;
        yield return null;
        gem.transform.position = end.GetComponent<Collider2D>().ClosestPoint(gem.transform.position);
        yield return new WaitForSeconds(0.1f);
        if (gem == null)
        {
            Assert.Fail("Gems should not be destroyed when colliding with anything except Player");
        }
        gem.transform.position = player.transform.position;
        yield return new WaitForSeconds(0.1f);
        if (gem != null)
        {
            Assert.Fail("Gems should be destroyed when colliding with Player");
        }
    }
}
