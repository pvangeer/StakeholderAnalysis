using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class ForceFieldDiagramStakeholder : NotifyPropertyChangedObservable, IRankedStakeholder
    {
        private double influence;
        private double interest;

        public ForceFieldDiagramStakeholder(Stakeholder stakeholder, double interest, double influence)
        {
            Stakeholder = stakeholder;
            Influence = influence;
            Interest = interest;
        }

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