using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        private void mono(SortedDictionary<char, double> Letters, string buffer, SortedSet<char> Alph, ref int LettersCount)
        {
            char ch;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (Alph.count(buffer[i]))
                {


                    if (Letters.count((char)char.ToLower(buffer[i])))
                    {
                        Letters[(char)char.ToLower(buffer[i])] += 1;
                        LettersCount++;
                    }
                    else
                    {
                        Letters.Add((char)char.ToLower(buffer[i]), 1);
                        LettersCount++;
                    }
                }
                else
                {
                    continue;
                }
            }
        }


        private void duo(SortedDictionary<string, double> BigramMap, string buffer, SortedSet<char> Alph, ref int BigramCount, bool Flag)
        {
            string Bigram;
            for (int i = 0; i < buffer.Length; i += 1 + Flag)
            { 
                if (Alph.count(buffer[i]) && Alph.count(buffer[i + 1]))
                { 
                    Bigram.push_back(char.ToLower(buffer[i]));
                    Bigram.push_back(char.ToLower(buffer[i + 1])); 
                    if (BigramMap.count(Bigram))
                    {
                        BigramMap[Bigram] += 1;
                        Bigram = "";
                        BigramCount++;
                    }
                    else
                    {
                        BigramMap.Add(Bigram, 1);
                        Bigram = "";
                        BigramCount++;
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        private void Filter(string FilePath, SortedSet<char> Alph, string Destination)
        {
            ifstream fin = new ifstream(FilePath);
            ofstream fout = new ofstream(Destination);
            string buffer;
            if (fin.is_open())
            {
                while (fin.peek() != EOF)
                {
                    getline(fin, buffer);
                    fin.seekg(fin.tellg());
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (Alph.count(buffer[i]))
                        {
                            fout << buffer[i];
                        }
                        else
                        {
                            continue;
                        }
                    }
                    buffer = "";
                }

            }
            fin.close();
            fout.close();
        }


        private void ShowMap(SortedDictionary<char, double> Letters, int LettersCount)
        {
            for (var it = Letters.cbegin(); it != Letters.cend(); it++)
            {
                Console.Write("[ ");
                Console.Write(it.first);
                Console.Write(" : ");
                Console.Write(it.second / LettersCount);
                Console.Write(" ]\n");
            }
        }
        private void ShowMap(SortedDictionary<string, double> Letters, int BigramCount)
        {
            double Sum = 0;
            for (var it = Letters.cbegin(); it != Letters.cend(); it++)
            {
                Sum += it.second;
                Console.Write("[ ");
                Console.Write(it.first);
                Console.Write(" : ");
                Console.Write(it.second / BigramCount);
                Console.Write(" ]\n");

            }
        }


        private double BigramEntropy(SortedDictionary<string, double> BigramMap, int BigramCount)
        {
            double Entropy = 0;
            for (var it = BigramMap.cbegin(); it != BigramMap.cend(); it++)
            {
                Entropy += (-(it.second / BigramCount) * log2((it.second) / BigramCount));
            }
            return Entropy / 2;
        }


        private double MonogramEntropy(SortedDictionary<char, double> Map, int LettersCount)
        {
            double Entropy = 0;
            for (var it = Map.cbegin(); it != Map.cend(); it++)
            {
                Entropy += (-(it.second / LettersCount) * log2((it.second) / LettersCount));
            }
            return Entropy;
        }

        static int Main()
        {
            uint start_time = clock();
            setlocale(LC_ALL, "rus");
            SortedSet<char> Alph = { 'à', 'À', 'á', 'Á', 'â', 'Â', 'ã', 'Ã', 'ä', 'Ä', 'å', 'Å', '¸', '¨', 'æ', 'Æ', 'ç', 'Ç', 'è', 'È', 'é', 'É', 'ê', 'Ê', 'ë', 'Ë', 'ì', 'Ì', 'í', 'Í', 'î', 'Î', 'ï', 'Ï', 'ð', 'Ð', 'ñ', 'Ñ', 'ò', 'Ò', 'ó', 'Ó', 'ô', 'Ô', 'õ', 'Õ', 'ö', 'Ö', '÷', '×', 'ø', 'Ø', 'ù', 'Ù', 'ú', 'Ú', 'û', 'Û', 'ü', 'Ü', 'ý', 'Ý', 'þ', 'Þ', 'ÿ', 'ß' };


            Alph.Add(' '); 


            if (false)
            {
                if (Alph.count(' '))
                {
                    string Destination = "..\\..\\parsed_text.txt";
                    Filter("..\\..\\original_text.txt", Alph, Destination);


                }
                else
                {
                    string Destination = "..\\..\\ parsed_text_wo_spaces.txt";
                    Filter("..\\..\\original_text.txt", Alph, Destination);
                }
            }
           
            if (true)
            {
                ifstream fin = new ifstream("..\\..\\original_text.txt");
                
                int LettersCount = 0;
                SortedDictionary<char, double> Letters = new SortedDictionary<char, double>();
                if (fin.is_open())
                {
                    string buffer;
                    while (fin.peek() != EOF)
                    { 
                        getline(fin, buffer); 
                                              
                        fin.seekg(fin.tellg()); 
                        mono(Letters, buffer, Alph, LettersCount);
                        buffer = "";
                    }


                }
                else
                {
                    Console.Write("Error. File isn't opened");
                    Console.Write("\n");
                }
                ShowMap(Letters, LettersCount);
                Console.Write("LettersCount: ");
                Console.Write(LettersCount);
                Console.Write("\n");
                
                double MonoEntropy;
                MonoEntropy = MonogramEntropy(Letters, LettersCount);


                Console.Write("Monogram Entropy: ");
                Console.Write(MonoEntropy);
                Console.Write("\n");
                Console.Write("Redudancy: ");
                Console.Write(1 - MonoEntropy / log2(Alph.Count / 2));
                Console.Write("\n");
                Console.Write("*********************************************\n");
            }

            
            if (true)
            {
                SortedDictionary<string, double> BigramMap = new SortedDictionary<string, double>();
                ifstream fin2 = new ifstream();
                if (Alph.count(' '))
                {
                    fin2.open("..\\..\\Original_text.txt");
                }
                else
                {
                    fin2.open("..\\..\\parsed_text_wo_spaces.txt");
                }
                int BigramCount = 0;
                if (fin2.is_open())
                {
                    string buffer;
                    while (fin2.peek() != EOF)
                    { 
                        getline(fin2, buffer); 
                                               	
                        fin2.seekg(fin2.tellg()); 
                                                  
                                                  
                                                  
                        duo(BigramMap, buffer, Alph, BigramCount, 1); 
                        buffer = ""; 


                    }
                }
                else
                {
                    Console.Write("Error. File isn't opened");
                    Console.Write("\n");
                }
                fin2.close();
                ShowMap(BigramMap, BigramCount);
                double BiEntropy;
                BiEntropy = BigramEntropy(BigramMap, BigramCount);
                Console.Write("Bigram Entropy: ");
                Console.Write(BiEntropy);
                Console.Write("\n");
                Console.Write("Redudancy: ");
                Console.Write(1 - BiEntropy / log2(Alph.Count / 2));
                Console.Write("\n");
            }
            uint end_time = clock();
            double search_time = (end_time - start_time) / 1000;
            Console.Write("Performing time: ");
            Console.Write(search_time);
            Console.Write("\n");
            Console.Write("\n");


            system("pause");
        }

    }
}
