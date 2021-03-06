﻿using System;
namespace Microblink.Forms.Core.Recognizers
{
    /// <summary>
    /// Possible types of Machine Readable Travel Documents (MRTDs)
    /// </summary>
    public enum MrtdDocumentType
    {
        /// <summary>
        /// Unknown document type
        /// </summary>
        Unknown,
        /// <summary>
        /// Identity card
        /// </summary>
        IdentityCard,
        /// <summary>
        /// Passport.
        /// </summary>
        Passport,
        /// <summary>
        /// Visa.
        /// </summary>
        Visa,
        /// <summary>
        /// US Green Card .
        /// </summary>
        GreenCard,
        /// <summary>
        /// Malaysian PASS type IMM13P
        /// </summary>
        MalaysianPassIMM13P
    }

    /// <summary>
    /// Represents data extracted from MRZ (Machine Readable Zone) of Machine Readable Travel Document (MRTD).
    /// </summary>
    public interface IMrzResult
    {
        /// <summary>
        /// Gets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        MrtdDocumentType DocumentType { get; }

        /// <summary>
        /// Gets the primary identifier. If there is more than one component, they are separated with space.
        /// </summary>
        /// <value>The primary identifier.</value>
        string PrimaryId { get; }

        /// <summary>
        /// Gets the secondary identifier. If there is more than one component, they are separated with space.
        /// </summary>
        /// <value>The secondary identifier.</value>
        string SecondaryId { get; }

        /// <summary>
        /// Gets the issuer of the document.
        /// The issuer is three-letter or two-letter code which indicate the issuing State. 
        /// Three-letter codes are based on Aplha-3 codes for entities specified in ISO 3166-1, 
        /// with extensions for certain States. Two-letter codes are based on Alpha-2 codes for 
        /// entities specified in ISO 3166-1, with extensions for certain States.
        /// </summary>
        /// <value>The issuer.</value>
        string Issuer { get; }

        /// <summary>
        /// Gets the date of birth of the ID holder.
        /// </summary>
        /// <value>The date of birth.</value>
        IDate DateOfBirth { get; }

        /// <summary>
        /// Gets the document number.
        /// Document number contains up to 9 characters. Element does not exist on US 
        /// Green Card. To see which document was scanned use DocumentType property.
        /// </summary>
        /// <value>The document number.</value>
        string DocumentNumber { get; }

        /// <summary>
        /// Gets the nationality of the ID holder.
        /// The nationality of the holder represented by a three-letter or two-letter code. Three-letter
        /// codes are based on Alpha-3 codes for entities specified in ISO 3166-1, with extensions for certain
        /// States.Two-letter codes are based on Aplha-2 codes for entities specified in ISO 3166-1, with
        /// extensions for certain States.
        /// </summary>
        /// <value>The nationality.</value>
        string Nationality { get; }

        /// <summary>
        /// Gets the gender of the document holder.
        /// Gender is specified by use of the single initial, capital letter F for female,
        /// M for male or <code>&lt;</code> for unspecified.
        /// </summary>
        /// <value>The gender.</value>
        string Gender { get; }

        /// <summary>
        /// Gets the document code.
        /// Document code contains two characters. For MRTD the first character shall 
        /// be A, C or I.The second character shall be discretion of the issuing State or organization except
        /// that V shall not be used, and `C` shall not be used after `A` except in the crew member certificate.
        /// On machine-readable passports (MRP) first character shall be `P` to designate an MRP. One additional
        /// letter may be used, at the discretion of the issuing State or organization, to designate a particular
        /// MRP.If the second character position is not used for this purpose, it shall be filled by the filter
        /// character <code>&lt;</code>.
        /// </summary>
        /// <value>The document code.</value>
        string DocumentCode { get; }

        /// <summary>
        /// Gets the date of expiry of the document.
        /// </summary>
        /// <value>The date of expiry.</value>
        IDate DateOfExpiry { get; }

        /// <summary>
        /// Gets the first optional data.
        /// Contains empty string if not available. 
        /// Element does not exist on US Green Card.
        /// To see which document was scanned use the DocumentType property.
        /// </summary>
        /// <value>The first optional data.</value>
        string Opt1 { get; }

        /// <summary>
        /// Gets the second optional data.
        /// Contains empty string if not available. 
        /// Element does not exist on US Green Card.
        /// To see which document was scanned use the DocumentType property.
        /// </summary>
        /// <value>The second optional data.</value>
        string Opt2 { get; }

        /// <summary>
        /// Gets the alien number.
        /// Contains empty string if not available.
        /// Exists only on US Green Cards.
        /// To see which document was scanned use the DocumentType property.
        /// </summary>
        /// <value>The alien number.</value>
        string AlienNumber { get; }

        /// <summary>
        /// Gets the application receipt number.
        /// Contains empty string if not available.
        /// Exists only on US Green Cards.
        /// To see which document was scanned use the DocumentType property.
        /// </summary>
        /// <value>The application receipt number.</value>
        string ApplicationReceiptNumber { get; }

        /// <summary>
        /// Gets the immigrant case number.
        /// Contains empty string if not available.
        /// Exists only on US Green Cards.
        /// To see which document was scanned use the DocumentType property.
        /// </summary>
        /// <value>The immigrant case number.</value>
        string ImmigrantCaseNumber { get; }

        /// <summary>
        /// Gets entire Machine Readable Zone text from ID. This text is usually 
        /// used for parsing other elements.
        /// NOTE: This string is available only if OCR result was parsed successfully.
        /// </summary>
        /// <value>The MRZ text.</value>
        string MrzText { get; }

        /// <summary>
        /// Gets a value indicating whether Machine Readable Zone has been parsed.
        /// </summary>
        /// <value><c>true</c> if parsed; otherwise, <c>false</c>.</value>
        bool Parsed { get; }

        /// <summary>
        /// Gets a value indicating whether all check digits inside MRZ are correct.
        /// </summary>
        /// <value><c>true</c> if verified; otherwise, <c>false</c>.</value>
        bool Verified { get; }
    }

    /// <summary>
    /// Possible values for EUDL country fields.
    /// </summary>
    public enum EudlCountry
    {
        /// <summary>
        /// UK's driver's license.
        /// </summary>
        UK,
        /// <summary>
        /// German driver's license
        /// </summary>
        Germany,
        /// <summary>
        /// Austrian driver's license.
        /// </summary>
        Austria,
        /// <summary>
        /// Performs country detection and uses scanning parameters for detected country.
        /// </summary>
        Automatic
    }

    /// <summary>
    /// Possible supported detectors for documents containing face image.
    /// </summary>
    public enum DocumentFaceDetectorType
    {
        /// <summary>
        /// Uses document detector for TD1 size identity cards.
        /// </summary>
        TD1,
        /// <summary>
        /// Uses document detector for TD2 size identity cards.
        /// </summary>
        TD2,
        /// <summary>
        /// Uses MRTD detector for detecting documents with MRZ
        /// </summary>
        PassportAndVisas
    }

    /// <summary>
    /// Supported BlinkCard card issuer values.
    /// </summary>
    public enum CardIssuer
    {
        /// <summary>
        /// Unidentified Card
        /// </summary>
        Other,
        /// <summary>
        /// The American Express Company Card
        /// </summary>
        AmericanExpress,
        /// <summary>
        /// The Bank of Montreal ABM Card
        /// </summary>
        BmoAbm,
        /// <summary>
        /// China T-Union Transportation Card
        /// </summary>
        ChinaTUnion,
        /// <summary>
        /// China UnionPay Card
        /// </summary>
        ChinaUnionPay,
        /// <summary>
        /// Canadian Imperial Bank of Commerce Advantage Debit Card
        /// </summary>
        CibcAdvantageDebit,
        /// <summary>
        /// CISS Card
        /// </summary>
        Ciss,
        /// <summary>
        /// Diners Club International Card
        /// </summary>
        DinersClubInternational,
        /// <summary>
        /// Diners Club United States & Canada Card
        /// </summary>
        DinersClubUsCanada,
        /// <summary>
        /// Discover Card
        /// </summary>
        DiscoverCard,
        /// <summary>
        /// HSBC Bank Canada Card
        /// </summary>
        Hsbc,
        /// <summary>
        /// RuPay Card
        /// </summary>
        RuPay,
        /// <summary>
        /// InterPayment Card
        /// </summary>
        InterPayment,
        /// <summary>
        /// InstaPayment Card
        /// </summary>
        InstaPayment,
        /// <summary>
        /// The JCB Company Card
        /// </summary>
        Jcb,
        /// <summary>
        /// Laser Debit Card (deprecated)
        /// </summary>
        Laser,
        /// <summary>
        /// Maestro Debit Card
        /// </summary>
        Maestro,
        /// <summary>
        /// Dankort Card
        /// </summary>
        Dankort,
        /// <summary>
        /// MIR Card
        /// </summary>
        Mir,
        /// <summary>
        /// MasterCard Inc. Card
        /// </summary>
        MasterCard,
        /// <summary>
        /// The Royal Bank of Canada Client Card
        /// </summary>
        RbcClient,
        /// <summary>
        /// ScotiaBank Scotia Card
        /// </summary>
        ScotiaBank,
        /// <summary>
        /// TD Canada Trust Access Card
        /// </summary>
        TdCtAccess,
        /// <summary>
        /// Troy Card
        /// </summary>
        Troy,
        /// <summary>
        /// Visa Inc. Card
        /// </summary>
        Visa,
        /// <summary>
        /// Universal Air Travel Plan Inc. Card
        /// </summary>
        Uatp,
        /// <summary>
        /// Interswitch Verve Card
        /// </summary>
        Verve
    }

    public interface IImageExtensionFactors
    {
        /// <summary>
        /// Gets the image extension factor relative to full image height in UP direction.
        /// </summary>
        /// <value>Up factor.</value>
        float UpFactor { get; }

        /// <summary>
        /// Gets the image extension factor relative to full image height in RIGHT direction..
        /// </summary>
        /// <value>The right factor.</value>
        float RightFactor { get; }

        /// <summary>
        /// Gets image extension factor relative to full image height in DOWN direction.
        /// </summary>
        /// <value>Down factor.</value>
        float DownFactor { get; }

        /// <summary>
        /// Gets the image extension factor relative to full image height in LEFT direction.
        /// </summary>
        /// <value>The left factor.</value>
        float LeftFactor { get; }
    }

    /// <summary>
    /// Image extension factors factory. Use this to create an instance of IImageExtensionFactors.
    /// </summary>
    public interface IImageExtensionFactorsFactory
    {
        /// <summary>
        /// Creates the image extension factors.
        /// </summary>
        /// <returns>The image extension factors.</returns>
        /// <param name="upFactor">image extension factor relative to full image height in UP direction</param>
        /// <param name="downFactor">image extension factor relative to full image height in DOWN direction</param>
        /// <param name="leftFactor">image extension factor relative to full image width in LEFT direction</param>
        /// <param name="rightFactor">image extension factor relative to full image width in RIGHT direction</param>
        IImageExtensionFactors CreateImageExtensionFactors(float upFactor = 0.0f, float downFactor = 0.0f, float leftFactor = 0.0f, float rightFactor = 0.0f);
    }
}
