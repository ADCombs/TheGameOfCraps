using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TheGameOfCraps
{
	/// <summary>
	/// The Game of Craps 
	/// Fun Gambling game that happens in real time
	/// Bet against the house and win big!
	/// 
	/// Developer/Designer: Andrew Combs
	/// 
	/// Version: 1.1.0
	/// 
	/// </summary>
	public partial class MainWindow : Window
	{
		private int rolls; // Manages how many rolls player has made
		private ButtonSwitches switches = new ButtonSwitches(); // Manages all button switches for what is on and what is off
		private TheCage Master; // Manages all betting that happens within game
		public MainWindow()
		{
			InitializeComponent();
			rolls = 0;
			btnBetLess.Content = "<<";
			btnBetMore.Content = ">>";
		}

		#region "Menu Options"
		void Start_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetStartVal();
		}
		/** Handles the start of each new game **/
		void Start_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			int number;
			// int.TryParse(txtBankAccount.Text, out number); // Checks if the input coming in is a number or not provided by: https://stackoverflow.com/questions/38831059/check-if-string-is-numeric-in-one-line-of-code
			if (!String.IsNullOrEmpty(txtBankAccount.Text) && int.TryParse(txtBankAccount.Text, out number) && Int32.Parse(txtBankAccount.Text) >= 5 && Int32.Parse(txtBankAccount.Text) <= Int32.MaxValue && Int32.Parse(txtBankAccount.Text) % 5 == 0)
			{
				switches.SetRollSwitch(true);
				switches.SetStartGameSwitch(false);
				var bc = new BrushConverter();
				txtBankAccount.Background = (Brush)bc.ConvertFrom("#eee");
				txtBankAccount.IsEnabled = false;
				switches.SetDecreaseButton(true);
				switches.SetIncreaseButton(true);
				Master = new TheCage(Int32.Parse(txtBankAccount.Text));
			}

			else
				MessageBox.Show("Please input how many chips you are playing with (Whole number only); 5 chips minimum. Must input values of 5");
		}

		void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		/** Resets the entire game **/
		void Reset_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.ResetGame();
		}

		void Exit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		/** Exits the program **/
		void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if(MessageBox.Show("Are you sure you wish to exit?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				this.Close();
		}

		void About_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}
		/** Explaiins what the game is along with version and developer name **/
		void About_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("The Game of Craps: " +
				"\nA program based on the actual dice game! " +
				"\nGo up against the house by making wagers on the outcome of what the face of the dice are." +
				"\nPut in a betting amount and watch your money double or triple!" +
				"\n\nVersion 1.0.0 " +
				"\n.Net Framwork 6.1" +
				"\n32/64 bit" +
				
				"\n\nProgrammed/Developer: Andrew Combs");
		}

		void Rules_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		/** Explains rules of how the game is played **/
		void Rules_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Rules for The Game of Craps:" +
				"\nOn the first roll: " +
				"\nIf the sum is 7 or 11 on the first throw the roller/player wins." +
				"\nIf the sum is 2, 3 or 12 the roller/player loses, that is the house wins." + 
				"\nIf the sum is 4, 5, 6, 8, 9, or 10, that sum becomes the roller/player's \"point\"." +
				"\n\nPoints are awarded as follows: " +
				"\nIf player rolls the \"point\" total before rolling a 7 roller/player wins!" +
				"\nIf rolling a 7 before rolling the point value recieved on the first roll house wins!" +
				"\n\nTo play: Input an amount of chips you wish to play. CHIPS MUST BE IN INCREMENTS OF 5 TO BE ABLE TO PLAY (Minimum of 5 chip table)" +
				"\nOnce Value is inputed select \"play game\"" +
				"\nContinue to roll dice until you game is over. " +
				"\nReplay by pressing or inputting correct Hot Keys for Play Again" +
				"\nReset game will wipe all information out to start a full new game" +
				"\nHave Fun!");
		}

		void HotKeys_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetHotKeys();
		}
		/** Explains Hot Keys layout of the game **/
		void HotKeys_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("HotKeys for game: " +
				"\n\nRoll Dice: Ctrl+B" +
				"\nIncrease betting: Ctrl+N" +
				 "\nDecrease betting: Ctrl+V" +
				 "\nPlay Again button: Ctrl+P" +
				 "\nStart Game: Ctrl+S" +
				 "\nReset Game: Ctrl+U" +
				 "\nAbout: Ctrl+A" +
				 "\nRules: Ctrl+R" +
				 "\nHotKeys: Ctrl+Q");

		}
		#endregion

		#region "Button Mechanics"
		void PlayAgain_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetPlayAgainVal();
		}
		/** Handles playing the game again if the user wishes too **/
		void PlayAgain_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.rolls = 0;
			txtDie1Roll.Text = " ";
			txtDie2Roll.Text = " ";
			txtDieTotal.Text = " ";
			txtPointTotal.Text = " ";
			lblDisplayWinner.Visibility = Visibility.Hidden;
			switches.SetRollSwitch(true);
			switches.SetPlayAgainSwitch(false);
		}

		void Roll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetRollVal();
		}
		/** Handles the rolling of the dice and what mechanics of the game will run **/
		void Roll_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Random Roll = new Random();	
			rolls++;

			int dieOne = Roll.Next(1, 7);
			int dieTwo = Roll.Next(1, 7);
			this.CalcDisplayPoints(dieOne, dieTwo);
			Master.ChipsBet(Int32.Parse(txtBetAmount.Text));

			if (this.IsWinner(dieOne, dieTwo))
			{
				switches.SetRollSwitch(false);
				switches.SetPlayAgainSwitch(true);
			}

			if(Master.BankRupt())
			{
				MessageBox.Show("Bankrupt!", "Game Over");

				if (MessageBox.Show("Play Again?", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					ResetGame();
				}
				else
					this.Close();
			}
		}

		void IncreaseBet_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetIncreasedBet();
		}
		/** Handles increasing the bets made in the game **/
		void IncreaseBet_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if(Master.canAfford(Int32.Parse(txtBetAmount.Text)))
				txtBetAmount.Text = (Int32.Parse(txtBetAmount.Text) + 5).ToString();
			
		}

		void DecreaseBet_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = switches.GetDecreasedBet();
		}
		/** Handles decreasing the bets made in the game **/
		void DecreaseBet_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (Int32.Parse(txtBetAmount.Text) > 0 && Int32.Parse(txtBetAmount.Text) < Int32.MaxValue)
				txtBetAmount.Text = (Int32.Parse(txtBetAmount.Text) - 5).ToString();
		}

		#endregion

		#region "Game Mechanics"
		/** Calculates the winner of the game **/
		public bool IsWinner(int dieOne, int dieTwo)
		{

			int PointsToWin = Int32.Parse(txtPointTotal.Text);

			if (((dieOne + dieTwo) == 7 || (dieOne + dieTwo) == 11) && this.rolls == 1)
			{
				int win = Int32.Parse(txtPlayerWins.Text);
				txtPlayerWins.Text = (win + 1).ToString();
				lblDisplayWinner.Visibility = Visibility.Visible;
				lblDisplayWinner.Content = "WINNER!!!";
				Master.PayOut();
				txtBankAccount.Text = Master.AccountBalance().ToString();
				return true;
			}

			else if ((((dieOne + dieTwo) == 2) || ((dieOne + dieTwo) == 3) || ((dieOne + dieTwo) == 12)) && this.rolls == 1)
			{
				int win = Int32.Parse(txtHouseWins.Text);
				txtHouseWins.Text = (win + 1).ToString();
				lblDisplayWinner.Visibility = Visibility.Visible;
				lblDisplayWinner.Content = "LOSER!!!";
				Master.Collect();
				txtBankAccount.Text = Master.AccountBalance().ToString();
				if (Int32.Parse(txtBetAmount.Text) > Master.AccountBalance())
					txtBetAmount.Text = Master.AccountBalance().ToString();
				return true;
			}

			else if (this.rolls > 1 && (dieOne + dieTwo) == 7)
			{
				int win = Int32.Parse(txtHouseWins.Text);
				txtHouseWins.Text = (win + 1).ToString();
				lblDisplayWinner.Visibility = Visibility.Visible;
				lblDisplayWinner.Content = "LOSER!!!";
				Master.Collect();
				txtBankAccount.Text = Master.AccountBalance().ToString();
				if (Int32.Parse(txtBetAmount.Text) > Master.AccountBalance())
					txtBetAmount.Text = Master.AccountBalance().ToString();
				return true;
			}

			else if (PointsToWin == (dieOne + dieTwo) && this.rolls > 1)
			{
				int win = Int32.Parse(txtPlayerWins.Text);
				txtPlayerWins.Text = (win + 1).ToString();
				lblDisplayWinner.Visibility = Visibility.Visible;
				lblDisplayWinner.Content = "WINNER!!!";
				Master.PayOut();
				txtBankAccount.Text = Master.AccountBalance().ToString();
				return true;
			}

			return false;
		}
		/** Resets the game **/
		public void ResetGame()
		{
			txtDie1Roll.Text = "";
			txtDie2Roll.Text = "";
			txtDieTotal.Text = "";
			txtHouseWins.Text = "0";
			txtPlayerWins.Text = "0";
			txtPointTotal.Text = "";
			txtBankAccount.Text = "";
			txtBetAmount.Text = "0";
			lblDisplayWinner.Visibility = Visibility.Hidden;
			switches.SetPlayAgainSwitch(false);
			switches.SetIncreaseButton(false);
			switches.SetDecreaseButton(false);
			switches.SetRollSwitch(false);
			switches.SetStartGameSwitch(true);
			txtBankAccount.IsEnabled = true;
			txtBankAccount.Background = Brushes.White;
			this.rolls = 0;
			txtBankAccount.Focus();
		}
		/** Calculates The display results **/
		public void CalcDisplayPoints(int dieOne, int dieTwo)
		{

			txtDie1Roll.Text = dieOne.ToString();
			txtDie2Roll.Text = dieTwo.ToString();
			txtDieTotal.Text = (dieOne + dieTwo).ToString();

			if (this.rolls == 1)
				txtPointTotal.Text = (dieOne + dieTwo).ToString();
		}

		#endregion

	}
}
