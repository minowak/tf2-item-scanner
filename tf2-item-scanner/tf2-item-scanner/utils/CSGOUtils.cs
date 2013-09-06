using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tf2_item_scanner.utils
{
    class CSGOUtils : Utils
    {
        public override string SchemaFilename
        {
            get { return "csgo_item_schema.txt"; }
        }

        public override string AppId
        {
            get
            {
                return "730";
            }
        }

        public override string AppName
        {
            get
            {
                return "CS : GO";
            }
        }

        #region URLS

        public override string SchemaUrl
        {
            get { return "https://raw.github.com/SteamDatabase/SteamTracking/master/ItemSchema/CounterStrikeGlobalOffensive.json"; }
        }

        public override string BackpackUrl
        {
            get { return "http://api.steampowered.com/IEconItems_730/GetPlayerItems/v0001/?key=" + ApiKey + "&format=json&SteamID="; }
        }

        public override string ValueUrl
        {
            get { return "http://backpack.tf/api/IGetUsers/v2/?format=json&steamids="; }
        }

        #endregion
    }
}
