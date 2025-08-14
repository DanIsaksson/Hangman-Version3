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

          WordProvider wordProvider = new WordProvider();

          public void Run()
          {
               bool gameActive = true;
               while (gameActive)
               {
                    StartRound();
                    //PlayRound();
                    //gameActive = AskPlayAgain();
                    //Console.Clear();
               }
          }

          private void StartRound()
          {
               secretWord = wordProvider.GetRandomWord();
               wordState = new string('_', secretWord.Length).ToCharArray();
               guessesLeft = MaxGuesses;
               wrongLetters.Clear();
               Console.WriteLine(secretWord);
               Console.WriteLine(wordState);
               Console.ReadLine();
          }
     }
}

 
               //  __  __    _    ___ _   _   __  __ _____ _   _ _   _ 
               // |  \/  |  / \  |_ _| \ | | |  \/  | ____| \ | | | | |
               // | |\/| | / _ \  | ||  \| | | |\/| |  _| |  \| | | | |
               // | |  | |/ ___ \ | || |\  | | |  | | |___| |\  | |_| |
               // |_|  |_/_/   \_\___|_| \_| |_|  |_|_____|_| \_|\___/



               //  ____     _   __   __ _____ ____   _        _   __   __
               // / ___|   / \  |  \/  | ___  | _ \ | |      / \  \ \ / /
               // | |  _  / _ \ | |\/| |  _|  ||_) || |     / _ \  \ V / 
               // | |_| |/ ___ \| |  | | |___ | __/ | |__  / ___ \  | |  
               // \____/_/    \_\_|  |_|_____ |_|   |_____/_/   \_\ |_|