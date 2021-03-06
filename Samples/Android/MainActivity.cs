﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

using Android.Content;

using Com.Microblink.Entities.Recognizers.Blinkid.Mrtd;
using Com.Microblink.Entities.Recognizers.Blinkid.Eudl;
using Com.Microblink.Entities.Recognizers.Blinkbarcode.Usdl;
using Com.Microblink.Entities.Recognizers;
using Com.Microblink.Util;
using Com.Microblink;
using Com.Microblink.Intent;
using Com.Microblink.Uisettings;
using Android.Runtime;

namespace Android
{
	[Activity (Label = "BlinkID Xamarin", MainLauncher = true, Icon = "@mipmap/icon", HardwareAccelerated = true)]
	public class MainActivity : Activity
	{
        const int ACTIVITY_REQUEST_ID = 101;

        // MrtdRecognizer is used for scanning Machine Readable Travel Documents
        MrtdRecognizer mrtdRecognizer;

        // EudlRecognizer is used for scanning EU Driver's licenses
        EudlRecognizer eudlRecognizer;

        // UsdlRecognizer is used for scanning PDF417 barcode on back side of
        // US Driver's licenses
        UsdlRecognizer usdlRecognizer;

        // there are plenty of recognizers available - see Android documentation
        // for more information: https://github.com/BlinkID/blinkid-android/blob/master/README.md

        // RecognizerBundle is required for transferring recognizers via Intent to another activity
        // and for loading results from them back.
        RecognizerBundle recognizerBundle;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			RequestedOrientation = ScreenOrientation.Portrait;

			Button button = FindViewById<Button> (Resource.Id.startScanningButton);

			// Setup BlinkID before usage
			initBlinkId ();

            // check if BlinkID is supported on current device. Device needs to have camera with autofocus.
            if (RecognizerCompatibility.GetRecognizerCompatibilityStatus(this) != RecognizerCompatibilityStatus.RecognizerSupported) {
				button.Enabled = false;
				Toast.MakeText (this, "BlinkID is not supported!", ToastLength.Long).Show ();
			} else {
				button.Click += delegate {
                    // create a settings object for activity that will be used. For ID it's best to
                    // use DocumentUISettings. There are also other UI settings available - check Android documentation
                    var documentUISettings = new DocumentUISettings(recognizerBundle);

                    // start activity associated with given UI settings. After scanning completes,
                    // OnActivityResult will be invoked
                    ActivityRunner.StartActivityForResult(this, ACTIVITY_REQUEST_ID, documentUISettings);
				};
			}
		}

		private void initBlinkId ()
		{
            // set license key for Android with package name com.microblink.xamarin.blinkid
            MicroblinkSDK.SetLicenseKey("sRwAAAAeY29tLm1pY3JvYmxpbmsueGFtYXJpbi5ibGlua2lke7qv4mAhH4ywlU+/YDeeF3lzxt4df8Bdwz8iujH8xSkqiErF84W+CdB8oESylJFsxW8IfgpH4WpfP5Dz7oZVr17d9wI1VzsXIyUCe696qZt2yZIJC1a4MoLOlAK/PxFm9zsJPRIA4v/kP9mxIZhAzAZ0aRWfXncqnaXgQIqUmoeFqRwp2CwRTdM4rdoWV32bUQQcEgKu9J+24I7m1G/w8wj5upNZmtYtrmjDbtK0Ls7975kfaqX+QBhz62yB", this);

            // Since we plan to transfer large data between activities, we need to enable
            // PersistedOptimised intent data transfer mode.
            // for more information about transfer mode, check android documentation: https://github.com/blinkid/blinkid-android#-passing-recognizer-objects-between-activities
            MicroblinkSDK.IntentDataTransferMode = IntentDataTransferMode.PersistedOptimised;

            // create recognizers and bundle them into RecognizerBundle
            mrtdRecognizer = new MrtdRecognizer();
            mrtdRecognizer.SetReturnFullDocumentImage(true);
            eudlRecognizer = new EudlRecognizer();
            eudlRecognizer.SetReturnFullDocumentImage(true);
            usdlRecognizer = new UsdlRecognizer();

            recognizerBundle = new RecognizerBundle(mrtdRecognizer, eudlRecognizer, usdlRecognizer);
		}

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == ACTIVITY_REQUEST_ID && resultCode == Result.Ok) {

                // obtain image view that will display image of the document
                ImageView documentImageView = this.FindViewById<ImageView>(Resource.Id.documentImageView);

                // unfortunately, C# does not support covariant return types, so binding
                // of AAR loses the return type of the Java's GetResult method. Therefore, a cast is required.
                // This is always a safe cast, since the original object in Java is of correct type - type
                // information was lost during conversion to C# due to https://github.com/xamarin/java.interop/pull/216
                var mrtdResult = (MrtdRecognizer.Result)mrtdRecognizer.GetResult();
                var eudlResult = (EudlRecognizer.Result)eudlRecognizer.GetResult();
                var usdlResult = (UsdlRecognizer.Result)usdlRecognizer.GetResult();

                var message = "";

                // we can check ResultState property of the Result to see if the result contains scanned information
                if (mrtdResult.ResultState == Recognizer.Result.State.Valid) {
                    message += "MRTD recognizer result:\n" +
                        "PrimaryID: " + mrtdResult.MrzResult.PrimaryId + "\n" +
                         "SecondaryID: " + mrtdResult.MrzResult.SecondaryId + "\n" +
                         "Date of birth: " + mrtdResult.MrzResult.DateOfBirth.Date.Day + "." +
                                             mrtdResult.MrzResult.DateOfBirth.Date.Month + "." +
                                             mrtdResult.MrzResult.DateOfBirth.Date.Year + ".\n";

                    // image is now part of the result - no need for having separate image listener
                    documentImageView.SetImageBitmap(mrtdResult.FullDocumentImage.ConvertToBitmap());
                }
                if (eudlResult.ResultState == Recognizer.Result.State.Valid) {
                    message += "EUDL recognizer result:\n" +
                       "First name: " + eudlResult.FirstName + "\n" +
                       "Last name: " + eudlResult.LastName + "\n" +
                       "Birth data: " + eudlResult.BirthData + "\n" +
                       "Country: " + eudlResult.Country.ToString() + "\n";
                    documentImageView.SetImageBitmap(eudlResult.FullDocumentImage.ConvertToBitmap());
                }
                if (usdlResult.ResultState == Recognizer.Result.State.Valid) {
                    message += "USDL recognizer result:\n" +
                           "First name: " + usdlResult.FirstName + "\n" +
                           "Last name: " + usdlResult.LastName + "\n" +
                           "Address: " + usdlResult.Address + "\n";
                }

                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("BlinkID Results");
                alert.SetPositiveButton("OK", (senderAlert, args) => { });
                alert.SetMessage(message);
                alert.Show();
            }
        }
	}
}


