using System;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;
[InitializeOnLoad]
static class MyEditorScript
{
    static public TestRunnerApi testRunnerApi;
    static TestCallbacks testCallbacks= new TestCallbacks();

    static public String result = String.Empty;
    
    static MyEditorScript()
    {
        testRunnerApi = ScriptableObject.CreateInstance<TestRunnerApi>();
        testRunnerApi.RegisterCallbacks(testCallbacks);
    }
    [MenuItem("Window/General/Test Extensions/Run Play Mode Tests")]
    
    public static void RunPlayModeTests()
    {
        RunTests(TestMode.PlayMode);
    }
    private static void RunTests(TestMode testModeToRun)
    {
        testRunnerApi = ScriptableObject.CreateInstance<TestRunnerApi>();
        testRunnerApi.RegisterCallbacks(testCallbacks);
        var filter = new Filter()
        {
            testMode = testModeToRun
        };
        
        testRunnerApi.Execute(new ExecutionSettings(filter));
    }
    private class TestCallbacks : ICallbacks
    {
        public String results = String.Empty;
        private int testNum = 0;
        private bool next=true;
        public bool written = false;

        public void RunFinished(ITestResultAdaptor result)
        {
            if (!next && !written)
            {
                MyEditorScript.result = "Failed in test #" + testNum + "\n\n" + results;
                EditorWindow win = EditorWindow.GetWindow<CheckWindow>();
                win.SendEvent(EditorGUIUtility.CommandEvent("FinishedWrong"));
                written = true;
            }

            if (next)
            {
                MyEditorScript.result = "All tests passed!";
                EditorWindow win = EditorWindow.GetWindow<CheckWindow>();
                win.SendEvent(EditorGUIUtility.CommandEvent("FinishedOk"));
            }
        }
 
        public void RunStarted(ITestAdaptor testsToRun)
        {
            testNum = 0;
            results = String.Empty;
            next = true;
            written = false;
        }
        
        public void TestFinished(ITestResultAdaptor result)
        {
            if (next)
            {
                testNum++;
            }
            if (result.TestStatus == TestStatus.Failed)
            {
                if(next)
                    results=result.Message;
                next = false;
            }
        }

        public void TestStarted(ITestAdaptor test){ }
    }
}