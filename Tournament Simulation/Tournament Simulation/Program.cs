//Brasileirão Simulation

using System;
using Teams;

#region Creating teams

Team[] teams = new Team[20];
teams[0] = new Team("AME", 62);
teams[1] = new Team("APR", 72);
teams[2] = new Team("AMG", 73);
teams[3] = new Team("BAH", 56);
teams[4] = new Team("BOT", 70);
teams[5] = new Team("BRG", 65);
teams[6] = new Team("COR", 72);
teams[7] = new Team("CTB", 62);
teams[8] = new Team("CRU", 65);
teams[9] = new Team("CUI", 57);
teams[10] = new Team("FLA",86);
teams[11] = new Team("FLU",76);
teams[12] = new Team("FOR",65);
teams[13] = new Team("GOI",56);
teams[14] = new Team("GRE",65);
teams[15] = new Team("INT",63);
teams[16] = new Team("PAL",84);
teams[17] = new Team("SAN",63);
teams[18] = new Team("SFC",66);
teams[19] = new Team("VAS",60);

#endregion

#region Functions

//Creating Board
Team[] board = new Team[20];
void ResetBoard(Team[] actualBoard)
{
    for(int i = 0; i < actualBoard.Length; i++)
    {
        actualBoard[i].points = 0;
        actualBoard[i].goalsMade = 0;
        actualBoard[i].goalsTaken = 0;
        actualBoard[i].matches = 0;
    }
}
int CalculateGols(Team team)
{
    float strengthMultiplier = (float)team.strength / 100.0f;
    Random RandNumbGen = new Random();
    float golChance = RandNumbGen.Next(0, 100) * strengthMultiplier;

    if (golChance >= 0 && golChance < 30) { return 0; }
    else if(golChance >= 30 && golChance < 55) { return 1; }
    else if (golChance >= 55 && golChance < 75) { return 2; }
    else if (golChance >= 75 && golChance < 90) { return 3; }
    else{ return 4; }

}
void SimulateMatches(Team[] teams, string response)
{
    for(int i = 0; i < teams.Length; i++)
    {
        if (response.Equals("y"))
        {
            Console.WriteLine("\n{0}'s Matches:\n", teams[i].name);
        }
        
        for (int j = 0; j < teams.Length; j++)
        {
            if(i == j) //Garanting that the team will not match up with itself
            { 
                if (j < 19) { j++; }
                else { return; }
            } 

            //Generating the matches
            int team1Goals = CalculateGols(teams[i]);
            int team2Goals = CalculateGols(teams[j]);

            teams[i].goalsMade += team1Goals;
            teams[i].goalsTaken += team2Goals;

            teams[j].goalsMade += team2Goals;
            teams[j].goalsTaken += team1Goals;

            //Points System
            if(team1Goals > team2Goals) { teams[i].points += 3; }
            else if(team1Goals == team2Goals)
            {
                teams[i].points += 1;
                teams[j].points += 1;
            }
            else { teams[j].points += 3;}

            if (response.Equals("y"))
            {
                Console.WriteLine("{0} {1} X {2} {3}\n", teams[i].name, team1Goals, teams[j].name, team2Goals);
            }

        }
    }
}
Team[] SortArray(Team[] array, int leftIndex, int rightIndex)
{
    var i = leftIndex;
    var j = rightIndex;
    var pivot = array[leftIndex].points;
    while (i <= j)
    {
        while (array[i].points < pivot)
        {
            i++;
        }

        while (array[j].points > pivot)
        {
            j--;
        }
        if (i <= j)
        {
            Team temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            i++;
            j--;
        }
    }

    if (leftIndex < j)
        SortArray(array, leftIndex, j);
    if (i < rightIndex)
        SortArray(array, i, rightIndex);
    return array;
}
void PrintBoard(Team[] board, string response)
{
    int sg = 0;
    if (response.Equals("y"))
    {
        Console.WriteLine();
    }

    Console.WriteLine("Brasileirão Board\n");
    for (int j = 0; j < board.Length; j++)
    {
        sg = board[j].goalsMade - board[j].goalsTaken;

        //Color Scheme for the board
        if (j == 0) { Console.ForegroundColor = ConsoleColor.Green; }
        else if (j > 0 && j <= 3) { Console.ForegroundColor = ConsoleColor.Yellow; }
        else if (j > 3 && j < 6) { Console.ForegroundColor = ConsoleColor.Cyan; }
        else if (j >= 6 && j <= 15) { Console.ForegroundColor = ConsoleColor.White; }
        else {Console.ForegroundColor = ConsoleColor.Red; }


        
        if (j < 9) { Console.WriteLine(" {0} | {1} | Points: {2} | SG: {3}", j+1, board[j].name, board[j].points, sg); }
        else { Console.WriteLine(" {0}| {1} | Points: {2} | SG: {3}", j+1, board[j].name, board[j].points, sg); }
        
    }

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\n Congratulations {0}! (Champion)\n", board[0].name);

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(" Relegateds:");
    for(int relegateds = 16; relegateds < 20; relegateds++)
    {
        Console.WriteLine(" {0}", board[relegateds].name);
    }

    Console.ForegroundColor = ConsoleColor.White;
}
#endregion

#region CONSOLE OUTPUT
while (true)
{
    Console.WriteLine("BRASILIAN LEAGUE SIMULATION!\n");
    Console.WriteLine("Do you want to print the matches? [Y/N]");
    string response = Console.ReadLine().ToLower();
    Console.Clear();

    Console.WriteLine("BRASILIAN LEAGUE SIMULATION!\n");

    SimulateMatches(teams, response);

    board = SortArray(teams, 0, 19);
    Array.Reverse(board, 0, 20);

    PrintBoard(board, response);

    Console.WriteLine("\nDo you want to simulate the league again? [Y/N]");
    string simulateAgain = Console.ReadLine().ToLower();
    if (simulateAgain.Equals("y")) 
    {
        ResetBoard(teams);
        Console.Clear();
        continue; 
    }
    else { return; }
}


#endregion