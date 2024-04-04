
class CalculatorAPI_V1
{
    static string RequestFilePath = "/home/houdaifa/calculatorFiles/Request-File.txt";
    static string ResponseFilePath = "/home/houdaifa/calculatorFiles/Response-File.txt";

    public static void  HandleOnCommandFromOneApplication()
    {
        while(true)
        {

            var lines = File.ReadAllLines(RequestFilePath);

            if(lines.Length > 0) 
            {

                var operationAsString = lines[0];

                var calcOperation = CalcOperation.FromString(operationAsString);

                decimal result = calcOperation.Calc();

                File.AppendAllText(ResponseFilePath,"Result:" + result.ToString() + "\n");

                File.WriteAllText(RequestFilePath,""); // Empty the request file
            }
            else
            {
                Thread.Sleep(1000); // Pause the application for 1 second
            }
        }
    }
}