using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.Model
{
    public class StatisticOption
    {
        public static StatisticOption ItemSoldMinecraft => new StatisticOption("item_sold_minecraft");
        public static StatisticOption PrepaidCardRedeemedMinecraft => new StatisticOption("prepaid_card_redeemed_minecraft");
        public static StatisticOption ItemSoldCobalt => new StatisticOption("item_sold_cobalt");
        public static StatisticOption ItemSoldScrolls => new StatisticOption("item_sold_scrolls");
        public static StatisticOption PrepaidCardRedeemedCobalt => new StatisticOption("prepaid_card_redeemed_cobalt");
        public static StatisticOption ItemSoldDungeons => new StatisticOption("item_sold_dungeons");

        private StatisticOption(string key)
        {
            this.Key = key;
        }

        public string Key { get; private set; }
    }
}
