using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tf2_item_scanner.utils;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace tf2_item_scanner.engine
{
    class Backpack
    {
        private string _id;
        private long _size;
        private string _apiUrl;
        private Utils utils;

        private List<TF2Item> _items;

        public Backpack(string id, Utils utils)
        {
            _apiUrl = utils.BackpackUrl + id;
            _items = new List<TF2Item>();
            _id = id;
            this.utils = utils;
        }

        public bool Init()
        {
            try
            {
                string json = utils.GetJson(_apiUrl);

                JObject o = JObject.Parse(json);
                JObject result = (JObject)o["result"];

                _size = (long)result["num_backpack_slots"];

                JArray itemsArray = (JArray)result["items"];

                for (int i = 0; i < itemsArray.Count; i++)
                {
                    JObject it = (JObject)itemsArray[i];
                    ItemQuality quality = ItemQuality.Normal;

                    switch ((int)it["quality"])
                    {
                        case 0: quality = ItemQuality.Normal; break;
                        case 1: quality = ItemQuality.Genuine; break;
                        case 3: quality = ItemQuality.Vintage; break;
                        case 5: quality = ItemQuality.Unusual; break;
                        case 6: quality = ItemQuality.Unique; break;
                        case 8: quality = ItemQuality.Valve; break;
                        case 11: quality = ItemQuality.Strange; break;
                        case 13: quality = ItemQuality.Haunted; break;
                    }
                    _items.Add(new TF2Item((string)it["name"], (long)it["defindex"], quality));
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool HasItem(TF2Item item)
        {
            return _items.Where(x => x.DefinitionIndex == item.DefinitionIndex &&
                x.Quality == item.Quality).Count() > 0;
        }

        public TF2Item HasUnusual()
        {
            IEnumerable<TF2Item> found =  _items.Where(x => x.Quality == ItemQuality.Unusual 
                && x.DefinitionIndex != 267 && x.DefinitionIndex != 266);
            return found.Count() == 0 ? null : found.First();
        }

        #region PROPERTIES

        public long Size
        {
            get { return _size; }
        }

        public long ItemsCount
        {
            get { return _items.Count; }
        }

        public double Value
        {
            get
            {
                string api = utils.ValueUrl + _id;
                string json = utils.GetJson(api);

                try
                {
                    JObject o = JObject.Parse(json);
                    JObject result = (JObject)o["response"];
                    JObject players = (JObject)result["players"];
                    JObject player = (JObject)players["0"];

                    return ((double)player["backpack_value"]) * utils.RefPrice;
                }
                catch (Exception e)
                {
                    return 0.0;
                }
            }
        }

        #endregion
    }
}
