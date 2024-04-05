
using System.Security.Cryptography;

class CalcOperation
{
    Guid id;
    double leftOperand;
    double rightOperand;
    OperationType operationType;

    // Received String must be on this formate : Id=Num0|Op Num1 Num2
    static public CalcOperation FromString(string requestAsString)
    {
        // Id=1|ADD 121 31 (example)
        var Id = Guid.Parse(requestAsString.Split("|")[0].Split("=")[1]);
        var operationAsString = requestAsString.Split("|")[1];
        var parts = operationAsString.Split(" ");

        return new CalcOperation
        {   
            id = Id,
            leftOperand = double.Parse(parts[1]),
            rightOperand = double.Parse(parts[2]),
            operationType = parts[0] switch
            {
                "ADD" => OperationType.Add,
                "SUB" => OperationType.Sub,
                "MUL" => OperationType.Mul,
                "DIV" => OperationType.Div,
                _ => 0
            }
        };
    }

    public decimal Calc()
    {
        return operationType switch
        {
            OperationType.Add => (decimal)(leftOperand + rightOperand),            
            OperationType.Sub => (decimal)(leftOperand - rightOperand),
            OperationType.Mul => (decimal)(leftOperand * rightOperand),
            OperationType.Div => rightOperand == 0 ? 0 : (decimal)(leftOperand / rightOperand),
            _ => 0
        };
    }

    public Guid GetId()
    {
        return this.id;
    }
    private CalcOperation(){}

    enum OperationType {Add=1, Sub=2, Mul=3, Div=4};
}