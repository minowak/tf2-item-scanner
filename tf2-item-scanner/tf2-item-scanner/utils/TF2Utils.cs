using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tf2_item_scanner.utils
{
    class TF2Utils : Utils
    {
        public override string SchemaFilename
        {
            get { return "tf2_item_schema.txt"; }
        }

        public override string AppId
        {
            get
            {
                return "440";
            }
        }

        public override string AppName
        {
            get
            {
                return "Team Fortress 2";
            }
        }

        #region URLS

        public override string SchemaUrl
        {
            get { return "http://git.optf2.com/schema-tracking/plain/Team%20Fortress%202%20Schema?h=teamfortress2"; }
        }

        public override string BackpackUrl
        {
            get { return "http://api.steampowered.com/IEconItems_440/GetPlayerItems/v0001/?key=" + ApiKey + "&format=json&SteamID="; }
        }

        public override string ValueUrl
        {
            get { return "http://backpack.tf/api/IGetUsers/v2/?format=json&steamids="; }
        } 

        #endregion
    }
}
