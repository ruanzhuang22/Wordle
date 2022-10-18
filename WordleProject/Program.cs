using WordleProject;


wordList wordList = new wordList();
//Random wordle
string GetWord() 
{
    var random = new Random();
    var list = new List<string>(wordList.words);
    int index = random.Next(list.Count);
    string wordle = list[index];
    return wordle;
}
// remove char
string remove(char a, string word)
{
    int i = word.IndexOf(a);
    word = word.Remove(i, 1);
    return word;
}
// count char
int CountChar(char a, char[] wordChar)
{
    int count = 0;
    for (int i = 0; i < wordChar.Length; i++) { if (a == wordChar[i]) count++; }
    return count++;
}

// game wordle
// Stage 1:  random a word
string wordle = GetWord();
// Stage 3: input "y" to play game or "n" to stop game
Console.Write("\nWould you like to play My Wordle [y|n]?");
string comfirm;
bool check = true;
while (check)
{
    comfirm = Console.ReadLine();
    if (comfirm == "n")
    {
        Console.WriteLine("Thanks for playing!");
        check = false;
    }
    else if (comfirm == "y")
    {
        Console.WriteLine("Wordle is: " + wordle);
        Console.WriteLine("-------------\n| - - - - - |");
        char[] wordleChar = wordle.ToCharArray();
        bool win = false;
        char[] correctLetter = new char[5]; char[] used = new char[23];
        int posCorrect = 0; int posUsed = 0;  int turn = 0;
        do
        {
            Console.Write("\nPlease enter your guess - attempt" + (turn + 1) + " : ");
            string guess = Console.ReadLine();
            Console.WriteLine("You guess: " + guess+ "\n-------------");
            Console.WriteLine(guess);
            char[] guessChar = guess.ToCharArray();
            if (String.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Please input your guess!");
            }
            else
            {
                if (wordList.words.Contains(guess))
                {
                    if (guess == wordle)
                    {
                        Console.WriteLine("\nSolved in  " + (turn + 1) + " tries! Well done!");
                        win = true;
                        break;
                    }
                    else
                    {
                        int correctSpot = 0;
                        int wrongSpot = 0;
                        string remainLetterInWord = wordle;
                        string response = "";
                        for (int i = 0; i < guessChar.Length; i++)
                        {
                           
                            if (guessChar[i] == wordleChar[i])
                            {

                                //Stage 4: Replace "^" to the correct letter (correct:  both letter and position)
                                response += "^";
                                remainLetterInWord = remove(guessChar[i], remainLetterInWord);
                                // Stage 7: count the correct spot letter
                                correctSpot++;
                                // Stage 8: array correct letters
                                if (correctLetter.Contains(guessChar[i]) == false) { correctLetter[posCorrect] = guessChar[i]; posCorrect++; }
                            }
                            
                            else
                            {
                                // Stage 6: Replace "^" to the wrong letter ( wrong:  both letter and position)
                                response += "-";
                                // Stage 8: array used letters
                                if (used.Contains(guessChar[i]) == false){ used[posUsed] = guessChar[i]; posUsed++; }
                            }
                        }
                        char[] resChar = response.ToCharArray();
                        for (int j = 0; j < guessChar.Length; j++)
                        {
                            //Stage 5:  letter in the word but in the wrong position
                            if (remainLetterInWord.Contains(guessChar[j]) == true && guessChar[j] != wordleChar[j])
                            {
                                // Stage 14:  check the special cases
                                int countG = CountChar(guessChar[j], guessChar);
                                int countW = CountChar(guessChar[j], wordleChar);
                                // Stage 7: count the wrong spot letter
                                for (int k = 0; k < 5; k++) { if (j == k) { resChar[k] = '*'; wrongSpot++; } }
                                if (countG > countW) { remainLetterInWord = remove(guessChar[j], remainLetterInWord); }
                            }
                        }
                        string respo = new string(resChar);  Console.WriteLine(respo);
                        // Print correct and wrong spot 
                        Console.WriteLine("\nCorrect spot(^): " + correctSpot + " Wrong spot(*): " + wrongSpot);
                        // Print correct letters
                        Console.Write("Correct letters: "); foreach (char c in correctLetter) { Console.Write(c + " "); }
                        string resUsed = new string(used);
                        for (int c = 0; c < correctLetter.Length; c++) { if (used.Contains(correctLetter[c])) resUsed = resUsed.Replace(correctLetter[c], ' '); }
                        used = resUsed.ToCharArray();
                        // Print used letters
                        Console.Write("Used letters: "); foreach (char c in used) { if (c != ' ') Console.Write(c + " "); }
                    }
                }
                else if (guess.Length != wordle.Length) // ensure guess entered is a five letter word
                {
                    Console.WriteLine("Five letter words only please. Try again!");
                }
                else //  the guess is not valid word
                {
                    Console.WriteLine("Not word in list! Try again!");
                }
            }
            turn++;
        }
        while (turn < 6); // Stage 9 : the limit of turnplay
        if (win == false)
        {
            Console.WriteLine("\nOh no! Better luck next time!\nThe wordle was: " + wordle);
        }
        Console.Write("\nWould you like to play My Wordle [y|n]? ");
        wordle = GetWord();
    }
    else // Stage 11 : just "y" and "n" to active game
    {
        Console.Write("\nWould you like to play My Wordle [y|n]?");
    }
}










