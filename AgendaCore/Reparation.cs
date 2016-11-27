using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Reparation
    {
        #region Attributes  

        private int mId;
        
        private string mNom;
        
        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
        public int pId
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }

        /// <summary>
        /// Le nom de la réparation
        /// </summary>
        public string pNom
        {
            get
            {
                return mNom;
            }
            set
            {
                mNom = value;
            }
        }

        #endregion

        #region constructors

        public Reparation(int id, string nom)
        {
            pId = id;
            pNom = nom;
        }

        public Reparation(string nom)
        {
            pNom = nom;
        }

        #endregion

        #region Methods

        private bool Equals(Reparation rep)
        {
            return pNom == rep.pNom;
        }

        public override bool Equals(object obj)
        {
            bool ok = false;
            if (obj is Reparation)
            {
                ok = Equals(obj as Reparation);
            }
            return ok;
        }

        public override string ToString()
        {
            return pNom;
        }


        #endregion
    }
}
