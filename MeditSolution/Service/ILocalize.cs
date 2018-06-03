using System;
using System.Globalization;

namespace MeditSolution.Service
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale(CultureInfo ci);
	}
}
