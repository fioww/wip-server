using System.IO;
using common.resources;
using terrain;
using wServer.networking;

namespace wServer.realm.worlds.logic
{
    public class Test : World
    {        
		static readonly string[] testMusic = { "Test" };

        private static ProtoWorld _testProto = new ProtoWorld
        {
            name = "Test World",
            sbName = "Test World",
            id = 0,
            setpiece = false,
            showDisplays = false,
            background = 0,
            blocking = 0,
            difficulty = 0,
            isLimbo = false,
            maps = Empty<string>.Array,
            persist = false,
            portals = Empty<int>.Array,
            restrictTp = false,
            wmap = Empty<byte[]>.Array,
            music = testMusic
        };

        public bool JsonLoaded { get; private set; }
		
        public Test() : base(_testProto) { }

        protected override void Init() { }

        public void LoadJson(string json)
        {
            if (!JsonLoaded)
            {
                FromWorldMap(new MemoryStream(Json2Wmap.Convert(Manager.Resources.GameData, json)));
                JsonLoaded = true;
            }

            InitShops();
        }
    }
}