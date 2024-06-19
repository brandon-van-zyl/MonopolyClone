using MonopolyClone;

var players = new List<BankInput>()
{
    new(100, "Brandon", '1', 5),
    new(100, "Celeste", '2',11)

};
var bank = new Bank(players);
var board = new Board();

GameFunctions.Main(bank, board);
