using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace tf2_item_scanner.utils
{
    static class Utils
    {
        public static string SchemaFilename
        {
            get { return "item_schema.txt"; }
        }
        #region APIKEY

        private static string _apiKey;

        public static string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        #endregion

        #region URLS

        public static string SchemaUrl
        {
            get { return "http://git.optf2.com/schema-tracking/plain/Team%20Fortress%202%20Schema?h=teamfortress2"; }
        }

        public static string BackpackUrl
        {
            get { return "http://api.steampowered.com/IEconItems_440/GetPlayerItems/v0001/?key=" + _apiKey + "&format=json&SteamID="; }
        }

        public static string ValueUrl
        {
            get { return "http://backpack.tf/api/IGetUsers/v2/?format=json&steamids="; }
        }

        public static string OwnedGamesUrl
        {
            get { return "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + _apiKey + "&format=json&SteamID="; }
        }

        public static string SummariesUrl
        {
            get { return "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + _apiKey + "&steamids="; }
        }

        public static string FriendsUrl
        {
            get
            {
                return "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + _apiKey + "&relationship=friend&steamid=";
            }
        }

        #endregion

        #region MORESTUFF

        public static double RefPrice
        {
            get { return 0.35; }
        }

        public static string GetJson(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string json = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                json = sr.ReadToEnd();
            }

            return json;
        }

        #endregion

        #region STEAMIDCONVERT

        public static string GetId(String id)
        {
            if (id.StartsWith("STEAM"))
            {
                String[] tmp = id.Split(':');
                long x = long.Parse(tmp[1]);
                long y = long.Parse(tmp[2]);
                y *= 2;
                y += x;
                long result = 76561197960265728;
                result += y;
                return result.ToString();
            }
            else
            {
                return fromVanity(id);
            }
        }

        private static string fromVanity(string vanity)
        {
            string apiUrl = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=" + _apiKey + "&vanityurl=" + vanity;
            string json = null;
            try
            {
                json = GetJson(apiUrl);
                if (json == null)
                {
                    return vanity;
                }
            }
            catch (Exception e)
            {
                return vanity;
            }
            
            try
            {
                JObject o = JObject.Parse(json);
                JObject result = (JObject)o["response"];
                string getId = (string)result["steamid"];
                if (getId == null || getId.Length == 0)
                {
                    return vanity;
                }
                return getId;
            }
            catch (Exception e)
            {
                return vanity;
            }
        }

        #endregion
    }
}
