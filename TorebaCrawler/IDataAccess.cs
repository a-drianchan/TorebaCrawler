using System.Collections.Generic;

namespace TorebaCrawlerCore
{
    public interface IDataAccess
    {
        List<Replay> GetReplayByReplayID(int replayID);
        void InsertMachine(Machine machine);
        void InsertReplay(Replay replay);
    }
}