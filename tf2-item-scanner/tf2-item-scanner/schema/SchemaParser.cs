using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using tf2_item_scanner.engine;
using Newtonsoft.Json.Linq;
using tf2_item_scanner.utils;

namespace tf2_item_scanner.schema
{
    class SchemaParser
    {
        private string _fileName;

        public SchemaParser(string fileName)
        {
            _fileName = fileName;
        }

        public List<TF2Item> Parse()
        {
            List<TF2Item> items = new List<TF2Item>();

            string schema = File.ReadAllText(_fileName);

            JObject o = JObject.Parse(schema);
            JObject result = (JObject)o["result"];
            JArray array = (JArray)result["items"];

            for (int i = 0; i < array.Count; i++)
            {
                JObject item = (JObject)array[i];
                long defIndex = (long)item["defindex"];
                string name = (string)item["item_name"];
                string imgUrl = (string)item["image_url"];
                imgUrl.TrimStart(new char[] { '\\' });

                TF2Item it = new TF2Item(name, defIndex, ItemQuality.Normal);
                it.Image = imgUrl;
                items.Add(it);
            }

            return items;
        }
    }
}
