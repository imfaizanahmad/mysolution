using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRM.Common
{
    public class Util
    {

        int CustomNumber = 36;
        int Seperator = 10;

        public string DigitalId(int databaseCampaignId)
        {
            if (databaseCampaignId < 1) return "0";

            int hex = databaseCampaignId;
            string hexStr = string.Empty;

            while (databaseCampaignId > 0)
            {
                hex = databaseCampaignId % CustomNumber;

                if (hex < Seperator)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                databaseCampaignId /= CustomNumber;
            }

            return hexStr;
        }

    }
}
