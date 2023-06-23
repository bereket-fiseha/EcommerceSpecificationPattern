
using Domain.DTO.OrderModule.OrderDTOS;

using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITaxService
    {

        public Task<IEnumerable<TaxQueryDTO>> GetAllTaxsAsync();



        public Task<TaxQueryDTO> GetTaxByIdAsync(Guid id);

        public Task CreateTaxAsync(TaxCommandDTO Tax);


        public Task UpdateTaxAsync(TaxCommandDTO Tax);


        public Task DeleteTaxAsync(TaxCommandDTO Tax);







    }
}
