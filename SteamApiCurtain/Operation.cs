using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SteamApiCurtain
{
    public class Operation
    {
        private const string baseUrl = "http://api.steampowered.com/";

        /// <summary>
        /// Function that allows to revise all accessible <b color="green">Apps</b> in <b color="green">Steam</b> 
        /// <i>not using Newtonsoft.Json</i>
        /// </summary>
        /// <returns>Return the list of <b color="green">Apps</b>, what now being in <b color="green">Steam</b></returns>
        public static List<App> GetAllApp()
        {
            try
            {
                string url = $"{baseUrl}ISteamApps/GetAppList/v2";

                DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Applist));
                Applist list = (Applist)data.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(GetJSON(url))));

                return list.applist.apps;
            }
            catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Function that allows to revise all accessible <b color="green">Apps</b> in <b color="green">Steam</b> 
        /// <i>using Newtonsoft.Json</i>
        /// </summary>
        /// <returns>Return the list of <b color="green">Apps</b>, what now being in <b color="green">Steam</b></returns>
        public static List<App> GetAllApp_v2()
        {
            try
            {
                string url = $"{baseUrl}ISteamApps/GetAppList/v2";
                
                JObject jobj = JObject.Parse(GetJSON(url));
                IList<JToken> json_apps = jobj["applist"]["apps"].Children().ToList();

                List<App> apps = new List<App>();
                foreach (JToken app in json_apps)
                    apps.Add(JsonConvert.DeserializeObject<App>(app.ToString()));

                return apps;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the object with the identifiers of all user games and the playtime for 2 weeks and general playtime
        /// </summary>
        /// <param name="key">The key is for the use Steam API</param>
        /// <param name="steamid">User id(x64) in Steam</param>
        /// <returns>Returns a list of games currently owned by the user</returns>
        public static List<OwnedGame> GetOwnedGames(string key, string steamid)
        {
            try
            {
                string url = $"{baseUrl}IPlayerService/GetOwnedGames/v1/?key={key}&steamid={steamid}";

                DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(OwnedGamesResponse));
                OwnedGamesResponse list = (OwnedGamesResponse)data.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(GetJSON(url))));

                return list.response.games;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the object with the identifiers of all user games and the playtime for 2 weeks and general playtime
        /// </summary>
        /// <param name="key">The key is for the use Steam API</param>
        /// <param name="steamid">User id(x64) in Steam</param>
        /// <returns>Returns a list of games currently owned by the user</returns>
        public static List<OwnedGame> GetOwnedGames_v2(string key, string steamid)
        {
            try
            {
                string url = $"{baseUrl}IPlayerService/GetOwnedGames/v1/?key={key}&steamid={steamid}";

                JObject jobj = JObject.Parse(GetJSON(url));
                IList<JToken> json_games = jobj["response"]["games"].Children().ToList();

                List<OwnedGame> apps = new List<OwnedGame>();
                foreach (JToken app in json_games)
                {
                    apps.Add(JsonConvert.DeserializeObject<OwnedGame>(app.ToString()));
                }
                return apps;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Getting the basic information of a Steam user, namely <b color="green">id, name, avatar</b>
        /// </summary>
        /// <param name="key">The key is for the use Steam API</param>
        /// <param name="steamid">User/users id(x64) in Steam</param>
        /// <returns>Returns user information</returns>
        public static List<Player> GetPlayerSummaries(string key, string[] steamid)
        {
            try
            {
                if (steamid != null && steamid.Length > 0)
                {
                    byte player_id = 0;
                    string steamids = steamid[player_id++];
                    while (player_id < steamid.Length)
                        steamids += "," + steamid[player_id++];

                    string url = $"{baseUrl}ISteamUser/GetPlayerSummaries/v0002/?key={key}&steamids={steamids}";
                    
                    JObject jobj = JObject.Parse(GetJSON(url));
                    IList<JToken> json_players = jobj["response"]["players"].Children().ToList();

                    List<Player> players = new List<Player>();
                    foreach (JToken player in json_players)
                        players.Add(JsonConvert.DeserializeObject<Player>(player.ToString()));

                    return players;
                }
                else throw new ArgumentNullException(nameof(steamid), "Argument cannot be null or empty");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Last played Steam games
        /// </summary>
        /// <param name="key">The key is for the use Steam API</param>
        /// <param name="steamid">User id(x64) in Steam</param>
        /// <param name="count">Number of games to show<br/>If count = null, the default amount of games was displaying
        /// </param>
        /// <returns>Returns the list of the last played games</returns>
        public static List<App> GetRecentlyPlayedGames(string key, string steamid, int? count = null)
        {
            try
            {
                if (steamid != null)
                {
                    string url = $"{baseUrl}IPlayerService/GetRecentlyPlayedGames/v1/?key={key}&steamid={steamid}";
                    if (count != null) url += $"&count={count}";

                    JObject jobj = JObject.Parse(GetJSON(url));
                    IList<JToken> json_apps = jobj["response"]["games"].Children().ToList();

                    List<App> apps = new List<App>();
                    foreach (JToken app in json_apps)
                        apps.Add(JsonConvert.DeserializeObject<App>(app.ToString()));

                    return apps;
                }
                else throw new ArgumentNullException(nameof(steamid), "Argument cannot be null or empty");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Steam Friends List, which has a public profile
        /// </summary>
        /// <param name="key">The key is for the use Steam API</param>
        /// <param name="steamid">User id(x64) in Steam</param>
        /// <returns>Returns a list of friends but fills in <b>only SteamID</b>It should be borne in mind!</returns>
        public static List<Player> GetFriendList(string key, string steamid)
        {
            try
            {
                if (steamid != null)
                {
                    string url = $"{baseUrl}ISteamUser/GetFriendList/v1/?key={key}&steamid={steamid}";

                    JObject jobj = JObject.Parse(GetJSON(url));
                    IList<JToken> json_friends = jobj["friendslist"]["friends"].Children().ToList();

                    List<Player> friends = new List<Player>();
                    foreach (JToken friend in json_friends)
                    {
                        friends.Add(JsonConvert.DeserializeObject<Player>(friend.ToString()));
                    }
                    return friends;
                }
                else throw new ArgumentNullException(nameof(steamid), "Argument cannot be null");
            }
            catch (Exception)
            {
                throw;
            }
        }



        //helpers
        private static string GetJSON(string url)
        {
            string json = null;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                        json = sr.ReadToEnd();
                    }
                }
            }
            return json;
        }
    }
}
