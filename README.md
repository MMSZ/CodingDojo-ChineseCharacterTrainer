CodingDojo-ChineseCharacterTrainer
==================================

Reference implementation for TDD Coding Dojo


### Iteration 1:
Write a WPF application that shows the following chinese characters (one after another) and allows the user to type in the correct pinyin including the tone:
	你 ni3 - 不 bu4 - 车 che1

After every answer the entered text should be reset.

### Iteration 2:
After every word the user is informed whether it was correct or not and is shown the correct solution.
Correct answers should be shown in green, incorrect answers in red.

### Iteration 3:
After all three words a summary should be shown how many questions have been answered correct and how many incorrect.
Also show the time it took to answer all questions.

### Iteration 4:
Show the number of correct and incorrect answers at all times.

### Iteration 5:
Modify the application to show a button that allows the user to choose a csv file. Once the user has selcted a file load all characters, solutions and translations from that file. The format of the file should be as follows:
你,ni3,you
不,bu4,no
车,che1,vehicle;car

### Iteration 6:
The pinyin should be formatted more nicely with the correct tones instead of the trailing numbers.
e.g. che1 should be chē

List of vowels and their unicode representation:
> a ā = U+0101 á = U+00E1 ǎ = U+01CE à = U+00E0  
> e ē = U+0113 é = U+00E9 ě = U+011B è = U+00E8  
> i ī = U+012B í = U+00ED ǐ = U+01D0 ì = U+00EC  
> o ō = U+014D ó = U+00F3 ǒ = U+01D2 ò = U+00F2  
> u ū = U+016B ú = U+00FA ǔ = U+01D4 ù = U+00F9  
> ü ǖ = U+01D6 ǘ = U+01D8 ǚ = U+01DA ǜ = U+01DC  

Rules for placing the tone mark:

1.  If there is an a, e, or o, it will take the tone mark; in the case of ao, the mark goes on the a.
2.  Otherwise, the vowels are -iu or -ui, in which case the second vowel takes the tone mark.

Answers should support both formats. E.g. 车 accepts che1 and chē as correct answer.

### Iteration 7:
Modify the program to not directly start training after opening a file.
Instead add functionality to import a dictionary file into a local database from the user interface.
Also add functionality to choose from all dictionaries in the database to start the training.

### Iteration 8:
Modify the program to connect to a WCF service to store and retrieve dictionaries. The WCF service stores the dictionaries in a database again.

### Iteration 9:
Allow the user to upload her highscore after all questions are answered. The user has to provide a name that is between 3 and 12 characters long.
The highscore is based on the time it took to answer all questions and a penalty of 5 seconds for every wrong answer.

### Iteration 10:
After the user has uploaded her highscore she is shown a list of top 10 highscores and the position of her current attempt and her best attempt.
Also show this list of highscores next to the dictionary selection.
