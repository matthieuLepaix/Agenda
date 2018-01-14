using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgendaCore
{
    public class Reparation
    {
        #region Attributes  

        private int id;
        
        private string nom;
        
        #endregion

        #region Properties

        /// <summary>
        /// L'identifiant du client.
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Le nom de la réparation
        /// </summary>
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }

        #endregion

        #region constructors

        public Reparation(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }

        public Reparation(string nom)
        {
            Nom = nom;
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return Id;
        }

        private bool Equals(Reparation rep)
        {
            return Nom == rep.Nom;
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
            return Nom;
        }


        #endregion
    }
}
