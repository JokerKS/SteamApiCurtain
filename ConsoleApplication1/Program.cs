using SteamApiCurtain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LibraryTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "0C947898C7FF2FB1F3D9CA63310460C1";
            string testId1 = "76561198117795575", testId2 = "76561198095672553", testId3 = "76561198023414915";

            SteamApiManager manager = new SteamApiManager();

            manager.GetAllApp();
            manager.GetAllApp_v2();

            manager.GetOwnedGames(key, testId3);
            manager.GetOwnedGames_v2(key, testId3);

            manager.GetPlayerSummaries(key, new string[] { testId1, testId2 });

            manager.GetRecentlyPlayedGames(key, testId1);

            manager.GetFriendList(key, testId1);

            Console.ReadKey();
        }
    }
}
