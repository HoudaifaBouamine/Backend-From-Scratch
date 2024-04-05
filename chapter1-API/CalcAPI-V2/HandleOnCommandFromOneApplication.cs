
using System.Runtime.InteropServices;
using System.Xml;

class CalculatorAPI_V1
{
    static string RequestFilePath = "/home/houdaifa/calculatorFiles/Request-File.txt";
    static string ResponseFilePath = "/home/houdaifa/calculatorFiles/Response-File.txt";

    public static void  HandleOnCommandFromOneApplication()
    {
        while(true)
        {

            var lines = File.ReadAllLines(RequestFilePath);

            if(lines.Length == 0)
                Thread.Sleep(1000);
            else
            {
                foreach(var operationAsString in lines)
                {
                    var calcOperation = CalcOperation.FromString(operationAsString);

                    decimal result = calcOperation.Calc();

                    var response = $"Id={calcOperation.GetId()}|Result:{result}\n";
                    File.AppendAllText(ResponseFilePath,response);

                    System.Console.WriteLine(response);// for debugging

                }

                var lines2 = File.ReadAllLines(RequestFilePath); // Get All requests
                var linesToWriteBack = lines2.Where(l=>! lines.Contains(l));
                File.WriteAllLines(RequestFilePath,linesToWriteBack);
            }
            
        }
    }
}