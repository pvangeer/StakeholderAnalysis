using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagramStakeholder : NotifyPropertyChangedObservable, IRankedStakeholder
    {
        private double attitude;
        private double impact;

        public AttitudeImpactDiagramStakeholder(Stakeholder stakeholder, double attitude, double impact)
        {
            Stakeholder = stakeholder;
            Impact = impact;
            Attitude = attitude;
        }

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

        public Stakeholder Stakeholder { get; }

        public int Rank { get; set; }

        private double ValidateRelativeValue(double value)
        {
            if (double.IsNaN(value)) throw new NaNValueException();

            if (value < 0 || value > 1.0) throw new ValueOutOfBoundsException();

            return value;
        }
    }
}