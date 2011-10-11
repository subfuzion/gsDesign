using ClassLibrary1.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using System.Diagnostics;

namespace ClassLibrary1.Tests
{
    
    
    /// <summary>
    ///This is a test class for RingBufferTest and is intended
    ///to contain all RingBufferTest Unit Tests
    ///</summary>
   [TestClass()]
   public class RingBufferTest {


      private TestContext testContextInstance;

      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
      public TestContext TestContext {
         get {
            return testContextInstance;
         }
         set {
            testContextInstance = value;
         }
      }

      #region Additional test attributes
      // 
      //You can use the following additional attributes as you write your tests:
      //
      //Use ClassInitialize to run code before running the first test in the class
      //[ClassInitialize()]
      //public static void MyClassInitialize(TestContext testContext)
      //{
      //}
      //
      //Use ClassCleanup to run code after all tests in a class have run
      //[ClassCleanup()]
      //public static void MyClassCleanup()
      //{
      //}
      //
      //Use TestInitialize to run code before running each test
      //[TestInitialize()]
      //public void MyTestInitialize()
      //{
      //}
      //
      //Use TestCleanup to run code after each test has run
      //[TestCleanup()]
      //public void MyTestCleanup()
      //{
      //}
      //
      #endregion

      // debugging helper
      private void PrintRing<T>(RingBuffer<T> ring) {
         foreach (var item in ring) {
            Debug.Print(item == null ? "<NULL>" : item.ToString());
         }
      }

      [TestMethod()]
      public void ItemTest() {
         int size = 10;
         RingBuffer<int> ring = new RingBuffer<int>(size);
         // add 100 items to a 10 items large buffer
         for (int i = 0; i < 100; i++) {
            ring.Add(i);
         }

         // check count and one item
         Assert.AreEqual(ring.Count, size);
         Assert.AreEqual(ring[9], 99);
         // add another item and validate relative positions
         ring[8] = 66;
         Assert.AreEqual(ring[8], 66);
         Assert.AreEqual(ring[9], 98);
         Assert.AreEqual(ring.Count, size);
      }

      [TestMethod()]
      [DeploymentItem("ClassLibrary1.dll")]
      public void CountTest() {
         int size = 10;
         RingBuffer<int> ring = new RingBuffer<int>(size);
         // add 100 items to a 10 items buffer
         for (int i = 0; i < 100; i++) {
            ring.Add(i);
         }
         // check cound
         Assert.AreEqual(ring.Count, size);

         // insert another item and check count to stay equal
         ring.Insert(0, 0);
         Assert.AreEqual(ring.Count, size);

         // remove one item and check if cound decreases
         ring.Remove(0);
         Assert.AreEqual(ring.Count, size - 1);

         // remove another item and check if count decreases again
         ring.RemoveAt(1);
         Assert.AreEqual(ring.Count, size - 2);
      }

      [TestMethod()]
      [DeploymentItem("ClassLibrary1.dll")]
      public void CapacityTest() {
         // simple property assignement
         RingBuffer<int> ring = new RingBuffer<int>(10);
         Assert.AreEqual(ring.Capacity, 10);
      }

      [TestMethod()]
      [DeploymentItem("ClassLibrary1.dll")]
      public void GetEnumeratorTest1() {
         int size = 10;
         RingBuffer<int> ring = new RingBuffer<int>(size);
         // add 105 items to a 10 items buffer
         for (int i = 0; i < 105; i++) {
            ring.Add(i);
         }

         // helper list with same items for validation
         List<int> list = new List<int>();

         // add the last 10 items of the buffer to the list and 
         // cmpare them
         Enumerable.Range(95, size).ToList().ForEach(i => list.Add(i));
         Assert.IsTrue(ring.SequenceEqual(list));
         // remove one item from list and buffern and compare
         ring.RemoveAt(0);
         list.RemoveAt(0);
         Assert.IsTrue(ring.SequenceEqual(list));
         // remove another item and compare both
         ring.Remove(99);
         list.Remove(99);
         PrintRing(ring);
         Assert.IsTrue(ring.SequenceEqual(list));
         // insert an item to both (and ajust the list count) and compare them
         list.Insert(3, 66);
         while (list.Count > size) list.RemoveAt(list.Count - 1);
         ring.Insert(3, 66);
         Assert.IsTrue(ring.SequenceEqual(list));
         // insert another item to both (and ajust the list count) and compare them
         list.Insert(3, 77);
         while (list.Count > size) list.RemoveAt(list.Count - 1);
         ring.Insert(3, 77);
         Assert.IsTrue(ring.SequenceEqual(list));
         // insert another item to both (and ajust the list count) and compare them
         list.Insert(3, 88);
         while (list.Count > size) list.RemoveAt(list.Count - 1);
         ring.Insert(3, 88);
         Assert.IsTrue(ring.SequenceEqual(list));
      }

      [TestMethod()]
      public void RemoveAtTest() {
         int size = 10;
         RingBuffer<int> ring = new RingBuffer<int>(size);
         // fill 5 items into a 10 item buffer
         Enumerable.Range(0, 5).ToList().ForEach(i => ring.Add(i));
         
         // validate correct new item at same position after deletion
         int value = ring[3];
         ring.RemoveAt(3);
         Assert.AreEqual(ring[3], value + 1);

         // again, validate correct new item after deletion of another one
         value = ring[0];
         ring.RemoveAt(0);
         Assert.AreEqual(ring[0], value + 1);

         // remove an item and validate if the count decreased
         value = ring.Count;
         ring.RemoveAt(ring.Count - 1);
         Assert.AreEqual(value - 1, ring.Count);

         // add 105 new items to the buffer
         Enumerable.Range(0, 105).ToList().ForEach(i => ring.Add(i));

         // validate correct new item at same position after deletion
         value = ring[3];
         ring.RemoveAt(3);
         Assert.AreEqual(ring[3], value + 1);

         // again, validate correct new item after deletion of another one
         value = ring[0];
         ring.RemoveAt(0);
         Assert.AreEqual(ring[0], value + 1);

         // remove an item and validate if the count decreased
         value = ring.Count;
         ring.RemoveAt(ring.Count - 1);
         Assert.AreEqual(value - 1, ring.Count);
      }

      [TestMethod()]
      public void RemoveTest() {
         int size = 10;
         RingBuffer<int> ring = new RingBuffer<int>(size);
         // add 5 items to a 10 item buffer
         Enumerable.Range(0, 5).ToList().ForEach(i => ring.Add(i));

         // remove one item and validate if the new value at the previosu position is valid
         int value = ring[3];
         ring.Remove(value);
         Assert.AreEqual(ring[3], value + 1);

         // remove the next item and validate the new item at the same position
         value = ring[0];
         ring.Remove(value);
         Assert.AreEqual(ring[0], value + 1);

         // remove another item and validate the count
         value = ring.Count;
         ring.Remove(ring[ring.Count - 1]);
         Assert.AreEqual(value - 1, ring.Count);

         // add 105 new items to exceed the buffer capacity
         Enumerable.Range(0, 105).ToList().ForEach(i => ring.Add(i));

         // remove one item and validate if the new value at the previosu position is valid
         value = ring[3];
         ring.Remove(value);
         Assert.AreEqual(ring[3], value + 1);

         // remove the next item and validate the new item at the same position
         value = ring[0];
         ring.Remove(value);
         Assert.AreEqual(ring[0], value + 1);

         // remove another item and validate the count
         value = ring.Count;
         ring.Remove(ring[ring.Count - 1]);
         Assert.AreEqual(value - 1, ring.Count);
      }

      [TestMethod()]
      public void InsertTest() {
         RingBuffer<int> ring = new RingBuffer<int>(5);

         // insert an item and validate it
         ring.Insert(0, 0);
         Assert.AreEqual(ring[0], 0);
         // insert another item and validate it
         ring.Insert(1, 1);
         Assert.AreEqual(ring[1], 1);
         // insert an item at an existing position and validate it
         ring.Insert(0, 100);
         Assert.AreEqual(ring[0], 100);
         // insert another item at an existing position and validate
         ring.Insert(1, 101);
         Assert.AreEqual(ring[1], 101);
         // insert y.a. item
         ring.Insert(4, 1004);
         Assert.AreEqual(ring[4], 1004);
         // insert y.a. item and validate the item and the count
         ring.Insert(1, 10001);
         Assert.AreEqual(ring[1], 10001);
         Assert.AreEqual(ring.Count, ring.Capacity);
      }

      [TestMethod()]
      public void IndexOfTest() {
         RingBuffer<int> ring = new RingBuffer<int>(5);

         // validate zero is not present
         Assert.AreEqual(ring.IndexOf(0), -1);
         // add an item and validate the index of
         ring.Add(1);
         Assert.AreEqual(ring.IndexOf(1), 0);
         // add 50 new items
         Enumerable.Range(0, 50).ToList().ForEach(i => ring.Add(i));
         // validate 0 and 1 are not present any more
         Assert.AreEqual(ring.IndexOf(0), -1);
         Assert.AreEqual(ring.IndexOf(1), -1);
         // validate 45 is present
         Assert.AreEqual(ring.IndexOf(45), 0);

      }

      [TestMethod()]
      public void GetEnumeratorTest() {
         RingBuffer<int> ring = new RingBuffer<int>(10);
         // helper list to be filled with expected data for validation
         List<int> list = new List<int>();

         // add 10 items to a 10 item buffer and validate it against the list
         Enumerable.Range(0, 10).ToList().ForEach(i => ring.Add(i));
         Enumerable.Range(0, 10).ToList().ForEach(i => list.Add(i));
         Assert.IsTrue(ring.SequenceEqual(list));

         // add 1000 items to a 10 items buffer and validate it against the 
         // list containing the expected data
         Enumerable.Range(0, 1000).ToList().ForEach(i => ring.Add(i));
         list.Clear();
         Enumerable.Range(990, 10).ToList().ForEach(i => list.Add(i));
         Assert.IsTrue(ring.SequenceEqual(list));
      }

      [TestMethod()]
      public void CopyToTest() {
         RingBuffer<int> ring = new RingBuffer<int>(10);
         // helper list to be filled with expected data for validation
         List<int> list = new List<int>();
         int[] arr;

         // add 10 items, copy them into an array and validate the result
         Enumerable.Range(0, 10).ToList().ForEach(i => ring.Add(i));
         Enumerable.Range(0, 10).ToList().ForEach(i => list.Add(i));
         arr = new int[10];
         ring.CopyTo(arr, 0);
         Assert.IsTrue(arr.SequenceEqual(list));

         // add 1000 items to the 10 items buffer, copy to an array and validate the result
         Enumerable.Range(0, 1000).ToList().ForEach(i => ring.Add(i));
         list.Clear();
         Enumerable.Range(990, 10).ToList().ForEach(i => list.Add(i));
         arr = new int[10];
         ring.CopyTo(arr, 0);
         Assert.IsTrue(arr.SequenceEqual(list));

         // copy the data to an array with a non-zero start position and validate
         arr = new int[11];
         ring.CopyTo(arr, 1);
         list.Insert(0, 0);
         Assert.IsTrue(arr.SequenceEqual(list));
         
      }

      [TestMethod()]
      public void ContainsTest() {
         RingBuffer<int> ring = new RingBuffer<int>(5);

         // validate zero is not present
         Assert.IsFalse(ring.Contains(0));
         // add 1 and validate 1 as present and 0 as not present
         ring.Add(1);
         Assert.IsFalse(ring.Contains(0));
         Assert.IsTrue(ring.Contains(1));
         // add some values
         ring.Add(2);
         ring.Add(4);
         ring.Add(39);
         ring.Add(200);
         ring.Add(323);
         // validate 1 is not present any more (since we exceeded the buffer capacity)
         Assert.IsFalse(ring.Contains(1));
         // validate some expected values
         Assert.IsTrue(ring.Contains(200));
         Assert.IsTrue(ring.Contains(2));
      }

      [TestMethod()]
      public void ClearTest() {
         RingBuffer<int> ring = new RingBuffer<int>(10);
         // add one item
         ring.Add(1);
         // clear the buffer
         ring.Clear();
         // validate 1 is not present any more
         Assert.IsFalse(ring.Contains(1));
         // validate the item count is zero
         Assert.AreEqual(ring.Count, 0);
      }

      [TestMethod()]
      public void AddTest() {
         RingBuffer<int> ring = new RingBuffer<int>(5);
         // add 1 and validate its existence and the count
         ring.Add(1);
         Assert.AreEqual(ring.Count, 1);
         Assert.AreEqual(ring[0], 1);
         // add another value and validate it
         ring.Add(44);
         Assert.AreEqual(ring.Count, 2);
         Assert.AreEqual(ring[1], 44);
         // add some move values (exceeding the capacity)
         ring.Add(234);
         ring.Add(232);
         ring.Add(22);
         ring.Add(3240);
         ring.Add(324);
         // validate the last added item and the capacity
         Assert.AreEqual(ring[ring.Count - 1], 324);
         Assert.AreEqual(ring.Count, ring.Capacity);
      }

      [TestMethod()]
      public void PerformanceTest() {
         RingBuffer<string> ring = new RingBuffer<string>(100);
         // add some initial values
         Enumerable.Range(0, 100).ToList().ForEach(i => ring.Add(Guid.NewGuid().ToString()));

         Stopwatch watch = Stopwatch.StartNew();

         // create a pseudo data source
         int count = 1000000;
         List<string> source = new List<string>(count);
         Enumerable.Range(0, count).ToList().ForEach(i => source.Add(Guid.NewGuid().ToString()));
         watch.Stop();
         long fillDuration = watch.ElapsedMilliseconds;
         TestContext.WriteLine("Duration to simply fill a List<string> with {0} items: {1}ms", count, fillDuration);

         watch = Stopwatch.StartNew();
         // loop over the source, add the items to the buffer and do some look backs
         for (int i = 0; i < source.Count; i++) {
            string item = source[i];

            ring.Add(item);
            for (int j = 0; j < 10; j++) {
               item = ring[ring.Count - j - 1];
			   }
			}
         watch.Stop();
         long workDuration = watch.ElapsedMilliseconds;
         TestContext.WriteLine("Duration to add all items to the RingBuffer and always move ten items back: {0}ms", workDuration);

         Assert.IsTrue(workDuration < fillDuration / 2);
      }
   }
}
