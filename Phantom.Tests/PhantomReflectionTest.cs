using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Phantom.Tests
{
    [TestFixture]
    class PhantomReflectionTest
    {
        [Test]
        public void Indexer()
        {
            object person = new Person { Name = "Wagner" };

            var reflection = new PhantomReflection(person);

            reflection["Age"] = 24;

            Assert.AreEqual("Wagner", reflection["Name"]);
            Assert.AreEqual(24, reflection["Age"]);
        }

        [Test]
        public void MethodValid()
        {
            object name = "Francine";

            var reflection = new PhantomReflection(name);

            var valid = reflection.IsMethodValid("Split");

            Assert.AreEqual(true, valid);
        }

        [Test]
        public void InvokeMethod()
        {
            object name = "Arthur";

            var reflection = new PhantomReflection(name);

            var upperName = reflection.InvokeMethod("ToUpper");

            Assert.AreEqual("ARTHUR", upperName);
        }

        [Test]
        public void InvokeMethodIfIsValid()
        {
            object name = "Arthur";

            var reflection = new PhantomReflection(name);

            var validResult = reflection.InvokeMethodIfIsValid("ToUpper");

            var invalidResult = reflection.InvokeMethodIfIsValid("MethodNotFound");

            Assert.AreEqual("ARTHUR", validResult);
            Assert.IsNull(invalidResult);
        }
    }
}