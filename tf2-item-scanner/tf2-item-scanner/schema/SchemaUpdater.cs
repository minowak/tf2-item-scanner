using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using tf2_item_scanner.utils;

namespace tf2_item_scanner.schema
{
    class SchemaUpdater
    {
        public static void UpdateSchema()
        {
            WebRequest request = WebRequest.Create(Utils.SchemaUrl);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string schema = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                schema = sr.ReadToEnd();
            }

            if (schema != String.Empty)
            {
                File.WriteAllText(Utils.SchemaFilename, schema);
            }
        }
    }
}
