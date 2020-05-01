using common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace wServer
{
    public class MarketSweeper
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(MarketSweeper));

        private readonly Timer _tmr = new Timer(60000); // Maybe increase time? The database can be huge for market.
        private readonly Database _db;

        public MarketSweeper(Database db)
        {
            _db = db;
        }

        public void Run()
        {
            _log.Info("Market Sweeper started.");
            _tmr.Elapsed += (sender, e) =>
            {
                _db.CleanMarket();
            };
            _tmr.Start();
        }
    }
}
