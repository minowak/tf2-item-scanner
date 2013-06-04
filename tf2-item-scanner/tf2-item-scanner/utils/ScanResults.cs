using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using tf2_item_scanner.engine;

namespace tf2_item_scanner.utils
{
    [Serializable()]
    class ScanResults : ISerializable
    {
        public List<TF2Item> SelectedItems { get; set; }
        public List<SteamProfile> ProfilesFound { get; set; }
        public List<string> Scanned { get; set; }

        public ScanResults(List<TF2Item> selected, List<SteamProfile> found, List<string> scanned)
        {
            SelectedItems = selected;
            ProfilesFound = found;
            Scanned = scanned;
        }

        public ScanResults(SerializationInfo info, StreamingContext ctx)
        {
            SelectedItems = (List<TF2Item>)info.GetValue("Selected", typeof(List<TF2Item>));
            ProfilesFound = (List<SteamProfile>)info.GetValue("Profiles", typeof(List<SteamProfile>));
            Scanned = (List<string>)info.GetValue("Scanned", typeof(List<string>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("Selected", SelectedItems);
            info.AddValue("Profiles", ProfilesFound);
            info.AddValue("Scanned", Scanned);
        }
    }
}
