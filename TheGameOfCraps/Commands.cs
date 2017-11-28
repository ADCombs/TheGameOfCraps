
using System.Windows.Input;

namespace TheGameOfCraps

{
	public static class Commands
	{
		// Reroute Commands that are implemented by the ICommand. That allows us to point between the top layer and bottom layer of WPFs treelike Structure
		// Definictions of routed commands along with how to use routed commands https://joshsmithonwpf.wordpress.com/2008/03/18/understanding-routed-commands/
		public static readonly RoutedCommand Exit = new RoutedCommand();
		public static readonly RoutedCommand HotKeys = new RoutedCommand();
		public static readonly RoutedCommand Start = new RoutedCommand();
		public static readonly RoutedCommand Reset = new RoutedCommand();
		public static readonly RoutedCommand About = new RoutedCommand();
		public static readonly RoutedCommand Rules = new RoutedCommand();
		public static readonly RoutedCommand Roll = new RoutedCommand();
		public static readonly RoutedCommand PlayAgain = new RoutedCommand();
		public static readonly RoutedCommand DecreaseBet = new RoutedCommand();
		public static readonly RoutedCommand IncreaseBet = new RoutedCommand();

	}
}
