using System;

namespace Hangman
{
    public class WordProvider
    {
        string[] Words =  new[]
          {
               "DELIVER", "ROCK", "ANIME", "STRAWMAN", "BERRY", 
               "SEARCH", "GOOSE", "BLOOD", "GUTS", "PEACE"
          };

        public Random random = new Random();

        public string GetRandomWord()
        {
            int index = random.Next(Words.Length);
            return Words[index].ToLowerInvariant();
        }
    }
}