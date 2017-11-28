using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGameOfCraps
{

	/** TheCage Class Stores all information on betting that goes on in craps game. And determines what is owed to the player / how much the player needs to pay 
	 * Creator: Andrew Combs
	 * Version 1.5.0**/
    class TheCage
    {
		private int UsersAccount;
		private int Chips;
		public TheCage(int BetAmount)
		{
			UsersAccount = BetAmount;
			Chips = 0;
		}

		public void ChipsBet(int chips)
		{
			this.Chips = chips;
		}

		public int GetChipAmount()
		{
			return this.Chips;
		}

		public bool BankRupt()
		{
			return (UsersAccount) <= 0;
		}

		public void PayOut()
		{
			this.UsersAccount += 2 * Chips;
		}

		public int AccountBalance()
		{
			if (UsersAccount > 0)
				return this.UsersAccount;
			else
				return 0;
		}

		public void Collect()
		{
			UsersAccount -= Chips;
		}

		public bool canAfford(int BetAmount)
		{
			return UsersAccount - BetAmount > 0;
		}
    }
}
