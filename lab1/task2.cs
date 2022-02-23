string[] stopwords = new string[] { "i", "he", "she", "it", "they", "them", "you", "at", "a", "that", "for", "the", "to", "of" };
var uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";

(string, int, int[])[] dict = new (string, int, int[])[20];
int dictSize = 20;
int curDictSize = 0;//sometimes can't use array.Length

string temp = "";
int line = 0;
using (StreamReader reader = new StreamReader("C:/Users/Nestor/Desktop/lab1/task2input.txt"))
{
    try
    {
    getFromFileLoop://char by char
        char ch = (char)reader.Read();

        if (lowercaseLetters.Contains(ch))//is ok
        {
            temp += ch;
            goto getFromFileLoop;
        }
        else if (uppercaseLetters.Contains(ch))//uppercase -> swap with lowercase
        {
            temp += lowercaseLetters[uppercaseLetters.IndexOf(ch)];
        }

        if (ch == ' ' || ch == ' ' || ch == '.' ||
            ch == ',' || ch == '\n' || ch == ';' ||
            ch == ':' || ch == '!' || ch == '\'' ||
            ch == '?' || ch == '(' || ch == ')' ||
            ch == '"' || ch == '&' || ch == '/' ||
            ch == '_' || ch == '-' || ch == '\n')//check for symbols
        {
            if (ch == '\n')//next line
            {
                line++;
            }
            if (temp == "")//end of word
            {
                goto getFromFileLoop;
            }
            int countS = 0;
        stopWordsCheckLoop:
            if (countS != stopwords.Length)
            {
                if (stopwords[countS] == temp)
                {
                    temp = "";
                    goto getFromFileLoop;
                }
                countS++;
                goto stopWordsCheckLoop;
            }

            int countC = 0;
        countLoop:
            if (countC != curDictSize)
            {
                if (dict[countC].Item1 == temp)
                {
                    if (dict[countC].Item2 <= 100)
                    {
                        dict[countC].Item3[dict[countC].Item2] = line / 45 + 1;//rounds to get page num (i.e. line 43 43/45+1=1.95 -> page #1)
                        dict[countC].Item2++;
                    }
                    temp = "";
                    goto getFromFileLoop;
                }
                countC++;
                goto countLoop;
            }
            int[] occurs = new int[101];//occrurences (won`t output if 100+)
            occurs[0] = line / 45 + 1;
            dict[curDictSize] = (temp, 1, occurs);
            temp = "";
            curDictSize++;
        }

        if (dictSize == curDictSize)//dynamic
        {
            int countC = 0;
            dictSize *= 2;
            (string, int, int[])[] tempDict = dict;
            dict = new (string, int, int[])[dictSize];
        copyLoop:
            dict[countC] = tempDict[countC];
            countC++;
            if (countC != curDictSize)
            {
                goto copyLoop;
            }
        }
        if (!reader.EndOfStream)
            goto getFromFileLoop;
    }
    catch (IOException e)
    { throw e; }
}

int i = 0, j = 0, letter;//two indexes for sorting + letter index
bool swapNeeded;
sortLoop:
int wordLength;
letter = 0;
swapNeeded = false;
if (dict[i].Item1.Length < dict[j].Item1.Length)
{
    wordLength = dict[i].Item1.Length;
}
else
{
    wordLength = dict[j].Item1.Length;
    swapNeeded = true;
}

swapLoop:
if (dict[i].Item1[letter] < dict[j].Item1[letter])
{ swapNeeded = false; }
else if (dict[i].Item1[letter] > dict[j].Item1[letter])
{ swapNeeded = true; }
else
{
    letter++;
    if (letter < wordLength)
    { goto swapLoop; }
}
if (swapNeeded)
{
    (string, int, int[]) swapTemp = dict[i];
    dict[i] = dict[j];
    dict[j] = swapTemp;
}

j++;
if (j == curDictSize)
{
    i++;
    j = i;
}
if (i != curDictSize)
    goto sortLoop;





int countO = 0, countW = 0;//output and word counters
printLoop:
if (curDictSize != countO)
{
    if (dict[countO].Item2 == 101)//skip
    {
        countO++;
        goto printLoop;
    }
    Console.Write(dict[countO].Item1 + ":  ");//print word

printPagesLoop:
    if (countW != dict[countO].Item2 - 1)
    {
        Console.Write(dict[countO].Item3[countW] + ", ");//print occurences
        countW++;
        goto printPagesLoop;
    }
    Console.WriteLine(dict[countO].Item3[dict[countO].Item2 - 1]);//last occurence (ioor exc)
    countO++;
    countW = 0;
    goto printLoop;
}
