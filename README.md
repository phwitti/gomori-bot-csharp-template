# gomori-bot-csharp-template
A template for writing gomori bots in C#, that work with @nnmm's arbiter. See https://github.com/phwitti/gomori-rules for the ruleset.

## How to use
 - In Phwitti.GomoriBot derive from the GomoriBotBase and at least override `GetActionForBoardAndHand(Board _board, Hand _hand)`
 - Then, use an object of that class in the Main-Method of the Program class in Phwitti.GomoriShell
 - See GomoriBotRandom for a simple random-bot implementation

## License
 - MIT
