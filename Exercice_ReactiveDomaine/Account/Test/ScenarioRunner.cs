using Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.ScenarioReporting;

namespace Test
{
    public class ScenarioRunner : ReflectionBasedScenarioRunner<object, List<object>, object>
    {
        private AccountCommandHandler CmdHandler;
        private EventStoreMock evenstore;
        private Guid id;
        public ScenarioRunner()
        {
            evenstore = new EventStoreMock();
            CmdHandler = new AccountCommandHandler(evenstore);
        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {
            IReadOnlyList<object> list = new List<object>();
            
            list = evenstore.dict[id];
                return Task.FromResult((list));
        }



        protected override Task Given(IReadOnlyList<object> givens)
        {
            return Task.CompletedTask;
        }


        protected override Task When(List<object> when)
        {
            dynamic d = when[0];
            this.id = d.Id;

            foreach (var elem in when)
            {
                dynamic cmd = elem;
                this.CmdHandler.Handle(cmd);
            }
            return Task.CompletedTask;
        }
    }

}
