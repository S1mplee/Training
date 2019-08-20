﻿using DDD.DomainModel;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.ScenarioReporting;

namespace Tests
{
    public class Class1
    {
        [Theory]
        [InlineData("",5000,20000,500)]
        [InlineData("Sami", -5000, 20000,600)]
        [InlineData("Sami", 5000, -20000,355)]
        [InlineData("Sami", 5000, -20000, -355)]
        public void Create_WithInvalidInputShouldThrowException(string name,decimal overdraft,decimal wirelimit,decimal cash)
        {
            var acc = new AccountBalance();
            Assert.Throws<ArgumentException>(() => acc.Create(Guid.NewGuid(), name, overdraft, wirelimit,cash));
        }

        [Theory]
        [InlineData("mohamed", 5000, 20000,236)]
        [InlineData("mohamed", 500, 2000,689)]
        [InlineData("mohamed", 5000.60, 20000.50,565)]
        public async Task Create_WithValidInputsShouldGenerateEvent(string name,decimal overdraft,decimal wirelimit,decimal cash)
        {
            var sc = new ScenarioRunner1();
            var g = Guid.NewGuid();
            var cmd = new AccountCreate(g, name,overdraft, wirelimit, cash);
            var evt = new AccountCreated(g, name,overdraft, wirelimit, cash, false);

            await sc.Run(def => def.Given()
            .When(cmd)
            .Then(evt));
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        [InlineData(-200.56)]
        public void DeposeCheque_WithInvalidAmountShouldThrowException(decimal amount)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed", 500, 200, 1000);
            Assert.Throws<ArgumentException>(() => acc.DeposeCheque(amount));
        }

        [Theory]
        [InlineData(652)]
        [InlineData(750)]
        [InlineData(1000)]
        public async Task DeposeCheque_AccountUnblocked_WithValidAmount_ShouldGenerateOneEvent(decimal amount)
        {
            var sc = new ScenarioRunner2();

            var g = Guid.NewGuid();

            var cmd = new ChequeDepose(g,amount);
            var evt = new ChequeDeposed(g, amount);


            await sc.Run(def => def.Given()
            .When(cmd)
            .Then(evt));
        }

        [Theory]
        [InlineData(800)]
        [InlineData(750)]
        [InlineData(1000)]
        [InlineData(2000)]
        public async Task DeposeCheque_AccountBlocked_WithValidAmount_ShouldUnBlockAccount(decimal amount)
        {
            var sc = new ScenarioRunner3();

            var g = Guid.NewGuid();
            var cmd = new ChequeDepose(g, amount); // command
            var evt = new AccountUnBlocked(g);  // expected event

            await sc.Run(def => def.Given()
           .When(cmd)
           .Then(evt));
        }

        [Theory]
        [InlineData(800)]
        [InlineData(750)]
        [InlineData(1000)]
        [InlineData(1500)]
        [InlineData(1400)]
        [InlineData(1499)]
        public async Task WithdrawhCash_ShouldWorkWithValidamount(decimal amount)
        {
            var sc = new ScenarioRunner4();
            var g = Guid.NewGuid();
            var cmd = new CashWithdraw(g, amount);
            var evt = new CashWithdrawn(g, amount); 

            await sc.Run(def => def.Given()
           .When(cmd)
           .Then(evt));
        }

        [Theory]
        
        [InlineData(1501)]
        [InlineData(1600)]
        [InlineData(3000)]
        public async Task WithdrawCash_ShouldThrowException_AndGenerateanEvent_IfAmountInvalid(decimal amount)
        {
            var sc = new ScenarioRunner6();
            var acc = new AccountBalance();

            var g = Guid.NewGuid();
            var cmd = new CashWithdraw(g, amount);
            var evt = new AccountBlocked(g);
            Assert.Throws<ArgumentException>(() => acc.WithdrawCash(amount));

            await sc.Run(def => def.Given()
          .When(cmd)
          .Then(evt));

        }

        [Theory]

        [InlineData(100)]
        [InlineData(199)]
        [InlineData(50)]
        public async Task WireTransfer_SHouldGenerateOneEvent_IfAmountValid(decimal amount)
        {
            var sc = new ScenarioRunner7();
 
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            var cmd = new CashTransfer(g, g2, amount);
            var evt = new CashTransfered(g, g2,amount);

            await sc.Run(def => def.Given()
         .When(cmd)
         .Then(evt));

        }

        [Theory]

        [InlineData(201)]
        [InlineData(300)]
        [InlineData(400)]
        public async Task WireTransfer_SHouldGeneratetwoEvent_IfAmountInValid(decimal amount)
        {
            var sc = new ScenarioRunner8();

            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            var g2 = Guid.NewGuid();
            var cmd = new CashTransfer(g, g2, amount);
            var evt1 = new CashTransfered(g, g2, amount);
            var evt2 = new AccountBlocked(g);


            await sc.Run(def => def.Given()
           .When(cmd)
            .Then(evt1,evt2));



        }
    }
}
