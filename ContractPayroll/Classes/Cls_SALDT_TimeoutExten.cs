using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractPayroll.Reports.DS_rptMthlySalDTTableAdapters
{
    public partial class sp_Cont_MthlySalDTTableAdapter
    {
        public int SelectCommandTimeout
        {
            get
            {
                return (this._commandCollection[0].CommandTimeout);
            }

            set
            {
                if (this._commandCollection != null)
                {
                    for (int i = 0; i < this._commandCollection.Length; i++)
                    {
                        if ((this._commandCollection[i] != null))
                        {
                            ((System.Data.SqlClient.SqlCommand)
                             (this._commandCollection[i])).CommandTimeout = value;
                        }
                    }
                }
                
            }
        }
    }

    public partial class sp_Cont_MthlySalTPARegisterTableAdapter
    {
        public int SelectCommandTimeout
        {
            get
            {
                return (this._commandCollection[0].CommandTimeout);
            }

            set
            {
                if (this._commandCollection != null)
                {
                    for (int i = 0; i < this._commandCollection.Length; i++)
                    {
                        if ((this._commandCollection[i] != null))
                        {
                            ((System.Data.SqlClient.SqlCommand)
                             (this._commandCollection[i])).CommandTimeout = value;
                        }
                    }
                }
            }
        }
    }
    
}

namespace ContractPayroll.Reports.DS_rptMthlyTPADTTableAdapters
{
    public partial class sp_Cont_MthlySalTPARegisterTableAdapter
    {
        public int SelectCommandTimeout
        {
            get
            {
                return (this._commandCollection[0].CommandTimeout);
            }

            set
            {
                if (this._commandCollection != null)
                {
                    for (int i = 0; i < this._commandCollection.Length; i++)
                    {
                        if ((this._commandCollection[i] != null))
                        {
                            ((System.Data.SqlClient.SqlCommand)
                             (this._commandCollection[i])).CommandTimeout = value;
                        }
                    }
                }
            }
        }
    }

    public partial class sp_Cont_MthlyTPADTTableAdapter
    {
        public int SelectCommandTimeout
        {
            get
            {
                return (this._commandCollection[0].CommandTimeout);
            }

            set
            {

                if (this._commandCollection != null)
                {
                    for (int i = 0; i < this._commandCollection.Length; i++)
                    {
                        if ((this._commandCollection[i] != null))
                        {
                            ((System.Data.SqlClient.SqlCommand)
                             (this._commandCollection[i])).CommandTimeout = value;
                        }
                    }
                }
            }
        }
    }

}