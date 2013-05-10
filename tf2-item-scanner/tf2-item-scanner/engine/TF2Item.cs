using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tf2_item_scanner.utils;

namespace tf2_item_scanner.engine
{
    class TF2Item
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
    }
}
