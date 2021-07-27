using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTests
{
    [SetUpFixture]
    public class AssemblyInit
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            
        } 

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}
