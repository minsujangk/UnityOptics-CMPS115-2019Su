using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestForData
    {
        // this test script is checking basic property for the game demo
        // because we have trouble with how to use built-in test runner 
        // in the Unity, we decide to have more test on the script instead
        // of using test runner.
        // first test is basically testing two game objects positions, which
        // indicates if two game objects have the same distances or not at the
        // very beginning.
        // second test is checking whether the game is running properly or not
        // the basic idea is at the very beginning, it will count the frame 
        // count and as long as it gets to 10 we will return true which means
        // test pass. if during 10 frames, our demo cannot run properly, it will
        // return false, which mean that our test fail
        
        [UnityTest]
        public IEnumerator TestForDataWithEnumeratorPasses()
        {
			//creating a new game object
			var go = new GameObject();
			//add Component form the property of the game object
    		go.AddComponent<Rigidbody>();
    		var originalPosition = go.transform.position.y;
    		yield return new WaitForFixedUpdate();
    		// assert which mean checking whether two variables are equal or not
    		// in this case, we assume it is not equal. if it is true, we return
    		// true. if it is false, we return false
			Assert.AreNotEqual(originalPosition, go.transform.position.y);
		}
		
		[UnityTest]
		public IEnumerator MonoBehaviourTest_Works()
		{
    		yield return new MonoBehaviourTest<MyMonoBehaviourTest>();
		}

		public class MyMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
		{
			// we set new variable frameCount
    		private int frameCount;
    		// create another variable which return true if frameCount is greater
    		// than 10
    		public bool IsTestFinished
    	{
        	get { return frameCount > 10; }
    	}
    	
    		// in update function, we increment the frameCount by 1 each time

     		void Update()
     		{
        		frameCount++;
     		}
		}
    }
}
