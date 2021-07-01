using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TorebaCrawlerCore
{
    public class DataAccess : IDataAccess
    {

        //       public List<Machine> GetMachine(string PrizeName)
        //       {
        //           using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SampleDB")))
        //           {
        ////               var output = connection.Query<Machine>("dbo.Machine_GetByPrizeName @Name", new { Name = Name }).ToList();

        //               return output;
        //           }

        //       }

        public void InsertMachine(Machine machine)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TorebaMachineDB")))
            {
                List<Machine> machines = new List<Machine>();
                machines.Add(machine);

                connection.Execute("dbo.Machine_InsertUpdate @MachineID, @PrizeID, @MachineName, @Cost, @MachineType, @CostType, @StatusType, @SpecialTagType", machines);
            }
        }

        public void InsertReplay(Replay replay)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TorebaMachineDB")))
            {
                List<Replay> replays = new List<Replay>();
                replays.Add(replay);
                connection.Execute("dbo.Replay_Insert @ReplayID, @MachineID, @ReplayLink, @PrizeName, @Time", replays);
            }
        }

        public List<Replay> GetReplayByReplayID(int replayID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("TorebaMachineDB")))
            {
                var output = connection.Query<Replay>("dbo.Replay_GetByReplayID @ReplayID", new { ReplayID = replayID }).ToList();

                return output;
            }
        }

    }
}
