using StakeholderAnalysis.Data.Exceptions;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class OnionDiagramStakeholder : NotifyPropertyChangedObservable, IRankedStakeholder
    {
        private double left;
        private double top;

        public OnionDiagramStakeholder(Stakeholder stakeholder, double left, double top)
        {
            Stakeholder = stakeholder;
            Top = top;
            Left = left;
        }

        public Stakeholder Stakeholder { get; }

        public int Rank { get; set; }

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