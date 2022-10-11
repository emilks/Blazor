using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskinPark.Server.Entities
{
    public class MachineTableEntity: TableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Data { get; set; }

        public DateTime DataTime { get; set; }
    }
}
