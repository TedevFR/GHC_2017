using System;

namespace GHC_2017
{
    class Program
    {
        const string inputFilePathTest = "Inputs/fichier.txt";
        const string inputFilePathExample = "Inputs/kittens.in";
        const string inputFilePathSmall = "Inputs/me_at_the_zoo.in";
        const string inputFilePathMedium = "Inputs/trending_today.in";
        const string inputFilePathBig = "Inputs/videos_worth_spreading.in";

        const string outpoutFilePathTest = "Outputs/fichier.out";
        const string outputFilePathExample = "Outputs/kittens.out";
        const string outputFilePathSmall = "Outputs/me_at_the_zoo.out";
        const string outputFilePathMedium = "Outputs/trending_today.out";
        const string outputFilePathBig = "Outputs/videos_worth_spreading.out";

        static void Main(string[] args)
        {
            Solve(inputFilePathTest, outpoutFilePathTest);
            Solve(inputFilePathExample, outputFilePathExample);
            Solve(inputFilePathSmall, outputFilePathSmall);
            Solve(inputFilePathMedium, outputFilePathMedium);
            Solve(inputFilePathBig, outputFilePathBig);

            Console.WriteLine("Done !");
            Console.ReadKey();
        }

        private static void Solve(string inputFilePathExample, string outputFilePathExample)
        {
            Solver solver = new Solver(inputFilePathExample, outputFilePathExample);
            solver.Solve();
        }
    }
}