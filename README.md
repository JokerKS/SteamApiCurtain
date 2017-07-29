# SteamApiCurtain
A library that makes it easy and convenient to take data through the Steam API.

## Using:
1. For most operations, you need to get ApiKey at the url: http://steamcommunity.com/dev/apikey
2. You need to connect the library itself(SteamApiCurtain.dll)
3. All functions are called static, so you do not need to create any objects

## Operations:

1. ```List<App> GetAllApp()```

Get all Steam applications currently registered without the use of the library Newtonsoft.Json
diff
+ It is recommended to use this function because it works faster with a large amount of data, and there are plenty of applications in Steam green

*Returns a list of objects with application identifiers and its name*

2. ```List<App> GetAllApp_v2()```

Get all Steam applications currently registered using the library Newtonsoft.Json

This function is performed for approximately half a second longer than the previous one

*Returns a list of objects with application identifiers and its name*

3. ```List<OwnedGame> GetOwnedGames(string key, string steamid)```

Get the Steam user application without using the library Newtonsoft.Json

*Returns a list of objects with game identifiers, the amount of time played in 2 weeks, and the amount of time played in all the time*

4. ```List<OwnedGame> GetOwnedGames_v2(string key, string steamid)```

Get the Steam user application using the library Newtonsoft.Json

It usually performed faster than the function above

*Returns a list of objects with game identifiers, the amount of time played in 2 weeks, and the amount of time played in all the time*

5. ```List<Player> GetPlayerSummaries(string key, string[] steamid)```

Obtaining basic user/users information, namely Steam ID, name and link to avatar

6. ```List<App> GetRecentlyPlayedGames(string key, string steamid, int? count = null)```

Getting the latest played games, namely its ID and title

7. ```List<Player> GetFriendList(string key, string steamid)```

Get a list of friends with a public profile

## Example of using:
```
string key = "0C947898C7FF2FB1F3D9CA63310460C1";
string steamId = "76561198117795575"

List<App> apps = Operation.GetAllApp();
List<OwnedGame> ownedgames = Operation.GetOwnedGames_v2(key, steamId);
List<Player> friends = Operation.GetFriendList(key, steamId);
```
