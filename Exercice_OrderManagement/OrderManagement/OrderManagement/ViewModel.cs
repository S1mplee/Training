using System;
using DotNetify;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using TESTDICT;

namespace HelloWorld
{
    public class ViewModel : BaseVM
    {
        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; Changed(nameof(Price)); }
        }

        public List<string> Assets = new List<string>();

        public Dictionary<string, OrderDetail> dict { get; set; }
       

        private string _qts;
        public string Qts
        {
            get { return _qts; }
            set { _qts = value; Changed(nameof(Qts)); }
        }


        private Service App;

        public List<string> SimpleDropDownOptions => new List<string> { "Buy", "Sell" };
        public string SimpleDropDownValue
        {
            get => Get<string>() ?? "";
            set
            {
                Set(value);
                Changed(() => SimpleDropDownResult);
            }
        }
        public string SimpleDropDownResult => $"You selected : "+SimpleDropDownValue;

        public List<string> SimpleDropDownOptions2 => new List<string> { "Asset1", "Asset2","Asset3" };
        public string SimpleDropDownValue2
        {
            get => Get<string>() ?? "";
            set
            {
                Set(value);
                Changed(() => SimpleDropDownResult2);
            }
        }
        public string SimpleDropDownResult2 => $"You selected : " + SimpleDropDownValue2;

        public string history { get; set; }

        public string message1 { get; set; }

        public string message2 { get; set; }

        public string message3 { get; set; }

        public string Total { get; set; }


        public ViewModel(Service app)
        {
            dict = new Dictionary<string, OrderDetail>();
            this.App = app;
             App.Bootstrap();
            // Thread.Sleep(1000);
            this.history = App._readModel.history;
            this.Assets = App._readModel.Assets.Keys.ToList();
            this.dict = App._readModel.Assets;
            this.Total = ""+ App._readModel.Total;

            Changed(nameof(history));
            Changed(nameof(Assets));
            Changed(nameof(dict));
            Changed(nameof(Total));


        }

        public Action<bool> ButtonClicked => e =>
        {
            try
            {
                if (string.IsNullOrEmpty(SimpleDropDownValue) || string.IsNullOrEmpty(SimpleDropDownValue2))
                {
                    this.message3 = "You Should Pick Action and the Asset !";
                    throw new InvalidOperationException("");
                }
                if (string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Qts)) {
                    this.message1 = "Empty Fields !";
                    this.message2 = "Empty Fields !";
                    throw new ArgumentException("Empty Fields !"); }
                if (!int.TryParse(Qts, out int qts))
                {
                    this.message2 = "Quantite should be numeric !";
                    throw new ArgumentException("Quantite should be numeric !");
                }
                if (!decimal.TryParse(Price, out decimal price))
                {
                    this.message1 = "Price should be numeric !";
                    throw new ArgumentException("Price should be numeric !"); }

                if (qts <= 0 || price <= 0) throw new ArgumentException("Values Should Be postive !");

                var Cmd = new CreateOrder(Guid.NewGuid(), SimpleDropDownValue, SimpleDropDownValue2, price, qts);

                if (SimpleDropDownValue.ToLower().Equals("sell"))
                {
                    if (!App._readModel.Assets.TryGetValue(SimpleDropDownValue2, out OrderDetail or))
                    {
                        this.message3 = " You dont have Any Assets To sell";
                        throw new InvalidOperationException("You dont have Any Assets To sell ");
                    }
                    else if (or.Quantite < qts)
                    {
                        this.history += " Can t Sell " + Cmd.Quantite + " " + Cmd.Asset + " Not Enough Quantite \n";
                        throw new InvalidOperationException("You dont have Any Assets To sell ");
                    }else
                    {
                        App.cmd.Handle(Cmd);
                    }
                }
                else
                {
                    App.cmd.Handle(Cmd);

                }

                Thread.Sleep(1000);
                this.history = App._readModel.history;
                Changed(nameof(history));
            }catch(Exception ex)
            {
                Changed(nameof(message3));
                Changed(nameof(message1));
                Changed(nameof(message2));
                Changed(nameof(history));
            }

            this.Total = "" + App._readModel.Total;

            Changed(nameof(dict));
            Changed(nameof(Total));

        };

    }
    
}