using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTester
{
    public class TestCase
    {
        List<TestStep> _steps = new List<TestStep>();

        public TestCase AddStep(Action<IContext> action, string name = "un-named")
        {
            _steps.Add(new TestStep(action, name));
            return this;
        }

        public TestCase AddStep(Func<IContext, StepResult> action, string name = "un-named")
        {
            _steps.Add(new TestStep(action, name));
            return this;
        }

        public TestCase AddStep(Func<IContext, Task> action, string name = "un-named")
        {
            _steps.Add(new TestStep(action, name));
            return this;
        }

        public TestCase AddStep(Func<IContext, Task<StepResult>> action, string name = "un-named")
        {
            _steps.Add(new TestStep(action, name));
            return this;
        }

        public async Task RunTest(Context context)
        {
            foreach (var testStep in _steps)
            {
                await testStep.ExecuteStep(context);
            }
        }
    }
}