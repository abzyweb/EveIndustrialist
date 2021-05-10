using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSwaggerConnection.ESI_Communication.Operations.Wallet;

namespace EoiData.FileSystemDataClasses
{
    public class FileSystemDataAsset
    {
        public long TypeId { get; set; }
        public int Quantity { get; set; }
        public List<RawEsiTransaction> Transactions { get; set; }

        public FileSystemDataAsset()
        {
            Transactions = new List<RawEsiTransaction>();
        }

        internal void AddTransaction(RawEsiTransaction transaction)
        {
            if (Transactions.FirstOrDefault(x => x.transaction_id == transaction.transaction_id) != null)
                return;

            Transactions.Add(transaction);
        }

        internal void Clean()
        {
            var orderedTransactions = GetOrderedTransactions();
            var quantity = this.Quantity;

            foreach (var transaction in orderedTransactions)
            {
                if (quantity <= 0)
                    Transactions.Remove(transaction);
                else
                    quantity -= transaction.quantity;
                
            }
        }

        internal List<RawEsiTransaction> GetOrderedTransactions()
        {
            return Transactions.OrderByDescending(x => DateTime.Parse(x.date).Ticks).ToList();
        }
    }
}
