using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    /// <summary>
    /// 编号生成器
    /// </summary>
    public class IdGenerator
    {
        private long m_identity;
        private long m_seedDefaultValue = 1;
        private long m_seed;
        private long m_max = 9999999; // 共24位
        private object m_locker = new object();

        private IdGenerator()
        {
            InitIdentity();
        }

        private static IdGenerator m_instance = null;
        private static object m_assistantObj = new object();
        public static IdGenerator Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_assistantObj)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new IdGenerator();
                        }
                    }
                }
                return m_instance;
            }
        }

        private void InitIdentity()
        {
            string identityStr = ConfigurationManager.AppSettings["Identity"] ?? "1";
            this.m_identity = byte.Parse(identityStr);
        }
        /// <summary>
        /// 获取编号（自动生成）
        /// </summary>
        /// <returns></returns>
        public decimal GenerateId()
        {
            decimal stamp = decimal.Parse(DateTime.Now.ToString("yyMMddHHmmssfff") + this.m_identity) * (decimal)Math.Pow(10, this.m_max.ToString().Length);
            lock (m_locker)
            {
                if (this.m_seed >= this.m_max)
                {
                    this.m_seed = m_seedDefaultValue;
                }
                else
                {
                    this.m_seed++;
                }
                return stamp + this.m_seed;
            }
        }
    }
}