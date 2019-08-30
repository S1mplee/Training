using Account;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class WireTransfertLimit : TestService
    {
        public WireTransfertLimit() : base()
        {

        }

        [Fact]
        public void can_wiretransfet_validCompte()
        {
            var g = Guid.NewGuid();
            var g2 = Guid.NewGuid();
            var cmd = new CreateAccount(g, "MMMMMMMMMMMMMMM");
            var cmd2 = new CreateAccount(g2, "GGGGGGGGGG");
            this.Command.Handle(cmd);
            this.Command.Handle(cmd2);
            var cmd3 = new TransferCash(g, g2, 100);
            Assert.IsType<Success>(this.Command.Handle(cmd3));
            Thread.Sleep(2000);
            Assert.Equal(1100, this._readModel.list.Find(x => x.Id == g2).cash);

        }

        [Fact]
        public void cannot_transfertCash_to_invalid_compte()
        {
            var cmd = new TransferCash(Guid.NewGuid(), Guid.NewGuid(), 100);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd));
        }

        [Fact]
        public void can_block_account_surpass_wire_limit()
        {
            var g = Guid.NewGuid();
            var g2 = Guid.NewGuid();
            var cmd = new CreateAccount(g, "MMMMMMMMMMMMMMM");
            var cmd2 = new CreateAccount(g2, "GGGGGGGGGG");
            this.Command.Handle(cmd);
            this.Command.Handle(cmd2);
            var cmd3 = new TransferCash(g, g2, 1000);
            Assert.IsType<Success>(this.Command.Handle(cmd3));
            Thread.Sleep(2000);
            Assert.True(this._readModel.list.Find(x => x.Id == g).blocked);

        }
    }
}
