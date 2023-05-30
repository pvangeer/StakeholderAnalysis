namespace StakeholderAnalysis.Visualization.Behaviors
{
    public interface ISelectionRegister
    {
        bool IsSelectedObject(object o);

        void SelectObject(object o);
    }
}