﻿namespace Microblink.Forms.Core.Recognizers
{
    public interface ISimNumberRecognizer : IRecognizer
    {
        

        /// <summary>
        /// Gets the result.
        /// </summary>
        ISimNumberRecognizerResult Result { get; }
    }

    public interface ISimNumberRecognizerResult : IRecognizerResult {
        
        /// <summary>
        /// Recognized SIM number from barcode or empty string if recognition failed. 
        /// </summary>
        string SimNumber { get; }
        
    }
}