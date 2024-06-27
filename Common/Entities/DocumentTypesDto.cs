using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class DocumentTypesDto
    {
        public TransactionTypeEnum Transaction_Type { get; set; }
        public string Document_Name { get; set; }
        public bool Required { get; set; }
    }
}
