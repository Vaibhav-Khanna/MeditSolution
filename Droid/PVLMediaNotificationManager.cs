using System;
using System.Diagnostics;
using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V4.Media;
using Android.Support.V4.Media.Session;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using static Android.Support.V4.Media.App.NotificationCompat;

namespace MeditSolution.Droid
{
    /// <summary>
    /// PVL Media notification manager.
    /// </summary>
    public class PVLMediaNotificationManager : IAndroidMediaNotificationManager
    {
        public IMediaQueue MediaQueue { get; set; }
        public MediaSessionCompat.Token SessionToken { get; set; }
        public MediaSessionCompat Session { get; set; }
        internal const int _notificationId = 1;
        static String CHANNEL_ID = "ponyville_live_channel";

        Intent _intent;

        //PendingIntent _pendingCancelIntent;
        PendingIntent _pendingIntent;
        NotificationCompat.Style _notificationStyle = new MediaStyle();
        Context _applicationContext;
        NotificationCompat.Builder _builder;

        public PVLMediaNotificationManager(Context applicationContext, Type serviceType)
        {
            _applicationContext = applicationContext;
            _intent = new Intent(_applicationContext, serviceType);

            var mainActivity =
                _applicationContext.PackageManager.GetLaunchIntentForPackage(_applicationContext.PackageName);

            _pendingIntent = PendingIntent.GetActivity(_applicationContext, 0, mainActivity,
                PendingIntentFlags.UpdateCurrent);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                createChannel();
        }

        void createChannel()
        {
            NotificationManager notificationManager = (NotificationManager)_applicationContext.GetSystemService(Context.NotificationService);

            // The id of the channel.
            String id = CHANNEL_ID;
            // The user-visible name of the channel.
            Java.Lang.ICharSequence name = new Java.Lang.String(id);
            // The user-visible description of the channel.
            String description = "Media playback controls";
            //var importance = NotificationPriority.Max;
            var importance = NotificationImportance.Max;
            NotificationChannel channel = new NotificationChannel(id, name, importance);
            // Configure the notification channel.
            channel.SetShowBadge(false);
            channel.LockscreenVisibility = NotificationVisibility.Public;
            channel.Description = description;
            notificationManager.CreateNotificationChannel(channel);
        }

        /// <summary>
        /// Starts the notification.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        public void StartNotification(IMediaFile mediaFile)
        {
            StartNotification(mediaFile, true, false);
        }

        /// <summary>
        /// When we start on the foreground we will present a notification to the user
        /// When they press the notification it will take them to the main page so they can control the music
        /// </summary>
        public void StartNotification(IMediaFile mediaFile, bool mediaIsPlaying, bool canBeRemoved)
        {
            var icon = _applicationContext.ApplicationInfo.Icon;

            _builder = new NotificationCompat.Builder(_applicationContext, CHANNEL_ID);

            SetMetadata(mediaFile);
            AddActionButtons(mediaIsPlaying);

            var _style = new MediaStyle();
            _style.SetMediaSession(SessionToken);

            if (_builder.MActions.Count >= 3)
                _style.SetShowActionsInCompactView(0, 1, 2);
            if (_builder.MActions.Count == 2)
                _style.SetShowActionsInCompactView(0, 1);
            if (_builder.MActions.Count == 1)
                _style.SetShowActionsInCompactView(0);

            _builder.SetStyle(_style);

            _builder.SetSmallIcon(icon);
            _builder.SetContentIntent(_pendingIntent);
            _builder.SetOngoing(mediaIsPlaying);
            _builder.SetColorized(true);
            _builder.SetVisibility(1);

            NotificationManagerCompat.From(_applicationContext)
                .Notify(_notificationId, _builder.Build());
        }

        public void StopNotifications()
        {
            NotificationManagerCompat nm = NotificationManagerCompat.From(_applicationContext);
            nm.Cancel(_notificationId);
        }

        public void UpdateNotifications(IMediaFile mediaFile, MediaPlayerStatus status)
        {
            try
            {
                var isPlaying = status == MediaPlayerStatus.Playing || status == MediaPlayerStatus.Buffering;
                var isPersistent = status == MediaPlayerStatus.Playing || status == MediaPlayerStatus.Buffering || status == MediaPlayerStatus.Paused;
                var nm = NotificationManagerCompat.From(_applicationContext);
                if (nm != null && _builder != null)
                {
                    SetMetadata(mediaFile);
                    AddActionButtons(isPlaying);
                    _builder.SetOngoing(isPersistent);
                    nm.Notify(_notificationId, _builder.Build());
                }
                else
                {
                    StartNotification(mediaFile, isPlaying, false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                StopNotifications();
            }
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }


        private void SetMetadata(IMediaFile mediaFile)
        {
            _builder.SetContentTitle(mediaFile?.Metadata?.Title ?? string.Empty);
            _builder.SetContentText(mediaFile?.Metadata?.Artist ?? string.Empty);
            _builder.SetContentInfo(mediaFile?.Metadata?.Album ?? string.Empty);

            if (mediaFile.Metadata.AlbumArtUri != null)
            {
                try
                {
                    var icon = GetImageBitmapFromUrl(mediaFile.Metadata.AlbumArtUri);
                    _builder.SetLargeIcon(icon);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Failed to download: " + mediaFile.Metadata.AlbumArtUri);
                    Debug.WriteLine(e);
                }
            }
            else
            {
                _builder.SetLargeIcon(mediaFile?.Metadata?.Art as Bitmap ?? mediaFile?.Metadata?.AlbumArt as Bitmap ?? null);
            }
        }

        private NotificationCompat.Action GenerateActionCompat(int icon, string title, string intentAction)
        {
            _intent.SetAction(intentAction);

            PendingIntentFlags flags = PendingIntentFlags.UpdateCurrent;
            if (intentAction.Equals(MediaServiceBase.ActionStop))
                flags = PendingIntentFlags.CancelCurrent;

            PendingIntent pendingIntent = PendingIntent.GetService(_applicationContext, 1, _intent, flags);

            return new NotificationCompat.Action.Builder(icon, title, pendingIntent).Build();
        }

        private void AddActionButtons(bool mediaIsPlaying)
        {
            _builder.MActions.Clear();

            Console.WriteLine("Adding action button: " + mediaIsPlaying.ToString());

            _builder.AddAction(mediaIsPlaying
                               ? GenerateActionCompat(Android.Resource.Drawable.IcMediaPause, "Pause", MediaServiceBase.ActionPause)
                               : GenerateActionCompat(Android.Resource.Drawable.IcMediaPlay, "Play", MediaServiceBase.ActionPlay));
        }

        public void UpdateMetadata(IMediaFile currentTrack)
        {
            MediaMetadataCompat.Builder builder = new MediaMetadataCompat.Builder();

            if (currentTrack != null)
            {
                builder
                    .PutString(MediaMetadata.MetadataKeyAlbum, currentTrack.Metadata.Album)
                    .PutString(MediaMetadata.MetadataKeyArtist, currentTrack.Metadata.Artist)
                    .PutString(MediaMetadata.MetadataKeyTitle, currentTrack.Metadata.Title);
            }
            else
            {
                builder
                    .PutString(MediaMetadata.MetadataKeyAlbum,
                               Session?.Controller?.Metadata?.GetString(MediaMetadata.MetadataKeyAlbum))
                    .PutString(MediaMetadata.MetadataKeyArtist,
                               Session?.Controller?.Metadata?.GetString(MediaMetadata.MetadataKeyArtist))
                    .PutString(MediaMetadata.MetadataKeyTitle,
                               Session?.Controller?.Metadata?.GetString(MediaMetadata.MetadataKeyTitle));
            }

            var resources = _applicationContext.Resources;
            //TODO add option to use either PVL, Poniverse, Station or Album art to Options (and here)
            //builder.PutBitmap(MediaMetadata.MetadataKeyAlbumArt, currentTrack?.Metadata.AlbumArt as Bitmap);

            // Uncomment these lines and replace the "splash" with your own drawable resource to use a lock screen pic
            //var id = Resource.Drawable.splash;
            //var art = BitmapFactory.DecodeResource(resources, id);
            //builder.PutBitmap(MediaMetadata.MetadataKeyAlbumArt, art);

            Session?.SetMetadata(builder.Build());
        }
    }
}
