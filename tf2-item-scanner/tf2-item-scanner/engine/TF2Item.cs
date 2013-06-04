using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tf2_item_scanner.utils;
using System.Runtime.Serialization;

namespace tf2_item_scanner.engine
{
    [Serializable()]
    class TF2Item : ISerializable
    {
        private string _name;
        private long _defIndex;
        private ItemQuality _quality = ItemQuality.Normal;
        private string _imgUrl;

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Image
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }

        public long DefinitionIndex
        {
            get { return _defIndex; }
            set { _defIndex = value; }
        }

        public ItemQuality Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public TF2Item(SerializationInfo info, StreamingContext ctx)
        {
            _name = (string)info.GetValue("Name", typeof(string));
            _defIndex = (long)info.GetValue("DefinitionIndex", typeof(long));
            _quality = (ItemQuality)info.GetValue("Quality", typeof(ItemQuality));
            _imgUrl = (string)info.GetValue("ImgUrl", typeof(string));
        }

        public TF2Item(string name, long defIndex, ItemQuality quality)
        {
            _name = name;
            _defIndex = defIndex;
            _quality = quality;
        }

        public TF2Item(TF2Item copy)
        {
            _name = copy.Name;
            _defIndex = copy.DefinitionIndex;
            _quality = copy.Quality;
            _imgUrl = copy.Image;
        }

        #endregion

        #region OVERRIDES

        public override string ToString()
        {
            return _name;
        }

       /* public static bool operator ==(TF2Item a, TF2Item b)
        {
            if (a == null || b == null)
                return false;
            return a.DefinitionIndex == b.DefinitionIndex && a.Quality == b.Quality;
        }

        public static bool operator !=(TF2Item a, TF2Item b)
        {
            if (a == null || b == null)
                return true;
            return a.DefinitionIndex != b.DefinitionIndex || a.Quality != b.Quality;
        }*/

        #endregion

        #region METHODS

        public string Serialize()
        {
            return Name + ";" + Quality + ";" + DefinitionIndex + ";" + Image;
        }

        public static TF2Item Deserialize(string serialized)
        {
            if (serialized.Length == 0)
            {
                return null;
            }
            string[] attrs = serialized.Split(';');
            TF2Item item = new TF2Item(attrs[0], long.Parse(attrs[2]), (ItemQuality)Enum.Parse(ItemQuality.Normal.GetType(), attrs[1]));
            item.Image = attrs[3];

            return item;
        }

        #endregion

        #region SERIALIZATION

        public void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("Name", _name);
            info.AddValue("DefinitionIndex", _defIndex);
            info.AddValue("Quality", _quality);
            info.AddValue("ImgUrl", _imgUrl);
        }

        #endregion
    }
}
