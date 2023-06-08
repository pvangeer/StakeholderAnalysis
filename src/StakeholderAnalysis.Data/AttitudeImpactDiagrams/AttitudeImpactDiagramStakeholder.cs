using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagramStakeholder : NotifyPropertyChangedObservable, IRankedStakeholder
    {
        private double left;
        private double top;

        public AttitudeImpactDiagramStakeholder(Stakeholder stakeholder, double top, double left)
        {
            Stakeholder = stakeholder;
            Left = left;
            Top = top;
        }

        public double Left
        {
            get => left;
            set => left = value;
        }
        
        public double Top
        {
            get => top;
            set => top = ValidateRelativeValue(value);
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