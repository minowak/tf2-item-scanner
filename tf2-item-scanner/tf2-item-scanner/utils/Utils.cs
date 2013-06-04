using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using tf2_item_scanner.engine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

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

        #region SAVING

        public static void SaveScan(string filename, List<TF2Item> items, List<SteamProfile> results, List<string> scanned)
        {
            ScanResults scan = new ScanResults(items, results, scanned);
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, scan);
            stream.Close();
        }

        public static void LoadScan(string filename, out List<TF2Item> items, out List<SteamProfile> results, out List<string> scanned)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            ScanResults sr = (ScanResults)bf.Deserialize(stream);
            stream.Close();

            items = sr.SelectedItems;
            results = sr.ProfilesFound;
            scanned = sr.Scanned;
        }

        #endregion

        #region STATUS

        public static List<string> GetUsersFromStatus(string status)
        {
            List<string> users = new List<string>();

            string[] lines = status.Split('\n');
            foreach (string line in lines)
            {
                int index = line.IndexOf("STEAM_0:");
                if (index != -1)
                {
                    string user = line.Substring(index, 18);
                    users.Add(user);
                }
            }

            return users;
        }

        #endregion
    }
}
