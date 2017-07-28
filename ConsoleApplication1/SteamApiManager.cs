using SteamApiCurtain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTesting
{
    class SteamApiManager
    {
        static Stopwatch stopWatch = new Stopwatch();
        public void GetAllApp()
        {
            Output.ShowInfo("Getting all the Steam applications(without Newtonsoft.Json)...");

            stopWatch.Restart();
            List<App> apps = Operation.GetAllApp();
            stopWatch.Stop();

            string appsListFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\AppList.txt");

            if (apps != null)
            {
                using (StreamWriter sw = new StreamWriter(appsListFilePath))
                {
                    foreach (var app in apps)
                        sw.WriteLine($"{app.AppId} ... {app.Name}");
                }
            }
            Output.ShowResult("Applications have been got and saved to a file \'AppList.txt\'!");
            GetElapsedTime();
            Console.WriteLine($"Last App is \'{apps.Last().Name}\' with id={apps.Last().AppId}\n");
        }

        public void GetAllApp_v2()
        {
            Output.ShowInfo("Getting all the Steam applications(using Newtonsoft.Json)...");

            stopWatch.Restart();
            List<App> apps = Operation.GetAllApp_v2();
            stopWatch.Stop();

            string appsListFilePath_v2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\AppList_v2.txt");

            if (apps != null)
            {
                using (StreamWriter sw = new StreamWriter(appsListFilePath_v2))
                {
                    foreach (var app in apps)
                        sw.WriteLine($"{app.AppId} ... {app.Name}");
                }
            }
            Output.ShowResult("Applications have been got and saved to a file \'AppList_v2.txt\'!");
            GetElapsedTime();
            Console.WriteLine($"Last App is \'{apps.Last().Name}\' with id={apps.Last().AppId}\n");
        }

        public void GetOwnedGames(string key, string steamId)
        {
            Output.ShowInfo("Getting list of games currently owned by the user(without Newtonsoft.Json)...");

            stopWatch.Restart();
            List<OwnedGame> ownedgames = Operation.GetOwnedGames(key, steamId);
            stopWatch.Stop();

            string ownedGamesListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\OwnedGamesList.txt");
            if (ownedgames != null)
            {
                using (StreamWriter sw = new StreamWriter(ownedGamesListPath))
                {
                    foreach (var game in ownedgames)
                        sw.WriteLine($"{game.GameId}\t{game.Playtime2Weeks}\t{game.PlaytimeForever}");
                }
            }
            GetElapsedTime();
            Output.ShowResult("Owned games have been got and saved to a file \'OwnedGamesList.txt\'!\n");
        }

        public void GetOwnedGames_v2(string key, string steamId)
        {
            Output.ShowInfo("Getting list of games currently owned by the user(using Newtonsoft.Json)...");

            stopWatch.Restart();
            List<OwnedGame> ownedgames = Operation.GetOwnedGames_v2(key, steamId);
            stopWatch.Stop();

            string ownedGamesListPath_v2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\OwnedGamesList_v2.txt");
            if (ownedgames != null)
            {
                using (StreamWriter sw = new StreamWriter(ownedGamesListPath_v2))
                {
                    foreach (var game in ownedgames)
                        sw.WriteLine($"{game.GameId}\t{game.Playtime2Weeks}\t{game.PlaytimeForever}");
                }
            }
            GetElapsedTime();
            Output.ShowResult("Owned games have been got and saved to a file \'OwnedGamesList_v2.txt\'!\n");
        }

        public void GetPlayerSummaries(string key, string[] steamIds)
        {
            try
            {
                Output.ShowInfo("Getting player summaries...");

                stopWatch.Restart();
                List<Player> players = Operation.GetPlayerSummaries(key, steamIds);
                stopWatch.Stop();

                if (players != null)
                {
                    for (int i = 0; i < players.Count; i++)
                        Console.WriteLine($"Player {i+1} --> SteamId: {players[i].SteamID}, Name: {players[i].Name}");
                }

                GetElapsedTime();
            }
            catch (ArgumentNullException e)
            {
                Output.ShowError(e.Message);
            }
            catch (Exception e)
            {
                Output.ShowError(e.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        public void GetRecentlyPlayedGames(string key, string steamId)
        {
            try
            {
                Output.ShowInfo("Getting recently played games...");

                stopWatch.Restart();
                List<App> recently_app = Operation.GetRecentlyPlayedGames(key, steamId);
                stopWatch.Stop();

                if (recently_app != null)
                {
                    foreach (var item in recently_app)
                        Console.WriteLine($"AppId: {item.AppId} \tName: {item.Name}");
                }
                GetElapsedTime();
            }
            catch (ArgumentNullException e)
            {
                Output.ShowError(e.Message);
            }
            catch (Exception e)
            {
                Output.ShowError(e.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        public void GetFriendList(string key, string steamId)
        {
            try
            {
                Output.ShowInfo("Getting Steam friends list...");

                stopWatch.Restart();
                List<Player> friends = Operation.GetFriendList(key, steamId);
                stopWatch.Stop();

                GetElapsedTime();

                if (friends != null)
                {
                    for (int i = 0; i < friends.Count; i++)
                        Console.WriteLine($"\tFriend {i + 1} --> {friends[i].SteamID}");
                }
            }
            catch (ArgumentNullException e)
            {
                Output.ShowError(e.Message);
            }
            catch (Exception e)
            {
                Output.ShowError(e.Message);
            }
        }

        private static void GetElapsedTime()
        {
            TimeSpan ts = stopWatch.Elapsed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Time: {ts.Seconds:00}:{ts.Milliseconds / 10:00}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
