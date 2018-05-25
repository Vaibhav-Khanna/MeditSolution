using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MeditSolution.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

        #region Setting Constants

	    private static readonly string stringDefault = string.Empty;
	    private static readonly int intDefault = 0;
	    public static readonly bool booleanDefault = false;

	    private const string IsLoggedInKey = "IsLoggedIn";
	    private const string TokenKey = "Token";
	    private const string TokenExpirationKey = "TokenExpiration";
	    private const string UserKey = "User";
	    private const string LanguageKey = "Language";
	    private const string VoiceKey = "Voice";
        private const string ReminderUniqueKey = "ReminderUniqueKey";
		private const string ReminderStorageKey = "ReminderStorageKey";
	    private const string MeditationProgressKey = "MeditationProgress";

        #endregion

        public static bool IsLoggedIn
	    {
	        get { return AppSettings.GetValueOrDefault(IsLoggedInKey, booleanDefault); }
	        set { AppSettings.AddOrUpdateValue(IsLoggedInKey, value); }
	    }

	    public static string Token
        {
	        get { return AppSettings.GetValueOrDefault(TokenKey, stringDefault); }
	        set { AppSettings.AddOrUpdateValue(TokenKey, value); }
	    }

	    public static int TokenExpiration
        {
	        get { return AppSettings.GetValueOrDefault(TokenExpirationKey, intDefault); }
	        set { AppSettings.AddOrUpdateValue(TokenExpirationKey, value); }
	    }

	    public static string User
	    {
	        get { return AppSettings.GetValueOrDefault(UserKey, stringDefault); }
	        set { AppSettings.AddOrUpdateValue(UserKey, value); }
	    }

	    public static string Language
        {
	        get { return AppSettings.GetValueOrDefault(LanguageKey, stringDefault); }
	        set { AppSettings.AddOrUpdateValue(LanguageKey, value); }
	    }

	    public static string Voice
        {
	        get { return AppSettings.GetValueOrDefault(VoiceKey, stringDefault); }
	        set { AppSettings.AddOrUpdateValue(VoiceKey, value); }
	    }

	    public static string MeditationProgress
        {
	        get { return AppSettings.GetValueOrDefault(MeditationProgressKey, stringDefault); }
	        set { AppSettings.AddOrUpdateValue(MeditationProgressKey, value); }
	    }

		public static int ReminderUniqueID
		{
			get { return AppSettings.GetValueOrDefault(ReminderUniqueKey, intDefault); }
			set { AppSettings.AddOrUpdateValue(ReminderUniqueKey, value); }
		}

		public static string ReminderStorage
		{
            get { return AppSettings.GetValueOrDefault(ReminderStorageKey, stringDefault); }
			set { AppSettings.AddOrUpdateValue(ReminderStorageKey, value); }
		}
    }
}
