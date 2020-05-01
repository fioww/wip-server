using System;
using System.Collections.Generic;
using System.Linq;
using common;
using common.resources;
using wServer.networking.packets;
using wServer.networking.packets.incoming;
using wServer.networking.packets.incoming.market;
using wServer.networking.packets.outgoing;
using wServer.networking.packets.outgoing.market;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.networking.handlers.market
{
    class MarketAddHandler : PacketHandlerBase<MarketAdd>
    {
        public override PacketId ID => PacketId.MARKET_ADD;

        protected override void HandlePacket(Client client, MarketAdd packet)
        {
            client.Manager.Logic.AddPendingAction(t => 
            {
                var player = client.Player;
                int[] bannedRanks = { 50, 60, 70, 80, 90 }; // Admins can sell, but please, be careful.
                if (player == null || IsTest(client) || (player.Rank.Equals(bannedRanks) && player.Admin.Equals(true)))
                {
                    return;
                }

                if (packet.Hours < 120 || packet.Hours > 120) /* Client only has the options 3, 6, 12 and 24 hours, though if someone wanted they could change that */
                {
                    client.SendPacket(new MarketAddResult
                    {
                        Code = MarketAddResult.INVALID_UPTIME,
                        Description = "Invalid uptime. Only available uptime is 120 hours (5 days.)"
                    });
                    return;
                }

                if (packet.Price <= 0) /* Client has this check, but check it incase it was modified */
                {
                    client.SendPacket(new MarketAddResult
                    {
                        Code = MarketAddResult.INVALID_PRICE,
                        Description = "You cannot sell items for 0 or less."
                    });
                    return;
                }

                if (!Enum.IsDefined(typeof(CurrencyType), packet.Currency) || packet.Currency == (int)CurrencyType.GuildFame) /* Make sure its a valid currency and its NOT GuildFame */
                {
                    client.SendPacket(new MarketAddResult
                    {
                        Code = MarketAddResult.INVALID_CURRENCY,
                        Description = "Invalid currency."
                    });
                    return;
                }

                for (var i = 0; i < packet.Slots.Length; i++)
                {
                    byte slotId = packet.Slots[i];

                    if (player.Inventory[slotId] == null) /* Make sure they are selling valid items */
                    {
                        client.SendPacket(new MarketAddResult
                        {
                            Code = MarketAddResult.SLOT_IS_NULL,
                            Description = $"The slot {slotId} is empty or invalid."
                        });
                        return;
                    }

                    Item item = player.Inventory[slotId];
                    if (Banned(item)) /* Client has this check, but check it incase it was modified */
                    {
                        client.SendPacket(new MarketAddResult
                        {
                            Code = MarketAddResult.ITEM_IS_SOULBOUND,
                            Description = "You cannot sell soulbound items."
                        });
                        return;
                    }

                    player.Inventory[slotId] = null; /* Set the slot to null */
                    player.Manager.Database.AddMarketData(
                        client.Account, item.ObjectType, player.AccountId, player.Name, packet.Price,
                        DateTime.UtcNow.AddHours(packet.Hours).ToUnixTimestamp(), (CurrencyType)packet.Currency); /* Add it to market */

                    // probably wont need to use, only buy logs
                    //Log.DebugFormat("{0}:{1} added a {2} for {3} on the market", player.Name, player.AccountId, item.ObjectType, packet.Price);
                }

                client.SendPacket(new MarketAddResult
                {
                    Code = -1,
                    Description = $"Successfully added {packet.Slots.Length} items to the market."
                });
            });
        }

        private static bool Banned(Item item) /* What you add here you must add client sided too */
        {
            return item.Soulbound;
        }
    }
}
