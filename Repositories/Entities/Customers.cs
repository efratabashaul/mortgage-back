using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{

    public enum Family_Status
    {
        Single,
        Married,
        Divorced,
        Widow
    }
    public enum Job_Status
    {
        Employed,
        SelfEmployed
    }
    public enum Connection
    {
        whatup,
        email
    }

    public enum Customer_Type
    {
        l,
        c,
        a
    }


    public class Customers
    {
        public int Id { get; set; }
        public int Lead_id { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public Connection Connection { get; set; }
        public string t_z { get; set; }/*identity number*/
        public DateTime? birthDate { get; set; }
        public Family_Status Family_status { get; set; }
        public int Number_of_people_in_house { get; set; }
        public string? Address { get; set; }
        public Job_Status Job_status { get; set; }
        public string Work_business_name { get; set; }
        public Customer_Type Customer_type {  get; set; }
        public string Job_description { get; set; }
        public decimal Avarage_monthly_salary { get; set; }
        public int Years_in_current_position { get; set; }
        public decimal Income_rent { get; set; }
        public decimal Income_Government_Endorsement { get; set; }
        public decimal Income_other { get; set; }
        public decimal Expenses_rent { get; set; }
        public decimal Expenses_loans { get; set; }
        public decimal Expenses_other { get; set; }
        public string Property_city { get; set; }
        public TransactionTypeEnum Transaction_type { get; set; }
        public decimal Estimated_price_by_customer { get; set; }
        public decimal Estimated_price_by_sales_agreement { get; set; }
        public bool Has_other_properties { get; set; }
        public decimal Amount_of_loan_requested { get; set; }
        public DateTime? LastSynced { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime? created_at { get; set; }=DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;

    }
}
