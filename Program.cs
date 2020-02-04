using System;
using System.Threading.Tasks;
using DSharpPlus;
using System.IO;

namespace NullBot
{
    class Program
    {

        static DiscordClient bot;
        public static string path = "token.txt";
        public static string executor = "/null ";
        public static string msg = "";

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            bot = new DiscordClient(new DiscordConfiguration
            {
                Token = File.ReadAllText(path),
                TokenType = TokenType.Bot
            });

            bot.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith(executor + "ping"))
                    await e.Message.RespondAsync("pong!");

                if (e.Message.Content.ToLower().StartsWith(executor + "rps"))
                    await e.Message.RespondAsync("Choose your weapon!");
            };

            bot.MessageDeleted += async e =>
            {
                msg += e.Message.Author.Username + e.Message.Content + "\n";
                File.AppendAllText("auditDEL.txt", msg);
            };

            await bot.ConnectAsync();
            await Task.Delay(-1);
        }

    }
}
