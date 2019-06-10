using System;
using System.Collections.Generic;
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
            var context = new Context(new Config(new Dictionary<string, string>(){{"param1", "value1"}}));

            await new TestCase()
                .AddStep(x => Console.WriteLine("FirstStep"))
                .AddStep(async (x) =>
                {
                    await Task.Delay(5000);
                    Console.WriteLine("Second test");
                    return StepResult.Fail;
                })
                .AddStep(async (x) => Console.WriteLine("Step 3"))
                .AddStep(x =>
                {
                    Console.WriteLine("Step 4");
                    return StepResult.Pass;
                })
                .RunTest(context);

        }
    }
}