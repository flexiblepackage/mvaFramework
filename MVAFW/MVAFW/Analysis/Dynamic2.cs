using System;
using System.Collections.Generic;
using System.Text;

using U8 = System.Char;
using I16 = System.Int16;
using U16 = System.UInt16;
using I32 = System.Int32;
using U32 = System.UInt32;
using F32 = System.Single;
using F64 = System.Double;

using MVAFW.TestItemColls;

namespace MVAFW.Analysis
{
    class Dynamic2
    {
        private static void
        __print_title(
              string title
            , string subject
            )
        {
            Console.Write("\n{0}, {1} --\n", title, subject);
        }
        private static void
        __dump(
              U32[] array
            , U32 count
            , U32 column
            )
        {
            for (U32 i = 0; i < count; ++i)
            {
                Console.Write("{0:d} ", array[i]);

                if (column - 1 == i % column)
                {
                    Console.Write("\n");
                }
            }
            Console.Write("\n");
        }
        private static void
        __dump(
              F64[] array
            , U32 count
            , U32 column
            )
        {
            for (U32 i = 0; i < count; ++i)
            {
                Console.Write("{0:0.000} ", array[i]);

                if (column - 1 == i % column)
                {
                    Console.Write("\n");
                }
            }
            Console.Write("\n");
        }
        public static void
        Pause()
        {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
        public static void
        TEST_is_power_of_two()
        {
            const string __title = "TEST_is_power_of_two";

            __print_title(__title, "1");

            for (System.UInt32 i = 0; i <= 31; ++i)
            {
                Console.Write(FFT2.is_power_of_two(i));
                Console.Write(' ');
                if (7 == i % 8)
                {
                    Console.Write('\n');
                }
            }
            Console.Write('\n');
        }
        public static void
        TEST_fft_errmsg()
        {
            const string __title = "TEST_fft_errmsg";

            __print_title(__title, "1");

            for (U32 i = 0; i < 8; ++i)
            {
                Console.WriteLine(FFT2.fft_errmsg(i));
            }
            Console.Write('\n');
        }
        public static void
        TEST_fft_double()
        {
            const string __title = "TEST_fft_double";

            __print_title(__title, "1");

            const U32 __n = 8;
            F64[] __src_r =
            {
                0, 1, 1, 1, 0, 1, 0, 1
            };
            F64[] __src_i = new F64[__n];
            F64[] __tgt_r = new F64[__n];
            F64[] __tgt_i = new F64[__n];

            for (U32 i = 0; i < 8; ++i)
            {
                __src_i[i] = 0;
                __tgt_r[i] = 0;
                __tgt_i[i] = 0;
            }

            U32 err =
            FFT2.fft_double(
                  FFT2.FFT_FORWARD   // [IN ] 0) forward, x) inverse transform
                , __n               // [IN ] number of samples, must be power of 2
                , __src_r           // [IN ] source samples, reals
                , __src_i           // [IN ] source samples, imaginaries, could be NULL
                , __tgt_r           // [OUT] target outputs, reals
                , __tgt_i           // [OUT] target outputs, imaginaries
                );                  // [RET] 0) no error, x) error index

            if (FFT2.FFT__E__NOERR != err) { goto _ERR_1_; }

            __dump(__tgt_r, __n, 8);
            __dump(__tgt_i, __n, 8);

            Console.Write('\n');
            return;

        _ERR_1_:
            Console.Write("[ERROR] {0}: {1}\n", __title, FFT2.fft_errmsg(err));
            return;
        }
        public static void
        TEST_dynamic_performance(
                                 F64[] __Dout
                               , U32 __n
                               , U32 __harm_count
                               , U32 __span
                               , bool ifDump
                               , out F64 __SINAD
                               , out F64 __SNR
                               , out F64 __THD
                               , out F64 __ENOB
                               , out F64 __SFDR
                               )
        {
            const string __title = "TEST_dynamic_performance";

            __print_title(__title, "1");

            // to use "square wave" as input
            //
            // %%% numpt = 128;
            // %%% Dout  = transpose(1:numpt);
            // %%% Dout(1) = 1.0;
            // %%% for i=2:numpt,
            // %%%     if 0 == mod(i, 4),
            // %%%         Dout(i) = ~Dout(i-1);
            // %%%     else
            // %%%         Dout(i) =  Dout(i-1);
            // %%%     end
            // %%% end
            // %%% Dout = Dout +0.001;
            //

        /*  mark by Bernie
            const U32 __n = 128;
            F64[] __Dout = new F64[__n];
            __Dout[0] = 1.0;
            U32 i;
            for (i = 1; i < __n; ++i)
            {

                if (0 == i % 4)
                {
                    if (0.5 > __Dout[i - 1])
                    {
                        __Dout[i] = 1.0;
                    }
                    if (0.5 < __Dout[i - 1])
                    {
                        __Dout[i] = 0.0;
                    }
                }
                else
                {
                    __Dout[i] = __Dout[i - 1];
                }
            }
            for (i = 0; i < __n; ++i)
            {
                __Dout[i] += 0.001;
            }*/

            // %%% Doutw = Dout.*hanning(length(Dout));
            //
            F64[] __Doutw = new F64[__n];

            U32 actual;
            actual =
            FFT2.kaiser(
                  __n       // [IN ] number of samples, must be power of 2
                , __Dout    // [IN ] source samples, reals, could be null
                , __Doutw   // [OUT] target results, reals
                ,38
                );          // [RET] number of processing, actual

            Console.Write("hanning:\n");
            if (ifDump == true)
            {
                __dump(__Doutw, __n, 8);
            }


            // -------------- Performing the FFT ------------- //
            //
            // %%% Dout_spec = fft(Doutw);
            //     Recalculate to dB
            // %%% Dout_dB = 20*log10(abs(Dout_spec));
            //     Determine power spectrum
            // %%% spectP = (abs(Dout_spec)).*(abs(Dout_spec));
            //
            F64[] __power = new F64[__n];
            F64[] __phase = new F64[__n];

            U32 __error_index;
            __error_index =
            FFT2.fft_spectrum_double(
                  __n           // [IN ] number of samples, must be power of 2
                , __Doutw       // [IN ] source samples, real part
                , null          // [IN ] source samples, image part, could be NULL
                , __power       // [OUT] power outputs, reals only
                , __phase       // [OUT] phase outputs, reals only, radians
                );              // [RET] 0) no error, x) error index
            if (FFT2.FFT__E__NOERR != __error_index)
            {
                // still running even error
                //
                Console.Write(
                  "[ERROR] {0}, {1}: {2:d}, {3}\n"
                , __title
                , "fft_spectrum_double"
                , __error_index
                , FFT2.fft_errmsg(__error_index)
                );
            }

            Console.Write("fft_spectrum_double, power:\n");
            if (ifDump == true)
            {
                __dump(__power, __n, 8);
            }

            Console.Write("fft_spectrum_double, phase:\n");
            if (ifDump == true)
            {
                __dump(__phase, __n, 8);
            }


            // search the harmonic bins
            //
            // approximate search span for harmonics on each side
            //
            // %%% spanh = 20;
            // %%% harmonic_number = 6;
            // %%% notdc = 3;
            // %%% span = max(round(dwSamples/200),5);
            //

            //const U32 __harm_count = 3;  //mark by Bernie
            U32[] __harm_indexes = new U32[__harm_count];
            F64[] __harm_powers = new F64[__harm_count];

            //U32 __span = __n / 200;
            //U32 __span = 47;            

            /*mark by Bernie
            F64 __SINAD = 0.0;
            F64 __SNR = 0.0;
            F64 __THD = 0.0;
            F64 __ENOB = 0.0;
            F64 __SFDR = 0.0;
             */

            U32 __MsbIndex = 0;
            F64 __MsbPower = 0.0;
            F64 dummy = 0;

            __error_index =
            FFT2.dynamic_performance(
                  __n >> 1          // [IN ] half number of samples, must be power of 2
                , __power           // [IN ] array, fft power spectrum
                , 3                 // [IN ] parameter used to avoid the dc stuff
                , __n >> 4          // [IN ] approximate search span for harmonics on each side
                , __span            // [IN ] span of the main signal frequency on each side
                , 1                 // [IN ] span of the harmonic frequency on each side
                , __harm_count      // [IN ] harmonic count\
                ,0
                , __harm_indexes    // [OUT] array, harmonic frequencies (indexes)
                , __harm_powers     // [OUT] array, harmonic powers
                , out __SINAD           // [OUT] pointer, signal-to-noise and distortion ratio
                , out __SNR             // [OUT] pointer, signal-to-noise ratio
                , out __THD             // [OUT] pointer, total harmonic distortion
                , out __ENOB            // [OUT] pointer, effective number of bits
                , out __SFDR            // [OUT] pointer, spurious-free dynamic range
                , out dummy
                , out __MsbIndex        // [OUT] pointer, maximum spurious frequency (index)
                , out __MsbPower        // [OUT] pointer, maximum spurious power
                ,0
                );                  // [RET] 0) no error, x) error index
            //U32 __spanx = 5;
            //U32 __prespan = __n / 200;
            //while (__prespan > (__harm_indexes[1] - __harm_indexes[0]))
            //{
            //    __prespan = __prespan - 1;
            //    if (__prespan == __harm_indexes[1] - __harm_indexes[0])
            //    {
            //        __prespan = __prespan - __spanx;
            //    }
            //}
            //while (__prespan >= (__harm_indexes[1] - __harm_indexes[0] - __spanx))
            //{
            //    __prespan = __prespan - 1;
            //}

            //if (__prespan < 5)
            //{
            //    __span = 5;
            //}
            //else
            //{
            //    __span = __prespan;
            //}

           // __error_index =
           //FFT.dynamic_performance(
           //      __n >> 1          // [IN ] half number of samples, must be power of 2
           //    , __power           // [IN ] array, fft power spectrum
           //    , 3                 // [IN ] parameter used to avoid the dc stuff
           //    , __n >> 4          // [IN ] approximate search span for harmonics on each side
           //    , __span            // [IN ] span of the main signal frequency on each side
           //    , 1                 // [IN ] span of the harmonic frequency on each side
           //    , __harm_count      // [IN ] harmonic count
           //    , __harm_indexes    // [OUT] array, harmonic frequencies (indexes)
           //    , __harm_powers     // [OUT] array, harmonic powers
           //    , out __SINAD           // [OUT] pointer, signal-to-noise and distortion ratio
           //    , out __SNR             // [OUT] pointer, signal-to-noise ratio
           //    , out __THD             // [OUT] pointer, total harmonic distortion
           //    , out __ENOB            // [OUT] pointer, effective number of bits
           //    , out __SFDR            // [OUT] pointer, spurious-free dynamic range
           //    , out __MsbIndex        // [OUT] pointer, maximum spurious frequency (index)
           //    , out __MsbPower        // [OUT] pointer, maximum spurious power
           //    );      



            if (FFT2.FFT__E__NOERR != __error_index)
            {
                // still running even error
                //
                Console.Write(
                  "[ERROR] {0}, {1}: {2:d}, {3}\n"
                , __title
                , "dynamic_performance"
                , __error_index
                , FFT2.fft_errmsg(__error_index)
                );
            }

            Console.Write("searched harmonic frequencies (defect=0x{0:X}) : \n", FFT2.FFT_DEFECT);
            __dump(__harm_indexes, __harm_count, 8);

            Console.Write("searched harmonic poweres : \n");
            __dump(__harm_powers, __harm_count, 8);

            Console.Write("SINAD = {0:0.000};\n", __SINAD);
            Console.Write("SNR   = {0:0.000};\n", __SNR);
            Console.Write("THD   = {0:0.000};\n", __THD);
            Console.Write("ENOB  = {0:0.000};\n", __ENOB);
            Console.Write("SFDR  = {0:0.000};\n", __SFDR);

            Console.Write("MsbIndex = {0:d};\n", __MsbIndex);
            Console.Write("MsbPower = {0:0.000};\n", __MsbPower);
        }

        public static double CMRR(AITestItem testItem, double[] dataWithSignal, double[] dataWithShort, int testChannel)
        {
            F64[][] __Doutw = new F64[1][];
            F64[][] __power = new F64[1][];
            F64[][] __phase = new F64[1][];
            double[] maxPower = new double[1];
            uint[] maxIndex = new uint[1];

            F64[][] __DoutwShort = new F64[1][];
            F64[][] __powerShort = new F64[1][];
            F64[][] __phaseShort = new F64[1][];
            double[] maxPowerShort = new double[1];
            uint[] maxIndexShort = new uint[1];
            
            for (int channel = 0; channel < 1; channel++)
            {
                __Doutw[channel] = new F64[testItem.DataCount];
                __power[channel] = new F64[testItem.DataCount];
                __phase[channel] = new F64[testItem.DataCount];

                __DoutwShort[channel] = new F64[testItem.DataCount];
                __powerShort[channel] = new F64[testItem.DataCount];
                __phaseShort[channel] = new F64[testItem.DataCount];
            }

            for (int channel = 0; channel < 1; channel++)
            {
                U32 actual;
                actual =
                FFT2.hanning(
                      testItem.DataCount       // [IN ] number of samples, must be power of 2
                      , dataWithSignal    // [IN ] source samples, reals, could be null
                    , __Doutw[channel]   // [OUT] target results, reals
                    );          // [RET] number of processing, actual

                U32 __error_index;
                __error_index =
                FFT2.fft_spectrum_double(
                      testItem.DataCount          // [IN ] number of samples, must be power of 2
                    , __Doutw[channel]       // [IN ] source samples, real part
                    , null          // [IN ] source samples, image part, could be NULL
                    , __power[channel]       // [OUT] power outputs, reals only
                    , __phase[channel]       // [OUT] phase outputs, reals only, radians
                    );              // [RET] 0) no error, x) error index

              //------------Short Below
                U32 actualShort;
                actualShort =
                FFT2.hanning(
                      testItem.DataCount       // [IN ] number of samples, must be power of 2
                      ,dataWithShort    // [IN ] source samples, reals, could be null
                    , __DoutwShort[channel]   // [OUT] target results, reals
                    );          // [RET] number of processing, actual

                U32 __error_indexShort;
                __error_indexShort =
                FFT2.fft_spectrum_double(
                      testItem.DataCount          // [IN ] number of samples, must be power of 2
                    , __DoutwShort[channel]       // [IN ] source samples, real part
                    , null          // [IN ] source samples, image part, could be NULL
                    , __powerShort[channel]       // [OUT] power outputs, reals only
                    , __phaseShort[channel]       // [OUT] phase outputs, reals only, radians
                    );              // [RET] 0) no error, x) error index
            }

            uint notdc = 3;
            uint msbIndex = 0;
            double msbPowerRef = 0;

            // fine the main frequency of dataWithSignal
            for (uint i = notdc - 1; i < (testItem.DataCount / 2); i++)
            {
                if (Math.Abs(__power[0][i]) > Math.Abs(msbPowerRef))
                {
                    msbPowerRef = __power[0][i];
                    msbIndex = i;
                }
            }

            //get main frequency
            double actualSampleRate = 0;
            if (testItem.Simultaneous == true)
            {
                actualSampleRate = testItem.SampleRate;
            }
            else if (testItem.Simultaneous == false)
            {
                actualSampleRate = testItem.SampleRate / testItem.ChannelNumbers;
            }
            testItem.MainFrequency[testChannel] = (actualSampleRate / testItem.DataCount) * msbIndex;

            //get max power with Short
            for (int channel = 0; channel < 1; channel++)
            {
                for (uint i = ((int)(msbIndex - testItem.Span)) < notdc ? notdc-1 : (msbIndex - testItem.Span); i < msbIndex + testItem.Span; i++)
                {
                    if (Math.Abs(__powerShort[channel][i]) > Math.Abs(maxPowerShort[channel]))
                    {
                        maxPowerShort[channel] = __powerShort[channel][i];
                        maxIndexShort[channel] = i;
                        testItem.MainFrequency[channel] = (actualSampleRate / testItem.DataCount) * i;
                    }
                }
            }

            return 20 * Math.Log10(Math.Sqrt(Math.Abs(maxPowerShort[0])) / Math.Sqrt(Math.Abs(msbPowerRef)));
        }

        public static void TestCrossTalkAndSkew(AITestItem testItem, int baseChannel, bool isRawdata, out double[] crosstalks, out double[] skews)
        {
            F64[][] __Doutw = new F64[testItem.ChannelNumbers][];
            F64[][] __power = new F64[testItem.ChannelNumbers][];
            F64[][] __phase = new F64[testItem.ChannelNumbers][];
            double[] maxPower = new double[testItem.ChannelNumbers];
            uint[] maxIndex = new uint[testItem.ChannelNumbers];
            crosstalks = new double[testItem.ChannelNumbers];
            skews = new double[testItem.ChannelNumbers];

            for (int channel = 0; channel < testItem.ChannelNumbers; channel++)
            {
                __Doutw[channel] = new F64[testItem.DataCount];
                __power[channel] = new F64[testItem.DataCount];
                __phase[channel] = new F64[testItem.DataCount];
            }

            for (int channel = 0; channel < testItem.ChannelNumbers; channel++)
            {
                U32 actual;
                actual =
                FFT2.hanning(
                      testItem.DataCount       // [IN ] number of samples, must be power of 2
                      , isRawdata == true ? testItem.Datas[channel] : testItem.ScaledDatas[channel]    // [IN ] source samples, reals, could be null
                    , __Doutw[channel]   // [OUT] target results, reals
                    );          // [RET] number of processing, actual
                
                U32 __error_index;
                __error_index =
                FFT2.fft_spectrum_double(
                      testItem.DataCount          // [IN ] number of samples, must be power of 2
                    , __Doutw[channel]       // [IN ] source samples, real part
                    , null          // [IN ] source samples, image part, could be NULL
                    , __power[channel]       // [OUT] power outputs, reals only
                    , __phase[channel]       // [OUT] phase outputs, reals only, radians
                    );              // [RET] 0) no error, x) error index
            }

            uint notdc = 3;
            uint msbIndex = 0;
            double msbPowerRef = 0;

            // fine the main frequency of base channel
            for (uint i = notdc - 1; i < (testItem.DataCount / 2); i++)
            {
                if (Math.Abs(__power[baseChannel][i]) > Math.Abs(msbPowerRef))
                {
                    msbPowerRef = __power[baseChannel][i];
                    msbIndex = i;
                }
            }
          
            //get main frequency
            double actualSampleRate = 0;
            if (testItem.Simultaneous == true)
            {
                actualSampleRate = testItem.SampleRate;
            }
            else if (testItem.Simultaneous == false)
            {
                actualSampleRate = testItem.SampleRate / testItem.ChannelNumbers;
            }
            //testItem.MainFrequency[baseChannel] = (actualSampleRate / testItem.DataCount) * msbIndex;

            //get all channel max power
            for (int channel = 0; channel < testItem.ChannelNumbers; channel++)
            {
                testItem.MainFrequency[channel] = (actualSampleRate / testItem.DataCount) * msbIndex;
                for (uint i = msbIndex - testItem.Span; i < msbIndex + testItem.Span; i++)
                {
                    if (Math.Abs(__power[channel][i]) > Math.Abs(maxPower[channel]))
                    {
                        maxPower[channel] = __power[channel][i];
                        maxIndex[channel] = i;
                    }
                }
            }

            //cross talk and skew
            for (int channel = 0; channel < testItem.ChannelNumbers; channel++)
            {
                crosstalks[channel] = 20 * Math.Log10(Math.Sqrt(Math.Abs(maxPower[channel])) / Math.Sqrt(Math.Abs(msbPowerRef)));
                skews[channel] = __phase[channel][msbIndex] - __phase[baseChannel][msbIndex];/* / (2 * Math.PI * testItem.MainFrequency[baseChannel])*/
            }
        }        

        public static void  TEST_dynamic_performance(AITestItem testItem, F64[] __Dout, bool ifDump, int channel)                               
        {
            const string __title = "TEST_dynamic_performance";

            __print_title(__title, "1");

            // to use "square wave" as input
            //
            // %%% numpt = 128;
            // %%% Dout  = transpose(1:numpt);
            // %%% Dout(1) = 1.0;
            // %%% for i=2:numpt,
            // %%%     if 0 == mod(i, 4),
            // %%%         Dout(i) = ~Dout(i-1);
            // %%%     else
            // %%%         Dout(i) =  Dout(i-1);
            // %%%     end
            // %%% end
            // %%% Dout = Dout +0.001;
            //

            /*  mark by Bernie
                const U32 __n = 128;
                F64[] __Dout = new F64[__n];
                __Dout[0] = 1.0;
                U32 i;
                for (i = 1; i < __n; ++i)
                {

                    if (0 == i % 4)
                    {
                        if (0.5 > __Dout[i - 1])
                        {
                            __Dout[i] = 1.0;
                        }
                        if (0.5 < __Dout[i - 1])
                        {
                            __Dout[i] = 0.0;
                        }
                    }
                    else
                    {
                        __Dout[i] = __Dout[i - 1];
                    }
                }
                for (i = 0; i < __n; ++i)
                {
                    __Dout[i] += 0.001;
                }*/

            // %%% Doutw = Dout.*hanning(length(Dout));
            //
            F64[] __Doutw = new F64[testItem.DataCount];

            U32 actual;
            actual =
            FFT2.kaiser(
                  testItem.DataCount       // [IN ] number of samples, must be power of 2
                , __Dout    // [IN ] source samples, reals, could be null
                , __Doutw   // [OUT] target results, reals
                ,20
                );          // [RET] number of processing, actual


            Console.Write("hann:\n");
            if (ifDump == true)
            {
                __dump(__Doutw, testItem.DataCount, 8);
            }


            // -------------- Performing the FFT ------------- //
            //
            // %%% Dout_spec = fft(Doutw);
            //     Recalculate to dB
            // %%% Dout_dB = 20*log10(abs(Dout_spec));
            //     Determine power spectrum
            // %%% spectP = (abs(Dout_spec)).*(abs(Dout_spec));
            //
            F64[] __power = new F64[testItem.DataCount];
            F64[] __phase = new F64[testItem.DataCount];

            U32 __error_index;
            __error_index =
            FFT2.fft_spectrum_double(
                  testItem.DataCount          // [IN ] number of samples, must be power of 2
                , __Doutw       // [IN ] source samples, real part
                , null          // [IN ] source samples, image part, could be NULL
                , __power       // [OUT] power outputs, reals only
                , __phase       // [OUT] phase outputs, reals only, radians
                );              // [RET] 0) no error, x) error index
            if (FFT2.FFT__E__NOERR != __error_index)
            {
                // still running even error
                //
                Console.Write(
                  "[ERROR] {0}, {1}: {2:d}, {3}\n"
                , __title
                , "fft_spectrum_double"
                , __error_index
                , FFT2.fft_errmsg(__error_index)
                );
            }

            Console.Write("fft_spectrum_double, power:\n");
            if (ifDump == true)
            {
                __dump(__power, testItem.DataCount, 8);
            }

            Console.Write("fft_spectrum_double, phase:\n");
            if (ifDump == true)
            {
                __dump(__phase, testItem.DataCount, 8);
            }


            // search the harmonic bins
            //
            // approximate search span for harmonics on each side
            //
            // %%% spanh = 20;
            // %%% harmonic_number = 6;
            // %%% notdc = 3;
            // %%% span = max(round(dwSamples/200),5);
            //

            //const U32 __harm_count = 3;  //mark by Bernie
            U32[] __harm_indexes = new U32[testItem.HarmonicCount];
            F64[] __harm_powers = new F64[testItem.HarmonicCount];

            //U32 __span = __n / 200;
            //U32 __span = 47;            

            /*mark by Bernie
            F64 __SINAD = 0.0;
            F64 __SNR = 0.0;
            F64 __THD = 0.0;
            F64 __ENOB = 0.0;
            F64 __SFDR = 0.0;
             */

            U32 __MsbIndex = 0;
            F64 __MsbPower = 0.0;

            double sinad, snr, thd, enob, sfdr, dummy;
            uint notdc=8; //3

            __error_index =
            FFT2.dynamic_performance(
                testItem.DataCount >> 1          // [IN ] half number of samples, must be power of 2
                , __power           // [IN ] array, fft power spectrum
                , notdc                 // [IN ] parameter used to avoid the dc stuff
                ,5//testItem.DataCount >> 4          // [IN ] approximate search span for harmonics on each side
                , testItem.Span            // [IN ] span of the main signal frequency on each side
                , 4                 // [IN ] span of the harmonic frequency on each side
                , testItem.HarmonicCount     // [IN ] harmonic count
                ,0
                , __harm_indexes    // [OUT] array, harmonic frequencies (indexes)
                , __harm_powers     // [OUT] array, harmonic powers
                , out sinad           // [OUT] pointer, signal-to-noise and distortion ratio
                , out snr             // [OUT] pointer, signal-to-noise ratio
                , out thd             // [OUT] pointer, total harmonic distortion
                , out enob            // [OUT] pointer, effective number of bits
                , out sfdr            // [OUT] pointer, spurious-free dynamic range
                , out dummy
                , out __MsbIndex        // [OUT] pointer, maximum spurious frequency (index)
                , out __MsbPower        // [OUT] pointer, maximum spurious power
                ,0
                );                  // [RET] 0) no error, x) error index
            testItem.Sinad[channel] = sinad;
            testItem.Snr[channel] = snr;
            testItem.Thd[channel] = thd;
            testItem.Enob[channel] = enob;
            testItem.Sfdr[channel] = sfdr;

            
            //return frequency and power spectrum
            uint msbIndex = 0;
            double msbPowerRef = 0;

            double actualSampleRate = 0;
            if (testItem.Simultaneous == true)
            {
                actualSampleRate = testItem.SampleRate;
            }
            else if (testItem.Simultaneous == false)
            {
                actualSampleRate = testItem.SampleRate / testItem.ChannelNumbers;
            }

            for (uint i = notdc - 1; i < (testItem.DataCount / 2); i++)
            {
                if(Math.Abs(__power[i])>Math.Abs(msbPowerRef))
                {
                    msbPowerRef = __power[i];
                    msbIndex = i;
                }
            }

            testItem.MainFrequency[channel] = (actualSampleRate / testItem.DataCount) * msbIndex;
            testItem.MainPhase[channel] = __phase[msbIndex];
                        
            double absoluteSpectrumShiftDB;
            if (testItem.FullScaleRawdata == 0 || testItem.Resolution == 0)
            {
                absoluteSpectrumShiftDB = 0;
            }
            else
            {
                absoluteSpectrumShiftDB = 20 * Math.Log10(testItem.FullScaleRawdata / (Math.Pow(2, testItem.Resolution - 1) - 1));
            }

            for (int i = 0; i < testItem.DataCount / 2; i++)
            {
                testItem.Frequency[channel][i] = Math.Round((actualSampleRate / testItem.DataCount) * i, 2);
                testItem.Spectrums[channel][i] = 20 * Math.Log10(Math.Sqrt(Math.Abs(__power[i])) / Math.Sqrt(Math.Abs(msbPowerRef)));
                testItem.SpectrumsAbsolute[channel][i] = testItem.Spectrums[channel][i] + absoluteSpectrumShiftDB;
                testItem.Power[channel] = __power;
                testItem.Phase[channel] = __phase;
            }

            if (FFT2.FFT__E__NOERR != __error_index)
            {
                // still running even error
                //
                //Console.Write(
                //  "[ERROR] {0}, {1}: {2:d}, {3}\n"
                //, __title
                //, "dynamic_performance"
                //, __error_index
                //, FFT.fft_errmsg(__error_index)
                //);
            }

            //Console.Write("searched harmonic frequencies (defect=0x{0:X}) : \n", FFT2.FFT_DEFECT);
            //__dump(__harm_indexes, testItem.HarmonicCount, 8);

            //Console.Write("searched harmonic poweres : \n");
            //__dump(__harm_powers, testItem.HarmonicCount, 8);

            Console.Write("SINAD = {0:0.000};\n", testItem.Sinad[channel]);
            Console.Write("SNR   = {0:0.000};\n", testItem.Snr[channel]);
            Console.Write("THD   = {0:0.000};\n", testItem.Thd[channel]);
            Console.Write("ENOB  = {0:0.000};\n", testItem.Thd[channel]);
            Console.Write("SFDR  = {0:0.000};\n", testItem.Sfdr[channel]);

            //Console.Write("MsbIndex = {0:d};\n", __MsbIndex);
            //Console.Write("MsbPower = {0:0.000};\n", __MsbPower);
        }
    }
}
