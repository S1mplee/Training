using DotNetify;
using System;

namespace Reactjs_Account
{
    public class EditViewModel : BaseVM
    {
        public readonly Service _accountService;


        private string _ID;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; Changed(nameof(ID)); }
        }

        public string over { get; set; }

        public string daily { get; set; }

        public string message { get; set; }

        public EditViewModel(Service service)
        {
            daily = "";
            over = "";
            _accountService = service;
        }

        public EditViewModel()
        {
        }

        public Action<string> ButtonClicked => e =>
        {

            this.message = "";
            try
            {
                if (string.IsNullOrEmpty(over) && string.IsNullOrEmpty(daily))
                {
                    throw new ArgumentException("Empty Fields !");
                }
                var b1 = decimal.TryParse(over, out decimal Over);
                var b2 = decimal.TryParse(daily, out decimal Daily);
                
                if ((!string.IsNullOrEmpty(over) && !b1 ) || (!string.IsNullOrEmpty(daily) && !b2))
                {
                    throw new ArgumentException("Values Should Be numeric !");
                }

                if (!string.IsNullOrEmpty(over))
                {
                    var cmd = new SetOverdraftLimit(Guid.Parse(e), Over);
                    _accountService.Bus.Send(cmd);
                    this.message += "Set Over Draft Succes !";
                    Changed(nameof(message));
                }

                if (!string.IsNullOrEmpty(daily))
                {
                    var cmd = new SetDailyTransfertLimit(Guid.Parse(e), Daily);
                    _accountService.Bus.Send(cmd);
                    this.message += " Set Daily Transfert limit Success !";
                    Changed(nameof(message));
                }
            }catch(Exception ex)
            {
                this.message = ex.Message;
                Changed(nameof(this.message));
                throw new ArgumentException("");
            }
           
        };

    }
}
