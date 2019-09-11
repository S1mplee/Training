using DotNetify;
using System;
using System.Collections.Generic;
using Account;
using System.Threading;

namespace Reactjs_Account
{
    public class AccountViewModel : BaseVM
    {
        public readonly Service _accountService;

        private List<account> _list;
        public List<account> list 
        {
            get { return _list; }
            set { _list = value; Changed(nameof(list)); }
        }

        private string _value; // balance input 
        public string value
        {
            get { return _value; }
            set { _value = value; Changed(nameof(value)); }
        }

        private string _name; // Account Name 
        public string name
        {
            get { return _name; }
            set { _name = value; Changed(nameof(name)); }
        }


        private string _message; // error message
        public string message
        {
            get { return _message; }
            set { _message = value; Changed(nameof(message)); }
        }

        private Timer _timer;

        public AccountViewModel(Service service)
        {
            this.message = "";
            _accountService = service;
            list = new List<account>();
            list = _accountService._readModel.list;
            Changed(nameof(list));

            _timer = new Timer(state =>
            {
                Changed(nameof(list));
                PushUpdates();
            }, null, 0, 1000);
        }

        public override void Dispose() => _timer.Dispose();

        public Action<string> id => e =>
        {
            try
            {
                if (string.IsNullOrEmpty(this.value)) throw new ArgumentException("Invalid Input !");
                if (!int.TryParse(this.value, out int res)) throw new ArgumentException("The input Should be Number !");
                if (res <= 0 ) throw new ArgumentException("The input Should be Postive !");
                if (string.IsNullOrEmpty(e)) throw new ArgumentException("Invalid ID");

                this._accountService.Bus.Send(new DeposeCash(Guid.Parse(e), int.Parse(this.value)));
                // list = this._accountService._readModel.list;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                throw new ArgumentException();
            }

        };

        public Action<string> accountid => e =>
        {
            try
            {
                if (string.IsNullOrEmpty(this.value)) throw new ArgumentException("Invalid Value !");
                if (!int.TryParse(this.value, out int res)) throw new ArgumentException("The input Should be Number !");
                if (res <= 0) throw new ArgumentException("The input Should be Postive !");
                if (string.IsNullOrEmpty(e)) throw new ArgumentException("Invalid ID");

                this._accountService.Bus.Send(new WithDrawCash(Guid.Parse(e), int.Parse(this.value)));
                // list = this._accountService._readModel.list;
            }catch(Exception ex)
            {
                this.message = ex.Message;
                throw new ArgumentException();
            }
           
        };

             

        public Action<bool> clicked => _ =>
        {
            try
            {
                if (string.IsNullOrEmpty(this.name) || string.IsNullOrEmpty(this.name)) throw new ArgumentException("Empty Inputs !");
                this._accountService.Bus.Send(new CreateAccount(Guid.NewGuid(), this.name));

            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                throw new ArgumentException();
            }
        };

        public Action<string> Tid => e =>
        {
            try
            {
                if (string.IsNullOrEmpty(this.value)) throw new ArgumentException("Invalid Value !");
                if (!int.TryParse(this.value, out int res)) throw new ArgumentException("The input Should be Number !");
                if (res <= 0) throw new ArgumentException("The input Should be Postive !");
                if (string.IsNullOrEmpty(e)) throw new ArgumentException("Invalid ID");

                var cmd = new TransferCash(Guid.Parse(e), res, DateTime.Now);
                _accountService.Bus.Send(cmd);
            }catch(Exception ex)
            {
                this.message = ex.Message;
                throw new ArgumentException();
            }
       

        };

        public Action<string> ID => e =>
        {
            this.EditID = e;
            Changed(nameof(EditID));
        };

        public string EditID { get; set; }

    }

    
}
