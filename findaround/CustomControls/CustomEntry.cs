

namespace MAUI_Custom_Controls.CustomControls;

public class CustomEntry : Entry
{
	public CustomEntry()
	{
		BackgroundColor = Color.FromHex("#333333");
		TextColor = Colors.White;
		FontSize = 20;
		//CharacterSpacing = 20;
		FontFamily = "Roboto";
		CursorPosition = 1;
		Keyboard = Keyboard.Default;
		HorizontalTextAlignment = TextAlignment.Start;
		ReturnType = ReturnType.Default;
		MaxLength = 24;

		TextChanged += (s, e) =>
		{
#if __ANDROID__
			//this.TextColor = Colors.White;
			//this.BackgroundColor = Colors.Red;

#elif WINDOWS8_0_OR_GREATER

			this.BackgroundColor = Colors.Pink;

#endif
		};

		Completed += (s, e) =>
		{
			this.FontSize = 20;

#if __ANDROID__
			//this.TextColor = Colors.Green;
#endif
		};

		ReturnCommand = new Command(() =>
		{
            this.FontSize = 20;

#if __ANDROID__
            //this.TextColor = Colors.Green;
#endif
        });

		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntry), (handler, view) =>
		{
			if (view is CustomEntry)
			{
#if __ANDROID__
				//handler.PlatformView.SetSelectAllOnFocus(true);
#endif
			}
		});
	}
}