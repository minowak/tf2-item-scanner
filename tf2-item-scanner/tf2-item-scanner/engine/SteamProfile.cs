using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tf2_item_scanner.engine
{
    class SteamProfile
    {
        private string _name;
        private string _id;
        private double _value;
        private bool _f2p;
        private TF2Item _itemFound;
        private string _state;

        #region PROPERTIES

        public string State
        {
            get { return _state; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Id
        {
            get { return _id; }
        }

        public TF2Item ItemFound
        {
            get { return _itemFound; }
        }

        public double Value
        {
            get { return _value; }
        }

        #endregion

        #region CONSTRUCTORS

        public SteamProfile(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public SteamProfile(string name, string id, bool f2p, double value, TF2Item itemFound, string state)
        {
            _name = name;
            _id = id;
            _f2p = f2p;
            _value = value;
            _itemFound = itemFound;
            _state = state;
        }

        #endregion

        #region OVERRIDES

        public override string ToString()
        {
            return _name + "(" + _id + ")" + String.Format("%.2f", _value) + "$";
        }

        #endregion

        public string Serialize()
        {
            return _name + ";" + _id + ";" + _state;
        }

        public static SteamProfile Deserialize(string serialized)
        {
            string[] ss = serialized.Split(';');
            return new SteamProfile(ss[0], ss[1], Boolean.Parse(ss[2]), 0.0, null, ss[2]);
        }

        public bool IsF2P()
        {
            return _f2p;
        }
    }
}
