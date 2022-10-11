using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskinPark.Shared;
using MaskinPark.Server.Entities;
using System.Reflection.PortableExecutable;
using Machine = MaskinPark.Shared.Machine;

namespace MaskinPark.Server.Extensions
{
    public static class Mapper
    {
        public static MachineTableEntity ToTableEntity(this Machine machine)
        {
            return new MachineTableEntity
            {
                RowKey = machine.Id,
                Name = machine.Name,
                Status = machine.Status,
                Data = machine.Data,
                DataTime = machine.DataTime,
                PartitionKey = "Todo"
            };
        }

        public static Machine ToMachine(this MachineTableEntity machineTable)
        {
            return new Machine
            {
                Id = machineTable.RowKey,
                Name = machineTable.Name,
                Status = machineTable.Status,
                Data = machineTable.Data,
                DataTime = machineTable.DataTime,
            };
        }
    }
}
