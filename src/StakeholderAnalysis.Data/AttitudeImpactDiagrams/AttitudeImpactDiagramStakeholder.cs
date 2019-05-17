using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagramStakeholder : NotifyPropertyChangedObservable
    {
        private double attitude;
        private double impact;

        public AttitudeImpactDiagramStakeholder(Stakeholder stakeholder, double attitude, double impact)
        {
            Stakeholder = stakeholder;
            Impact = impact;
            Attitude = attitude;
        }

        public Stakeholder Stakeholder { get; }

        public double Attitude
        {
            get => attitude;
            set => attitude = ValidateRelativeValue(value);
        }

        public double Impact
        {
            get => impact;
            set => impact = ValidateRelativeValue(value);
        }

        private double ValidateRelativeValue(double value)
        {
            if (double.IsNaN(value))
            {
                throw new NaNValueException();
            }

            if (value < 0 || value > 1.0)
            {
                throw new ValueOutOfBoundsException();
            }

            return value;
        }
    }
}
