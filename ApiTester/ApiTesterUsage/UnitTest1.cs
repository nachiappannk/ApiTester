using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
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
                .AddStep(x => TestSteps.Login1(x))
                .RunTest(context);

            Action<IContext> s1;
            Func<IContext, StepResult> s2;
            Func<IContext, Task> s3;
            Func<IContext, Task<StepResult>> s4;
            s2 = TestSteps.Login1;


        }
    }


    public static class TestSteps
    {
        public static async Task<StepResult> Login(IContext context)//s3 and s4
        {
            await Task.Delay(1000);
            return StepResult.FailAndBreak;
        }

        public static StepResult Login1(IContext context)
        {
            return StepResult.FailAndBreak;
        }
    }


}