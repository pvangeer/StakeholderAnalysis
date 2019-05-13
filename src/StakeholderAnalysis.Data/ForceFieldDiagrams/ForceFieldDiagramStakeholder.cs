using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class ForceFieldDiagramStakeholder : NotifyPropertyChangedObservable
    {
        private double interest;
        private double influence;

        public ForceFieldDiagramStakeholder(Stakeholder stakeholder, double interest, double influence)
        {
            Stakeholder = stakeholder;
            Influence = influence;
            Interest = interest;
        }

        public Stakeholder Stakeholder { get; }

        public double Interest
        {
            get => interest;
            set => interest = ValidateRelativeValue(value);
        }

        public double Influence
        {
            get => influence;
            set => influence = ValidateRelativeValue(value);
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
