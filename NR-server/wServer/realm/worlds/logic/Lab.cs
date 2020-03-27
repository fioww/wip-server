using common.resources;
using wServer.networking;

namespace wServer.realm.worlds.logic
{
    public class Lab : World
    {
        public Lab(ProtoWorld proto, Client client = null) : base(proto)
        {
        }

        protected override void Init()
        {
            var template = DungeonTemplates.GetTemplate(Name);

            FromDungeonGen(Rand.Next(), template);
        }
    }
}
