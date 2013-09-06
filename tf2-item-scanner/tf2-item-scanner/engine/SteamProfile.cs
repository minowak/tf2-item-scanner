using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using tf2_item_scanner.utils;

namespace tf2_item_scanner.engine
{
    [Serializable()]
    class SteamProfile : ISerializable
    {
        private string _name;
        private string _id;
        private double _value;
        private bool _f2p;
        private TF2Item _itemFound;
        private string _state;
        private long _played;
        private Utils utils;

        #region PROPERTIES

        public long Played
        {
            get { return _played; }
        }

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

        public SteamProfile(string name, string id, Utils utils)
        {
            _name = name;
            _id = id;
            this.utils = utils;
        }

        public SteamProfile(SerializationInfo info, StreamingContext ctx, Utils utils)
        {
            _name = (string)info.GetValue("Name", typeof(string));
            _id = (string)info.GetValue("Id", typeof(string));
            _value = (double)info.GetValue("Value", typeof(double));
            _f2p = (bool)info.GetValue("F2P", typeof(bool));
            _itemFound = (TF2Item)info.GetValue("ItemFound", typeof(TF2Item));

            _state = new SteamUser(_id, utils).State;

            this.utils = utils;
        }

        public SteamProfile(string name, string id, bool f2p, double value, TF2Item itemFound, string state, long played, Utils utils)
        {
            _name = name;
            _id = id;
            _f2p = f2p;
            _value = value;
            _itemFound = itemFound;
            _state = state;
            _played = played;
            this.utils = utils;
        }

        #endregion

        #region OVERRIDES

        public override string ToString()
        {
            return _name + "(" + _id + ")" + String.Format("%.2f", _value) + "$";
        }

        #endregion

        #region METHODS

        public bool IsF2P()
        {
            return utils.AppId == "440" ? _f2p : false;
        }

        #endregion

        #region SERIALIZATION

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("Name", _name);
            info.AddValue("Id", _id);
            info.AddValue("Value", _value);
            info.AddValue("F2P", _f2p);
            info.AddValue("ItemFound", _itemFound);
        }

        #endregion
    }
}
