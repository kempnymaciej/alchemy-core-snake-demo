namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// Represents a trigger.
    /// </summary>
    public class SimpleTrigger
    {
        /// <summary>
        /// An event that is triggered when <c>Trigger()</c> is called.
        /// </summary>
        public event System.Action Triggered;

        /// <summary>
        /// Invokes the <c>Triggered</c> event.
        /// </summary>
        public void Trigger() => Triggered?.Invoke();
    } 
}
