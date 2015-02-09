public class Program
    {
    static OutputPort data = new OutputPort(Pins.GPIO_PIN_D12, false);
    static OutputPort clock = new OutputPort(Pins.GPIO_PIN_D11, false);
    static OutputPort latch = new OutputPort(Pins.GPIO_PIN_D13, false);
    
    public static void Main()
    {
        int pins = 128;
        Thread.Sleep(500);
        bool back = false;
        while (true)
        {
            latch.Write(false);
            // Lower Clock
            clock.Write(false);
            string bit = DecimalToBinary(pins);
            for (var i = 16;i >= 0; i--)
            {
                if (bit != null && i < bit.Length)
                 {                         
                     bool pinStatus = (bit[i] == '1' ? true : false);                         
                     SetCurrentPin(pinStatus);                         
                     NextPin();                     
                 }
                 else                     
                 {                         
                     SetCurrentPin(false);                         
                     NextPin();                     
                 }                    
           }                 
           latch.Write(true);                 
           if (pins == 128)                     
                 pins = 0;                 
           else                     
                 pins = 128;                 
           Thread.Sleep(1000);             
     }         
}          

private static string DecimalToBinary(int input)         
{             
    int decimalNumber = input;             
    int remainder;             
    string result = string.Empty;             
    while (decimalNumber > 0)
    {
        remainder = decimalNumber % 2;
        decimalNumber /= 2;
        result = remainder.ToString() + result;
    }

    return result.ToString();
}

private static void NextPin()
{
    clock.Write(true);
    clock.Write(false);
}

private static void SetCurrentPin(bool pin)
{
    data.Write(pin);
}
