using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tf2_item_scanner.utils;
using Newtonsoft.Json.Linq;

namespace tf2_item_scanner.engine
{
    class SteamUser
    {
        private string _apiUrl;
        private string _apiUrl2;
        private string _apiUrl3;
        private string _id;
        private Backpack _backpack;
        private long _timePlayed;
        private long _lastOnline;
        private List<string> _friends = new List<string>();
        private string _name;
        private string _state;

        public SteamUser(string id)
        {
            _id = id;
            _apiUrl = Utils.OwnedGamesUrl + id;
            _apiUrl2 = Utils.SummariesUrl + id;
            _apiUrl3 = Utils.FriendsUrl + id;
        }

        #region PROPERTIES

        public string Id
        {
            get { return _id; }
        }

        public long PlayTime
        {
            get { return _timePlayed; }
        }

        public long LastOnline
        {
            get { return _lastOnline; }
        }

        public string Name
        {
            get { return _name; }
        }

        #endregion

        public bool IsPremium()
        {
            return _backpack.Size >= 300;
        }

        public bool Init()
        {
            try
            {
                string json = Utils.GetJson(_apiUrl);
                JObject o = JObject.Parse(json);

                JObject response = (JObject)o["response"];

                JArray games = (JArray)response["games"];

                if (games != null)
                {
                    for (int i = 0; i < games.Count; i++)
                    {
                        JObject game = (JObject)games[i];
                        if ((string)game["appid"] == "440")
                        {
                            _timePlayed = (long)game["playtime_forever"];
                            break;
                        }
                    }
                }

                json = Utils.GetJson(_apiUrl2);
                o = JObject.Parse(json);

                response = (JObject)o["response"];
                JObject player = (JObject)((JArray)response["players"])[0];
                _lastOnline = (long)player["lastlogoff"];
                _name = (string)player["personaname"];
                switch ((int)player["personastate"])
                {
                    case 0: _state = "Offline"; break;
                    case 5:
                    case 6:
                    case 1: _state = "Online"; break;
                    case 2: _state = "Busy"; break;
                    case 3: _state = "Away"; break;
                    case 4: _state = "Snooze"; break;
                }

                json = Utils.GetJson(_apiUrl3);

                o = JObject.Parse(json);
                response = (JObject)o["friendslist"];
                JArray friendsArray = (JArray)response["friends"];

                for (int i = 0; i < friendsArray.Count; i++)
                {
                    JObject friend = (JObject)friendsArray[i];
                    _friends.Add((string)friend["steamid"]);
                }

                _backpack = new Backpack(_id);

                return _backpack.Init();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region PROPERTIES

        public List<string> FriendsIds
        {
            get { return _friends; }
        }

        public string State
        {
            get { return _state; }
        }

        public double Value
        {
            get { return _backpack.Value; }
        }

        public long ItemsCount
        {
            get { return _backpack.ItemsCount; }
        }

        #endregion

        public bool HasItem(TF2Item item)
        {
            return _backpack.HasItem(item);
        }

        public TF2Item HasUnusual()
        {
            return _backpack.HasUnusual();
        }
    }
}
