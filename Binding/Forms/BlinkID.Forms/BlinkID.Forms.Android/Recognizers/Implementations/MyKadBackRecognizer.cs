﻿using Microblink.Forms.Droid.Recognizers;
using Microblink.Forms.Core.Recognizers;

[assembly: Xamarin.Forms.Dependency(typeof(MyKadBackRecognizer))]
namespace Microblink.Forms.Droid.Recognizers
{
    public sealed class MyKadBackRecognizer : Recognizer, IMyKadBackRecognizer
    {
        Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer nativeRecognizer;

        MyKadBackRecognizerResult result;

        public MyKadBackRecognizer() : base(new Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer())
        {
            nativeRecognizer = NativeRecognizer as Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer;
            result = new MyKadBackRecognizerResult(nativeRecognizer.GetResult() as Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer.Result);
        }

        public override IRecognizerResult BaseResult => result;

        public IMyKadBackRecognizerResult Result => result;

        
        public bool DetectGlare 
        { 
            get => nativeRecognizer.ShouldDetectGlare(); 
            set => nativeRecognizer.SetDetectGlare(value);
        }
        
        public uint FullDocumentImageDpi 
        { 
            get => (uint)nativeRecognizer.FullDocumentImageDpi; 
            set => nativeRecognizer.FullDocumentImageDpi = (int)value;
        }
        
        public bool ReturnFullDocumentImage 
        { 
            get => nativeRecognizer.ShouldReturnFullDocumentImage(); 
            set => nativeRecognizer.SetReturnFullDocumentImage(value);
        }
        
        public bool ReturnSignatureImage 
        { 
            get => nativeRecognizer.ShouldReturnSignatureImage(); 
            set => nativeRecognizer.SetReturnSignatureImage(value);
        }
        
        public uint SignatureImageDpi 
        { 
            get => (uint)nativeRecognizer.SignatureImageDpi; 
            set => nativeRecognizer.SignatureImageDpi = (int)value;
        }
        
    }

    public sealed class MyKadBackRecognizerResult : RecognizerResult, IMyKadBackRecognizerResult
    {
        Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer.Result nativeResult;

        internal MyKadBackRecognizerResult(Com.Microblink.Entities.Recognizers.Blinkid.Malaysia.MyKadBackRecognizer.Result nativeResult) : base(nativeResult)
        {
            this.nativeResult = nativeResult;
        }
        public IDate DateOfBirth => nativeResult.DateOfBirth.Date != null ? new Date(nativeResult.DateOfBirth.Date) : null;
        public string ExtendedNric => nativeResult.ExtendedNric;
        public Xamarin.Forms.ImageSource FullDocumentImage => Utils.ConvertAndroidBitmap(nativeResult.FullDocumentImage.ConvertToBitmap());
        public string Nric => nativeResult.Nric;
        public string Sex => nativeResult.Sex;
        public Xamarin.Forms.ImageSource SignatureImage => Utils.ConvertAndroidBitmap(nativeResult.SignatureImage.ConvertToBitmap());
    }
}