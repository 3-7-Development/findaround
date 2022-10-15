using System;
namespace findaround.Helpers
{
	public class KeyboardHelpers : TriggerAction<Entry>
	{
        protected override void Invoke(Entry sender)
        {
            sender.Unfocus();
        }
    }
}

