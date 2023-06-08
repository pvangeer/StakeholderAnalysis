using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class PositionedStakeholder : NotifyPropertyChangedObservable, IRankedStakeholder
    {
        private double top;
        private double left;

        public PositionedStakeholder(Stakeholder stakeholder, double left, double top)
        {
            Stakeholder = stakeholder;
            this.top = top;
            this.left = left;
        }

        public double Left
        {
            get => left;
            set => left = ValidateRelativeValue(value);
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