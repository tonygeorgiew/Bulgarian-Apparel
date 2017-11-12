using Bulgarian_Apparel.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulgarian_Apparel.Data.Models;
using Bulgarian_Apparel.Data.Models.Contracts;
using Bulgarian_Apparel.Data.SaveContext;

namespace Bulgarian_Apparel.Services
{
    public class PaymentTypesService : IPaymentTypesService
    {
        private readonly IEfRepository<PaymentType> paymentTypesRepo;
        private readonly IUnitOfWork UoW;

        public PaymentTypesService(IEfRepository<PaymentType> paymentTypesRepo, IUnitOfWork UoW)
        {
            this.paymentTypesRepo = paymentTypesRepo;
            this.UoW = UoW;
        }


        public IQueryable<PaymentType> GetAll()
        {
            return this.paymentTypesRepo.All;
        }
    }
}
