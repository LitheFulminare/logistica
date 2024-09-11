using System;
using System.IO;

// paths dos arquivos
string caminhoesPath = "data/caminhoes.txt";
string produtosPath = "data/produtos.txt";
string unidadesPath = "data/unidades.txt";

// Lê os dados dos caminhões
using (StreamReader reader = new StreamReader(caminhoesPath))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        // Process each line
        Console.WriteLine(line);  // Example: print the line
    }
}
