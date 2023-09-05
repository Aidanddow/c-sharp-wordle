
/*
 *  A Wordle game, made with C#
 */

using System;
using System.Collections.Generic;

public class Game {

    public string[] wordlist;
    public string answer;

    public static void Main(string[] args) {
        var g = new Game();
    }

    Game() {
        this.initialiseWords();
        this.chooseRandomWord();
        this.play();
    }

    void play() {

        var won = false;
        var tries = 6;
        
        while (!won && tries > 0) {
            Console.WriteLine("You have " + tries + " tries left!");
            var guess = getGuess("");
            won = this.getWordSuccess(guess);
            tries -= 1;
        }

        if (tries == 0) {
            Console.WriteLine("Unlucky, the word was: " + this.answer);
        } else {
            Console.WriteLine("Congratulations, the word was " + this.answer);
        }
    }

    public bool getWordSuccess(string guess) {
        // Show the matching 
        var result = guess.Zip(
            this.answer.ToLower(), 
            (i, j) => i == j ? "🟩" : (this.answer.Contains(i) ? "🟨" : "⬛️")
        );
        
        Console.WriteLine(string.Join("", result));
        return (guess == this.answer);
    }

    public void initialiseWords() {
        var textFile = "wordlist.txt";
        var text = File.ReadAllText(textFile);
        string[] lines = text.Split();

        this.wordlist = lines;
    }

    public void chooseRandomWord() {
        Random random = new Random();
        int randNum = random.Next(0, this.wordlist.Count());
        this.answer = this.wordlist[randNum].Trim();
    }

    public string getGuess(string guess) {
        if (!this.wordlist.Contains(guess)) {
            Console.WriteLine("Enter a guess: ");
            guess = Console.ReadLine();
            return getGuess(guess);
        }
        return guess;
    }
}
