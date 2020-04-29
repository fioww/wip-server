using System;
using System.Collections.Generic;
using System.Linq;
using common.resources;

namespace common
{
    public class FameStats
    {
        public int CalculateTotal(
            XmlData data, DbChar character)
        {
            int f = 0;

            //Well Equiped
            var bonus = character.Items.Take(4).Where(x => x != 0xffff).Sum(x => data.Items[x].FameBonus) / 100.0;
            f += (int) ((character.Fame + f) * bonus);

            return character.Fame + f;
        }

        public IEnumerable<Tuple<string, string, int>> GetBonuses(
            XmlData data, DbChar character)
        {
            int f = 0;
            //Well Equiped
            var bonus = character.Items.Take(4).Where(x => x != 0xffff).Sum(x => data.Items[x].FameBonus) / 100.0;
            if (bonus > 0)
            {
                var val = (int)((character.Fame + f) * bonus);
                f += val;
                yield return Tuple.Create("Well Equipped", "Bonus for equipment", val);
            }
        }
    }
}