using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using WindowsInput;
using WindowsInput.Native;

public class B1_PlayerAnimationsTest
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;
    private SpriteRenderer sr;
    private Animator anim;
    private AnimationClip[] aclips;
    private AnimationClip idle, jump, walk;
    
    [UnityTest, Order(1)]
    public IEnumerator NecessaryComponents()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        yield return null;
        sr = player.GetComponent<SpriteRenderer>();
        anim = PMHelper.Exist<Animator>(player);
        Assert.NotNull(anim,
            "<Animator> component should be attached to player in order to perform animations!");
        Assert.NotNull(anim.runtimeAnimatorController,
            "There should be created controller, attached to <Animator> component!");
        aclips = anim.runtimeAnimatorController.animationClips;
    }

    [UnityTest, Order(2)]
    public IEnumerator CheckAnimationClips()
    {
        yield return null;
        if (aclips.Length != 3)
        {
            Assert.Fail("There should be added 3 clips to Player's animator: Idle, Jump, Walk");
        }

        AnimationClip idle = Array.Find(aclips, clip => clip.name.Equals("Idle"));
        
        if (idle == null) Assert.Fail("There should be a clip in Player's animator, called \"Idle\"");
        if (idle.legacy) Assert.Fail("\"Idle\" clip should be animated by animator");
        if (idle.empty) Assert.Fail("\"Idle\" clip in Player's animator should have animation keys");
        if (!idle.isLooping) Assert.Fail("\"Idle\" clip in Player's animator should be looped"); 
        
        AnimationClip jump = Array.Find(aclips, clip => clip.name.Equals("Jump"));
        
        if (jump == null) Assert.Fail("There should be a clip in Player's animator, called \"Jump\"");
        if (jump.legacy) Assert.Fail("\"Jump\" clip should be animated by animator");
        if (jump.empty) Assert.Fail("\"Jump\" clip in Player's animator should have animation keys");
        if (!jump.isLooping) Assert.Fail("\"Jump\" clip in Player's animator should be looped"); 
        
        AnimationClip walk = Array.Find(aclips, clip => clip.name.Equals("Walk"));
        
        if (walk == null) Assert.Fail("There should be a clip in Player's animator, called \"Walk\"");
        if (walk.legacy) Assert.Fail("\"Walk\" clip should be animated by animator");
        if (walk.empty) Assert.Fail("\"Walk\" clip in Player's animator should have animation keys");
        if (!walk.isLooping) Assert.Fail("\"Walk\" clip in Player's animator should be looped");
    }

    [UnityTest, Order(3)]
    public IEnumerator CheckTransitionsIdle()
    {
        Time.timeScale = 10;
        yield return new WaitForSeconds(1);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idle")
        {
            Assert.Fail("While character is not moving - \"Idle\" clip should be played");
        }
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(0.2f);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Walk")
        {
            Assert.Fail("While character is moving on the ground to the right - \"Walk\" clip should be played");
        }

        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return new WaitForSeconds(0.3f);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Jump")
        {
            Assert.Fail("While character is in air - \"Jump\" clip should be played");
        }
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        yield return new WaitForSeconds(2f);
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        yield return new WaitForSeconds(0.2f);
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Walk")
        {
            Assert.Fail("While character is moving on the ground to the left - \"Walk\" clip should be played");
        }

        if (!sr.flipX)
        {
            Assert.Fail("If character is moving to the left - sprite should be flipped " +
                        "(SpriteRenderer's flipX option should be checked)");
        }
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        
        IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        yield return new WaitForSeconds(0.3f);
        
        if (!sr.flipX)
        {
            Assert.Fail("If character has jumped after moving to the left - sprite should be flipped " +
                        "(SpriteRenderer's flipX option should be checked)");
        }

        yield return new WaitForSeconds(2f);
        
        if (!sr.flipX)
        {
            Assert.Fail("If character has moved left and stopped moving - sprite should still be flipped " +
                        "(SpriteRenderer's flipX option should be checked)");
        }
    }
}