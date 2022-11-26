using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.BlockChain.RPC.Model
{

    public class CreateWalletRPCModel : RPCModel
    {
    }
    public class TransactionInfoRPCModel : RPCModel
    {
    }
    public class WalletInfoRPCModel : RPCModel
    {
    }
    public class WithrowResponseRPCModel : RPCModel
    {
    }

    public class RPCModel
    {
        public RPCModel()
        {
            CreateDate = DateTime.UtcNow;
        }

        public DateTime CreateDate { get; private set; }
    }

}
