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
using System.Xml;

namespace tf2_item_scanner.utils
{
    /**
     * Default utils.
     */
    class Utils
    {
        public virtual string SchemaFilename { get; set; }

        #region APIKEY

        protected string _apiKey;

        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        #endregion

        public virtual string AppId { get; set; }

        public virtual string AppName { get; set; }

        #region URLS

        public virtual string SchemaUrl { get; set; }

        public virtual string BackpackUrl { get; set; }

        public virtual string ValueUrl { get; set; }

        public string OwnedGamesUrl
        {
            get { return "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + _apiKey + "&format=json&SteamID="; }
        }

        public string SummariesUrl
        {
            get { return "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + _apiKey + "&steamids="; }
        }

        public string FriendsUrl
        {
            get
            {
                return "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + _apiKey + "&relationship=friend&steamid=";
            }
        }

        #endregion

        #region MORESTUFF

        public double RefPrice
        {
            get { return 0.35; }
        }

        public string GetJson(string url)
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

        public string GetId(String id)
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

        private string fromVanity(string vanity)
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

        public void SaveScan(string filename, List<TF2Item> items, List<SteamProfile> results, List<string> scanned)
        {
            ScanResults scan = new ScanResults(items, results, scanned);
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, scan);
            stream.Close();
        }

        public void LoadScan(string filename, out List<TF2Item> items, out List<SteamProfile> results, out List<string> scanned)
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

        public List<string> GetUsersFromStatus(string status)
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

        public List<string> GetUsersFromGroup(string groupname)
        {
            List<string> users = new List<string>();

            string apiUrl;
            if (groupname.StartsWith("http"))
            {
                apiUrl = groupname + "/memberslistxml/?xml=1";
            }
            else
            {
                apiUrl = "http://steamcommunity.com/groups/" + groupname + "/memberslistxml/?xml=1";
            }
            string xmlContent;

            try
            {
                xmlContent = GetJson(apiUrl);
            }
            catch (Exception e)
            {
                return users;
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                bool steamid = false;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "steamID64")
                            {
                                steamid = true;
                            };
                            break;
                        case XmlNodeType.Text:
                            if (steamid)
                            {
                                users.Add(reader.Value);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            steamid = false;
                            break;
                    }
                    
                }
            }

            return users;
        }

        #endregion
    }
}
