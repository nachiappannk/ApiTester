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
            var isPass = true;
            var exit = false;
            for (int i = 0; i < _steps.Count; i++)
            {
                if(exit) break;
                var stepName = _steps[i].Name;
                context.Log($"Started Step {i} with {stepName}");
                try
                {
                    var result = await _steps[i].ExecuteStep(context);
                    switch (result)
                    {
                        case StepResult.Pass:
                            context.Log($"Passed: Started Step {i} with {stepName}");
                            break;
                        case StepResult.Fail:
                            context.Log($"Failed: Started Step {i} with {stepName}");
                            isPass = false;
                            break;
                        case StepResult.FailAndBreak:
                            context.Log($"Failed and Broke: Started Step {i} with {stepName}");
                            isPass = false;
                            exit = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                    context.Log($"Failed due to Exception: Started Step {i} with {stepName} {e.StackTrace}");
                }
                context.Log("");
            }

            foreach (var log in context.GetLogs())
            {
                Console.WriteLine(log);
            }

            if (!isPass) throw new Exception("Failed");
        }
        
    }
}