namespace StakeholderAnalysis.Visualization
{
    public interface ISelectionRegister
    {
        bool IsSelected(object o);

        void Select(object o);
    }
}
