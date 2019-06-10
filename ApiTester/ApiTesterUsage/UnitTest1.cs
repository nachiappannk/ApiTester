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
            //print logs
            //Get test result
            //Exception
            //Exception Continutation
            //Returned results
            



            var context = new Context(new Config(new Dictionary<string, string>(){{"param1", "value1"}}));
            context.SetService<HttpClient>("httpClient", new HttpClient());
            await new TestCase()
                .AddStep(x => x.Log("FirstStep"))
                .AddStep(async (x) =>
                {
                    var httpClient = x.GetService<HttpClient>("httpClient");
                    x.Log("Hash code is "+httpClient.GetHashCode());
                    await Task.Delay(1000);
                    x.Log("Second test");
                    return StepResult.Fail;
                })
                .AddStep(async (x) => x.Log("Step 3"))
                .AddStep(x =>
                {
                    x.Log("Step 4");
                    return StepResult.Pass;
                })
                .RunTest(context);

        }
    }
}