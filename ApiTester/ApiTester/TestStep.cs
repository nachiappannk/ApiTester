using System;
using System.Threading.Tasks;

namespace ApiTester
{
    public enum StepResult
    {
        Pass,
        Fail,
        FailAndBreak
    }


    public class TestStep
    {
        private Func<IContext, Task<StepResult>> _testStepCode;

        public TestStep(Action<IContext> testAction, string name)
        {
            Name = name;
            _testStepCode = async (context) =>
            {
                testAction.Invoke(context);
                return StepResult.Pass;
            };
        }

        public TestStep(Func<IContext, StepResult> testAction, string name)
        {
            Name = name;
            _testStepCode = async (context) => testAction.Invoke(context);
        }

        public TestStep(Func<IContext, Task> testAction, string name)
        {
            Name = name;
            _testStepCode = async (context) =>
            {
                await testAction.Invoke(context);
                return StepResult.Pass;
            };
        }

        public TestStep(Func<IContext, Task<StepResult>> testAction, string name)
        {
            Name = name;
            _testStepCode = async (context) => await testAction.Invoke(context);
        }

        public string Name { get; }

        public async Task ExecuteStep(IContext context)
        {
            await _testStepCode.Invoke(context);
        }
    }
}