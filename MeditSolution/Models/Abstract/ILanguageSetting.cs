using System;
using System.ComponentModel;

namespace MeditSolution.Models.Abstract
{
	public interface ILanguageSetting : INotifyPropertyChanged
	{
		bool? DefaultLanguageEnglish { get; set; }

		bool? DefaultGenderMan { get; set; }

		bool IsPresenterPage { get; set; }
	}
}
