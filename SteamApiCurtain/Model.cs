using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SteamApiCurtain
{
    [DataContract]
    internal class Applist
    {
        ///JSON
        ///"applist": {
		/// "apps": [
		///	    {
		///		    "appid": 5,
		///		    "name": "Dedicated Server"
        ///
        ///     },
		///	    {
		///		    "appid": 7,
		///		    "name": "Steam Client"
		///	    },
        ///     ...
        /// ]
        ///}

        [DataMember]
        public Apps applist { get; set; }
    }
    [DataContract]
    internal class Apps
    {
        [DataMember]
        public List<App> apps { get; set; }
    }

    [Serializable]
    [DataContract]
    public class App
    {
        ///JSON
        ///{
	    ///		"appid": 7,
	    ///		"name": "Steam Client"
		///	}
        [DataMember(Name = "appid")]
        public int AppId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
    

    [DataContract]
    internal class OwnedGamesResponse
    {
        [DataMember]
        public OwnedGames response { get; set; }
    }

    [DataContract]
    internal class OwnedGames
    {
        [DataMember]
        public List<OwnedGame> games { get; set; }
    }

    [DataContract]
    public class OwnedGame
    {
        ///JSON
        ///{
		///		"appid": 72850,
		///		"playtime_2weeks": 283,
		///		"playtime_forever": 11139
		///}


        [DataMember(Name = "appid")]
        public virtual int GameId { get; set; }
        
        [DataMember(Name = "playtime_forever")]
        public virtual int PlaytimeForever { get; set; }
        
        [DataMember(Name = "playtime_2weeks")]
        public virtual int? Playtime2Weeks { get; set; }
    }

    [DataContract]
    public class Player
    {
        [DataMember(Name = "steamid")]
        public string SteamID { get; set; }
        [DataMember(Name = "personaname")]
        public string Name { get; set; }
        [DataMember(Name = "avatarfull")]
        public string AvatarFull { get; set; }
    }
    public class PlayerAllInfo : Player
    {
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public int lastlogoff { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public int avatar { get; set; }
        public int avatarmedium { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
        public string loccountrycode { get; set; }
    }
}
