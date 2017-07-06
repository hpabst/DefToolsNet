using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefToolsNet.Models;

namespace DefToolsNet.Models
{
    public class WowPlayer
    {
        [Key, Column(Order = 0)]
        public string Name { get; set; }
        [Key, Column(Order = 1)]
        public string Realm { get; set; }
        public WowClass PlayerClass { get; set; }

        private static string _defaultName = "UNKNOWN_PLAYER_NAME";
        private static string _defaultRealm = "UNKNOWN_REALM";
        private static WowClass _defaultClass = WowClass.Unknown;
        private static WowPlayer _defaultPlayer;

        public WowPlayer(string name, string realm, WowClass playerClass)
        {
            this.Name = name;
            this.Realm = realm;
            this.PlayerClass = playerClass;
        }

        public bool Matches(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            WowPlayer i = obj as WowPlayer;
            if ((object)i == null)
            {
                return false;
            }

            return String.Compare(this.Name.ToLower(), i.Name.ToLower()) == 0 &&
                    String.Compare(this.Realm.ToLower(), i.Realm.ToLower()) == 0 &&
                    this.PlayerClass == i.PlayerClass;
        }

        public static WowPlayer GetDefaultPlayer()
        {
            if(WowPlayer._defaultPlayer == null)
            {
                WowPlayer._defaultPlayer = new WowPlayer(_defaultName, _defaultRealm, _defaultClass);
            }

            return WowPlayer._defaultPlayer;
        }
    }
}
