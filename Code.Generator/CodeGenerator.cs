namespace Code.Generator
{
    public static class CodeGenerator
    {
        private const string LETTERS = "ACDEFGHKLMNPRTXYZ";
        private const string NUMERIC_CHARACTERS = "234579";
        private const int CHARACTER_COUNT = 8;
        private const string WOWEL_CHARACTERS = "AE";
        private const string SILENT_CHARACTERS = "CDFGHKLMNPRTXYZ";
        private static string AllCharacters => new string(LETTERS.Union(NUMERIC_CHARACTERS).ToArray());
        private static readonly Random Random;
        static CodeGenerator()
        {
            Random = new Random();
        }

        /// <summary>
        /// En az bir sessiz, bir sesli harf ve bir sayıdan oluşan bir kod oluşturur. Kod içerisinde sessiz ile sesli harflerin miktarlarına göre onları hece oluşturacak şekilde ayarlar.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomPassword()
        {
            try
            {
                int passwordLength = CHARACTER_COUNT;
                string code = "";

                #region Fill Password

                if (!HasWovelCharacter(code))
                {
                    var newChar = GenerateWovelCharacter();
                    code += newChar;
                }
                if (!HasSilentCharacter(code))
                {
                    var newChar = GenerateSilentCharacter();
                    code += newChar;
                }
                if (!HasNumericCharacter(code))
                {
                    var newChar = GenerateNumericCharacter();
                    code += newChar;
                }

                int characterCount = passwordLength - code.Length;

                for (int i = 0; i < characterCount; i++)
                {
                    var newChar = GenerateRandomCharacter();
                    code += newChar;
                }
                #endregion

                MixUpPassword(ref code);
                return code;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        #region Control Methods
        private static bool HasNumericCharacter(string code)
        {
            return code.Any(x => char.IsDigit(x));
        }
        private static bool HasWovelCharacter(string code)
        {
            return code.Any(x => WOWEL_CHARACTERS.Contains(x));
        }
        private static bool HasSilentCharacter(string code)
        {
            return code.Any(x => SILENT_CHARACTERS.Contains(x));
        }
        #endregion

        #region Generators
        private static char GenerateNumericCharacter()
        {
            return NUMERIC_CHARACTERS[Random.Next(NUMERIC_CHARACTERS.Length)];
        }
        private static char GenerateWovelCharacter()
        {
            return WOWEL_CHARACTERS[Random.Next(WOWEL_CHARACTERS.Length)];
        }
        private static char GenerateSilentCharacter()
        {
            return SILENT_CHARACTERS[Random.Next(SILENT_CHARACTERS.Length)];
        }
        private static char GenerateRandomCharacter()
        {
            return AllCharacters[Random.Next(AllCharacters.Length)];
        }
        #endregion

        /// <summary>
        /// Şifreyi sessiz ile sesli harflerin miktarlarına göre heceler haline getirir, sayılar ve en son da sessiz veya sesli harflerden geriye kalan harfler gelecek şekilde ayarlar
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static void MixUpPassword(ref string code)
        {
            string finalPassword = "";
            List<char> unusedLetters = new List<char>();

            #region Bir sessiz bir sesli harf eklenir ve kullanılmayan harfler bulunur
            var silentLetters = code.Where(x => SILENT_CHARACTERS.Contains(x)).ToList();
            var wowelLetters = code.Where(x => WOWEL_CHARACTERS.Contains(x)).ToList();
            var allLetters = silentLetters.Concat(wowelLetters).ToList();

            int silentCharactersCount = silentLetters.Count();
            int wowelCharactersCount = wowelLetters.Count();

            int syllableCount = silentCharactersCount > wowelCharactersCount ? wowelCharactersCount : silentCharactersCount;

            List<char> usedLetters = new List<char>();

            for (int i = 0; i < syllableCount; i++)
            {
                finalPassword += silentLetters[i];
                finalPassword += wowelLetters[i];
                usedLetters.Add(silentLetters[i]);
                usedLetters.Add(wowelLetters[i]);
            }
            unusedLetters = allLetters.GetUnusedLetters(usedLetters);
            #endregion

            #region Sayılar eklenir
            var numbers = code.Where(x => NUMERIC_CHARACTERS.Contains(x)).ToList();
            for (int i = 0; i < numbers.Count(); i++)
            {
                finalPassword += numbers[i];
            }
            #endregion

            #region Kullanılmamış harfler eklenir
            for (int i = 0; i < unusedLetters.Count(); i++)
            {
                finalPassword += unusedLetters[i];
            }
            #endregion

            code = finalPassword;

        }

        /// <summary>
        /// Kullanılmamış olan harfleri döner
        /// </summary>
        /// <param name="allLetters"></param>
        /// <param name="usedLetters"></param>
        /// <returns></returns>
        public static List<char> GetUnusedLetters(this List<char> allLetters, List<char> usedLetters)
        {
            foreach (var usedLetter in usedLetters)
            {
                allLetters.Remove(usedLetter);
            }

            return allLetters;
        }

    }
}