namespace JustFunctional.Core
{
    public class Maths
    {

        /// <summary>
        /// Calcula el Factorial de un número.
        /// </summary>
        /// <param name="Ceiling">Número al que se le va a hallar el factorial.</param>
        /// <returns></returns>
        public static uint Factorial(uint Ceiling)
        {
            uint fact = 1;
            for (uint i = 1; i <= Ceiling; i++)
            {
                fact *= i;
            }
            return fact;
        }
    }
}
