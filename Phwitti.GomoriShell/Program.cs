using Newtonsoft.Json;
using Phwitti.Gomori;
using Phwitti.GomoriBot;
using Phwitti.GomoriShell;
using System;
using System.Linq;
using System.Text;

public class Program
{
    static void Main(string[] args)
    {
        GomoriBotBase bot = new GomoriBotGreedySimple();
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            string? line = Console.ReadLine();

            if (string.IsNullOrEmpty(line))
                break;

            var request = JsonConvert.DeserializeObject<JsonObjectRequest>(line);
            var bEndGame = false;

            switch (request.Type)
            {
                case JsonObjectRequestType.StartGame:
                    bot.Initialize(request.Color.ToDeckColor());
                    Console.WriteLine(JsonConvert.SerializeObject(JsonObjectResponse.Okay));
                    break;
                case JsonObjectRequestType.PlayFirstTurn:
                case JsonObjectRequestType.PlayTurn:
                    request.ToBoardAndHand(out Board board, out Hand hand, out int iColumnOffset, out int iRowOffset);

                    Console.WriteLine(request.Type == JsonObjectRequestType.PlayFirstTurn
                        ? JsonConvert.SerializeObject(
                            bot.GetActionForBoardAndHand(board, hand)?.ToShellAction(iColumnOffset, iRowOffset).ToFirstTurnResponse())
                        : JsonConvert.SerializeObject(
                            bot.EnumerateActionsForBoardAndHand(board, hand).Select(x => x.ToShellAction(iColumnOffset, iRowOffset)).ToArray()));
                    break;
                case JsonObjectRequestType.Bye:
                default:
                    bEndGame = true;
                    break;
            }

            if (bEndGame)
                break;
        }
    }
}
