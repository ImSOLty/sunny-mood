using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class A0_ObjectsTest
{
    private GameObject background,
        player,
        grid,
        ground,
        camera,
        levelend;
    
    
    [UnityTest, Order(1)]
    public IEnumerator ObjectsExistCheck()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        background = PMHelper.Exist("Background");
        player = PMHelper.Exist("Player");
        levelend = PMHelper.Exist("LevelEnd");
        Assert.NotNull(background, "There should be a background, named \"Background\" on scene!");
        Assert.NotNull(player, "There should be a player, named \"Player\" on scene!");
        Assert.NotNull(levelend, "There should be level end, named \"LevelEnd\" on scene!");
    }
    
    [UnityTest, Order(2)]
    public IEnumerator BasicObjectComponentsCheck()
    {
        yield return null;
        SpriteRenderer backSR = PMHelper.Exist<SpriteRenderer>(background);
        Assert.NotNull(backSR,
            "There should be a <SpriteRenderer> component on \"Background\"'s object!");
        Assert.NotNull(backSR.sprite,
            "There should be sprite, attached to \"Background\"'s <SpriteRenderer>!");
        
        SpriteRenderer playerSR = PMHelper.Exist<SpriteRenderer>(player);
        Assert.NotNull(playerSR,
            "There should be a <SpriteRenderer> component on \"Player\"'s object!");
        Assert.NotNull(playerSR.sprite,
            "There should be sprite, attached to \"Player\"'s <SpriteRenderer>!");
        
        Collider2D playerCL = PMHelper.Exist<Collider2D>(player);
        Assert.NotNull(playerCL,
            "There should be a <Collider2D> component on \"Player\"'s object!");
        Assert.False(playerCL.isTrigger,
            "\"Player\"'s <Collider2D> should not be triggerable!");
        
        Rigidbody2D playerRB = PMHelper.Exist<Rigidbody2D>(player);
        Assert.NotNull(playerRB,
            "There should be a <Rigidbody2D> component on \"Player\"'s object to perform collisions!");
        Assert.That(playerRB.bodyType==RigidbodyType2D.Dynamic,
            "\"Player\"'s <Rigidbody2D> type should be Dynamic in order to perform correct collisions!");
        Assert.That(playerRB.constraints==RigidbodyConstraints2D.FreezeRotation,
            "\"Player\"'s <Rigidbody2D> should not be rotated, change it's constraints!");
        
        Collider2D levelendCL = PMHelper.Exist<Collider2D>(levelend);
        Assert.NotNull(levelendCL,
            "There should be a <Collider2D> component on \"LevelEnd\"'s object!");
        Assert.True(levelendCL.isTrigger,
            "\"LevelEnd\"'s <Collider2D> should be triggerable!");
    }

    [UnityTest, Order(3)]
    public IEnumerator GridCheck()
    {
        yield return null;
        grid = PMHelper.Exist("Grid");
        ground = PMHelper.Exist("Ground");
        Assert.NotNull(grid,
            "There should be a tilemap grid, named \"Grid\" on scene!");
        Assert.NotNull(ground,
            "There should be a ground tilemap, named \"Ground\" on scene!");
    }
    
    [UnityTest, Order(4)]
    public IEnumerator BasicGridComponentsCheck()
    {
        yield return null;
        Grid gridGR = PMHelper.Exist<Grid>(grid);
        Assert.NotNull(gridGR,
            "There should be a <Grid> component on \"Grid\"'s object to use tilemaps!");
        Assert.That(gridGR.cellLayout==GridLayout.CellLayout.Rectangle,
            "\"Grid\"'s <Grid> component should have Rectangle layout!");
        Assert.That(gridGR.cellSwizzle==GridLayout.CellSwizzle.XYZ,
            "\"Grid\"'s <Grid> component should have XYZ swizzle!");

        Tilemap groundTM = PMHelper.Exist<Tilemap>(ground);
        TilemapRenderer groundTMR = PMHelper.Exist<TilemapRenderer>(ground);
        Assert.NotNull(groundTM,
            "There should be a <Tilemap> component on \"Ground\"'s object to use tilemaps!");
        Assert.NotNull(groundTMR,
            "There should be a <TilemapRenderer> component on \"Ground\"'s object to view created tilemaps!");
        bool correctSize = groundTM.size.x > 0
                           && groundTM.size.y > 0;
        Assert.True(correctSize,
            "There should be added tiles to \"Ground\"'s tilemap!");
        
        Collider2D groundCL = PMHelper.Exist<Collider2D>(ground);
        Assert.NotNull(groundCL,
            "There should be a <Collider2D> component on \"Ground\"'s object!");
        Assert.False(groundCL.isTrigger,
            "\"Ground\"'s <Collider2D> should not be triggerable!");
        
        Rigidbody2D groundRB = PMHelper.Exist<Rigidbody2D>(ground);
        Assert.NotNull(groundRB,
            "There should be a <Rigidbody2D> component on \"Ground\"'s object to perform collisions!");
        Assert.That(groundRB.bodyType==RigidbodyType2D.Static,
            "\"Ground\"'s <Rigidbody2D> type should be Static in order to perform correct collisions!");
    }

    [UnityTest, Order(5)]
    public IEnumerator CameraCheck()
    {
        yield return null;
        camera = PMHelper.Exist("Main Camera");
        Assert.NotNull(camera, 
            "There should be a camera, named \"Main Camera\" on scene!");
        Assert.NotNull(PMHelper.Exist<Camera>(camera),
            "\"Main Camera\"'s object should have attached <Camera> component!");
    }
    
    [UnityTest, Order(6)]
    public IEnumerator ChildrenCheck()
    {
        yield return null;
        Assert.That(PMHelper.Child(background,camera),
            "\"Background\"'s object should be a child of \"Main Camera\" object in order to move camera with background!");
        Assert.That(PMHelper.Child(ground,grid),
            "\"Ground\"'s object should be a child of \"Grid\" object, because tilemap's should be a child of grid!");
    }
    
    [UnityTest, Order(7)]
    public IEnumerator SortingCheck()
    {
        yield return null;
        SpriteRenderer backSR = background.GetComponent<SpriteRenderer>();
        SpriteRenderer playerSR = player.GetComponent<SpriteRenderer>();
        SpriteRenderer levelendSR = levelend.GetComponent<SpriteRenderer>();
        TilemapRenderer groundSR = ground.GetComponent<TilemapRenderer>();

        bool correctSort = 
                backSR.sortingOrder<levelendSR.sortingOrder && 
                levelendSR.sortingOrder<groundSR.sortingOrder && 
                groundSR.sortingOrder<playerSR.sortingOrder;
        Assert.True(correctSort,"Sorting layers should be placed in correct order: Background < LevelEnd < Ground < Player!");
    }
}
