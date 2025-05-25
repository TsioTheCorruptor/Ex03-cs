
namespace CostumExceptions
{
    public class ValueRangeException: Exception
    {
        private readonly float m_minValue;
        private readonly float m_maxValue;
        public float MinValue { 
            get
            {
                return m_minValue;
            }
        }
        public float MaxValue
        {
            get
            {
                return m_maxValue;
            }
        }

        public ValueRangeException(string message,float minValue,float maxValue) : base(message)
        {
            m_minValue = minValue;
            m_maxValue = maxValue;
        }

    }
}
