using System;
using System.Collections.Generic;

public class Program
{
	static List<byte> stack = new List<byte>();	
	static string buffer = "";
	
	public static void Main()
	{
		string test = "Grisar flyger över borås";
		
		var tempBuff = System.Text.Encoding.UTF8.GetBytes (test);
		
		foreach(var t in tempBuff)
		{
			Console.WriteLine("S: " + Convert.ToString(t,2));			
			foreach(var b in Convert.ToString(t,2).PadLeft(8,'0'))
			{	
				Reciever(b);				
			}
			
		}	
		
		
		Writer();
	}
	
	public static void Reciever(char i)
	{
		if(buffer.Length < 8)
		{			
			buffer += i;						
		}
		else
		{	
			Console.WriteLine("R: " + buffer);
			stack.Add(Convert.ToByte(buffer,2));
			
			buffer = i + "";
		}			
		
	}
	
	public static void Writer()
	{
		if(buffer.Length > 0)
		{
			Console.WriteLine("R: " + buffer);
			stack.Add(Convert.ToByte(buffer,2));
		}
		
		var message = System.Text.Encoding.UTF8.GetChars(stack.ToArray());
		Console.WriteLine(message);
	}
}
