// FFT.cs : Fast Fourier Transform, FFT
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;

using U8  = System.Char;
using I16 = System.Int16;
using U16 = System.UInt16;
using I32 = System.Int32;
using U32 = System.UInt32;
using F32 = System.Single;
using F64 = System.Double;

namespace MVAFW.Analysis
{
    public class FFT2
    {
#if x86
    private const String NativeDll = "FFT32.dll";
#else
        private const String NativeDll = "FFT64.dll";
#endif
        // magic numbers
        //
        public const F64 FFT_PI = (3.14159265358979323846264338327);
        public const F64 FFT_R2D = (180.0 / FFT_PI);
        public const F64 FFT_D2R = (FFT_PI / 180.0);
        public const U32 FFT_FORWARD = (0);
        public const U32 FFT_INVERSE = (1);
        public const U32 FFT_FALSE = (0);
        public const U32 FFT_TRUE = (1);
        public const U32 FFT_DEFECT = (0xFFFFFFFF);

        // error numbers
        //
        public const U32 FFT__E__NOERR = (0);
        public const U32 FFT__E__INVALID_ARGUMENT = (1);
        public const U32 FFT__E__UNAVAILABLE_MEMORY = (2);
        public const U32 FFT__E__NOT_POWER_OF_TWO = (3);
        public const U32 FFT__E__DEFECTIVE_HARMONIC = (4);

        /*
        **  The following function tests whether its argument is a power of two for any
        **  non-negative exponent k: x==2^k
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            is_power_of_two(
            U32 dwNumber        // [IN ] number for checking, x
            );                          // [RET] 1) yes, 0) no

        /*
        **  The following function return the number of bits needed of its argument
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            number_of_bits_needed(
            U32 dwPowerOfTwo    // [IN ] number, must be power of two
            );                          // [RET] number of bits needed of its argument

        /*
        **  The following function reverses the bit order of index number
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            reverse_bits(
            U32 dwIndex         // [IN ] index number
            , U32 dwNumBits       // [IN ] number of bits of index
            );                          // [RET] reversed bit order of the index number

        /*
        **  The following function returns an "abstract frequency" of a given index into a
        **  buffer with a given number of frequency samples.
        **  Multiply return value by sampling rate to get frequency expressed in Hz.
        */
        [DllImport(NativeDll)]
        public static extern
            F64
            index_to_frequency(
            U32 dwSamples       // [IN ] number of samples
            , U32 dwIndex         // [IN ] index
            );                          // [RET] abstract frequency

        /*
        **  This returns a static constant error string corresponded its argument
        */
        /*
        */
        [DllImport(NativeDll)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern
            string
            fft_errmsg(
            U32 dwIndex         // [IN ] error index, result of the following functions
            );                          // [RET] error string corresponded its argument

        /*
        **  The fft() computes the Fourier transform or inverse transform of the complex inputs
        **  to produce the complex outputs. The number of samples must be a power of two to
        **  do the recursive decomposition of the FFT algorithm.
        **
        **  The fft_double() maybe faster than fft_float(), because the argument's type of the
        **  internal computing functions is double.
        **
        **  If you pass lpSrcImag = 0, these functions will "pretend" that it is an array of
        **  all zeroes.
        **
        **  <<< CHECKING >>>
        **  U32  __error_index = fft_xxx(...);
        **  if ( __error_index ) { puts(fft_errmsg(__error_index)); return false; }
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            fft_float(
            U32 IsInverse       // [IN ] 0) forward, x) inverse transform
            , U32 dwSamples       // [IN ] number of samples, must be power of 2
            , F32[] lpSrcReal       // [IN ] source samples, reals
            , F32[] lpSrcImag       // [IN ] source samples, imaginaries, could be NULL
            , F32[] lpTgtReal       // [OUT] target outputs, reals
            , F32[] lpTgtImag       // [OUT] target outputs, imaginaries
            );                          // [RET] 0) no error, x) error index

        [DllImport(NativeDll)]
        public static extern
            U32
            fft_double(
            U32 IsInverse       // [IN ] 0) forward, x) inverse transform
            , U32 dwSamples       // [IN ] number of samples, must be power of 2
            , F64[] lpSrcReal       // [IN ] source samples, reals
            , F64[] lpSrcImag       // [IN ] source samples, imaginaries, could be NULL
            , F64[] lpTgtReal       // [OUT] target outputs, reals
            , F64[] lpTgtImag       // [OUT] target outputs, imaginaries
            );                          // [RET] 0) no error, x) error index

        [DllImport(NativeDll)]
        public static extern
            U32
            fft_spectrum_float(
            U32 dwSamples       // [IN ] number of samples, must be power of 2
            , F32[] lpSrcReal       // [IN ] source samples, real part
            , F32[] lpSrcImag       // [IN ] source samples, image part, could be NULL
            , F32[] lpPowerSp       // [OUT] power outputs, reals only
            , F32[] lpPhaseSp       // [OUT] phase outputs, reals only, radians
            );                          // [RET] 0) no error, x) error index

        [DllImport(NativeDll)]
        public static extern
            U32
            fft_spectrum_double(
            U32 dwSamples       // [IN ] number of samples, must be power of 2
            , F64[] lpSrcReal       // [IN ] source samples, real part
            , F64[] lpSrcImag       // [IN ] source samples, image part, could be NULL
            , F64[] lpPowerSp       // [OUT] power outputs, reals only
            , F64[] lpPhaseSp       // [OUT] phase outputs, reals only, radians
            );                          // [RET] 0) no error, x) error index

        /*
        **  These will do one array multiplication with the input array and the N-point
        **  symmetric Hanning/Hamming window in a column array.
        **
        **  If you pass lpSources = 0, these will get pure "window" values.
        **
        **  <<< CHECKING >>>
        **  U32  __actual = haxxing(dwSamples, ...);
        **  if ( __actual != dwSamples ) { return false; }
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            hanning(
            U32 dwSamples       // [IN ] number of samples
            , F64[] lpSources       // [IN ] source samples, reals, could be 0
            , F64[] lpTargets       // [OUT] target results, reals
            );                          // [RET] actual processed count

        [DllImport(NativeDll)]
        public static extern
            U32
            hamming(
            U32 dwSamples       // [IN ] number of samples
            , F64[] lpSources       // [IN ] source samples, reals, could be 0
            , F64[] lpTargets       // [OUT] target results, reals
            );                          // [RET] actual processed count

        [DllImport(NativeDll)]
        public static extern
            U32
            kaiser(
            U32 dwSamples       // [IN ] number of samples
            , F64[] lpSources     // [IN ] source samples, reals, could be 0
            , F64[] lpTargets     // [OUT] target results, reals
            , F64   beta            // [IN ] beta nember for kaiser window
            );                          // [RET] actual processed count

        /*
        **  This is used to calculate SNR, SINAD, THD, ENOB and SFDR values.
        **
        **  SINAD  : Signal-to-Noise and Distortion Ratio
        **  SNR    : Signal-to-Noise Ratio
        **  THD    : Total Harmonic Distortion
        **  ENOB   : Effective Number of Bits
        **  SFDR   : Spurious-Free Dynamic Range
        **  MSB    : Maximum Spurious Bin
        **
        **  There are some suggested values as follow:
        **      dwMainSpan <-- max(5, dwSamples/200)
        **      dwHarmSpan <-- 1
        **      dwSearch   <-- max(dwHarmSpan, dwHalf >> 4)
        **      range(center, span) = [center-span, center+span]
        **
        **  [NOTE] Defective Harmonic <-- FFT_DEFECT (0xFFFFFFFF);
        **         For this procedure to work, ensure the folded back high order harmonics
        **         do not overlap with dc or signal or lower order harmonics.
        **
        **  <<< CHECKING >>>
        **  U32  __error_index = dynamic_performance(...);
        **  if ( __error_index ) { puts(fft_errmsg(__error_index)); return false; }
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            dynamic_performance(
            U32 dwHalf          // [IN ] half number of samples, must be power of 2
            , F64[] lpSpectrum      // [IN ] array, fft power spectrum
            , U32 dwIndexDc       // [IN ] parameter used to avoid the dc stuff
            , U32 dwSearch        // [IN ] approximate search span for harmonics on each side
            , U32 dwMainSpan      // [IN ] span of the main signal frequency on each side
            , U32 dwHarmSpan      // [IN ] span of the harmonic frequency on each side
            , U32 dwHarmCount     // [IN ] harmonic count
            , U32 dwRBuzzHarmCount // [IN ] harmonic count start from H9
            , U32[] lpHarmIndexes   // [OUT] array, harmonic frequencies (indexes)
            , F64[] lpHarmPowers    // [OUT] array, harmonic powers
            , out F64 lpSINAD         // [OUT] pointer, signal-to-noise and distortion ratio
            , out F64 lpSNR           // [OUT] pointer, signal-to-noise ratio
            , out F64 lpTHD           // [OUT] pointer, total harmonic distortion
            , out F64 lpENOB          // [OUT] pointer, effective number of bits
            , out F64 lpSFDR          // [OUT] pointer, spurious-free dynamic range
            , out F64 lpRubBuzz         // [OUT] pointer, R&B
            , out U32 lpMsbIndex      // [OUT] pointer, maximum spurious frequency (index)
            , out F64 lpMsbPower      // [OUT] pointer, maximum spurious power
            , U32 dwMainInde     // [IN ] index of fundemental, 0 for auto search to max
            );                          // [RET] 0) no error, x) error index

        /*
        **  iba : index-based array   <-- [ c1d1, c2d1, c3d1, ..., c1d2, c2d2, c3d2, ... ]
        **  cba : channel-based array <-- [ c1d1, c1d2, ..., c2d1, c2d2, ..., c3d1, c3d2, ... ]
        **
        **  [NOTE] length(array) = dwChannels * dwSamples;
        */
        [DllImport(NativeDll)]
        public static extern
            void
            iba2cba(
            U32 dwChannels     // [IN ] number of channels
            , U32 dwSamples      // [IN ] number of samples for each channels
            , F64[] lpIba          // [IN ] source index-based array
            , F64[] lpCba          // [OUT] target channel-based array
            );

        [DllImport(NativeDll)]
        public static extern
            void
            cba2iba(
            U32 dwChannels     // [IN ] number of channels
            , U32 dwSamples      // [IN ] number of samples for each channels
            , F64[] lpCba          // [IN ] source channel-based array
            , F64[] lpIba          // [OUT] target index-based array
            );

        /*
        **  <<< CHECKING >>>
        **  F64[]  __ending = log10_xxx(dwLen, ..., lpTgt, ...);
        **  if ( __ending != lpTgt +dwLen ) { return false; }
        */
        [DllImport(NativeDll)]
        public static extern
            F64
            log10_single(
            F64 x               // [IN ] source number <-- x
            );                          // [RET] log10 value   <-- log10(x)

        [DllImport(NativeDll)]
        public static extern
            F64[]
            log10_range(
            U32 dwLen           // [IN ] array length      <-- n
            , F64[] lpSrc           // [IN ] source array      <-- xx
            , F64[] lpTgt           // [OUT] target array, log <-- log10(xx)
            );                          // [RET] ending pointer    <-- lpTgt +n

        [DllImport(NativeDll)]
        public static extern
            F64[]
            log10_complex(
            U32 dwLen           // [IN ] array length              <-- n
            , F64[] lpSrcReal       // [IN ] source array, real part   <-- a
            , F64[] lpSrcImag       // [IN ] source array, image part  <-- b
            , F64[] lpTgt           // [OUT] target array, log complex <-- 0.5*log10(a^2+b^2)
            );                          // [RET] ending pointer            <-- lpTgt +n

        [DllImport(NativeDll)]
        public static extern
            F64[]
            log10_db(
            U32 dwLen           // [IN ] array length           <-- n
            , F64[] lpSrc           // [IN ] source array           <-- x
            , F64[] lpTgt           // [OUT] target array, db value <-- d+c*log10(x)
            , F64 cdMult          // [OPT] constant multiplier    <-- c               = 1.0
            , F64 cdBias          // [OPT] constant bias          <-- d               = 0.0
            );                          // [RET] ending pointer         <-- lpTgt +n

        [DllImport(NativeDll)]
        public static extern
            F64[]
            log10_power(
            U32 dwLen           // [IN ] array length              <-- n
            , F64[] lpSrcReal       // [IN ] source reals              <-- a
            , F64[] lpSrcImag       // [IN ] source images             <-- b
            , F64[] lpTgtPw         // [OUT] target powers, could be 0 <-- a^2+b^2
            , F64[] lpTgtPd         // [OUT] target powers, db value   <-- d+c*log10(a^2+b^2)
            , F64 cdMult          // [OPT] constant multiplier       <-- c            = 1.0
            , F64 cdBias          // [OPT] constant bias             <-- d            = 0.0
            );                          // [RET] ending pointer            <-- lpTgtPd +n

        [DllImport(NativeDll)]
        public static extern
            F64[]
            log10_normalize(
            U32 dwLen           // [IN ] array length                 <-- n
            , F64[] lpSrcReal       // [IN ] source reals                 <-- a
            , F64[] lpSrcImag       // [IN ] source images                <-- b
            , F64[] lpTgtPw         // [OUT] target powers, could be 0    <-- a^2+b^2
            , F64[] lpTgtPd         // [OUT] target powers, normalize db  <-- d-m+c*log10(a^2+b^2)
            , F64 cdMult          // [OPT] constant multiplier          <-- c         = 1.0
            , F64 cdBias          // [OPT] constant bias                <-- d         = 0.0
            , F64[] lpMaxPd         // [OPT] maximum db value, could be 0 <-- m         = 0
            );                          // [RET] ending pointer               <-- lpTgtPd +n

        /*
        **  The fft2() returns the two-dimensional discrete Fourier transform (DFT) of source samples,
        **  computed with a fast Fourier transform (FFT) algorithm. The result samples is the same size as source.
        */
        [DllImport(NativeDll)]
        public static extern
            U32
            fft2_float(
            U32 IsInverse       // [IN ] 0) forward, x) inverse transform
            , U32 dwWidth         // [IN ] number of samples, must be power of 2, width
            , U32 dwHeight        // [IN ] number of samples, must be power of 2, height
            , F32[] lpSrcReal       // [IN ] source samples, real part
            , F32[] lpSrcImag       // [IN ] source samples, image part, could be 0
            , F32[] lpTgtReal       // [OUT] target samples, real part
            , F32[] lpTgtImag       // [OUT] target samples, image part
            );                          // [RET] 0) no error, x) error index

        [DllImport(NativeDll)]
        public static extern
            U32
            fft2_double(
            U32 IsInverse       // [IN ] 0) forward, x) inverse transform
            , U32 dwWidth         // [IN ] number of samples, must be power of 2, width
            , U32 dwHeight        // [IN ] number of samples, must be power of 2, height
            , F64[] lpSrcReal       // [IN ] source samples, real part
            , F64[] lpSrcImag       // [IN ] source samples, image part, could be 0
            , F64[] lpTgtReal       // [OUT] target samples, real part
            , F64[] lpTgtImag       // [OUT] target samples, image part
            );                          // [RET] 0) no error, x) error index


    } // class FFT
}
// EOF <FFT.cs>
