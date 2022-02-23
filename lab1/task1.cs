using System.Text;

var output = 25;

var wordCount = 0;
string[] stopwords = new string[] {"i", "he", "she", "it", "they", "them", "you", "at", "a", "that", "for", "in"};
var uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
var lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
string text = " " + new StreamReader("C:\\Users\\Nestor\\Desktop\\lab1\\task1input.txt", Encoding.Default).ReadToEnd() + "Σ";



var temp = "";
var i = 0;
toLowercaseLoop:
if (lowercaseLetters.Contains(text[i]))
{
    temp += text[i];
}
else if (uppercaseLetters.Contains(text[i]))
{
    temp += lowercaseLetters[uppercaseLetters.IndexOf(text[i])];
}
else if (i != 0 && (text[i] == '\n' || text[i] == ' ' || text[i] == '.' ||
text[i] == ',' || text[i] == '\n' ||
text[i] == ';' || text[i] == ':' || text[i] == '!' ||
text[i] == '\'' || text[i] == '?' || text[i] == '(' ||
text[i] == ')' || text[i] == '"' || text[i] == '&' ||
text[i] == '/' || text[i] == '_' || text[i] == '-'))
{
    temp += " ";
}
else if (text[i] == 'Σ')
{
    text = temp + " Σ";
    goto toLowercaseLoopEnd;
}
i += 1;
goto toLowercaseLoop;
toLowercaseLoopEnd:

string[] allWords = new string[30];

i = 0;
var countA = 0;
temp = "";
toStringArrayLoop:
if (countA == allWords.Length)
{
    string[] newArr = new string[allWords.Length * 2];
    int countC = 0;
copyLoop:
    if (countC < allWords.Length)
    {
        newArr[countC] = allWords[countC];
        countC++;
        goto copyLoop;
    }
    allWords = newArr;
}

if (text[i] == 'Σ')
{
    goto toStringArrayLoopEnd;
}
else if (text[i] != ' ')
{
    temp += text[i];
    i++;
}
else if (text[i] == ' ')
{
    i++;

    int countS = 0;
stopWordsCheckLoop:
    if (temp == stopwords[countS])
    {
        temp = "";
        goto toStringArrayLoop;
    }
    else if (stopwords.Length - 1 == countS)
    {
        goto stopWordsCheckLoopEnd;
    }
    countS++;
    goto stopWordsCheckLoop;

stopWordsCheckLoopEnd:
    allWords[countA] = temp;
    temp = "";
    countA++;
}

goto toStringArrayLoop;

toStringArrayLoopEnd:
wordCount = countA;




string[] countedWords = new string[wordCount];
int[] counters = new int[wordCount];

i = 0;

temp = "";



int countW = 0;
i = -1;

loopOne:
i++;
if (i <= wordCount)
{
    int j = i;

loopTwo:
    j++;
    if (j <= wordCount)
    {
        if (countW > wordCount || allWords[i] == "")
        {
            goto loopOne;
        }
        if (countedWords[0] == "" && i == 0)
        {
            countedWords[0] = allWords[i];
            counters[0] = 1;
        }
        else if (allWords[i] == allWords[j])
        {
            countedWords[i] = allWords[i];
            counters[i]++;
            countW++;
        }
        else if (!countedWords.Contains(allWords[i]))
        {
            countedWords[i] = allWords[i];
            counters[i]++;
            countW++;
        }

        goto loopTwo;
    }
    else
    {
        goto loopOne;
    }
}
else
{
    goto loopOneEnd;
}
loopOneEnd:


/*
//^same without goto
int countW=0;
for(i=0; i<=wordCount; i++){
    for(int j=i+1; j<=wordCount; j++){
        if(countW>wordCount||words[i]==""){
            break;
        }
        if(wordsArr[0]==""&&i==0){
            wordsArr[0]=words[i];
            countArr[0]=1;
        }
        else if(words[i]==words[j]){
            wordsArr[i]=words[i];
            countArr[i]++;
            countW++;
        }
        else if(!wordsArr.Contains(words[i])){
            wordsArr[i]=words[i];
            countArr[i]++;
            countW++;
        }
    }
}
*/

i = 0;
String[] sortedCountedWords = new String[wordCount];
int[] sortedCounters = new int[wordCount];

fillLoop:
int pos = 0, max = 0, min = 0;

sortLoop:
if (counters[pos] > max)
{
    max = counters[pos];
    min = pos;
}
pos++;
if (pos < wordCount)
{
    goto sortLoop;
}
sortedCounters[i] = max;
sortedCountedWords[i] = countedWords[min];
counters[min] = -1;


i++;
if (i < wordCount)
{
    goto fillLoop;
}

i = 0;
printLoop:
if( sortedCounters[i]!=0){
Console.WriteLine(sortedCountedWords[i] + " - " + sortedCounters[i]);
i++;
if (i < output && i<sortedCountedWords.Count())
{
    goto printLoop;
}
}

/*
//^same without goto
Console.WriteLine("\n");
for (int f = 0; f < wordCount; f++)
{
    Console.WriteLine(sortedCountedWords[f] + " : " + sortedCounters[f]);
};
*/
