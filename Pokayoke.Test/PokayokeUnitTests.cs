using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System;

using com.Lighteous.Pokayoke;
using com.Lighteous.Pokayoke.Enums;


namespace Pokayoke.Test {


    [TestClass]
    public class PokayokeUnitTests {
        private TestHarness TestHarness { get; set; } = new TestHarness(null);

        private IConfiguration config { get; set; }

        [TestInitialize]
        public void Startup() {
            TestHarness = new TestHarness(null);
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").SetBasePath(Environment.CurrentDirectory).AddEnvironmentVariables().Build();
        }

        [TestCleanup]
        public void Teardown() {
            TestHarness = null;
            config = null;
        }

        [TestMethod]
        public void TestHarnessTest() {

            var connString = config.GetSection("ConnectionStrings").GetSection("ApplicationContext").Value;

            TestHarness = new TestHarness(new List<IPokayoke> { new InternetConnectionPokayoke()
                                                              , new PingPokayoke("www.yahoo.com")
                                                              , new DatabasePokayoke(connString, DbConnectionType.SQLServer)
                                                              , new WebServicePokayoke("Temp Converter Service", "https://www.w3schools.com/XML/tempconvert.asmx")
                                                              , new WebAPIPokayoke("Cat Facts API", "https://cat-fact.herokuapp.com", "/facts/58e0088b0aac31001185ed09")
                                                              , new DirectoryPokayoke("Base Directory", Environment.CurrentDirectory)
                                        });
            TestHarness.Test();

            foreach(var evt in TestHarness.Events) {
                Console.WriteLine(evt.Note);
            }

            Assert.IsTrue(TestHarness.Pass);
        }


        [TestMethod]
        public void TestEntityPokayoke() {
            var testEntity = new TestEntity();
            testEntity.CustomerEmail = "somevalue";
            testEntity.CustomerPhone = "somevalue";
            testEntity.RangeValue = 21;

            var entityPokayoke = new EntityPokayoke<TestEntity>(testEntity);

            TestHarness = new TestHarness(new List<IPokayoke> { entityPokayoke });
            TestHarness.Test();

            Assert.IsTrue(TestHarness.Pass);
        }

    }

    public class TestEntity {
        public TestEntity() { }

        [RegularExpression(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}", ErrorMessage = "Value format MUST be an email address.")]
        [MaxLength(20, ErrorMessage = "Max length is 20 characters.")]
        [MinLength(6, ErrorMessage = "Min length is 6 characters.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Min length is 6 characters. Max length is 20 characters.")]
        [EmailAddress]
        [Required(ErrorMessage = "CustomerEmail is required.")]
        [NotMapped]
        public string CustomerEmail { get; set; }

        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Value format MUST be a phone number.")]
        [MaxLength(20, ErrorMessage = "Max length is 20 characters.")]
        [MinLength(10, ErrorMessage = "Min length is 10 characters.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Min length is 10 characters. Max length is 20 characters.")]
        [Phone]
        [Required(ErrorMessage = "CustomerPhone is required.")]
        [NotMapped]
        public string CustomerPhone { get; set; }


        [Range(1, 20, ErrorMessage = "Value can only be between 1 and 20")]
        public int RangeValue { get; set; }
    }
}
