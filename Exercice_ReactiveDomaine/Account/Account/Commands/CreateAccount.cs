using ReactiveDomain.Messaging;
using System;

namespace Account
{
    
    public class CreateAccount : Command
    {
        public readonly Guid Id;
        public string Holdername;
      
        
        public CreateAccount(Guid id, string name) 
        {
            Id = id;
            Holdername = name;
        }
    }
    
}
