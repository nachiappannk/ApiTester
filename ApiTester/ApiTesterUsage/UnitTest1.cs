using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiTester;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //Get test result
            //Exception Continutation
            
            var context = new Context(new Config(new Dictionary<string, string>(){{"param1", "value1"}, { "param2", "value2" }}),
                new Config(new Dictionary<string, string>() { { "param1", "my-value" }}));
            context.SetService<HttpClient>("httpClient", new HttpClient());
            await new TestCase()
                .AddStep(x => x.Log("FirstStep"))
                .AddStep(async (x) =>
                {
                    var httpClient = x.GetService<HttpClient>("httpClient");
                    x.Log("Hash code is "+httpClient.GetHashCode());
                    x.Log(x.GetValue("param1"));
                    x.Log(x.GetValue("param2"));
                    await Task.Delay(1000);
                    x.Log("Second test");
                    return StepResult.Fail;
                })
                .AddStep(async (x) => x.Log("Step 3"))
                .AddStep(x =>
                {
                    x.Log("Step 4");
                    Assert.AreEqual(4,5);
                })
                .RunTest(context);

        }
    }
}