using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;



namespace MVAFW.TestItemColls.VISA
{

    public class FindRsrc : TestItem
    {

        public FindRsrc()
        {
            ChannelNumbers = 10;
            rsrcType = findType.INSTR;
        }

        public findType rsrcType { get; set; }
        public enum findType
        {
            INSTR = '1',
            INTFC = '2'
        }

        public override void doTest()
        {
            base.doTest();

            int err, RM, findList, numInstrs;
            StringBuilder Read = new StringBuilder(2048);

            err = visa32.viOpenDefaultRM(out RM);
            err = visa32.viFindRsrc(RM, "?*" + rsrcType, out findList, out numInstrs, Read);

            for (int i = 0; i < numInstrs; i++)
            {               
                Values[i] = Read.ToString();
                err = visa32.viFindNext(findList, Read);
            }

            visa32.viClose(RM);
        }
    }
}
