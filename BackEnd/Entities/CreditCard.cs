namespace BackEnd.Entities
{
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }

        //Create from an existing card. Create the luhn number if it's unknow
        public CreditCard(string pCompleteNumber)
        {
            pCompleteNumber = pCompleteNumber.Replace(" ", "");
            if (pCompleteNumber.Length != 16)
            {
                
                    throw new Exception("CreditCard Number invalide");

            }
            else
            {
                CreditCardNumber = pCompleteNumber.Substring(0, 16);
            }

        }

        //Create a brand new Credit Card number
        public CreditCard()
        {
            int randomLastNumber = new Random().Next(100, 1000);
            CreditCardNumber = "497401850223" + randomLastNumber.ToString() + "0";
            int luhnNumber = 10 - CreateLuhnNumber();
            CreditCardNumber = "497401850223" + randomLastNumber.ToString() + luhnNumber;
            
        }

        //Get the card number in string
        public string getCreditCard()
        {
            return $"{CreditCardNumber.Substring(0, 4)} " +
                   $"{CreditCardNumber.Substring(4, 4)} " +
                   $"{CreditCardNumber.Substring(8, 4)} " +
                   $"{CreditCardNumber.Substring(12, 4)} ";
        }


        //verify the card number by Luhn Algorithme
        public bool LuhnCheck()
        {
            int LuhnNumber = CreateLuhnNumber();

            if (LuhnNumber == 0) return true;
            else return false;
        }

        //Compute the Luhn Number of the actuel credit card number 
        public int CreateLuhnNumber()
        {
            int sum = 0;
            bool doubleDigit = false;

            for (int i = CreditCardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(CreditCardNumber[i].ToString());

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 10)
                    {
                        digit = digit - 10 + 1;
                    }
                }
                sum += digit;
                doubleDigit = !doubleDigit;
            }
            return sum % 10;
        }
    }
}
