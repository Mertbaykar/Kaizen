using Code.Generator;

for (int i = 0; i < 10; i++)
{
	try
	{
        string password = CodeGenerator.GenerateRandomPassword();
        Console.WriteLine(password);
        //Console.ReadKey();
    }
	catch (Exception ex)
	{

		throw;
	}
   
}