using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;

namespace Repositories.Repositories
{
    public class CustomerRepository : IRepository<Customers>
    {
        private readonly IContext _context;

        public CustomerRepository(IContext context)
        {
            _context = context;
        }
        public async Task<Customers> AddItemAsync(Customers item)
        {
            await _context.Customers.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Customers.Remove(await GetAsync(id));
            await _context.save();
        }

        public async Task<List<Customers>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers> GetAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post(Customers item)
        {
            await _context.Customers.AddAsync(item);
            await _context.save();
        }

        public async Task UpdateAsync(int id, Customers entity)
        {
            var customer = await GetAsync(id);
            customer.Lead_id = entity.Lead_id;
            customer.Last_Name = entity.Last_Name;
            customer.First_Name = entity.First_Name;
            customer.Email = entity.Email;
            customer.Phone = entity.Phone;
            customer.Connection=entity.Connection;
            customer.t_z = entity.t_z;
            customer.birthDate = entity.birthDate;
            customer.Family_status = entity.Family_status;
            customer.Number_of_people_in_house = entity.Number_of_people_in_house;
            customer.Address = entity.Address;
            customer.Job_status = entity.Job_status;
            customer.Work_business_name = entity.Work_business_name;
            customer.Job_description = entity.Job_description;
            customer.Avarage_monthly_salary = entity.Avarage_monthly_salary;
            customer.Years_in_current_position = entity.Years_in_current_position;
            customer.Income_rent = entity.Income_rent;
            customer.Income_Government_Endorsement = entity.Income_Government_Endorsement;
            customer.Income_other = entity.Income_other;
            customer.Property_city = entity.Property_city;
            customer.Transaction_type = entity.Transaction_type;
            customer.Estimated_price_by_customer = entity.Estimated_price_by_customer;
            customer.Estimated_price_by_sales_agreement = entity.Estimated_price_by_sales_agreement;
            customer.Has_other_properties = entity.Has_other_properties;
            customer.Amount_of_loan_requested= entity.Amount_of_loan_requested; 
            customer.LastSynced= entity.LastSynced;
            customer.IsArchived= entity.IsArchived;
            customer.created_at = entity.created_at;
            customer.updated_at = entity.updated_at;
            await _context.save();
        }

        public async Task<Customers> UpdateItemAsync(int id, Customers entity)
        {
            var customer = await GetAsync(id);
            customer.Lead_id = entity.Lead_id;
            customer.Last_Name = entity.Last_Name;
            customer.First_Name = entity.First_Name;
            customer.Email = entity.Email;
            customer.Phone = entity.Phone;
            customer.Connection = entity.Connection;
            customer.t_z = entity.t_z;
            customer.birthDate = entity.birthDate;
            customer.Family_status = entity.Family_status;
            customer.Number_of_people_in_house = entity.Number_of_people_in_house;
            customer.Address = entity.Address;
            customer.Job_status = entity.Job_status;
            customer.Work_business_name = entity.Work_business_name;
            customer.Job_description = entity.Job_description;
            customer.Avarage_monthly_salary = entity.Avarage_monthly_salary;
            customer.Years_in_current_position = entity.Years_in_current_position;
            customer.Income_rent = entity.Income_rent;
            customer.Income_Government_Endorsement = entity.Income_Government_Endorsement;
            customer.Income_other = entity.Income_other;
            customer.Property_city = entity.Property_city;
            customer.Transaction_type = entity.Transaction_type;
            customer.Estimated_price_by_customer = entity.Estimated_price_by_customer;
            customer.Estimated_price_by_sales_agreement = entity.Estimated_price_by_sales_agreement;
            customer.Has_other_properties = entity.Has_other_properties;
            customer.Amount_of_loan_requested = entity.Amount_of_loan_requested;
            customer.LastSynced = entity.LastSynced;
            customer.IsArchived = entity.IsArchived;
            customer.created_at = entity.created_at;
            customer.updated_at = entity.updated_at;
            await _context.save();
            return customer;
        }
    }
}
