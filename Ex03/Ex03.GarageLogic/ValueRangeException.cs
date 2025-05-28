
namespace CostumExceptions
{
    public class ValueRangeException: Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;
        public float MinValue { 
            get
            {
                return r_MinValue;
            }
        }
        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public ValueRangeException(string message,float minValue,float maxValue) : base(message)
        {
            r_MinValue = minValue;
            r_MaxValue = maxValue;
        }

    }
}
