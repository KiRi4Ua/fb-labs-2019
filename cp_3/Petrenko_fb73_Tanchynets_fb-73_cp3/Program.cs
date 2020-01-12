using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private const string alpha = "абвгдежзийклмнопрстуфхцчшщыьэюя";

        private void set_locale(wfstream file)
        {
            setlocale(LC_ALL, "");
            locale loc = new locale("");
            cout.imbue(loc);
            file.imbue(loc);
        }

        private void read_file(string str, wfstream file)
        {
            getline(file, str);
        }

        private int get_from_alphabet(char c)
        {
            for (int i = 0; i < 31; i++)
            {
                if (c != alpha[i])
                {
                    continue;
                }
                else
                {
                    return (i);
                }
            }
            return (-1);
        }


        
        private int create_bigrams(string str, int cur, int flag, int str_size, int[] bigrams, int[] converted_text)
        {
            int alpha_size = 31;
            int bigram_size = alpha_size * alpha_size;
            int[] bigram = new int[bigram_size];
            int i = -1;
            int[] conv_text = new int[str_size / 2];
            while (++i < bigram_size)
            {
                bigram[i] = 0;
            }
            i = -1;
            while (++i < str_size)
            {
                switch (flag)
                {
                    case 0:
                        {
                            flag = 1;
                            cur += 31 * get_from_alphabet(str[i]);
                            break;
                        }
                    case 1:
                        {
                            flag = 0;
                            cur = get_from_alphabet(str[i]) + cur;
                            bigram[cur]++;
                            int ind = i / 2;
                            
                            conv_text[ind] = cur;
                            cur = 0;
                            break;
                        }
                }
            }
            bigrams[0] = bigram;
            return (conv_text);
        }

        
        private void search_best(int[] bigrams, int[] best_bigrams)
        {
            int[] candidates = new int[5];
            int best;
            int i = 0;
            int l;
            i = -1;
            while (++i < 5 && (l = -1) && !(best = 0))
            {
                while (++l < 961)
                {
                    (bigrams[l] > bigrams[best]) ? (best = l) : 0;
                }
                candidates[i] = best;
                bigrams[best] = -1;
            }
            best_bigrams[0] = candidates;
        }
       
        private object create_rus_bigrams(int[] rus_bigrams)
        {
            int[] rus = new int[5];
            const string data1 = "снтне";
            const string data2 = "тооан";
            int i = -1;
            while (++i < 5)
            {
                rus[i] = get_from_alphabet(data1[i]) * 31 + get_from_alphabet(data2[i]);
            }
            rus_bigrams[0] = rus;
        }
        private void normal(ref int num)
        {
            (num < 0) ? (num = 961 + num) : 0;
        }

        private int calc(ref int a, ref int b, ref int x, ref int y)
        {
            int q;
            int r;
            int x1;
            int x2;
            int y1;
            int y2;
            x2 = 1;
            x1 = 0;
            y2 = 0;
            y1 = 1;
            if (b == 0)
            {
                x = 1;
                y = 0;
                return (a);
            }
            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                a = b;
                b = r;
                x2 = x1, x1 = x, y2 = y1, y1 = y;
            }
            x = x2;
            y = y2;
            return (a);
        }

        private int evklid(int a, int n)
        {
            int d;
            int x;
            int y;
            d = calc(ref a, ref n, ref x, ref y);
            if (d == 1)
            {
                if (x > 0)
                {
                    return x;
                }
                else
                {
                    return (n + x);
                }
            }
            return (0);

        }

        private int gcd(int razx, int temp)
        {
            return ((temp == 0) ? razx : gcd(temp, razx % temp));
        }

        private void get_a(int razx, int razy, ref int size_of_a, ref int[] mas_a)
        {
            normal(ref razx);
            normal(ref razy);
            int min = gcd(razx, 961);
            mas_a = new int[min];
            if (min != 1 && razy % min == 0)
            {
                razx = razx / min;
                razy = razy / min;
                int start;
                start = evklid(razx, 961 / min) * razy;
                start %= 961;
                normal(ref start);
                int i = -1;
                while (++i < min)
                {
                    size_of_a++;
                    mas_a[i] = start + i * 961;
                    mas_a[i] %= 961;
                    normal(ref mas_a[i]);
                }
            }
            else
            {
                if (min == 1)
                {
                    size_of_a = min;
                    int i = 0;
                    mas_a[i] = (evklid(razx, 961) * razy) % 961;
                    normal(ref mas_a[i]);
                }
            }
        }

        private void get_b(ref int rus, ref int best, ref int size_of_a, int[] mas_a, ref int[] mas_b)
        {
            mas_b = new int[size_of_a];
            int i = -1;
            while (++i < size_of_a)
            {
                mas_b[i] = (best - mas_a[i] * rus);
                mas_b[i] %= 961;
                normal(ref mas_b[i]);
            }
        }



private double index(int size, ref string tmp, double ret)
    {
        int[] count = new int[31];
        int i = -1;
        while (++i < 31)
        {
            count[i] = 0;
        }
        i = -1;
        while (++i < size)
        {
            count[get_from_alphabet(tmp[i])] += 1;
        }
        i = -1;
        while (++i < 31)
        {
            double f = ((double)((count[i] - 1) * (count[i]))) / ((double)((size - 1) * (size)));
        }

        for (int i = 0; i < 31; i++)
        {
            double first = (double)(count[i] * (count[i] - 1));
            double second = first / (double)(size * (size - 1));
            ret += second;
        }
        return (ret);
    }

    private void print(int[] mas_a, int[] mas_b, ref string tmp, int cur)
    {
        Console.Write(mas_a[cur]);
        Console.Write(" ");
        Console.Write(mas_b[cur]);
        Console.Write("\n");
        Console.Write(tmp);
        Console.Write("\n\n\n");
    }

    private bool verify_text(int[] mas_a, ref int[] mas_b, ref int size_of_a, ref int bigrams, int size, ref int[] conv_text)
    {
        Console.Write(mas_a[0]);
        Console.Write("\n");
        string tmp = new string(new char[size * 2]);
        tmp = tmp.Substring(0, size * 2);
        int cur = -1;
        while (++cur < size_of_a)
        {
            if (evklid(mas_a[cur], 961) == 0)
            {
                continue;
            }
            else
            {
                double index_text = 0;
                int i = -1;
                while (++i < size)
                {
                    int forcin;
                    int x = (evklid(mas_a[cur], 961) * (conv_text[i] - mas_b[cur])) % 961;
                    
                    normal(x);
                   
                    tmp = StringFunctions.ChangeCharacter(tmp, i * 2, alpha[(x - (x % 31)) / 31]);
                    tmp = StringFunctions.ChangeCharacter(tmp, 1 + i * 2, alpha[x % 31]);
                }
                index_text = index(size * 2, ref tmp, 0);
                Console.Write("index is ");
                Console.Write(index_text);
                Console.Write("\n");
                if (index_text > 0.055)
                {
                    print(mas_a, mas_b, ref tmp, cur);
                    return (1) != 0;
                }
            }
        }
        return (0) != 0;
    }

    private void process_all(ref int bigrams, ref int conv, int[] best, int[] rus, int size)
    {
        int i;
        int l;
        int j;
        int k;
        for (i = 4; i >= 0; i--, l = 0)
        {
            for (; l < 5; l++, j = 0)
            {
                for (; j < 5; j++, k = 0)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    for (; k < 5; k++)
                    {
                        if (k == l)
                        {
                            continue;
                        }
                        int size_of_a = 0;
                        int[] mas_a;
                        int[] mas_b;
                        get_a(rus[i] - rus[j], best[l] - best[k], size_of_a, mas_a);

                        get_b(rus[i], best[l], size_of_a, mas_a, mas_b);
                        Console.Write("bigram (");
                        Console.Write(rus[i]);
                        Console.Write(",");
                        Console.Write(rus[j]);
                        Console.Write(") (");
                        Console.Write(best[l]);
                        Console.Write(", ");
                        Console.Write(best[k]);
                        Console.Write(")");
                        Console.Write("\n");
                        Console.Write("a = ");
                        Console.Write(mas_a[0]);
                        Console.Write(" b = ");
                        Console.Write(mas_b[0]);
                        Console.Write("\n");
                        if (verify_text(mas_a, ref mas_b, ref size_of_a, ref bigrams, size / 2, ref conv))
                        {
                            goto escape;
                        }
                    }
                }
            }
        }
    escape:
        return;
    }

   
    internal static class StringFunctions
    {
        
        public static string ChangeCharacter(string sourceString, int charIndex, char newChar)
        {
            return (charIndex > 0 ? sourceString.Substring(0, charIndex) : "")
                + newChar.ToString() + (charIndex < sourceString.Length - 1 ? sourceString.Substring(charIndex + 1) : "");
        }

        
        public static bool IsXDigit(char character)
        {
            if (char.IsDigit(character))
                return true;
            else if ("ABCDEFabcdef".IndexOf(character) > -1)
                return true;
            else
                return false;
        }

        
        public static string StrChr(string stringToSearch, char charToFind)
        {
            int index = stringToSearch.IndexOf(charToFind);
            if (index > -1)
                return stringToSearch.Substring(index);
            else
                return null;
        }

       
        public static string StrRChr(string stringToSearch, char charToFind)
        {
            int index = stringToSearch.LastIndexOf(charToFind);
            if (index > -1)
                return stringToSearch.Substring(index);
            else
                return null;
        }

        
        public static string StrStr(string stringToSearch, string stringToFind)
        {
            int index = stringToSearch.IndexOf(stringToFind);
            if (index > -1)
                return stringToSearch.Substring(index);
            else
                return null;
        }

        
        private static string activeString;
        private static int activePosition;
        public static string StrTok(string stringToTokenize, string delimiters)
        {
            if (stringToTokenize != null)
            {
                activeString = stringToTokenize;
                activePosition = -1;
            }

           
            if (activeString == null)
                return null;

            
            if (activePosition == activeString.Length)
                return null;

           
            activePosition++;
            while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) > -1)
            {
                activePosition++;
            }

            
            if (activePosition == activeString.Length)
                return null;

            
            int startingPosition = activePosition;

            
            do
            {
                activePosition++;
            } while (activePosition < activeString.Length && delimiters.IndexOf(activeString[activePosition]) == -1);

            return activeString.Substring(startingPosition, activePosition - startingPosition);
        }
    }
    void process_all(int bigrams, int converted_text, int best_bigrams, int rus_bigrams, int size)
    {
        throw new NotImplementedException();
    }


static void Main(string[] args)
    {
        wfstream file = new wfstream(args[1]);
        set_locale(file);
        string str;
        Console.Write(alpha);
        Console.Write("\n");
        read_file(str, file);
        Console.Write("our file is\n");
        Console.Write(str);
        Console.Write("\n");
        int size = str.Length;
        int bigrams_size = size / 2;
        
        int bigrams;
        
        int converted_text;
        converted_text = create_bigrams(str, 0, 0, size, bigrams, converted_text);
        
        int best_bigrams;
        
        int rus_bigrams;
        create_rus_bigrams(rus_bigrams);
        search_best(bigrams, best_bigrams);
        process_all(bigrams, converted_text, best_bigrams, rus_bigrams, size);
    }

    
}
}
