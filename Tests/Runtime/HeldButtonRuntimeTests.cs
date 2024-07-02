using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class HeldButtonRuntimeTests
    {
        private HeldButton m_HeldButtonComponent;
    
        [OneTimeSetUp]
        public void Setup()
        {
            GameObject heldButton = new GameObject("Held Button");
            m_HeldButtonComponent = heldButton.AddComponent<HeldButton>();
        }
    
        /// <summary>
        /// Returns whether the actual value is within the expected value plus or minus an allowed delta.
        /// actual = expected +/- delta.
        /// </summary>
        private bool WithinDelta(float actual, float expected, float delta)
        {
            return actual >= expected - delta && actual <= expected + delta;
        }
    
        [UnityTest]
        public void IsHeldTest()
        {
            // Tests that the IsHeld bool is registering correctly.
            Assert.False(m_HeldButtonComponent.IsHeld);
            m_HeldButtonComponent.Hold();
            Assert.True(m_HeldButtonComponent.IsHeld);
            m_HeldButtonComponent.Hold();
            Assert.True(m_HeldButtonComponent.IsHeld);
            m_HeldButtonComponent.Release();
            Assert.False(m_HeldButtonComponent.IsHeld);
            m_HeldButtonComponent.Release();
            Assert.False(m_HeldButtonComponent.IsHeld);
        }
    
        public static IEnumerable<float[]> Held_Time_TestCases()
        {
            yield return new[] { 0.5f, 0.05f };
            yield return new[] { 1.0f, 0.05f };
            yield return new[] { 3.0f, 0.05f };
        }
    
        // Values = [test holding time, allowed discrepency]
        [UnityTest]
        public IEnumerator HeldTimeTest([ValueSource(nameof(Held_Time_TestCases))] float[] args)
        {
            // Tests that the HeldTime value is registering correctly.
            Assert.False(m_HeldButtonComponent.IsHeld);
        
            m_HeldButtonComponent.Hold();
            yield return new WaitForSeconds(args[0]);
            Assert.IsTrue(WithinDelta(m_HeldButtonComponent.HeldTime, args[0], args[1]), $"expected = ({args[0]}). actual = ({m_HeldButtonComponent.HeldPercent})");
            m_HeldButtonComponent.Release();
        }

        public static IEnumerable<float[]> Held_Percent_TestCases()
        {
            yield return new[] { 0.5f, 0.5f, 1.0f, 0.05f };
            yield return new[] { 0.5f, 1.0f, 1.0f, 0.05f };
            yield return new[] { 2.0f, 0.5f, 0.25f, 0.05f };
            yield return new[] { 2.0f, 0.0f, 0.0f, 0.05f };
            yield return new[] { 0.0f, 0.0f, 1.0f, 0.05f };
            yield return new[] { 0.0f, 1.0f, 1.0f, 0.05f };
        }

        // Values = [total hold time, test holding time, expected percent, allowed discrepency]
        [UnityTest]
        public IEnumerator HeldPercentTest([ValueSource(nameof(Held_Percent_TestCases))] float[] args)
        {
            // Tests that the HeldTime value is registering correctly.
            Assert.False(m_HeldButtonComponent.IsHeld);
            m_HeldButtonComponent.holdTime = args[0];
            
            m_HeldButtonComponent.Hold();
            yield return new WaitForSeconds(args[1]);
            Assert.IsTrue(WithinDelta(m_HeldButtonComponent.HeldPercent, args[2], args[3]), $"expected = ({args[2]}). actual = ({m_HeldButtonComponent.HeldPercent})");
            m_HeldButtonComponent.Release();
        }
    
        [UnityTest]
        public IEnumerator DecayTimeTest()
        {
            // Tests that the button decays properly on release.
            yield return null;
            Assert.Fail();
        }
    
        [UnityTest]
        public IEnumerator ResetTimeTest()
        {
            // Tests that the button resets after the correct time.
            yield return null;
            Assert.Fail();
        }
    
        [UnityTest]
        public IEnumerator AutoResetTest()
        {
            // Tests that the button only resets itself when autoReset is true.
            yield return null;
            Assert.Fail();
        }
    
        [UnityTest]
        public IEnumerator CompleteHoldTest()
        {
            // Tests when the button is after the full duration.
            yield return null;
            Assert.Fail();
        }
    
        [UnityTest]
        public IEnumerator EarlyReleaseTest()
        {
            // Tests when the button is released early.
            yield return null;
            Assert.Fail();
        }
    }
}
