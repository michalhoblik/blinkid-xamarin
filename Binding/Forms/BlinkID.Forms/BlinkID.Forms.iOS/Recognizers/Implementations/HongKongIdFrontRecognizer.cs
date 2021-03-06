﻿using Microblink.Forms.iOS.Recognizers;
using Microblink.Forms.Core.Recognizers;

[assembly: Xamarin.Forms.Dependency(typeof(HongKongIdFrontRecognizer))]
namespace Microblink.Forms.iOS.Recognizers
{
    public sealed class HongKongIdFrontRecognizer : Recognizer, IHongKongIdFrontRecognizer
    {
        MBHongKongIdFrontRecognizer nativeRecognizer;

        HongKongIdFrontRecognizerResult result;

        public HongKongIdFrontRecognizer() : base(new MBHongKongIdFrontRecognizer())
        {
            nativeRecognizer = NativeRecognizer as MBHongKongIdFrontRecognizer;
            result = new HongKongIdFrontRecognizerResult(nativeRecognizer.Result);
        }

        public override IRecognizerResult BaseResult => result;

        public IHongKongIdFrontRecognizerResult Result => result;

        
        public bool DetectGlare 
        { 
            get => nativeRecognizer.DetectGlare; 
            set => nativeRecognizer.DetectGlare = value;
        }
        
        public bool ExtractCommercialCode 
        { 
            get => nativeRecognizer.ExtractCommercialCode; 
            set => nativeRecognizer.ExtractCommercialCode = value;
        }
        
        public bool ExtractDateOfBirth 
        { 
            get => nativeRecognizer.ExtractDateOfBirth; 
            set => nativeRecognizer.ExtractDateOfBirth = value;
        }
        
        public bool ExtractDateOfIssue 
        { 
            get => nativeRecognizer.ExtractDateOfIssue; 
            set => nativeRecognizer.ExtractDateOfIssue = value;
        }
        
        public bool ExtractFullName 
        { 
            get => nativeRecognizer.ExtractFullName; 
            set => nativeRecognizer.ExtractFullName = value;
        }
        
        public bool ExtractResidentialStatus 
        { 
            get => nativeRecognizer.ExtractResidentialStatus; 
            set => nativeRecognizer.ExtractResidentialStatus = value;
        }
        
        public bool ExtractSex 
        { 
            get => nativeRecognizer.ExtractSex; 
            set => nativeRecognizer.ExtractSex = value;
        }
        
        public uint FaceImageDpi 
        { 
            get => (uint)nativeRecognizer.FaceImageDpi; 
            set => nativeRecognizer.FaceImageDpi = value;
        }
        
        public uint FullDocumentImageDpi 
        { 
            get => (uint)nativeRecognizer.FullDocumentImageDpi; 
            set => nativeRecognizer.FullDocumentImageDpi = value;
        }
        
        public IImageExtensionFactors FullDocumentImageExtensionFactors 
        { 
            get => new ImageExtensionFactors(nativeRecognizer.FullDocumentImageExtensionFactors); 
            set => nativeRecognizer.FullDocumentImageExtensionFactors = (value as ImageExtensionFactors).NativeFactors;
        }
        
        public bool ReturnFaceImage 
        { 
            get => nativeRecognizer.ReturnFaceImage; 
            set => nativeRecognizer.ReturnFaceImage = value;
        }
        
        public bool ReturnFullDocumentImage 
        { 
            get => nativeRecognizer.ReturnFullDocumentImage; 
            set => nativeRecognizer.ReturnFullDocumentImage = value;
        }
        
    }

    public sealed class HongKongIdFrontRecognizerResult : RecognizerResult, IHongKongIdFrontRecognizerResult
    {
        MBHongKongIdFrontRecognizerResult nativeResult;

        internal HongKongIdFrontRecognizerResult(MBHongKongIdFrontRecognizerResult nativeResult) : base(nativeResult)
        {
            this.nativeResult = nativeResult;
        }
        public string CommercialCode => nativeResult.CommercialCode;
        public IDate DateOfBirth => nativeResult.DateOfBirth != null ? new Date(nativeResult.DateOfBirth) : null;
        public IDate DateOfIssue => nativeResult.DateOfIssue != null ? new Date(nativeResult.DateOfIssue) : null;
        public string DocumentNumber => nativeResult.DocumentNumber;
        public Xamarin.Forms.ImageSource FaceImage => nativeResult.FaceImage != null ? Utils.ConvertUIImage(nativeResult.FaceImage.Image) : null;
        public Xamarin.Forms.ImageSource FullDocumentImage => nativeResult.FullDocumentImage != null ? Utils.ConvertUIImage(nativeResult.FullDocumentImage.Image) : null;
        public string FullName => nativeResult.FullName;
        public string ResidentialStatus => nativeResult.ResidentialStatus;
        public string Sex => nativeResult.Sex;
    }
}