using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Phantom;

namespace Phantom.Tests
{
    [TestFixture]
    class PhantomExtensionsTest
    {
        [Test]
        public void Get()
        {
            object person = new Person { Name = "Arthur", Age = 4 };

            Assert.AreEqual("Arthur", person.Get("Name"));
        }

        [Test]
        public void GetString()
        {
            object person = new Person { Name = "Arthur", Age = 4 };

            Assert.AreEqual("Arthur", person.GetString("Name"));
        }

        [Test]
        public void Set()
        {
            object person = new Person { Name = "Arthur", Age = 4 };

            person.Set("Name", "Arthur Andrade");

            Assert.AreEqual("Arthur Andrade", person.Get("Name"));
        }

        [Test]
        public void Invoke()
        {
            object name = "Arthur";

            name = name.Invoke("ToString");

            var upperName = name.Invoke("ToUpper");

            Assert.AreEqual("ARTHUR", upperName);
        }

        [Test]
        public void InvokeWithParameters()
        {
            object name = "Francine Santos";

            var splitName = name.Invoke("Split", new[] { ' ' }) as string[];

            Assert.AreEqual("Santos", splitName[1]);
        }

        [Test]
        public void Properties()
        {
            object person = new Person { Name = "Arthur", Age = 4 };

            var propertiesInfo = person.Properties();

            propertiesInfo[1].SetValue(person, 5, null);

            Assert.AreEqual(5, person.Get("Age"));
        }

        [Test]
        public void Values()
        {
            object person = new Person { Name = "Francine", Age = 22 };

            var properties = person.Values();

            Assert.AreEqual(3, properties.Count);
            Assert.AreEqual("Francine", properties["Name"]);
        }

        [Test]
        public void ImportValues()
        {
            object mother = new Person { Name = "Francine", Age = 22 };
            object son = new Person { Age = 4, Gender = "M" };

            mother.ImportValues(son);

            Assert.AreEqual(4, mother.Get("Age"));
        }

        [Test]
        [ExpectedException(typeof(PhantomException))]
        public void MethodNotFoundException()
        {
            object name = "Wagner";

            name.Invoke("MethodNotFound");
        }

        [Test]
        [ExpectedException(typeof(PhantomException))]
        public void PropertyNotFoundException()
        {
            object person = new Person { Name = "Francine", Age = 22 };

            person.Get("Height");
        }
    }

    class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
    }
}