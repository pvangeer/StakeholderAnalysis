namespace StakeholderAnalysis.Visualization.Behaviors
{
    public interface ISelectionRegister
    {
        bool IsSelected(object o);

        void Select(object o);
    }
}
