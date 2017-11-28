using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGameOfCraps
{
	/** ButtonSwitches is a class to hold onto information to be able to disable/enable button and menu mechanics when needed. 
	 * Creator: Andrew Combs
	 * Version 1.3.0 **/
    class ButtonSwitches
    {
		private bool PlayAgain;
		private bool Roll;
		private bool StartGame;
		private bool IncreaseBet;
		private bool DecreaseBet;
		private bool HotKeys;
		public ButtonSwitches()
		{
			PlayAgain = false;
			Roll = false;
			StartGame = true;
			IncreaseBet = false;
			DecreaseBet = false;
			HotKeys = true;
		}

		#region "Set Values"
		public void SetRollSwitch(bool value)
		{
			this.Roll = value;
		}

		public void SetPlayAgainSwitch(bool value)
		{
			this.PlayAgain = value;
		}

		public void SetStartGameSwitch(bool value)
		{
			this.StartGame = value;
		}

		public void SetIncreaseButton(bool value)
		{
			this.IncreaseBet = value;
		}

		public void SetDecreaseButton(bool value)
		{
			this.DecreaseBet = value;
		}

		public void SetHotKeys(bool value)
		{
			this.HotKeys = value;
		}
		#endregion

		#region "Get Values"
		public bool GetRollVal()
		{
			return Roll;
		}

		public bool GetPlayAgainVal()
		{
			return PlayAgain;
		}

		public bool GetStartVal()
		{
			return StartGame;
		}

		public bool GetIncreasedBet()
		{
			return IncreaseBet;
		}

		public bool GetDecreasedBet()
		{
			return DecreaseBet;
		}

		public bool GetHotKeys()
		{
			return HotKeys;
		}
#endregion

	}
}
