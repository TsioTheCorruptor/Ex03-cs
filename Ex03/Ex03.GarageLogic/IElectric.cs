
namespace Ex03.GarageLogic.Electric
{
    public interface IElectric
    {
        public float GetMaxCharge();
        public float GetRemaningCharge();
        public void Charge(float i_time);

    }
}
