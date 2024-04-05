using System.IO.Compression;

string RequestFilePath = "/home/houdaifa/calculatorFiles/Request-File.txt";
string ResponseFilePath = "/home/houdaifa/calculatorFiles/Response-File.txt";

while(true)
{

    // Using the API
    var opData = GetUserInput();
    
    var requestId = Guid.NewGuid();
    var requestString = "Id=" + requestId + "|" +opData.op + " " + opData.num1 + " " + opData.num2 + "\n";

    File.AppendAllText(RequestFilePath,requestString);

    while(true)
    {
        var lines = File.ReadAllLines(ResponseFilePath);
        bool IsRequestHandled = false;
        if(lines.Length == 0)
        {
            Thread.Sleep(500);// check for response every 0.5 second
        }

        else
        {
            foreach(var line in lines)
            {
                // Id=number|Result:number
                Guid reponseId = Guid.Parse(line.Split("|")[0].Split("=")[1]);

                if(reponseId == requestId)
                {
                    var response = line.Split("|")[1];

                    var parts = response.Split(":");

                    double result;
                    
                    if(double.TryParse(parts[1],out result))
                    {
                        System.Console.WriteLine($"\n --> Result:{result}");
                        
                        var lines2 = File.ReadAllLines(ResponseFilePath);
                        var linesToWriteBack = lines2.Where(l=>l != line);// get the unhandled requests
                        File.WriteAllLines(ResponseFilePath,linesToWriteBack);
                        
                        IsRequestHandled = true;
                        break;
                    }
                    else
                    {
                        System.Console.WriteLine("Error with the calculation API");
                    }
                }
            }

            if(IsRequestHandled)
                break;
        }
    }

}


OperationData GetUserInput()
{
    string opAsString;
    
    while(true)
    {
        System.Console.Write("Enter operation (+,-,*,/) >> ");
        char opAsCharacter = System.Console.ReadLine()[0];

        opAsString = opAsCharacter switch
        {
            '+'=> "ADD",
            '-'=> "SUB",
            '*'=> "MUL",
            '/'=> "DIV",
            _  => "Invalid"
        };

        if(opAsString == "Invalid")
        {
            System.Console.WriteLine("\nInvalid operation, try again >> ");
        }
        else
        {
            break;
        }
    }

    double num1;
    
    System.Console.Write("Enter the first number >> ");
    while(! double.TryParse(System.Console.ReadLine(),out num1))
    {
        System.Console.Write("Number not valied. try again >>");
    }

    double num2;
    System.Console.Write("Enter the second number >> ");
    while(! double.TryParse(System.Console.ReadLine(),out num2))
    {
        System.Console.Write("Number not valied. try again >> ");
    }

    return new OperationData(opAsString,num1,num2);
}
record OperationData(string op, double num1,double num2);