using Commands;
using DDD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.ScenarioReporting;

namespace Tests
{
    
    public class ScenarioRunner : ReflectionBasedScenarioRunner<Event, List<Command>, Event>
    {
        private CommandHandler CmdHandler;
        private EventStoreMock evenstore;
        private Guid id;
        public ScenarioRunner()
        {
            evenstore = new EventStoreMock();
            CmdHandler = new CommandHandler(evenstore);
        }
        protected override Task<IReadOnlyList<Event>> ActualResults()
        {
            IReadOnlyList<Event> list = new List<Event>();
            list = evenstore.dict[id];
            return Task.FromResult((list));
        }

        

        protected override Task Given(IReadOnlyList<Event> givens)
        {
            return Task.CompletedTask;
        }


        protected override Task When(List<Command> when)
        {
            dynamic d = when[0];
            this.id = d.AccountId;

            foreach (var elem in when )
            {
                dynamic cmd = elem;
                this.CmdHandler.Handle(cmd);
            }
            return Task.CompletedTask;
        }
    }

}

