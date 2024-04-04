
class CalcOperation
{
    double leftOperand;
    double rightOperand;
    OperationType operationType;

    // Received String must be on this formate : Op Num1 Num2
    static public CalcOperation FromString(string operationAsString)
    {
        // ADD 32930.121 1231.01 (example)
        
        var parts = operationAsString.Split(" ");

        return new CalcOperation
        {   
            leftOperand = double.Parse(parts[1]),
            rightOperand = double.Parse(parts[2]),
            operationType = parts[0] switch
            {
                "ADD" => OperationType.Add,
                "SUB" => OperationType.Sub,
                "MUL" => OperationType.Mul,
                "DIV" => OperationType.Div
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
        };
    }
    private CalcOperation(){}

    enum OperationType {Add=1, Sub=2, Mul=3, Div=4};
}