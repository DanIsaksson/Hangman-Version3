using System;
using System.Collections.Generic;

namespace Hangman
{
     public class Game()
     {
          // USE FOR MAXIMUM VALID ATTEMPTS
          const int MaxGuesses = 10;

          // INITIALIZING secretWord empty.
          string secretWord = "";
          char[] wordState = new char[0];
          int guessesLeft;
          List<char> wrongLetters = new();

          // Instance new wordProvider for use
          WordProvider wordProvider = new WordProvider();



          //  _                 
          // | |                
          // | |       ___   ___ _ __  
          // | |      / _ \ / _ \| '_ \ 
          // | |_____| (_) | (_) | |_) |
          // | ______|\___/ \___/| .__/ 
          //                     | |    
          //                     |_|    
          public void Run()
          {
               bool gameActive = true;
               while (gameActive)
               {
                    StartRound();
                    PlayRound();
                    gameActive = AskPlayAgain();
                    Console.Clear();
               }
          }

          public void StartRound()
          {
               secretWord = wordProvider.GetRandomWord();

               // Declaring, initializing and assigning _ to every slot in wordState
               wordState = new char[secretWord.Length];
               for (int i = 0; i < wordState.Length; i++) wordState[i] = '_';

               guessesLeft = MaxGuesses;
               wrongLetters.Clear(); // clear wrongLetters of entries for the new round
          }

          public void PlayRound()
          {
               // conditions for a round to keep going.
               while (guessesLeft > 0 && new string(wordState).Contains('_'))
               {
                    // display progress and guessed letters as well as attempts, check method
                    DisplayStatus();
                    // call on PromptGuess() to inform of details and assign user input to "input" string variable.
                    string input = PromptGuess();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                         Console.WriteLine("Please enter a letter or word.");
                         // "continue" skips the rest of the current loop and starts the next iteration
                         continue;
                    }

                    // LETTER & WORD GUESS INPUT
                    input = input.Trim().ToLower();

                    //Handle either 1char input (input.Length == 1 checks if the input length is just 1 char long)
                    if (input.Length == 1)
                    {
                         HandleLetterGuess(input[0]);
                    }
                    else //if it's not just 1 char, it's probably a word. Probably. So we check for thatin the HandleWordGuess() 
                    // method. using input as the string parameter
                    {
                         HandleWordGuess(input);
                    }
               }

               DisplayStatus();
               if (guessesLeft > 0)
               {
                    Console.WriteLine("Congratulations! You guessed the word!");
               }
               else
               {
                    Console.WriteLine($"Sorry, you lost. The word was \"{secretWord}\".");
               }
          }

          //assigned to string "input" variable in PlayRound()
          public string PromptGuess()
          {
               Console.Write("Enter a letter or guess the whole word: ");
               // ?? means that if the user sends a null string, the program returns it as empty instead.
               return Console.ReadLine() ?? string.Empty;
          }

          public void DisplayStatus()
          {
               Console.WriteLine();
               //new string(char[] array) creates a string from a character array (wordState)
               // We can't use ToStrign() because it would print the type name like " Word: System.Char[] "
               // will try with string.Join("", wordState)  as well.
               Console.WriteLine($"Word Progress: {new string(wordState)}");

               // Simply add a little line to tell no wrongLetters entries exist yet, otherwise present them.
               if(wrongLetters.Count == 0)
               {
                    Console.WriteLine();
                    Console.WriteLine($"Wrong letters: --- No wrong guesses yet ---");
               }
               else
               {
               // string.Join takes the elements of the wrongLetters List<char> collection, 
               // converts them to strings and joins them together with the string ", " in-between each item.
                    Console.WriteLine($"Wrong letters: {string.Join(", ", wrongLetters)}"); 
               }

               Console.WriteLine($"Wrong guesses left: {guessesLeft}");
               Console.WriteLine();
          }

          public void HandleLetterGuess(char letter)
          {
               // if the char from the letter variable is not a letter, it shows the contents of "Input must be a letter."
               if (!char.IsLetter(letter))
               {
                    Console.WriteLine("Input must be a letter.");
                    return;
               }

               //ARRAY.EXISTS
               // Inside an array (Array.Exists), specifically (wordState...), 
               // does (... is there one char that equals the value ofletter?)  !!! lambda expression !!!
               //WRONGLETTERS.CONTAINS
               // if wrongLetters variable contains the letter... do statement or expression..? I'll learn the difference one day.
               if (Array.Exists(wordState, c => c == letter) || wrongLetters.Contains(letter))
               {
                    Console.WriteLine($"You already guessed '{letter}'. Try something else.");
                    return; // Do not consume a guess
               }

               bool found = false;
               for (int i = 0; i < secretWord.Length; i++)
               {
                    if (secretWord[i] == letter)
                    {
                         wordState[i] = letter;
                         found = true;
                    }
               }

               if (!found)
               {
                    //adds letter char to wrongLetters List<char>
                    wrongLetters.Add(letter);
                    guessesLeft--;
               }
          }

        public void HandleWordGuess(string word)
        {   // we force word and secretWord to lowercase in case the input is like... Deliver or dELiver etc.
            if (word.ToLower() == secretWord.ToLower())
            {
               //convert the contents of secretWord to char-array 
               //with secretWord.ToCharArray() (note to self: meaning we can change the values of its contents)
               //and assign the value of this char array to the char array wordState.
               wordState = secretWord.ToCharArray();
            }
            else
            {
                Console.WriteLine("Incorrect word guess.");
                guessesLeft--;
            }
        }

        public static bool AskPlayAgain()
        {
            Console.Write("Play again? (y/n): ");
            // string? gives no errors if it's null.
            string? response = Console.ReadLine();
            //returns true or false value based on if the response is "y", other things means "n"
            return response != null && response.Trim().ToLower().StartsWith('y');
        }
     }
}