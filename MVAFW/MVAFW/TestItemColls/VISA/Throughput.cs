using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;



namespace MVAFW.TestItemColls.VISA
{

    public class Throughput : TestItem
    {

        public Throughput()
        {
            ChannelNumbers = 10;
        }

        public override void doTest()
        {
            base.doTest();

            int err, RM, findList, numIntfc,  SessionId0, SessionId1;
            StringBuilder Read = new StringBuilder(2048);

            byte[] AAA = Encoding.ASCII.GetBytes("ABC");

            err = visa32.viOpenDefaultRM(out RM);
            err = visa32.viFindRsrc(RM, "?*INTFC", out findList, out numIntfc, Read);
            //for (int i = 0; i < numIntfc; i++)
            //    err = visa32.viFindNext(findList, Read);


            err = visa32.viOpen(RM, Read.ToString(), 0, 0, out SessionId0);

            err = visa32.viFindNext(findList, Read);
            err = visa32.viOpen(RM, Read.ToString(), 0, 0, out SessionId1);

            err = visa32.viSetAttribute(SessionId0, visa32.VI_ATTR_TMO_VALUE, 3000);
            err = visa32.viSetAttribute(SessionId1, visa32.VI_ATTR_TMO_VALUE, 3000);


            err = visa32.viGpibCommand(SessionId0, "@!", 2, out int retCount);

            err = visa32.viPrintf(SessionId0, "abc");

            //err = visa32.viWrite(SessionId0, AAA, AAA.Length, out int jobId);

            //err = visa32.viFindNext(findList, Read);
            //err = visa32.viOpen(RM, Read.ToString(), 0, 0, out SessionId);
            err = visa32.viScanf(SessionId1, "%3s", Read);







            //err = visa32.viFindRsrc(RM, "?*INSTR", out findList, out numInstrs, Read);

            //for (int i = 0; i < numInstrs; i++)
            //{
            //    err = visa32.viFindNext(findList, Read);
            //    Values[i] = Read.ToString();
            //}

            visa32.viClose(RM);
        }
    }
}
