using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestForData
    {
        
        [UnityTest]
        public IEnumerator TestForDataWithEnumeratorPasses()
        {
	
			var go = new GameObject();
    		go.AddComponent<Rigidbody>();
    		var originalPosition = go.transform.position.y;
    		yield return new WaitForFixedUpdate();
			Assert.AreNotEqual(originalPosition, go.transform.position.y);
		}
		
		[UnityTest]
		public IEnumerator MonoBehaviourTest_Works()
		{
    		yield return new MonoBehaviourTest<MyMonoBehaviourTest>();
		}

		public class MyMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
		{
    		private int frameCount;
    		public bool IsTestFinished
    	{
        	get { return frameCount > 10; }
    	}

     		void Update()
     		{
        		frameCount++;
     		}
		}
    }
}
