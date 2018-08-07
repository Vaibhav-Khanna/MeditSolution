using System.Collections.Generic;
using MeditSolution.DataStore.Abstraction;
using MeditSolution.Helpers;
using Newtonsoft.Json;

namespace MeditSolution.Models.DataObjects
{
	public class User : BaseDataObject
	{
		public static string Type { get; set; }


		[JsonProperty("firstname")]
		public string Firstname { get; set; }

		[JsonProperty("lastname")]
		public string Lastname { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("currentProgramId")]
		public string CurrentProgramId { get; set; }

		[JsonProperty("currentMeditationId")]
		public string CurrentMeditationId { get; set; }

		[JsonProperty("paidPrograms")]
		public List<PaidProgram> PaidPrograms { get; set; }

		[JsonProperty("meditationsDone")]
		public List<MeditationsDone> MeditationsDone { get; set; }

		[JsonProperty("reminders", Required = Required.Default)]
		public List<Reminder> Reminders { get; set; }

		[JsonProperty("currentSessionMinutesMeditated")]
		public int? CurrentSessionMinutesMeditated { get; set; }

		[JsonProperty("totalMinutesMeditated")]
		public int? TotalMinutesMeditated { get; set; }

		[JsonProperty("maxDaysMeditation")]
		public int? MaxDaysMeditation { get; set; }

		[JsonProperty("lastDayMeditation")]
		public string LastDayMeditation { get; set; }

		[JsonProperty("recordMaxDaysMeditation")]
		public int? RecordMaxDaysMeditation { get; set; }

		[JsonProperty("subscription")]
		public SubscriptionType? Subscription { get; set; }

		[JsonProperty("isExplicitPremium")]
		public bool IsExplicitPremium { get; set; }

		[JsonProperty("explicitSubscriptionInfo")]
		public ExplicitSubscription ExplicitSubscriptionInfo { get; set; }

		[JsonProperty("paidAt")]
		public int? PaidAt { get; set; }

		[JsonProperty("currentLevel")]
		public int CurrentLevel { get; set; }

		[JsonProperty("oneSignalPushId")]
		public string OneSignalPushId { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; } = Settings.Language;

		[JsonProperty("voice")]
		public string Voice { get { return Settings.Voice; } }

		public string CurrentLevelIcon { get; set; }

		public List<string> ProgramsDone { get; set; }
	}

	public class MeditationsDone
	{
		public string id { get; set; }
		public bool level1Done { get; set; }
		public int doneLevel1At { get; set; }
		public bool level2Done { get; set; }
		public int doneLevel2At { get; set; }
		public bool level3Done { get; set; }
		public int doneLevel3At { get; set; }
		public bool level4Done { get; set; }
		public int doneLevel4At { get; set; }
	}

	public class Reminder
	{
		public int id { get; set; }
		public string reminderTime { get; set; }
		public bool isEnabled { get; set; }
		public bool isMondayEnabled { get; set; }
		public bool isTuesdayEnabled { get; set; }
		public bool isWednesdayEnabled { get; set; }
		public bool isThursdayEnabled { get; set; }
		public bool isFridayEnabled { get; set; }
		public bool isSaturdayEnabled { get; set; }
		public bool isSundayEnabled { get; set; }
	}

}