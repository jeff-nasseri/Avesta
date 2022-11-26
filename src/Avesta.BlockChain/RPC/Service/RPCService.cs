using Avesta.BlockChain.RPC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.BlockChain.RPC.Service
{
    public interface IRPCService
    {
        Task<CreateWalletRPCModel> CreateWallet();
        Task<TransactionInfoRPCModel> GetTransactionInfoByTransactionHash(string transactionHash);
        Task<WalletInfoRPCModel> GetWalletInfoByAdress(string walletAddres);
        Task<WithrowResponseRPCModel> Withrow(string pvKey, string destinationAdress, decimal amount);
    }


}
